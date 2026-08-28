
using System;
using System.Collections;
using System.Collections.Generic;

namespace ArrayDemo_38;

class Program
{
    static void Main(string[] args)
    {
        // ============================================================
        // ARRAYS OF VARIOUS DATA TYPES AND OBJECTS
        // C# vs JAVA
        // ============================================================

        // ------------------------------------------------------------
        // 1. INTEGER ARRAY
        // ------------------------------------------------------------

        // JAVA:
        // int[] numbers = { 10, 20, 30, 40, 50 };

        // C#:
        int[] numbers = { 10, 20, 30, 40, 50 };

        Console.WriteLine("Integer Array:");
        foreach (int n in numbers)
        {
            Console.WriteLine(n);
        }

        // Java:
        // for (int n : numbers) {
        //     System.out.println(n);
        // }


        // ------------------------------------------------------------
        // 2. DOUBLE ARRAY
        // ------------------------------------------------------------

        // JAVA:
        // double[] prices = { 10.5, 20.75, 30.99 };

        // C#:
        double[] prices = { 10.5, 20.75, 30.99 };

        Console.WriteLine("\nDouble Array:");
        foreach (double price in prices)
        {
            Console.WriteLine(price);
        }

        // Java:
        // for (double price : prices) {
        //     System.out.println(price);
        // }


        // ------------------------------------------------------------
        // 3. CHAR ARRAY
        // ------------------------------------------------------------

        // JAVA:
        // char[] letters = { 'A', 'B', 'C', 'D' };

        // C#:
        char[] letters = { 'A', 'B', 'C', 'D' };

        Console.WriteLine("\nCharacter Array:");
        foreach (char ch in letters)
        {
            Console.WriteLine(ch);
        }

        // Java:
        // for (char ch : letters) {
        //     System.out.println(ch);
        // }


        // ------------------------------------------------------------
        // 4. STRING ARRAY
        // ------------------------------------------------------------

        // JAVA:
        // String[] names = {
        //     "Ali",
        //     "Sara",
        //     "John"
        // };

        // C#:
        string[] names =
        {
            "Ali",
            "Sara",
            "John"
        };

        Console.WriteLine("\nString Array:");
        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        // Java:
        // for (String name : names) {
        //     System.out.println(name);
        // }


        // ------------------------------------------------------------
        // 5. BOOLEAN ARRAY
        // ------------------------------------------------------------

        // JAVA:
        // boolean[] flags = { true, false, true };

        // C#:
        bool[] flags = { true, false, true };

        Console.WriteLine("\nBoolean Array:");
        foreach (bool flag in flags)
        {
            Console.WriteLine(flag);
        }

        // Java:
        // for (boolean flag : flags) {
        //     System.out.println(flag);
        // }


        // ------------------------------------------------------------
        // 6. ARRAY USING NEW
        // ------------------------------------------------------------

        // JAVA:
        // int[] numbers2 = new int[5];

        // C#:
        int[] numbers2 = new int[5];

        numbers2[0] = 10;
        numbers2[1] = 20;

        Console.WriteLine("\nArray using new:");
        Console.WriteLine(numbers2[0]);
        Console.WriteLine(numbers2[1]);

        // Java:
        // numbers2[0] = 10;
        // numbers2[1] = 20;


        // ------------------------------------------------------------
        // 7. OBJECT ARRAY - SAME TYPE
        // ------------------------------------------------------------

        // JAVA:
        // Object[] objects = {
        //     "Ali",
        //     100,
        //     25.5,
        //     true
        // };

        // C#:
        object[] objects =
        {
            "Ali",
            100,
            25.5,
            true
        };

        Console.WriteLine("\nObject Array:");

        foreach (object obj in objects)
        {
            Console.WriteLine(
                obj + " -> " + obj.GetType().Name);
        }

        // Java:
        // for (Object obj : objects) {
        //     System.out.println(
        //         obj + " -> " + obj.getClass().getSimpleName()
        //     );
        // }

        // OUTPUT:
        // Ali -> String
        // 100 -> Int32
        // 25.5 -> Double
        // True -> Boolean


        // ------------------------------------------------------------
        // 8. MIXED DATA TYPES USING OBJECT[]
        // ------------------------------------------------------------

        // C# object[] can contain different data types.
        object[] mixed =
        {
            100,
            "Hello",
            25.5,
            true,
            'A'
        };

        Console.WriteLine("\nMixed Object Array:");

        foreach (object value in mixed)
        {
            Console.WriteLine(value);
        }

        // Java equivalent:
        //
        // Object[] mixed = {
        //     100,
        //     "Hello",
        //     25.5,
        //     true,
        //     'A'
        // };


        // ------------------------------------------------------------
        // 9. ARRAY OF CUSTOM OBJECTS
        // ------------------------------------------------------------

        // JAVA:
        // User[] users = {
        //     new User("Ali", 25),
        //     new User("Sara", 22)
        // };

        // C#:
        User[] users =
        {
            new User("Ali", 25),
            new User("Sara", 22)
        };

        Console.WriteLine("\nUser Object Array:");

        foreach (User user in users)
        {
            Console.WriteLine(
                user.Name + " - " + user.Age);
        }

        // Java:
        // for (User user : users) {
        //     System.out.println(
        //         user.name + " - " + user.age
        //     );
        // }


        // ------------------------------------------------------------
        // 10. PRODUCT OBJECT ARRAY
        // ------------------------------------------------------------

        // JAVA:
        // Product[] products = {
        //     new Product("Laptop", 50000),
        //     new Product("Mouse", 1000),
        //     new Product("Keyboard", 3000)
        // };

        // C#:
        Product[] products =
        {
            new Product("Laptop", 50000),
            new Product("Mouse", 1000),
            new Product("Keyboard", 3000)
        };

        Console.WriteLine("\nProduct Object Array:");

        foreach (Product product in products)
        {
            Console.WriteLine(
                product.Name + " : " + product.Price);
        }


        // ------------------------------------------------------------
        // 11. ACCESS OBJECT ARRAY BY INDEX
        // ------------------------------------------------------------

        Console.WriteLine("\nFirst Product:");

        Console.WriteLine(products[0].Name);
        Console.WriteLine(products[0].Price);

        // Java:
        // System.out.println(products[0].name);
        // System.out.println(products[0].price);


        // ------------------------------------------------------------
        // 12. MODIFY OBJECT IN ARRAY
        // ------------------------------------------------------------

        products[1].Price = 1500;

        Console.WriteLine("\nUpdated Mouse Price:");
        Console.WriteLine(products[1].Price);

        // Java:
        // products[1].price = 1500;
        // System.out.println(products[1].price);


        // ------------------------------------------------------------
        // 13. OBJECT ARRAY WITH CUSTOM OBJECT + PRIMITIVE VALUES
        // ------------------------------------------------------------

        object[] data =
        {
            new User("Ahmed", 30),
            100,
            "Pakistan",
            99.5,
            true
        };

        Console.WriteLine("\nMixed Object + Custom Object:");

        foreach (object value in data)
        {
            Console.WriteLine(value);
        }

        // Java:
        // Object[] data = {
        //     new User("Ahmed", 30),
        //     100,
        //     "Pakistan",
        //     99.5,
        //     true
        // };


        // ------------------------------------------------------------
        // 14. TYPE CHECKING
        // ------------------------------------------------------------

        object[] values =
        {
            100,
            "Hello",
            25.5,
            true
        };

        Console.WriteLine("\nType Checking:");

        foreach (object value in values)
        {
            if (value is int)
            {
                Console.WriteLine("Integer: " + value);
            }
            else if (value is string)
            {
                Console.WriteLine("String: " + value);
            }
            else if (value is double)
            {
                Console.WriteLine("Double: " + value);
            }
            else if (value is bool)
            {
                Console.WriteLine("Boolean: " + value);
            }
        }

        // Java equivalent:
        //
        // if (value instanceof Integer) {
        //     System.out.println("Integer: " + value);
        // }
        // else if (value instanceof String) {
        //     System.out.println("String: " + value);
        // }
        // else if (value instanceof Double) {
        //     System.out.println("Double: " + value);
        // }
        // else if (value instanceof Boolean) {
        //     System.out.println("Boolean: " + value);
        // }


        // ------------------------------------------------------------
        // 15. MULTI-DIMENSIONAL ARRAY
        // ------------------------------------------------------------

        // JAVA:
        // int[][] matrix = {
        //     {1, 2, 3},
        //     {4, 5, 6}
        // };

        // C#:
        int[,] matrix =
        {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };

        Console.WriteLine("\n2D Array:");

        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                Console.Write(matrix[row, col] + " ");
            }

