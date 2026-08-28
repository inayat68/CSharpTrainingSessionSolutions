using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _37_DictionaryCollectionDemo;

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }

    public User(string name, int age, string city)
    {
        Name = name;
        Age = age;
        City = city;
    }

    public override string ToString()
    {
        return $"{{name={Name}, age={Age}, city={City}}}";
    }
}

public class Product
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Brand { get; set; }

    public Product(string name, int price, string brand)
    {
        Name = name;
        Price = price;
        Brand = brand;
    }

    public override string ToString()
    {
        return $"{{name={Name}, price={Price}, brand={Brand}}}";
    }
}

public class Item
{
    public string Name { get; set; }
    public int Price { get; set; }

    public Item(string name, int price)
    {
        Name = name;
        Price = price;
    }
}

public class SetUser
{
    public int Id { get; set; }
    public string Name { get; set; }

    public SetUser(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"{{id={Id}, name='{Name}'}}";
    }

    // JAVA:
    // Override equals() and hashCode()
    //
    // C#:
    // Override Equals() and GetHashCode()
    // when logical equality is required.
}

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Java vs C# Collections ===");
        Console.WriteLine();

        // =========================================================
        // CHAPTER 12: MAP / HASHMAP
        // =========================================================

        /*
         JAVA                         C#
         -----------------------------------------------------------
         Map<K,V>                  -> Dictionary<K,V>
         HashMap<K,V>              -> Dictionary<K,V>
         TreeMap<K,V>              -> SortedDictionary<K,V>

         map.put(k,v)              -> map[k] = v
         map.get(k)                 -> map[k]
         map.remove(k)              -> map.Remove(k)
         map.containsKey(k)        -> map.ContainsKey(k)
         map.containsValue(v)      -> map.ContainsValue(v)
         map.size()                -> map.Count
         map.isEmpty()             -> map.Count == 0
         map.clear()               -> map.Clear()

         map.keySet()              -> map.Keys
         map.values()              -> map.Values
         map.entrySet()            -> map
         entry.getKey()            -> entry.Key
         entry.getValue()          -> entry.Value

         map.putIfAbsent(k,v)      -> map.TryAdd(k,v)
         map.getOrDefault(k,v)     -> TryGetValue()
         map.replace(k,v)          -> map[k] = v

         for (Entry e : map.entrySet())
                                   -> foreach (var e in map)

         JAVA Streams              -> C# LINQ
        */


        // =========================================================
        // 1. CREATE MAP
        // =========================================================

        Dictionary<object, object> map =
            new Dictionary<object, object>();

        // JAVA:
        // Map<Object, Object> map = new HashMap<>();

        map["name"] = "Ali";
        map["age"] = 25;
        map[1] = "Number Key";
        map[true] = "Boolean Key";

        Console.WriteLine(
            "Initial Map: " +
            string.Join(", ", map.Select(x => $"{x.Key}={x.Value}")));

        // JAVA:
        // map.put("name", "Ali");
        // map.put("age", 25);
        // map.put(1, "Number Key");
        // map.put(true, "Boolean Key");

        // C# Dictionary<object,object> can use different key types.
        // Dictionary<string,object> is more common for string keys.


        // =========================================================
        // 2. ADD / UPDATE - put()
        // =========================================================

        map["city"] = "Karachi";
        map["age"] = 30;

        Console.WriteLine(
            "After put(): " +
            string.Join(", ", map.Select(x => $"{x.Key}={x.Value}")));

        // JAVA:
        // map.put("city", "Karachi");
        // map.put("age", 30);

        // Java put() adds or replaces.
        // C# map[key] = value also adds or replaces.


        // =========================================================
        // 3. GET VALUE
        // =========================================================

        Console.WriteLine(
            $"Name: {map["name"]}, City: {map["city"]}");

        // JAVA:
        // System.out.println(
        //     "Name: " + map.get("name") +
        //     ", City: " + map.get("city")
        // );


        // =========================================================
        // 4. CONTAINS KEY
        // =========================================================

        Console.WriteLine(
            $"Has age? {map.ContainsKey("age")}, " +
            $"Has salary? {map.ContainsKey("salary")}");

        // JAVA:
        // map.containsKey("age")
        // map.containsKey("salary")


        // =========================================================
        // 5. REMOVE
        // =========================================================

        map.Remove("age");

        Console.WriteLine(
            "After remove age: " +
            string.Join(", ", map.Select(x => $"{x.Key}={x.Value}")));

        // JAVA:
        // map.remove("age");


        // =========================================================
        // 6. SIZE
        // =========================================================

        Console.WriteLine($"Map size: {map.Count}");

        // JAVA:
        // map.size();


        // =========================================================
        // 7. LOOP THROUGH MAP
        // =========================================================

        foreach (KeyValuePair<object, object> entry in map)
        {
            Console.WriteLine(
                $"{entry.Key} => {entry.Value}");
        }

        // JAVA:
        // for (Map.Entry<Object,Object> entry : map.entrySet())
        // {
        //     System.out.println(
        //         entry.getKey() + " => " +
        //         entry.getValue());
        // }


        // =========================================================
        // 8. KEYS / VALUES
        // =========================================================

        Console.WriteLine(
            "Keys: " +
            string.Join(", ", map.Keys));

        Console.WriteLine(
            "Values: " +
            string.Join(", ", map.Values));

        // JAVA:
        // map.keySet()
        // map.values()


        // =========================================================
        // 9. CLEAR MAP
        // =========================================================

        Dictionary<string, int> tempMap =
            new Dictionary<string, int>();

        tempMap["a"] = 1;
        tempMap["b"] = 2;

        tempMap.Clear();

        Console.WriteLine(
            $"After clear size: {tempMap.Count}");

        // JAVA:
        // tempMap.clear();
        // tempMap.size();


        // =========================================================
        // 10. STUDENT DATABASE
        // =========================================================

        Dictionary<int, string> students =
            new Dictionary<int, string>();

        students[101] = "Ahmed";
        students[102] = "Sara";
        students[103] = "Ali";

        Console.WriteLine(
            $"Student 102: {students[102]}");

        // JAVA:
        // Map<Integer,String> students = new HashMap<>();
        // students.put(101, "Ahmed");
        // students.put(102, "Sara");
        // students.get(102);


        // =========================================================
        // 11. SHOPPING CART
        // =========================================================

        Dictionary<string, int> cart =
            new Dictionary<string, int>();

        cart["Apple"] = 3;

        Console.WriteLine(
            $"Apple qty: {cart["Apple"]}");

        // JAVA:
        // Map<String,Integer> cart = new HashMap<>();
        // cart.put("Apple", 3);
        // cart.get("Apple");


        // =========================================================
        // 12. OBJECT AS KEY
        // =========================================================

        object user = new object();

        Dictionary<object, string> userMap =
            new Dictionary<object, string>();

        userMap[user] = "User Profile Data";

        Console.WriteLine(
            $"Object Key Value: {userMap[user]}");

        // JAVA:
        // Object user = new Object();
        // Map<Object,String> userMap = new HashMap<>();
        // userMap.put(user, "User Profile Data");


        // =========================================================
        // CHAPTER 13: ARRAYS
        // =========================================================

        // JAVA:
        // int[] intArray = {1,2,3,4,5};
        //
        // C#:
        // int[] intArray = {1,2,3,4,5};

        int[] intArray = { 1, 2, 3, 4, 5 };

        char[] charArray =
            { 'a', 'b', 'c' };

        string[] strArray =
        {
                "red",
                "blue",
                "green"
            };

        List<bool> traffic =
            new List<bool>();

        traffic.Add(true);
        traffic.Add(true);
        traffic.Add(false);

        // JAVA:
        // List<Boolean> traffic = new ArrayList<>();
        // traffic.add(true);
        // traffic.add(true);
        // traffic.add(false);


        // =========================================================
        // 1. LOOP ARRAY
        // =========================================================

        int[] numArray = { 3, 5, 7 };

        for (int i = 0; i < numArray.Length; i++)
        {
            Console.WriteLine(numArray[i]);
        }

        // JAVA:
        // for (int i = 0; i < numArray.length; i++)
        // {
        //     System.out.println(numArray[i]);
        // }

        foreach (int n in numArray)
        {
            Console.WriteLine(n);
        }

        // JAVA:
        // for (int n : numArray)
        // {
        //     System.out.println(n);
        // }


        // =========================================================
        // 2. MULTIPLICATION LOOP
        // =========================================================

        int[] myArray = { 1, 2, 3, 4 };

        foreach (int n in myArray)
        {
            Console.WriteLine(
                $"2 * value is: {n * 2}");
        }

        // JAVA:
        // for (int n : myArray)
        // {
        //     System.out.println(
        //         "2 * value is: " + (n * 2));
        // }


        // =========================================================
        // 3. ASCII GENERATION
        // =========================================================

        System.Text.StringBuilder sb =
            new System.Text.StringBuilder();

        for (int i = 65; i <= 122; i++)
        {
            sb.Append((char)i);
        }

        Console.WriteLine(sb);

        // JAVA:
        // StringBuilder sb = new StringBuilder();
        // for (int i = 65; i <= 122; i++)
        // {
        //     sb.append((char)i);
        // }
        // System.out.println(sb);


        // =========================================================
        // 4. LIST CREATION
        // =========================================================

        List<string> fruits =
            new List<string>
            {
                    "Apple",
                    "Banana",
                    "Mango"
            };

        Console.WriteLine(
            $"Fruits: [{string.Join(", ", fruits)}]");

        // JAVA:
        // List<String> fruits =
        //     new ArrayList<>(
        //         Arrays.asList("Apple", "Banana", "Mango")
        //     );


        // =========================================================
        // 5. ADD
        // =========================================================

        fruits.Add("Orange");

        // JAVA:
        // fruits.add("Orange");


        // =========================================================
        // 6. REMOVE LAST
        // =========================================================

        fruits.RemoveAt(fruits.Count - 1);

        // JAVA:
        // fruits.remove(fruits.size() - 1);


        // =========================================================
        // 7. UPDATE ELEMENT
        // =========================================================

        fruits[1] = "Grapes";

        // JAVA:
        // fruits.set(1, "Grapes");


        // =========================================================
        // 8. FOREACH
        // =========================================================

        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
        }

        // JAVA:
        // for (String fruit : fruits)
        // {
        //     System.out.println(fruit);
        // }


        // =========================================================
        // 9. LIST MAP / TRANSFORM
        // =========================================================

        List<string> upper =
            fruits.Select(f => f.ToUpper()).ToList();

        Console.WriteLine(
            $"Upper: [{string.Join(", ", upper)}]");

        // JAVA:
        // List<String> upper =
        //     fruits.stream()
        //           .map(String::toUpperCase)
        //           .collect(Collectors.toList());

        // C# LINQ Select() ≈ Java Stream map()


        // =========================================================
        // 10. FILTER
        // =========================================================

        List<string> longNames =
            fruits.Where(f => f.Length > 5).ToList();

        Console.WriteLine(
            $"Long names: [{string.Join(", ", longNames)}]");

        // JAVA:
        // List<String> longNames =
        //     fruits.stream()
        //           .filter(f -> f.length() > 5)
        //           .collect(Collectors.toList());

        // C# Where() ≈ Java Stream filter()


        // =========================================================
        // 11. FIND
        // =========================================================

        string found =
            fruits.FirstOrDefault(
                f => f.StartsWith("M"));

        Console.WriteLine(
            $"Found: {found}");

        // JAVA:
        // String found =
        //     fruits.stream()
        //           .filter(f -> f.startsWith("M"))
        //           .findFirst()
        //           .orElse(null);

        // C# FirstOrDefault() ≈ Java findFirst().orElse(null)


        // =========================================================
        // 12. CONTAINS
        // =========================================================

        Console.WriteLine(
            $"Contains Apple: {fruits.Contains("Apple")}");

        // JAVA:
        // fruits.contains("Apple");


        // =========================================================
        // 13. INDEX OF
        // =========================================================

        Console.WriteLine(
            $"Index of Mango: {fruits.IndexOf("Mango")}");

        // JAVA:
        // fruits.indexOf("Mango");


        // =========================================================
        // 14. SORT
        // =========================================================

        List<int> numbers =
            new List<int> { 40, 10, 100, 5 };

        numbers.Sort();

        Console.WriteLine(
            $"Sorted: [{string.Join(", ", numbers)}]");

        // JAVA:
        // Collections.sort(numbers);

        // C#:
        // numbers.Sort();


        // =========================================================
        // 15. REVERSE
        // =========================================================

        numbers.Reverse();

        Console.WriteLine(
            $"Reversed: [{string.Join(", ", numbers)}]");

        // JAVA:
        // Collections.reverse(numbers);


        // =========================================================
        // 16. SLICE / SUBLIST
        // =========================================================

        List<string> some =
            fruits.GetRange(0, 2);

        Console.WriteLine(
            $"Slice: [{string.Join(", ", some)}]");

        // JAVA:
        // fruits.subList(0, 2);

        // C# GetRange(start, count)
        // Java subList(fromIndex, toIndex)


        // =========================================================
        // 17. JOIN
        // =========================================================

        Console.WriteLine(
            string.Join(", ", fruits));

        // JAVA:
        // String.join(", ", fruits);


        // =========================================================
        // 18. CONCAT / ADD RANGE
        // =========================================================

        List<int> arr1 =
            new List<int> { 1, 2 };

        List<int> arr2 =
            new List<int> { 3, 4 };

        List<int> merged =
            new List<int>();

        merged.AddRange(arr1);
        merged.AddRange(arr2);

        Console.WriteLine(
            $"Merged: [{string.Join(", ", merged)}]");

        // JAVA:
        // List<Integer> merged = new ArrayList<>();
        // merged.addAll(arr1);
        // merged.addAll(arr2);

        // C# AddRange() ≈ Java addAll()


        // =========================================================
        // 19. REDUCE / SUM
        // =========================================================

        int sum = numbers.Sum();

        Console.WriteLine(
            $"Sum: {sum}");

        // JAVA:
        // int sum =
        //     numbers.stream()
        //            .mapToInt(Integer::intValue)
        //            .sum();

        // C# LINQ Sum() ≈ Java Stream sum()


        // =========================================================
        // 20. SHOPPING CART
        // =========================================================

        List<Item> cart2 =
            new List<Item>
            {
                    new Item("Laptop", 50000),
                    new Item("Mouse", 1000)
            };

        int total =
            cart2.Sum(item => item.Price);

        Console.WriteLine(
            $"Total: {total}");

        // JAVA:
        // List<Item> cart2 = Arrays.asList(
        //     new Item("Laptop", 50000),
        //     new Item("Mouse", 1000)
        // );

        // int total =
        //     cart2.stream()
        //          .mapToInt(item -> item.Price)
        //          .sum();


        // =========================================================
        // 21. ARRAY MATH
        // =========================================================

        int[] nums =
            { 10, 50, 20, 80, 30 };

        int max = nums.Max();
        int min = nums.Min();
        int totalSum = nums.Sum();

        double avg =
            totalSum / (double)nums.Length;

        Console.WriteLine(
            $"Max: {max}, Min: {min}, Avg: {avg}");

        // JAVA:
        // int max = Arrays.stream(nums).max().getAsInt();
        // int min = Arrays.stream(nums).min().getAsInt();
        // int sum = Arrays.stream(nums).sum();
        // double avg = Arrays.stream(nums).average().getAsDouble();

        // C# LINQ provides Max(), Min(), Sum(), Average()


        // =========================================================
        // 22. OBJECT ARRAY ANALYSIS
        // =========================================================

        Product[] products =
        {
                new Product("Laptop", 50000, "Dell"),
                new Product("Mouse", 1000, "Logitech"),
                new Product("Keyboard", 3000, "Dell")
            };

        Product maxProduct =
            products.OrderByDescending(p => p.Price)
                    .First();

        Console.WriteLine(
            $"Max Product: " +
            $"{maxProduct.Name} {maxProduct.Price}");

        // JAVA:
        // Product maxProduct =
        //     Arrays.stream(products)
        //           .max(Comparator.comparingInt(p -> p.price))
        //           .get();


        // =========================================================
        // 23. HASHMAP
        // =========================================================

        Dictionary<int, string> hashMap =
            new Dictionary<int, string>();

        hashMap[3] = "Mango";
        hashMap[1] = "Apple";
        hashMap[2] = "Banana";

        Console.WriteLine(
            "HashMap: " +
            string.Join(", ",
                hashMap.Select(x =>
                    $"{x.Key}={x.Value}")));

        // JAVA:
        // Map<Integer,String> hashMap =
        //     new HashMap<>();

        // hashMap.put(3, "Mango");
        // hashMap.put(1, "Apple");
        // hashMap.put(2, "Banana");

        // NOTE:
        // Java HashMap and C# Dictionary are
        // general-purpose key/value collections.


        // =========================================================
        // 24. LINKEDHASHMAP
        // =========================================================

        Dictionary<int, string> linkedHashMap =
            new Dictionary<int, string>();

        linkedHashMap[3] = "Mango";
        linkedHashMap[1] = "Apple";
        linkedHashMap[2] = "Banana";

        Console.WriteLine(
            "LinkedHashMap: " +
            string.Join(", ",
                linkedHashMap.Select(x =>
                    $"{x.Key}={x.Value}")));

        // JAVA:
        // Map<Integer,String> map =
        //     new LinkedHashMap<>();

        // Java LinkedHashMap explicitly maintains
        // insertion order.

        // Modern .NET Dictionary enumeration preserves
        // insertion order, but Dictionary is primarily
        // a key/value lookup collection.


        // =========================================================
        // 25. TREEMAP
        // =========================================================

        SortedDictionary<int, string> treeMap =
            new SortedDictionary<int, string>();

        treeMap[3] = "Mango";
        treeMap[1] = "Apple";
        treeMap[2] = "Banana";

        Console.WriteLine(
            "TreeMap: " +
            string.Join(", ",
                treeMap.Select(x =>
                    $"{x.Key}={x.Value}")));

        // JAVA:
        // Map<Integer,String> treeMap =
        //     new TreeMap<>();

        // TreeMap automatically sorts by key.
        // C# SortedDictionary<TKey,TValue>
        // provides the equivalent concept.


        // =========================================================
        // 26. HASHTABLE
        // =========================================================

        Hashtable hashTable =
            new Hashtable();

        hashTable[1] = "One";
        hashTable[2] = "Two";
        hashTable[3] = "Three";

        foreach (DictionaryEntry entry in hashTable)
        {
            Console.WriteLine(
                $"{entry.Key} = {entry.Value}");
        }

        // JAVA:
        // Hashtable<Integer,String> hashTable =
        //     new Hashtable<>();

        // C# Hashtable:
        // System.Collections.Hashtable
        //
        // It is non-generic and is generally a
        // legacy collection. Prefer Dictionary<TKey,TValue>.


        // =========================================================
        // 27. COMPARISON - SCORES
        // =========================================================

        Dictionary<string, int> scores =
            new Dictionary<string, int>();

        scores["Ali"] = 90;
        scores["Sara"] = 85;
        scores["John"] = 95;

        Console.WriteLine(
            "HashMap Scores: " +
            string.Join(", ",
                scores.Select(x =>
                    $"{x.Key}={x.Value}")));

        // JAVA:
        // Map<String,Integer> scores =
        //     new HashMap<>();


        SortedDictionary<string, int> sortedScores =
            new SortedDictionary<string, int>(scores);

        Console.WriteLine(
            "TreeMap Scores: " +
            string.Join(", ",
                sortedScores.Select(x =>
                    $"{x.Key}={x.Value}")));

        // JAVA:
        // Map<String,Integer> sortedScores =
        //     new TreeMap<>(scores);


        Dictionary<string, int> orderedScores =
            new Dictionary<string, int>();

        orderedScores["Ali"] = 90;
        orderedScores["Sara"] = 85;
        orderedScores["John"] = 95;

        Console.WriteLine(
            "Ordered Dictionary: " +
            string.Join(", ",
                orderedScores.Select(x =>
                    $"{x.Key}={x.Value}")));

        // JAVA:
        // LinkedHashMap<String,Integer> orderedScores =
        //     new LinkedHashMap<>();


        // =========================================================
        // QUICK JAVA -> C# COLLECTION SUMMARY
        // =========================================================

        /*
         JAVA COLLECTION              C# EQUIVALENT
         -------------------------------------------------
         ArrayList<T>              -> List<T>
         LinkedList<T>             -> LinkedList<T>
         Vector<T>                 -> List<T>*
         Stack<T>                  -> Stack<T>
         HashSet<T>                -> HashSet<T>
         LinkedHashSet<T>          -> No direct equivalent
         TreeSet<T>                -> SortedSet<T>

         HashMap<K,V>              -> Dictionary<K,V>
         LinkedHashMap<K,V>        -> Dictionary<K,V>*
         TreeMap<K,V>              -> SortedDictionary<K,V>
         Hashtable<K,V>            -> Hashtable*

         Java Stream API           -> C# LINQ

         stream.map()              -> Select()
         stream.filter()           -> Where()
         stream.count()            -> Count()
         stream.anyMatch()         -> Any()
         stream.allMatch()         -> All()
         stream.findFirst()        -> FirstOrDefault()
         stream.sum()              -> Sum()
         stream.max()              -> Max()
         stream.min()              -> Min()
         stream.sorted()            -> OrderBy()
         */

        Console.WriteLine();
        Console.WriteLine("=== Done ===");
    }
}
