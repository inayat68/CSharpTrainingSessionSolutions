using System;

namespace OperatorOverload_40;

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Operator overloading
    public static Point operator +(Point p1, Point p2)
    {
        return new Point(
            p1.X + p2.X,
            p1.Y + p2.Y
        );
    }
}

public class Program
{
    public static void Main()
    {
        Point p1 = new Point(10, 20);
        Point p2 = new Point(5, 8);

        // Normally + works with numbers.
        // Here, we define what + means for Point objects.

        Point result = p1 + p2;

        Console.WriteLine($"X = {result.X}");
        Console.WriteLine($"Y = {result.Y}");

        // OUTPUT:
        // X = 15
        // Y = 28
    }
}
