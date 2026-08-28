using System;

namespace ExceptionHandling_05;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 05_ExceptionHandling ===");
        Console.WriteLine("try-catch-finally");
        Console.WriteLine();

        // ============================================================
        // C# try-catch-finally is almost the same as Java.
        //
        // Java:
        // try { ... }
        // catch (Exception ex) { ... }
        // finally { ... }
        //
        // C#:
        // try { ... }
        // catch (Exception ex) { ... }
        // finally { ... }
        //
        // Main difference:
        // C# uses .NET exception classes such as
        // DivideByZeroException, NullReferenceException, etc.
        // ============================================================

        try
        {
            int a = 0;
            int x = 10 / a;
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Exception: {ex.GetType().Name}");
        }
        finally
        {
            Console.WriteLine("Finally executed.");
        }

        // OUTPUT:
        // Exception: DivideByZeroException
        // Finally executed.


        // ============================================================
        // MULTIPLE catch BLOCKS
        // ============================================================
        // C# also supports multiple catch blocks, just like Java.

        try
        {
            int number = int.Parse("ABC");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Format Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Parsing completed.");
        }

        // OUTPUT:
        // Format Error: The input string 'ABC' was not in a correct format.
        // Parsing completed.


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}