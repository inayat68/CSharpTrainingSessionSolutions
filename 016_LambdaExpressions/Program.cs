using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdaExpressions_10;

public class Program
{
    //Introduction to LINQ
    //https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/introduction-to-linq-queries
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 10_LambdaExpressions ===");
        Console.WriteLine("Lambda expressions");
        Console.WriteLine();

        // C# lambda: x => x > 3
        // Java equivalent: x -> x > 3

        var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

        var result = numbers
            .Where(x => x > 3)
            .ToList();

        Console.WriteLine(string.Join(", ", result));

        // OUTPUT:
        // 4, 5, 6

        // Java:
        // List<Integer> result = numbers.stream()
        //     .filter(x -> x > 3)
        //     .collect(Collectors.toList());


        // ============================================================
        // Lambda with multiple parameters
        // ============================================================

        var sum = (int a, int b) => a + b;

        Console.WriteLine(sum(10, 20));

        // OUTPUT:
        // 30

        // Java:
        // BiFunction<Integer, Integer, Integer> sum =
        //     (a, b) -> a + b;


        // ============================================================
        // Lambda + LINQ
        // ============================================================

        var doubled = numbers
            .Where(x => x > 3)
            .Select(x => x * 2)
            .ToList();

        Console.WriteLine(string.Join(", ", doubled));

        // OUTPUT:
        // 8, 10, 12

        // Java:
        // numbers.stream()
        //     .filter(x -> x > 3)
        //     .map(x -> x * 2)
        //     .collect(Collectors.toList());


        // C# LINQ        → Java Stream API
        // x => x > 3     → x -> x > 3
        // Where()        → filter()
        // Select()       → map()

        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}