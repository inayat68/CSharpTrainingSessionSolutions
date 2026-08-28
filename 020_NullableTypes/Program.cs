using System;

namespace NullableTypes_14;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 14_NullableTypes ===");
        Console.WriteLine("Nullable value/reference types");
        Console.WriteLine();


        // ============================================================
        // 1. NULLABLE VALUE TYPE - ?
        // ============================================================
        // int normally cannot contain null.
        // int? allows int to contain null.

        int? age = null;

        Console.WriteLine(age ?? 0);

        // OUTPUT:
        // 0

        // Java:
        // Integer age = null;
        // System.out.println(age != null ? age : 0);
        //
        // Java uses wrapper class Integer.
        // C# uses nullable value type int?.


        // ============================================================
        // 2. NULLABLE REFERENCE TYPE - ?
        // ============================================================
        // string? indicates that the reference may be null.

        string? name = null;

        Console.WriteLine(name?.Length ?? 0);

        // OUTPUT:
        // 0

        // Java:
        // String name = null;
        // System.out.println(
        //     name != null ? name.length() : 0
        // );
        //
        // Java does not have C# nullable reference type syntax.


        // ============================================================
        // C# NULL OPERATORS
        // ============================================================
        //
        // int?   → nullable value type
        // string? → nullable reference type
        // ??     → use value when null
        // ?.     → safely access member


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}