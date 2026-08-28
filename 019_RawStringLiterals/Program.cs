using System;

namespace RawStringLiterals_13;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 13_RawStringLiterals ===");
        Console.WriteLine("Verbatim and Raw String Literals");
        Console.WriteLine();


        // ============================================================
        // 1. VERBATIM STRING - @
        // ============================================================
        // Backslashes do not need escaping.

        string path = @"C:\Users\Ali\Documents\Test.txt";

        Console.WriteLine(path);

        // OUTPUT:
        // C:\Users\Ali\Documents\Test.txt

        // Java:
        // String path = "C:\\Users\\Ali\\Documents\\Test.txt";


        // ============================================================
        // 2. VERBATIM MULTILINE STRING
        // ============================================================

        string text = @"Hello,
                    This is a multiline
            C# string.";
                
        Console.WriteLine(text);

        // Java:
        // String text = """
        // Hello,
        // This is a multiline
        // Java string.
        // """;


        // ============================================================
        // 3. RAW STRING - """
        // ============================================================
        // C# 11+; useful for JSON, XML, SQL and multiline text.

        string json = """
        {
          "name": "Ali",
          "active": true
        }
        """;

        Console.WriteLine(json);

        // Java 15+:
        // String json = """
        // {
        //   "name": "Ali",
        //   "active": true
        // }
        // """;


        // ============================================================
        // @ VERBATIM vs """ RAW
        // ============================================================
        //
        // @""       → Backslashes do not need escaping
        // """...""" → Raw multiline text; minimal escaping
        //
        // C# 11+ Raw String:
        // string json = """
        // {
        //   "name": "Ali"
        // }
        // """;


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}