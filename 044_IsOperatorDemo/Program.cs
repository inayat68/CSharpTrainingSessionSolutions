using System;

namespace IsOperatorDemo_44;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== C# is Operator ===");

        // 1. Type checking
        object value = 34;
        Console.WriteLine(value is int); // True
        // Java equivalent:
        // System.out.println(value instanceof Integer); // true

        // 2. Type check + variable
        if (value is int number)
            Console.WriteLine($"Number: {number}"); // 34
        // Java equivalent:
        // if (value instanceof Integer number)
        //     System.out.println("Number: " + number);

        // 3. Null check
        string? input = null;
        Console.WriteLine(input is null); // True
        // Java equivalent:
        // System.out.println(input == null); // true

        // 4. Not null
        string result = "Hello";
        if (result is not null)
            Console.WriteLine(result);
        // Java equivalent:
        // if (result != null)
        //     System.out.println(result);

        // 5. Multiple type checks
        object a = 34;
        object b = "Hello";

        //x and text are pattern variables. They are created by the is operator when the type check succeeds.
        if (a is int x && b is string text)
            Console.WriteLine($"{x} - {text}");
        // Java equivalent:
        // if (a instanceof Integer x && b instanceof String text)
        //     System.out.println(x + " - " + text);

        // 6. Property pattern
        DateTime date = new DateTime(2026, 10, 2);
        Console.WriteLine(date is { Month: 10, Day: <= 7 }); // True

        // Java: No direct equivalent to C# property pattern.
        // Java normally uses date.getMonthValue() and date.getDayOfMonth().
    }
}