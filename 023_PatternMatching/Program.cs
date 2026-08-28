using System;

namespace PatternMatching_12;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 12_PatternMatching ===");
        Console.WriteLine("is and switch pattern matching");
        Console.WriteLine();

        // ============================================================
        // 1. is PATTERN
        // ============================================================
        // C# 'is' checks type and creates a typed variable.
        // Java uses 'instanceof'.

        object value = 42;

        if (value is int number && number > 10)
        {
            Console.WriteLine($"Integer > 10: {number}");
        }

        // OUTPUT:
        // Integer > 10: 42

        // Java:
        // if (value instanceof Integer number && number > 10)
        //     System.out.println("Integer > 10: " + number);


        // ============================================================
        // 2. switch EXPRESSION
        // ============================================================
        // C# switch expression returns a value.

        string result = value switch
        {
            int n when n > 10 => "Large integer",
            int => "Integer",
            _ => "Other"
        };

        Console.WriteLine(result);

        // OUTPUT:
        // Large integer

        // Java:
        // String result;
        // if (value instanceof Integer n && n > 10)
        //     result = "Large integer";
        // else if (value instanceof Integer)
        //     result = "Integer";
        // else
        //     result = "Other";


        // ============================================================
        // 3. TYPE PATTERN
        // ============================================================

        object name = "Ali";

        if (name is string text)
        {
            Console.WriteLine($"String: {text}");
        }

        // OUTPUT:
        // String: Ali

        // Java:
        // if (name instanceof String text)
        //     System.out.println("String: " + text);


        // C#                     Java
        // -----------------------------------------------
        // is                     instanceof
        // switch pattern         switch / instanceof
        // _                      default / else
        // when                   additional condition

        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}