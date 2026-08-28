using System;

namespace StructNullableDemo_43;

public struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== C# struct & nullable reference ===");

        // =========================================================
        // 1. struct
        // =========================================================
        // C#: struct is a value type.
        // Java: No direct equivalent; Java primarily uses classes
        // as reference types and separate primitive types.

        Point p1 = new Point(10, 20);
        Point p2 = p1;

        p2.X = 100;

        Console.WriteLine($"p1.X = {p1.X}"); // 10
        Console.WriteLine($"p2.X = {p2.X}"); // 100

        // p2 is a copy of p1 because Point is a value type.


        // =========================================================
        // 2. Nullable reference type
        // =========================================================
        // C#: string? explicitly allows null.
        // Java: String can directly contain null; Java has no
        // equivalent ? annotation.

        string? name = null;

        Console.WriteLine($"Name: {name ?? "No Name"}");
        // OUTPUT: Name: No Name

        // C#:
        // string? name = null;

        // Java:
        // String name = null;
    }
}