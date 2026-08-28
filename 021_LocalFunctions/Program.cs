using System;

namespace LocalFunctions_17;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 17_LocalFunctions ===");
        Console.WriteLine("Local function inside Main");
        Console.WriteLine();


        // ============================================================
        // LOCAL FUNCTION
        // ============================================================
        // C# allows a method/function to be defined inside another method.

        int Add(int a, int b) => a + b;

        Console.WriteLine(Add(10, 20));

        // OUTPUT:
        // 30

        // Java:
        // Java does NOT support local methods directly inside a method.
        //
        // A separate method is required:
        //
        // static int Add(int a, int b) {
        //     return a + b;
        // }
        //
        // System.out.println(Add(10, 20));


        // ============================================================
        // C# → JAVA
        // ============================================================
        //
        // C#:
        // int Add(int a, int b) => a + b;
        //
        // Java:
        // static int Add(int a, int b) {
        //     return a + b;
        // }
        //
        // Difference:
        // C# supports local functions.
        // Java does not support methods declared inside methods.


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}