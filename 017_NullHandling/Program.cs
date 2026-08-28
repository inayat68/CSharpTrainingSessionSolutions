using System;

namespace NullHandling_11;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 11_NullHandling ===");
        Console.WriteLine("Null handling");
        Console.WriteLine();


        // ============================================================
        // 1. NULL-COALESCING OPERATOR
        // ============================================================
        // ?? returns the right-side value when the left side is null.

        string? input = null;

        string value = input ?? "default";

        Console.WriteLine(value);

        // OUTPUT:
        // default

        // Java equivalent:
        // String input = null;
        // String value = input != null ? input : "default";


        // ============================================================
        // 2. NULL-CONDITIONAL OPERATOR
        // ============================================================
        // ?. safely accesses a member when the object is not null.

        string? name = null;

        Console.WriteLine(name?.Length);

        // OUTPUT:
        // <empty>

        // Java:
        // String name = null;
        // Java does not have a direct ?. operator.
        // Common approach:
        // System.out.println(name != null ? name.length() : null);


        // ============================================================
        // 3. NULL-COALESCING ASSIGNMENT
        // ============================================================
        // ??= assigns a value only when the variable is null.

        string? username = null;

        username ??= "Guest";

        Console.WriteLine(username);

        // OUTPUT:
        // Guest

        // Java:
        // String username = null;
        // if (username == null)
        //     username = "Guest";


        // ============================================================
        // C# NULL OPERATORS
        // ============================================================
        //
        // ??   → use default when null
        // ?.   → safely access member
        // ??=  → assign only when null
        //
        // Java has no direct equivalent for these operators.


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}