            Console.WriteLine();
        }

        // Java:
        // for (int row = 0; row < matrix.length; row++) {
        //     for (int col = 0; col < matrix[row].length; col++) {
        //         System.out.print(matrix[row][col] + " ");
        //     }
        //     System.out.println();
        // }


        // ------------------------------------------------------------
        // 16. JAGGED ARRAY
        // ------------------------------------------------------------

        // C# jagged array = array of arrays.
        //
        // JAVA:
        // int[][] jagged = {
        //     {1, 2},
        //     {3, 4, 5},
        //     {6}
        // };

        // C#:
        int[][] jagged =
        {
            new int[] { 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6 }
        };

        Console.WriteLine("\nJagged Array:");

        foreach (int[] row in jagged)
        {
            foreach (int value in row)
            {
                Console.Write(value + " ");
            }

            Console.WriteLine();
        }


        // ------------------------------------------------------------
        // 17. ARRAY LENGTH
        // ------------------------------------------------------------

        Console.WriteLine("\nArray Length:");
        Console.WriteLine(numbers.Length);

        // Java:
        // System.out.println(numbers.length);

        // C#:
        // numbers.Length
        //
        // Java:
        // numbers.length


        // ------------------------------------------------------------
        // 18. ARRAY OF OBJECTS + LINQ
        // ------------------------------------------------------------

        // C# can use LINQ over object arrays.

        int[] marks =
        {
            50, 80, 90, 60, 95
        };

        int highest = 0;

        foreach (int mark in marks)
        {
            if (mark > highest)
                highest = mark;
        }

        Console.WriteLine("\nHighest Mark: " + highest);

        // Java equivalent:
        //
        // int highest = 0;
        //
        // for (int mark : marks) {
        //     if (mark > highest)
        //         highest = mark;
        // }


        // ============================================================
        // QUICK COMPARISON
        // ============================================================

        /*
        +---------------------------+-----------------------------+-----------------------------+
        | ARRAY                     | JAVA                        | C#                          |
        +---------------------------+-----------------------------+-----------------------------+
        | Integer array             | int[]                       | int[]                       |
        | Double array              | double[]                    | double[]                    |
        | String array              | String[]                    | string[]                    |
        | Character array           | char[]                      | char[]                      |
        | Boolean array             | boolean[]                   | bool[]                      |
        | Object array              | Object[]                    | object[]                    |
        | Custom object array       | User[]                      | User[]                      |
        | Array length              | arr.length                  | arr.Length                  |
        | Array index               | arr[i]                      | arr[i]                      |
        | 2D array                  | int[][]                    | int[,] / int[][]            |
        | Jagged array              | int[][]                    | int[][]                     |
        | Type checking             | instanceof                 | is                          |
        | Cast                      | (Type)obj                  | (Type)obj                   |
        +---------------------------+-----------------------------+-----------------------------+
        */

        /*
         IMPORTANT:

         C# primitive/value types:
             int, double, bool, char

         C# reference/object types:
             string, User, Product, object

         object[] can hold different types:
             int
             string
             double
             bool
             custom objects

         Example:
             object[] data = { 10, "Ali", 25.5, true };

         Java equivalent:
             Object[] data = { 10, "Ali", 25.5, true };

         The main difference is that Java uses Object as the
         common reference type, while C# uses object (alias of
         System.Object).
        */
    }


    // ============================================================
    // SUPPORT CLASSES
    // ============================================================

    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            return $"{{name={Name}, age={Age}}}";
        }

        // JAVA:
        /*
        class User {

            String name;
            int age;

            User(String name, int age) {
                this.name = name;
                this.age = age;
            }

            @Override
            public String toString() {
                return "{name=" + name +
                       ", age=" + age + "}";
            }
        }
        */
    }


    class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{{name={Name}, price={Price}}}";
        }

        // JAVA:
        /*
        class Product {

            String name;
            int price;

            Product(String name, int price) {
                this.name = name;
                this.price = price;
            }

            @Override
            public String toString() {
                return "{name=" + name +
                       ", price=" + price + "}";
            }
        }
        */
    }
}