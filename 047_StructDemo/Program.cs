using System;

namespace StructDemo_47;
public class Program
{
    // C# struct is a value type.
    // Useful for small data objects such as Point, Date, Money, etc.
    struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Display()
        {
            Console.WriteLine($"Point: ({X}, {Y})");
        }
    }

    public static void Main(string[] args)
    {
        // Create a struct value
        Point p1 = new Point(10, 20);
        p1.Display();

        // Structs are copied by value
        Point p2 = p1;
        p2.X = 100;

        Console.WriteLine($"p1.X = {p1.X}"); // 10
        Console.WriteLine($"p2.X = {p2.X}"); // 100

        /*
        Java equivalent:

        // Java has no direct struct equivalent.
        // A class is normally used for the same purpose.

        class Point {
            int x;
            int y;

            Point(int x, int y) {
                this.x = x;
                this.y = y;
            }
        }

        Point p1 = new Point(10, 20);
        Point p2 = p1;

        p2.x = 100;

        // p1.x is also 100 because both references
        // point to the same object.
        */
    }
}