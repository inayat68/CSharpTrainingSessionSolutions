using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ComplexCollectionDemo_40;

class Mark
{
    public string Subject;
    public int Score;

    public Mark(string subject, int score)
    {
        Subject = subject;
        Score = score;
    }

    // JAVA:
    // class Mark {
    //     String subject;
    //     int score;
    //
    //     Mark(String subject, int score) {
    //         this.subject = subject;
    //         this.score = score;
    //     }
    // }
}


class Student
{
    public string FirstName;
    public string LastName;
    public int RollNum;
    public List<string> EmailAddresses;
    public List<Mark> Marks;

    public Student(
        string firstName,
        string lastName,
        int rollNum,
        List<string> emailAddresses,
        List<Mark> marks)
    {
        FirstName = firstName;
        LastName = lastName;
        RollNum = rollNum;
        EmailAddresses = emailAddresses;
        Marks = marks;
    }

    // JAVA:
    // class Student {
    //     String firstName;
    //     String lastName;
    //     int rollNum;
    //     List<String> emailAddresses;
    //     List<Mark> marks;
    //
    //     Student(...) {
    //         this.firstName = firstName;
    //         ...
    //     }
    // }
}


class Product
{
    public string Name;
    public int Price;
    public string Brand;

    public Product(string name, int price, string brand)
    {
        Name = name;
        Price = price;
        Brand = brand;
    }
}


