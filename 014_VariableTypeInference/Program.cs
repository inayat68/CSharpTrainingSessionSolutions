using System;

namespace VariableTypeInference_08;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 08_VariableTypeInference ===");
        Console.WriteLine("var and const");
        Console.WriteLine();


        // ============================================================
        // 1. var
        // ============================================================
        // C# 'var' lets the compiler infer the type.
        // It is still strongly typed.

        var name = "Hello";     // string
        var count = 5;         // int

        Console.WriteLine($"Name={name}, Count={count}");

        // name = 100;         // ❌ Compile-time error
        // count = "Five";     // ❌ Compile-time error

        // Java equivalent:
        // var name = "Hello"; // Java 10+
        // var count = 5;


        // ============================================================
        // 2. const
        // ============================================================
        // const value cannot be changed after declaration.

        const int maxCount = 5;

        Console.WriteLine($"Max Count={maxCount}");

        // maxCount = 10;      // ❌ Compile-time error

        // Java equivalent:
        // final int maxCount = 5;
        //
        // Java uses 'final' where C# commonly uses 'const'.


        // ============================================================
        // 3. var vs const
        // ============================================================
        //
        // C#:
        // var x = 10;         // Type inferred, value can change
        // const int y = 10;   // Type specified, value cannot change
        //
        // Java:
        // var x = 10;         // Java 10+
        // final int y = 10;   // Constant/reference cannot be reassigned


        // OUTPUT:
        // Name=Hello, Count=5
        // Max Count=5


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}