using System;
using System.Collections.Generic;

namespace UsingKeyword_25;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 25_UsingKeyword ===");
        Console.WriteLine("using keyword for namespaces");
        Console.WriteLine();

        // C# 'using' imports a namespace.
        // Java uses 'import' for the same purpose.

        var list = new List<string> { "Ali", "Saad" };

        Console.WriteLine(string.Join(", ", list));

        // OUTPUT:
        // Ali, Saad

        // Java:
        // import java.util.ArrayList;
        //
        // ArrayList<String> list = new ArrayList<>();
        // list.add("Ali");
        // list.add("Saad");
        //
        // System.out.println(String.join(", ", list));


        // ============================================================
        // C# using → Java import
        // ============================================================
        //
        // C#: using System.Collections.Generic;
        // Java: import java.util.ArrayList;
        //
        // C#: List<string>
        // Java: List<String>
        //
        // Note:
        // C# 'using' is also used for resource disposal,
        // which is different from Java's 'import'.


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}