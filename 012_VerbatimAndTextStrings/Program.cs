using System;

namespace VerbatimAndTextStrings_28;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 28_VerbatimAndTextStrings ===");
        Console.WriteLine("Verbatim and multiline strings");
        Console.WriteLine();

        // ============================================================
        // VERBATIM STRING - @
        // ============================================================
        // Backslashes do not need escaping.

        string path = @"C:\Projects\MyApp\data.txt";

        Console.WriteLine(path);

        // OUTPUT:
        // C:\Projects\MyApp\data.txt

        // Java:
        // String path = "C:\\Projects\\MyApp\\data.txt";


        // ============================================================
        // VERBATIM MULTILINE STRING
        // ============================================================

        string multiline = @"Line 1
Line 2
Line 3";

        Console.WriteLine(multiline);

        // Java 15+ Text Block:
        //
        // String multiline = """
        // Line 1
        // Line 2
        // Line 3
        // """;
        //
        // System.out.println(multiline);


        // ============================================================
        // C# vs JAVA
        // ============================================================
        //
        // C# @""          → Verbatim string
        // Java """..."""  → Text block
        //
        // C# @"" mainly avoids escaping backslashes.
        // Java text blocks are designed for multiline text.
        //
        // C# also supports raw strings using """ in C# 11+.


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}