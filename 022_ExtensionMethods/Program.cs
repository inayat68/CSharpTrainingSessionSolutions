using System;

namespace ExtensionMethods_15;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 15_ExtensionMethods ===");
        Console.WriteLine("Extension methods");
        Console.WriteLine();


        // ============================================================
        // EXTENSION METHOD
        // ============================================================
        // Adds a method to an existing type without modifying it.
        //
        // C# uses the 'this' keyword on the first parameter.

        string text = "hello";

        Console.WriteLine(text.Capitalize());

        // OUTPUT:
        // Hello

        // Java:
        // Java has no direct equivalent of C# extension methods.
        //
        // A utility method is commonly used instead:
        //
        // static String capitalize(String value) {
        //     return value.length() == 0
        //         ? value
        //         : Character.toUpperCase(value.charAt(0))
        //             + value.substring(1);
        // }
        //
        // System.out.println(capitalize(text));


        // ============================================================
        // C# → JAVA
        // ============================================================
        //
        // C#:
        // text.Capitalize();
        //
        // Java:
        // capitalize(text);
        //
        // C# extension methods provide instance-style syntax.


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}


// ================================================================
// EXTENSION METHOD CLASS
// ================================================================
// Must be a static class and the method must be static.

public static class StringExtensions
{
    public static string Capitalize(this string value)
    {
        return value.Length == 0
            ? value
            : char.ToUpper(value[0]) + value[1..];
    }
}