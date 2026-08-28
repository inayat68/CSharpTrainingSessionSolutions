using System;
using System.Collections.Generic;
using System.Linq;

namespace ListCollectionDemo_36;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 11_ListAndSet ===");
        Console.WriteLine("Java List / Set vs C# Collections");
        Console.WriteLine();

        // ============================================================
        // JAVA vs C# QUICK COMPARISON
        // ============================================================
        //
        // Java                         C#
        // ------------------------------------------------------------
        // List<T>                      List<T>
        // ArrayList<T>                 List<T>
        // LinkedList<T>                LinkedList<T>
        // Vector<T>                    No direct equivalent
        // Stack<T>                     Stack<T>
        // HashSet<T>                   HashSet<T>
        // LinkedHashSet<T>             No direct equivalent
        // TreeSet<T>                   SortedSet<T>
        //
        // add(x)                       Add(x)
        // add(i, x)                    Insert(i, x)
        // get(i)                       [i]
        // set(i, x)                    [i] = x
        // remove(i)                    RemoveAt(i)
        // remove(x)                    Remove(x)
        // size()                       Count
        // contains(x)                  Contains(x)
        // indexOf(x)                   IndexOf(x)
        // lastIndexOf(x)               LastIndexOf(x)
        // clear()                      Clear()
        // stream()                     LINQ
        // stream.map()                 Select()
        // stream.filter()              Where()
        // stream.anyMatch()            Any()
        // stream.allMatch()            All()
        //
        // ============================================================


        // ============================================================
        // 1. LIST / ARRAYLIST
        // ============================================================

        // Java:
        // List<String> list = new ArrayList<>();

        // C#:
        List<string> list = new List<string>();

        // Java: list.add("A");
        // C#:   list.Add("A");
        list.Add("A");
        list.Add("B");
        list.Add("C");
        list.Add("B");

        Console.WriteLine(
            "After Add(): " + string.Join(", ", list));

        // OUTPUT:
        // After Add(): A, B, C, B


        // Java: list.add(2, "X");
        // C#:   list.Insert(2, "X");
        list.Insert(2, "X");

        Console.WriteLine(
            "After Insert(): " + string.Join(", ", list));

        // OUTPUT:
        // After Insert(): A, B, X, C, B


        // Java: list.remove("B");
        // C#:   list.Remove("B");
        // Removes first matching value.
        list.Remove("B");

        Console.WriteLine(
            "After Remove(value): " + string.Join(", ", list));

        // OUTPUT:
        // After Remove(value): A, X, C, B


        // Java: list.remove(1);
        // C#:   list.RemoveAt(1);
        list.RemoveAt(1);

        Console.WriteLine(
            "After RemoveAt(index): " + string.Join(", ", list));

        // OUTPUT:
        // After RemoveAt(index): A, C, B


        // Java: list.get(1)
        // C#:   list[1]
        Console.WriteLine("Get index 1: " + list[1]);

        // OUTPUT:
        // Get index 1: C


        // Java: list.set(1, "Z");
        // C#:   list[1] = "Z";
        list[1] = "Z";

        Console.WriteLine(
            "After Set: " + string.Join(", ", list));

        // OUTPUT:
        // After Set: A, Z, B


        // Java: list.indexOf("B")
        // C#:   list.IndexOf("B")
        Console.WriteLine(
            "IndexOf B: " + list.IndexOf("B"));

        // OUTPUT:
        // IndexOf B: 2


        // Java: list.add("B");
        // C#:   list.Add("B");
        list.Add("B");

        Console.WriteLine(
            "List: " + string.Join(", ", list));

        // OUTPUT:
        // List: A, Z, B, B


        // Java: list.lastIndexOf("B")
        // C#:   list.LastIndexOf("B")
        Console.WriteLine(
            "LastIndexOf B: " + list.LastIndexOf("B"));

        // OUTPUT:
        // LastIndexOf B: 3


        // ============================================================
        // 2. ARRAYLIST
        // ============================================================

        // Java:
        // List<String> arrayList = new ArrayList<>();

        // C#:
        // List<T> is the normal equivalent.
        List<string> arrayList = new List<string>
            {
                "Apple",
                "Banana",
                "Mango"
            };

        Console.WriteLine(
            "ArrayList: " + string.Join(", ", arrayList));

        // OUTPUT:
        // ArrayList: Apple, Banana, Mango


        // Java: list.get(1)
        // C#:   list[1]
        Console.WriteLine(
            "Get index 1: " + arrayList[1]);

        // OUTPUT:
        // Get index 1: Banana


        // ============================================================
        // 3. LINKEDLIST
        // ============================================================

        // Java:
        // LinkedList<String> linkedList = new LinkedList<>();

        // C#:
        LinkedList<string> linkedList =
            new LinkedList<string>();

        linkedList.AddLast("Apple");
        linkedList.AddLast("Banana");
        linkedList.AddLast("Mango");

        // Java:
        // linkedList.add(1, "Orange");

        // C#:
        LinkedListNode<string>? bananaNode =
            linkedList.Find("Banana");

        if (bananaNode != null)
            linkedList.AddBefore(bananaNode, "Orange");

        Console.WriteLine(
            "LinkedList: " +
            string.Join(", ", linkedList));

        // OUTPUT:
        // LinkedList: Apple, Orange, Banana, Mango


        // Java: linkedList.remove("Banana");
        // C#:   linkedList.Remove("Banana");
        linkedList.Remove("Banana");

        Console.WriteLine(
            "After Remove: " +
            string.Join(", ", linkedList));

        // OUTPUT:
        // After Remove: Apple, Orange, Mango


        // ============================================================
        // 4. VECTOR
        // ============================================================

        // Java:
        // Vector<String> vector = new Vector<>();
        //
        // C# has no direct Vector equivalent.
        // List<T> is normally used instead.

        List<string> vector = new List<string>();

        vector.Add("A");
        vector.Add("B");
        vector.Add("C");

        Console.WriteLine(
            "Vector: " + string.Join(", ", vector));

        // OUTPUT:
        // Vector: A, B, C

        vector[1] = "Z";

        // Java: vector.set(1, "Z");
        // C#:   vector[1] = "Z";

        Console.WriteLine(
            "After Set: " + string.Join(", ", vector));

        // OUTPUT:
        // After Set: A, Z, C


        // ============================================================
        // 5. STACK
        // ============================================================

        // Java:
        // Stack<Integer> stack = new Stack<>();

        // C#:
        Stack<int> stack = new Stack<int>();

        // Java: stack.push(10);
        // C#:   stack.Push(10);
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        Console.WriteLine(
            "Stack: " + string.Join(", ", stack));

        // OUTPUT:
        // Stack: 30, 20, 10
        //
        // Stack = LIFO
        // Last In → First Out


        // Java: stack.pop()
        // C#:   stack.Pop()
        Console.WriteLine(
            "Pop: " + stack.Pop());

        // OUTPUT:
        // Pop: 30


        Console.WriteLine(
            "After Pop: " + string.Join(", ", stack));

        // OUTPUT:
        // After Pop: 20, 10


        // Java: stack.peek()
        // C#:   stack.Peek()
        Console.WriteLine(
            "Peek: " + stack.Peek());

        // OUTPUT:
        // Peek: 20


        // ============================================================
        // 6. HASHSET
        // ============================================================

        // Java:
        // Set<String> hashSet = new HashSet<>();

        // C#:
        HashSet<string> hashSet =
            new HashSet<string>();

        // Java: hashSet.add("Banana");
        // C#:   hashSet.Add("Banana");
        hashSet.Add("Banana");
        hashSet.Add("Apple");
        hashSet.Add("Mango");
        hashSet.Add("Apple"); // duplicate ignored

        Console.WriteLine(
            "HashSet: " + string.Join(", ", hashSet));

        // OUTPUT:
        // Apple, Mango, Banana
        // NOTE: HashSet order is not guaranteed.

        Console.WriteLine(
            "Contains Apple: " +
            hashSet.Contains("Apple"));

        // OUTPUT:
        // Contains Apple: True


        // ============================================================
        // 7. LINKEDHASHSET
        // ============================================================

        // Java:
        // Set<String> set = new LinkedHashSet<>();
        //
        // C# has no direct LinkedHashSet<T>.
        // List<T> + Contains() can provide similar behavior.

        List<string> linkedHashSet =
            new List<string>();

        void AddUnique(string value)
        {
            if (!linkedHashSet.Contains(value))
                linkedHashSet.Add(value);
        }

        AddUnique("Banana");
        AddUnique("Apple");
        AddUnique("Mango");
        AddUnique("Apple"); // ignored

        Console.WriteLine(
            "LinkedHashSet: " +
            string.Join(", ", linkedHashSet));

        // OUTPUT:
        // LinkedHashSet: Banana, Apple, Mango


        // ============================================================
        // 8. TREESET
        // ============================================================

        // Java:
        // Set<String> treeSet = new TreeSet<>();

        // C#:
        SortedSet<string> treeSet =
            new SortedSet<string>();

        treeSet.Add("Banana");
        treeSet.Add("Apple");
        treeSet.Add("Mango");
        treeSet.Add("Apple"); // duplicate ignored

        Console.WriteLine(
            "TreeSet: " + string.Join(", ", treeSet));

        // OUTPUT:
        // TreeSet: Apple, Banana, Mango


        // ============================================================
        // 9. LIST → LINQ
        // ============================================================

        // Java Stream:
        /*
        List<String> names =
            list.stream()
                .filter(x -> x.length() > 3)
                .map(String::toUpperCase)
                .collect(Collectors.toList());
        */

        // C# LINQ:
        List<string> names = list
            .Where(x => x.Length > 3)
            .Select(x => x.ToUpper())
            .ToList();

        Console.WriteLine(
            "LINQ: " + string.Join(", ", names));


        // ============================================================
        // 10. LIST / LINQ METHODS
        // ============================================================

        List<int> numbers =
            new List<int> { 10, 20, 30, 40, 50 };

        // Java:
        // numbers.stream().filter(x -> x > 25)

        // C#:
        List<int> greaterThan25 = numbers
            .Where(x => x > 25)
            .ToList();

        Console.WriteLine(
            "Where > 25: " +
            string.Join(", ", greaterThan25));

        // OUTPUT:
        // Where > 25: 30, 40, 50


        // Java:
        // stream.map(x -> x * 2)

        // C#:
        List<int> doubled = numbers
            .Select(x => x * 2)
            .ToList();

        Console.WriteLine(
            "Select * 2: " +
            string.Join(", ", doubled));

        // OUTPUT:
        // Select * 2: 20, 40, 60, 80, 100


        // Java:
        // stream.anyMatch(x -> x > 40)

        // C#:
        Console.WriteLine(
            "Any > 40: " +
            numbers.Any(x => x > 40));

        // OUTPUT:
        // Any > 40: True


        // Java:
        // stream.allMatch(x -> x > 0)

        // C#:
        Console.WriteLine(
            "All > 0: " +
            numbers.All(x => x > 0));

        // OUTPUT:
        // All > 0: True


        // Java:
        // stream.findFirst()

        // C#:
        Console.WriteLine(
            "First: " + numbers.First());

        // OUTPUT:
        // First: 10


        // Java:
        // stream.count()

        // C#:
        Console.WriteLine(
            "Count: " + numbers.Count);

        // OUTPUT:
        // Count: 5


        // ============================================================
        // 11. USER OBJECTS
        // ============================================================

        // Java:
        // User u = new User("Ali", 25, "Karachi");

        // C#:
        User u = new User(
            "Ali",
            25,
            "Karachi");

        Console.WriteLine(u.Name);
        Console.WriteLine(u.City);

        // OUTPUT:
        // Ali
        // Karachi


        // ============================================================
        // 12. DICTIONARY
        // ============================================================

        // Java:
        // Map<String, Object> userMap = new HashMap<>();

        // C#:
        Dictionary<string, object> userMap =
            new Dictionary<string, object>();

        // Java:
        // userMap.put("name", "Ali");
        // userMap.put("age", 25);

        // C#:
        userMap["name"] = "Ali";
        userMap["age"] = 25;

        Console.WriteLine(
            $"{{name={userMap["name"]}, age={userMap["age"]}}}");

        // OUTPUT:
        // {name=Ali, age=25}


        // ============================================================
        // 13. OBJECT → DICTIONARY
        // ============================================================

        Product product =
            new Product(
                "Laptop",
                50000,
                "Dell");

        Dictionary<string, object> productMap =
            new Dictionary<string, object>();

        productMap["name"] = product.Name;
        productMap["price"] = product.Price;
        productMap["brand"] = product.Brand;

        Console.WriteLine(
            $"{{name={productMap["name"]}, " +
            $"price={productMap["price"]}, " +
            $"brand={productMap["brand"]}}}");

        // OUTPUT:
        // {name=Laptop, price=50000, brand=Dell}


        // ============================================================
        // 14. LIST OF OBJECTS + LINQ
        // ============================================================

        // Java:
        /*
        List<Product> cart = List.of(
            new Product("Laptop", 50000, "Dell"),
            new Product("Mouse", 1000, "Logitech")
        );
        */

        // C#:
        List<Product> cart = new List<Product>
            {
                new Product("Laptop", 50000, "Dell"),
                new Product("Mouse", 1000, "Logitech")
            };

        // Java:
        // int total = cart.stream()
        //                 .mapToInt(x -> x.Price)
        //                 .sum();

        // C# LINQ:
        int total = cart.Sum(item => item.Price);

        // Java:
        // List<String> names =
        //     cart.stream()
        //         .map(x -> x.Name)
        //         .collect(Collectors.toList());

        // C#:
        List<string> productNames = cart
            .Select(item => item.Name)
            .ToList();

        Console.WriteLine("Total: " + total);
        Console.WriteLine(
            "Names: " +
            string.Join(", ", productNames));

        // OUTPUT:
        // Total: 51000
        // Names: Laptop, Mouse


        // ============================================================
        // 15. FILTER USERS
        // ============================================================

        List<User> users = new List<User>
            {
                new User("Ali", 25, "Karachi"),
                new User("Sara", 22, "Lahore")
            };

        // Java:
        /*
        List<User> filtered =
            users.stream()
                 .filter(u -> u.getAge() > 23)
                 .collect(Collectors.toList());
        */

        // C#:
        List<User> filtered = users
            .Where(user => user.Age > 23)
            .ToList();

        Console.WriteLine(
            "Filtered users: " + filtered.Count);

        // OUTPUT:
        // Filtered users: 1


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}


// ================================================================
// SUPPORT CLASSES
// ================================================================

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

    // Java:
    /*
    @Override
    public String toString() {
        return "{name=" + name +
               ", age=" + age +
               ", city=" + city + "}";
    }
    */
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

    // Java:
    /*
    @Override
    public String toString() {
        return "{name=" + name +
               ", price=" + price +
               ", brand=" + brand + "}";
    }
    */
}