class Program
{
    static void Main(string[] args)
    {
        // =========================================================
        // CHAPTER 17
        // STUDENT COMPLEX ITERATION
        // =========================================================

        /*
         JAVA:
         List<Student> students = new ArrayList<>();

         C#:
         List<Student> students = new List<Student>();

         Java ArrayList<T> -> C# List<T>
         Java add()       -> C# Add()
         Java get(i)      -> C# [i]
         Java size()      -> C# Count
        */

        List<Student> students = new List<Student>();

        students.Add(
            new Student(
                "Ali",
                "Khan",
                1,
                new List<string>
                {
                        "ali@gmail.com",
                        "abc@gmail.com"
                },
                new List<Mark>
                {
                        new Mark("English", 78),
                        new Mark("Maths", 90),
                        new Mark("Science", 80)
                }
            )
        );

        students.Add(
            new Student(
                "Sara",
                "Ahmed",
                12,
                new List<string>
                {
                        "sara@gmail.com",
                        "pqr@gmail.com"
                },
                new List<Mark>
                {
                        new Mark("English", 98),
                        new Mark("Maths", 56),
                        new Mark("Science", 68)
                }
            )
        );

        students.Add(
            new Student(
                "John",
                "Doe",
                15,
                new List<string>
                {
                        "john@gmail.com",
                        "xyz@gmail.com"
                },
                new List<Mark>
                {
                        new Mark("English", 55),
                        new Mark("Maths", 99),
                        new Mark("Science", 22)
                }
            )
        );


        // =========================================================
        // 1. FOREACH LOOP
        // =========================================================

        /*
         JAVA:
         for (Student s : students) {
             System.out.println(s.firstName);
         }

         C#:
         foreach (Student s in students)
         */

        Console.WriteLine("\n--- FOREACH ---");

        foreach (Student s in students)
        {
            Console.WriteLine(
                "Student: " +
                s.FirstName +
                " " +
                s.LastName);

            foreach (string email in s.EmailAddresses)
            {
                Console.WriteLine(
                    "  Email: " + email);
            }

            foreach (Mark m in s.Marks)
            {
                Console.WriteLine(
                    "  " +
                    m.Subject +
                    " = " +
                    m.Score);
            }
        }


        // =========================================================
        // 2. CLASSIC FOR LOOP
        // =========================================================

        /*
         JAVA:
         for (int i = 0; i < students.size(); i++) {
             Student s = students.get(i);
         }

         C#:
         for (int i = 0; i < students.Count; i++) {
             Student s = students[i];
         }
        */

        Console.WriteLine("\n--- CLASSIC FOR LOOP ---");

        for (int i = 0; i < students.Count; i++)
        {
            Student s = students[i];

            Console.WriteLine(
                "Student: " + s.FirstName);

            for (int j = 0; j < s.Marks.Count; j++)
            {
                Mark m = s.Marks[j];

                Console.WriteLine(
                    "  " +
                    m.Subject +
                    ": " +
                    m.Score);
            }
        }


        // =========================================================
        // 3. WHILE LOOP
        // =========================================================

        /*
         JAVA:
         int i = 0;

         while (i < students.size()) {
             Student s = students.get(i);
             i++;
         }

         C#:
         students.Count -> students.size()
         students[i]     -> students.get(i)
        */

        Console.WriteLine("\n--- WHILE LOOP ---");

        int studentIndex = 0;

        while (studentIndex < students.Count)
        {
            Student s = students[studentIndex];

            Console.WriteLine(
                "Student: " + s.FirstName);

            int emailIndex = 0;

            while (emailIndex < s.EmailAddresses.Count)
            {
                Console.WriteLine(
                    "  Email: " +
                    s.EmailAddresses[emailIndex]);

                emailIndex++;
            }

            studentIndex++;
        }


        // =========================================================
        // 4. LINQ - AVERAGE MARKS
        // =========================================================

        /*
         JAVA STREAM:

         students.stream()
             .map(s -> {
                 double avg = s.marks.stream()
                     .mapToInt(m -> m.score)
                     .average()
                     .orElse(0);

                 return s.firstName + " avg = " + avg;
             })
             .forEach(System.out::println);


         C# LINQ:
         Select() -> map()
         Average() -> average()
         ForEach() -> forEach()
        */

        Console.WriteLine("\n--- AVERAGE MARKS ---");

        students
            .Select(s =>
            {
                double average =
                    s.Marks
                        .Select(m => m.Score)
                        .DefaultIfEmpty(0)
                        .Average();

                return s.FirstName +
                       " avg = " +
                       average;
            })
            .ToList()
            .ForEach(Console.WriteLine);


        // =========================================================
        // CHAPTER 18
        // NESTED DICTIONARY
        // =========================================================

        /*
         JAVA:

         Map<String, Map<String, Integer>> studentMarks =
             new HashMap<>();

         C#:

         Dictionary<string, Dictionary<string, int>>
             studentMarks =
                 new Dictionary<string, Dictionary<string, int>>();
        */

        Console.WriteLine("\n--- NESTED DICTIONARY ---");

        Dictionary<string, Dictionary<string, int>>
            studentMarks =
            new Dictionary<string, Dictionary<string, int>>();


        Dictionary<string, int> alice =
            new Dictionary<string, int>();

        alice["Math"] = 85;
        alice["English"] = 78;
        alice["Science"] = 92;


        Dictionary<string, int> bob =
            new Dictionary<string, int>();

        bob["Math"] = 65;
        bob["English"] = 72;
        bob["Science"] = 60;


        Dictionary<string, int> charlie =
            new Dictionary<string, int>();

        charlie["Math"] = 95;
        charlie["English"] = 88;
        charlie["Science"] = 91;


        studentMarks["Alice"] = alice;
        studentMarks["Bob"] = bob;
        studentMarks["Charlie"] = charlie;


        // =========================================================
        // PRINT NESTED DICTIONARY
        // =========================================================

        /*
         JAVA:

         for (Map.Entry<String, Map<String, Integer>> entry
                 : studentMarks.entrySet()) {

             System.out.println(
                 entry.getKey() + " -> " + entry.getValue());
         }

         C#:

         foreach (var entry in studentMarks)
         */

        Console.WriteLine("\nAll Student Data:");

        foreach (var entry in studentMarks)
        {
            Console.WriteLine(
                entry.Key +
                " -> {" +
                string.Join(
                    ", ",
                    entry.Value.Select(
                        x => x.Key + "=" + x.Value)) +
                "}");
        }


        // =========================================================
        // AVERAGE MARKS PER STUDENT
        // =========================================================

        /*
         JAVA STREAM equivalent:

         Map<String, Double> avgMarks =
             studentMarks.entrySet()
                 .stream()
                 .collect(Collectors.toMap(
                     e -> e.getKey(),
                     e -> e.getValue()
                           .values()
                           .stream()
                           .mapToInt(Integer::intValue)
                           .average()
                           .orElse(0)
                 ));

         C#:
         ToDictionary() + Average()
        */

        Dictionary<string, double> avgMarks =
            studentMarks.ToDictionary(
                entry => entry.Key,
                entry => entry.Value.Values
                    .DefaultIfEmpty(0)
                    .Average());


        Console.WriteLine("\nAverage Marks:");

        foreach (var entry in avgMarks)
        {
            Console.WriteLine(
                entry.Key +
                " -> " +
                entry.Value);
        }


        // =========================================================
        // TOPPER
        // =========================================================

        /*
         JAVA STREAM:

         String topper =
             avgMarks.entrySet()
                 .stream()
                 .max(Map.Entry.comparingByValue())
                 .get()
                 .getKey();

         C# LINQ:
         OrderByDescending()
         First()
        */

        string topper =
            avgMarks
                .OrderByDescending(x => x.Value)
                .First()
                .Key;

        Console.WriteLine(
            "\nTopper: " + topper);


        // =========================================================
        // SUBJECT-WISE HIGHEST SCORE
        // =========================================================

        Dictionary<string, int> subjectTopScores =
            new Dictionary<string, int>();

        foreach (var student in studentMarks.Values)
        {
            foreach (var subject in student)
            {
                if (!subjectTopScores.ContainsKey(subject.Key) ||
                    subjectTopScores[subject.Key] < subject.Value)
                {
                    subjectTopScores[subject.Key] =
                        subject.Value;
                }
            }
        }

        Console.WriteLine(
            "\nSubject-wise Top Scores:");

        foreach (var entry in subjectTopScores)
        {
            Console.WriteLine(
                entry.Key +
                " -> " +
                entry.Value);
        }


        // =========================================================
        // NUMBER / TYPE OPERATIONS
        // =========================================================

        /*
         JAVA:
         Object x = 123;
         System.out.println(x instanceof Integer);

         C#:
         object x = 123;
         Console.WriteLine(x is int);
        */

        object x = 123;

        Console.WriteLine(
            "\nIs Integer: " +
            (x is int));


        double number = 10.5;

        Console.WriteLine(
            "Is NaN: " +
            double.IsNaN(number));

        Console.WriteLine(
            "Parse Int: " +
            int.Parse("123"));

        Console.WriteLine(
            "Parse Double: " +
            double.Parse("12.5"));

        Console.WriteLine(
            "Int Max: " +
            int.MaxValue);

        Console.WriteLine(
            "Int Min: " +
            int.MinValue);

        Console.WriteLine(
            "Binary: " +
            Convert.ToString(10, 2));

        Console.WriteLine(
            "Hex: " +
            Convert.ToString(255, 16));

        Console.WriteLine(
            "Fixed 2: " +
            12.3456.ToString("F2"));

        Console.WriteLine(
            "General 4: " +
            1234.5678.ToString("G4"));


        // =========================================================
        // MATH
        // =========================================================

        /*
         JAVA:
         Math.abs()
         Math.pow()
         Math.sqrt()
         Math.round()
         Math.floor()
         Math.ceil()
         Math.max()
         Math.min()
         Math.log()
         Math.log10()

         C#:
         Math.Abs()
         Math.Pow()
         Math.Sqrt()
         Math.Round()
         Math.Floor()
         Math.Ceiling()
         Math.Max()
         Math.Min()
         Math.Log()
         Math.Log10()
        */

        int a = -10;
        int b = 2;
        int c = 3;
        int d = 16;
        int e = 27;

        Console.WriteLine(
            "\nAbs: " +
            Math.Abs(a));

        Console.WriteLine(
            "Pow: " +
            Math.Pow(b, c));

        Console.WriteLine(
            "Sqrt: " +
            Math.Sqrt(d));

        Console.WriteLine(
            "Cube Root: " +
            Math.Round(
                Math.Pow(
                    e,
                    1.0 / 3.0)));

        Console.WriteLine(
            "Round: " +
            Math.Round(10.6));

        Console.WriteLine(
            "Floor: " +
            Math.Floor(10.9));

        Console.WriteLine(
            "Ceiling: " +
            Math.Ceiling(10.1));

        Console.WriteLine(
            "Max: " +
            Math.Max(10, 20));

        Console.WriteLine(
            "Min: " +
            Math.Min(10, 20));

        Console.WriteLine(
            "PI: " +
            Math.PI);

        Console.WriteLine(
            "E: " +
            Math.E);

        Console.WriteLine(
            "Sign: " +
            Math.Sign(-10));

        Console.WriteLine(
            "Natural Log: " +
            Math.Log(10));

        Console.WriteLine(
            "Log10: " +
            Math.Log10(10));


        // =========================================================
        // TRIGONOMETRY
        // =========================================================

        /*
         IMPORTANT:
         Java Math.sin(), cos(), tan()
         and C# Math.Sin(), Cos(), Tan()
         use RADIANS.

         Degrees -> Radians:
         radians = degrees * PI / 180
        */

        int angle = 90;

        double radians =
            angle * Math.PI / 180.0;

        Console.WriteLine(
            "\nSin 90: " +
            Math.Sin(radians));


        angle = 0;

        radians =
            angle * Math.PI / 180.0;

        Console.WriteLine(
            "Cos 0: " +
            Math.Cos(radians));


        angle = 45;

        radians =
            angle * Math.PI / 180.0;

        Console.WriteLine(
            "Tan 45: " +
            Math.Tan(radians));


        // =========================================================
        // HYPOTENUSE
        // =========================================================

        /*
         JAVA:
         double h = Math.sqrt(
             (3 * 3) + (4 * 4));

         C#:
         double h = Math.Sqrt(
             (3 * 3) + (4 * 4));
        */

        double hypotenuse =
            Math.Sqrt(
                (3 * 3) +
                (4 * 4));

        Console.WriteLine(
            "Hypotenuse: " +
            hypotenuse);


        // =========================================================
        // STRING OPERATIONS
        // =========================================================

        /*
         JAVA:
         String str = " Hello Java ";

         C#:
         string str = " Hello Java ";

         Java length() -> C# Length
         Java toUpperCase() -> C# ToUpper()
         Java trim() -> C# Trim()
         Java contains() -> C# Contains()
         Java indexOf() -> C# IndexOf()
         Java substring() -> C# Substring()
        */

        string str =
            " Hello Java ";

        Console.WriteLine(
            "\nLength: " +
            str.Length);

        Console.WriteLine(
            "Upper: " +
            str.ToUpper());

        Console.WriteLine(
            "Lower: " +
            str.ToLower());

        Console.WriteLine(
            "Trim: " +
            str.Trim());

        Console.WriteLine(
            "Contains: " +
            str.Contains("Hello"));

        Console.WriteLine(
            "IndexOf: " +
            str.IndexOf("H"));

        Console.WriteLine(
            "LastIndexOf: " +
            str.LastIndexOf("l"));

        Console.WriteLine(
            "Replace: " +
            str.Replace(
                "Java",
                "World"));

        Console.WriteLine(
            "Substring: " +
            str.Substring(2, 3));

        Console.WriteLine(
            "StartsWith: " +
            str.StartsWith(" H"));

        Console.WriteLine(
            "EndsWith: " +
            str.EndsWith(" "));


        // JAVA:
        // System.out.println(str.repeat(2));

        Console.WriteLine(
            "Repeat: " +
            string.Concat(
                Enumerable.Repeat(str, 2)));


        string[] parts =
            str.Trim().Split(" ");

        Console.WriteLine(
            "[" +
            string.Join(", ", parts) +
            "]");


        Console.WriteLine(
            (char)65);


        // =========================================================
        // ARRAY
        // =========================================================

        /*
         JAVA:
         int[] arr = {1, 2, 3};

         C#:
         int[] arr = {1, 2, 3};

         Both Java and C# arrays are fixed-size.
        */

        int[] arr =
        {
                1,
                2,
                3
            };

        Console.WriteLine(
            "\nArray: [" +
            string.Join(", ", arr) +
            "]");


        // =========================================================
        // LIST / ARRAYLIST
        // =========================================================

        /*
         JAVA:
         List<Integer> list =
             new ArrayList<>();

         C#:
         List<int> list =
             new List<int>();
        */

        List<int> list =
            new List<int>();

        list.Add(1);
        list.Add(2);

        list.Remove(1);

        Console.WriteLine(
            "List: [" +
            string.Join(", ", list) +
            "]");


        // =========================================================
        // LAMBDA
        // =========================================================

        /*
         JAVA:
         list.forEach(
             value -> System.out.println(value));

         C#:
         list.ForEach(
             value => Console.WriteLine(value));
        */

        list.ForEach(
            value =>
                Console.WriteLine(
                    "Value: " + value));


        // =========================================================
        // LINQ SELECT
        // =========================================================

        /*
         JAVA:
         List<Integer> mapped =
             list.stream()
                 .map(n -> n * 2)
                 .collect(Collectors.toList());

         C#:
         List<int> mapped =
             list.Select(n => n * 2)
                 .ToList();
        */

        List<int> mapped =
            list
                .Select(n => n * 2)
                .ToList();

        Console.WriteLine(
            "Mapped: [" +
            string.Join(", ", mapped) +
            "]");


        // =========================================================
        // DATE & TIME
        // =========================================================

        /*
         JAVA:
         LocalDate date = LocalDate.now();
         LocalDateTime dt = LocalDateTime.now();

         C#:
         DateTime date = DateTime.Now.Date;
         DateTime dt = DateTime.Now;
        */

        DateTime date =
            DateTime.Now.Date;

        DateTime dt =
            DateTime.Now;

        Console.WriteLine(
            "\nDate: " +
            date);

        Console.WriteLine(
            "DateTime: " +
            dt);

        Console.WriteLine(
            "Day: " +
            date.Day);

        Console.WriteLine(
            "DayOfWeek: " +
            date.DayOfWeek);

        Console.WriteLine(
            "Month: " +
            date.Month);

        Console.WriteLine(
            "Year: " +
            date.Year);

        Console.WriteLine(
            "Hour: " +
            dt.Hour);

        Console.WriteLine(
            "Minute: " +
            dt.Minute);


        DateTime newDate =
            date.AddDays(5);

        Console.WriteLine(
            "Date + 5 Days: " +
            newDate);


        Console.WriteLine(
            "Formatted: " +
            date.ToString(
                "yyyy-MM-dd"));


        // =========================================================
        // DICTIONARY
        // Java HashMap -> C# Dictionary
        // =========================================================

        /*
         JAVA:

         Map<String, Integer> obj =
             new HashMap<>();

         obj.put("a", 1);
         obj.put("b", 2);

         C#:

         Dictionary<string, int> obj =
             new Dictionary<string, int>();

         obj["a"] = 1;
         obj["b"] = 2;
        */

        Dictionary<string, int> obj =
            new Dictionary<string, int>();

        obj["a"] = 1;
        obj["b"] = 2;


        Console.WriteLine(
            "\nDictionary Keys: [" +
            string.Join(", ", obj.Keys) +
            "]");

        Console.WriteLine(
            "Dictionary Values: [" +
            string.Join(", ", obj.Values) +
            "]");


        /*
         JAVA:

         for (Map.Entry<String, Integer> e
                 : obj.entrySet()) {

             System.out.println(
                 e.getKey() + " = " + e.getValue());
         }

         C#:
        */

        foreach (
            KeyValuePair<string, int> entry
            in obj)
        {
            Console.WriteLine(
                entry.Key +
                " = " +
                entry.Value);
        }


        // =========================================================
        // HASHMAP
        // =========================================================

        Dictionary<string, string> map =
            new Dictionary<string, string>();

        map["id"] = "1";
        map["name"] = "Ali";

        Console.WriteLine(
            "\nName: " +
            map["name"]);

        Console.WriteLine(
            "Contains id: " +
            map.ContainsKey("id"));

        Console.WriteLine(
            "Count: " +
            map.Count);


        // =========================================================
        // HASHSET
        // =========================================================

        /*
         JAVA:

         Set<Integer> set =
             new HashSet<>();

         set.add(1);
         set.add(2);
         set.add(2);

         C#:

         HashSet<int> set =
             new HashSet<int>();

         set.Add(1);
         set.Add(2);
         set.Add(2);
        */

        HashSet<int> set =
            new HashSet<int>();

        set.Add(1);
        set.Add(2);
        set.Add(2);

        Console.WriteLine(
            "\nHashSet: [" +
            string.Join(", ", set) +
            "]");

        Console.WriteLine(
            "Contains 1: " +
            set.Contains(1));

        set.Remove(1);

        Console.WriteLine(
            "Count: " +
            set.Count);


        // =========================================================
        // REGEX
        // =========================================================

        /*
         JAVA:

         Pattern pattern =
             Pattern.compile("abc");

         Matcher matcher =
             pattern.matcher(text);

         C#:

         Regex pattern =
             new Regex("abc");

         MatchCollection matches =
             pattern.Matches(text);
        */

        string text =
            "abc123abc";

        Regex pattern =
            new Regex("abc");

        MatchCollection matches =
            pattern.Matches(text);


        foreach (Match match in matches)
        {
            Console.WriteLine(
                "\nMatch at: " +
                match.Index);
        }


        /*
         JAVA:

         text.replaceAll("abc", "X");

         C#:
        */

        Console.WriteLine(
            Regex.Replace(
                text,
                "abc",
                "X"));


        Console.WriteLine(
            "[" +
            string.Join(
                ", ",
                text.Split("123")) +
            "]");


        // =========================================================
        // TYPED ARRAY / BYTE BUFFER
        // =========================================================

        /*
         JAVA:

         ByteBuffer buffer =
             ByteBuffer.allocate(8);

         buffer.putInt(10);
         buffer.putInt(20);

         C# equivalent:
         byte[] buffer =
             new byte[8];

         C# commonly uses byte[],
         Memory<byte>, or Span<byte>.
        */

        byte[] buffer =
            new byte[8];


        BitConverter.GetBytes(10)
            .CopyTo(buffer, 0);

        BitConverter.GetBytes(20)
            .CopyTo(buffer, 4);


        Console.WriteLine(
            "\nBuffer Value 1: " +
            BitConverter.ToInt32(
                buffer,
                0));


        Console.WriteLine(
            "Buffer Value 2: " +
            BitConverter.ToInt32(
                buffer,
                4));


        // =========================================================
        // OBJECT ARRAY / COMPLEX OBJECT COLLECTION
        // =========================================================

        /*
         JAVA:

         Product[] products = {
             new Product("Laptop", 50000, "Dell"),
             new Product("Mouse", 1000, "Logitech"),
             new Product("Keyboard", 3000, "Dell")
         };

         C#:
        */

        Product[] products =
        {
                new Product(
                    "Laptop",
                    50000,
                    "Dell"),

                new Product(
                    "Mouse",
                    1000,
                    "Logitech"),

                new Product(
                    "Keyboard",
                    3000,
                    "Dell")
            };


        // =========================================================
        // FIND MAXIMUM OBJECT
        // =========================================================

        /*
         JAVA STREAM:

         Product maxProduct =
             Arrays.stream(products)
                 .max(
                     Comparator.comparingInt(
                         p -> p.price))
                 .orElse(null);

         C# LINQ:
        */

        Product maxProduct =
            products.OrderByDescending(p => p.Price).FirstOrDefault();

        Console.WriteLine(
            "\nMax Product: " +
            maxProduct.Name +
            " " +
            maxProduct.Price);


        // =========================================================
        // ARRAY ANALYTICS
        // =========================================================

        int[] nums =
        {
                10,
                50,
                20,
                80,
                30
            };


        /*
         JAVA:

         Arrays.stream(nums).max().getAsInt();

         C#:
         nums.Max();
        */

        int max =
            nums.Max();

        int min =
            nums.Min();

        int total =
            nums.Sum();

        double average =
            nums.Average();


        Console.WriteLine(
            "\nMax: " +
            max +
            ", Min: " +
            min +
            ", Total: " +
            total +
            ", Average: " +
            average);


        // =========================================================
        // FINAL JAVA -> C# QUICK REFERENCE
        // =========================================================

        /*
         =============================================================
         JAVA                         C#
         =============================================================

         ArrayList<T>                 List<T>
         List<T>                      List<T>
         HashMap<K,V>                 Dictionary<K,V>
         TreeMap<K,V>                 SortedDictionary<K,V>
         HashSet<T>                   HashSet<T>
         TreeSet<T>                   SortedSet<T>

         list.add(x)                  list.Add(x)
         list.get(i)                  list[i]
         list.set(i,x)                list[i] = x
         list.remove(i)              list.RemoveAt(i)
         list.size()                  list.Count

         map.put(k,v)                 map[k] = v
         map.get(k)                   map[k]
         map.remove(k)                map.Remove(k)
         map.containsKey(k)           map.ContainsKey(k)
         map.size()                   map.Count

         set.add(x)                   set.Add(x)
         set.remove(x)                set.Remove(x)
         set.contains(x)              set.Contains(x)

         stream.map()                 Select()
         stream.filter()              Where()
         stream.reduce()              Aggregate()
         stream.count()               Count()
         stream.anyMatch()            Any()
         stream.allMatch()            All()
         stream.findFirst()           FirstOrDefault()

         System.out.println()         Console.WriteLine()
         String                       string
         String.length()              Length
         String.toUpperCase()         ToUpper()
         String.toLowerCase()         ToLower()
         String.trim()                Trim()
         String.contains()            Contains()
         String.indexOf()             IndexOf()
         String.replace()             Replace()
         String.substring()           Substring()

         Math.abs()                   Math.Abs()
         Math.pow()                   Math.Pow()
         Math.sqrt()                  Math.Sqrt()
         Math.round()                 Math.Round()
         Math.floor()                 Math.Floor()
         Math.ceil()                  Math.Ceiling()
         Math.max()                   Math.Max()
         Math.min()                   Math.Min()
         Math.log()                   Math.Log()
         Math.log10()                 Math.Log10()

         LocalDate                    DateTime / DateOnly
         LocalDateTime                DateTime
         Pattern                      Regex
         Matcher                      Match / MatchCollection

         ByteBuffer                   byte[] / Memory<byte>
         CompletableFuture            Task / Task<T>
         lambda expression            lambda expression
         Stream API                   LINQ

         =============================================================
        */

        Console.WriteLine(
            "\n--- Program Completed ---");
    }
}
