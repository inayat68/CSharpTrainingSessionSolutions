using System;
using System.IO;

namespace UsingStatement_19;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 19_UsingStatement ===");
        Console.WriteLine("using statement and disposal");
        Console.WriteLine();

        // ============================================================
        // USING STATEMENT
        // ============================================================
        // 'using' automatically calls Dispose() when the scope ends.
        // Commonly used for files, streams, database connections, etc.

        using var writer = new StringWriter();

        writer.WriteLine("Hello");

        Console.WriteLine(writer.ToString());

        // OUTPUT:
        // Hello

        // Java equivalent:
        // Java uses try-with-resources for AutoCloseable resources.
        //
        // try (StringWriter writer = new StringWriter()) {
        //     writer.write("Hello\n");
        //     System.out.println(writer.toString());
        // }


        // ============================================================
        // USING BLOCK
        // ============================================================
        // C# also supports the traditional using block.

        using (var writer2 = new StringWriter())
        {
            writer2.WriteLine("Hello from using block");
            Console.WriteLine(writer2.ToString());
        }

        // Java:
        // try (StringWriter writer2 = new StringWriter()) {
        //     writer2.write("Hello from using block\n");
        //     System.out.println(writer2.toString());
        // }


        // C#                         Java
        // ------------------------------------------------------------
        // using                     try-with-resources
        // IDisposable               AutoCloseable
        // Dispose()                 close()
        // using var                 try (resource)


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}