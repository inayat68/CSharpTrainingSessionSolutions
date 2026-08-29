using System;
using System.Text;
using mm = System.Math;

namespace StandardLibraries_02;

public class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("=== Java vs C# Standard Libraries ===");
        Console.WriteLine();

        // StringBuilder
        StringBuilder sb = new StringBuilder();
        sb.Append("Hello ");
        sb.Append("C#");

        Console.WriteLine(sb.ToString());

        // Math.Pow() → raises a number to a power
        double result = mm.Pow(2, 3);

        Console.WriteLine("2^3 = " + result);

        Console.WriteLine();
        Console.WriteLine("=== JAVA vs C# ===");

        Console.WriteLine("Console  : Java System.out.println() -> C# Console.WriteLine()");
        Console.WriteLine("List     : Java ArrayList<String> -> C# List<string>");
        Console.WriteLine("String   : Java StringBuilder -> C# StringBuilder");
        Console.WriteLine("Math     : Java Math.pow(2, 3) -> C# Math.Pow(2, 3)");

        Console.WriteLine();
        Console.WriteLine("=== NAMESPACES ===");

        Console.WriteLine("Java StringBuilder : java.lang.StringBuilder");
        Console.WriteLine("C# StringBuilder   : System.Text.StringBuilder");

        Console.WriteLine("Java Math          : java.lang.Math");
        Console.WriteLine("C# Math             : System.Math");

        Console.WriteLine("Java Collections   : java.util");
        Console.WriteLine("C# Collections     : System.Collections.Generic");

        Console.WriteLine();
        Console.WriteLine("Done.");

        Console.WriteLine("");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

    }
}