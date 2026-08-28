using System;

namespace StringInterpolation_09;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 09_StringInterpolation ===");
        Console.WriteLine("String interpolation");
        Console.WriteLine();


        // ============================================================
        // C# STRING INTERPOLATION
        // ============================================================
        // Prefix the string with $ and put variables inside { }.

        string name = "Ali";
        int count = 5;

        Console.WriteLine($"Hello, {name}! Count: {count}");

        // OUTPUT:
        // Hello, Ali! Count: 5


        // Java equivalent:
        //
        // String name = "Ali";
        // int count = 5;
        //
        // Java does not use C#-style $ interpolation.
        // Common Java approach:
        //
        // System.out.println(
        //     String.format("Hello, %s! Count: %d", name, count)
        // );


        // ============================================================
        // C# INTERPOLATION WITH EXPRESSIONS
        // ============================================================

        int a = 10;
        int b = 20;

        Console.WriteLine($"Sum = {a + b}");

        // OUTPUT:
        // Sum = 30

        // Java:
        // System.out.println("Sum = " + (a + b));


        // ============================================================
        // NUMBER FORMATTING
        // ============================================================
        // C# supports format specifiers directly inside { }.

        double salary = 1234.5678;

        Console.WriteLine($"Salary = {salary:F2}");

        // OUTPUT:
        // Salary = 1234.57

        // Java:
        // System.out.printf("Salary = %.2f%n", salary);


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}