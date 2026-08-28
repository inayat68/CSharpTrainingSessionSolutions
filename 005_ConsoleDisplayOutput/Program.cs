using System;
using System.Diagnostics;

namespace ConsoleDisplayOutput_05;

public class Program
{
    public static void Main(string[] args)
    {

        // ============================================================
        // EMPTY WriteLine()
        // ============================================================

        Console.WriteLine();
        Console.Out.WriteLine();

        // OUTPUT:
        // Blank line


        //Console.Write();
        //NO Overload for Write()

        // ============================================================
        // 1. Console.Write()
        // ============================================================
        // Prints without moving to the next line.

        Console.Write("Hello ");
        Console.Write("World");

        // OUTPUT:
        // Hello World


        // ============================================================
        // 2. Console.WriteLine()
        // ============================================================
        // Prints and moves to the next line.

        Console.WriteLine("Hello");
        Console.WriteLine("World");

        // OUTPUT:
        // Hello
        // World


        // ============================================================
        // 3. Console.WriteLine() - VALUE
        // ============================================================

        Console.WriteLine(100);
        Console.WriteLine(123.45);

        // OUTPUT:
        // 100
        // 123.45

        // ============================================================
        // C# Console Input
        // ============================================================

        // C# → ReadLine()
        // Reads a complete line from the standard input.
        // Convenience/shortcut method for reading a line from standard input.
        string strText = Console.ReadLine();

        // Reads a complete line from the Console.In TextReader from standard input.
        string strText2 = Console.In.ReadLine();

        // C# → ReadKey()
        // Reads a single key press.
        ConsoleKeyInfo chrCharacter = Console.ReadKey();


        // ============================================================
        // JAVA Equivalent
        // ============================================================

        /*
        Java → Scanner
        Reads input from the console.

        import java.util.Scanner;

        Scanner scanner = new Scanner(System.in);

        String name = scanner.nextLine();   // Read complete line
        int age = scanner.nextInt();        // Read integer
        double salary = scanner.nextDouble(); // Read double

        // Java does NOT have a direct equivalent of C# Console.ReadKey().
        // Scanner is mainly used for console input.
        */


        // ============================================================
        // 4. Concatenation
        // ============================================================

        Console.WriteLine("Name: " + "Saad");

        // OUTPUT:
        // Name: Saad

        // Java:
        // System.out.println("Name: " + "Saad");


        // ============================================================
        // 5. STRING INTERPOLATION - $"" 
        // ============================================================
        // Modern C# way of inserting variables into strings.

        string name = "Saad";
        int age = 36;

        Console.WriteLine($"Name: {name}, Age: {age}");

        Console.WriteLine("Name: " + name + ", Age: " + age);

        // OUTPUT:
        // Name: Saad, Age: 36

        // Java:
        // System.out.println(
        //     "Name: " + name + ", Age: " + age
        // );


        // ============================================================
        // 6. COMPOSITE FORMAT - {0}, {1}, {2}
        // ============================================================
        // {0}, {1}, etc. refer to arguments by index.

        Console.WriteLine(
            "Name: {0}, Age: {1}",
            name,
            age);

        // OUTPUT:
        // Name: Saad, Age: 36

        // Java:
        // System.out.printf(
        //     "Name: %s, Age: %d%n",
        //     name, age);


        // ============================================================
        // 7. MULTIPLE VALUES
        // ============================================================

        Console.WriteLine(
            "Id={0} Name={1} Salary={2:F2}",
            101,
            "Ali",
            50000.75);

        // OUTPUT:
        // Id=101 Name=Ali Salary=50000.75


        // ============================================================
        // 8. STRING.Format()
        // ============================================================
        // Creates a formatted string without printing directly.

        var result = string.Format(
            "{0} - {1} = {2}",
            123,
            23,
            100);

        Console.WriteLine(result);

        // OUTPUT:
        // 123 - 23 = 100


        // ============================================================
        // 9. NUMBER FORMATTING
        // ============================================================

        Console.WriteLine("{0:F2}", 1234.567);

        // OUTPUT:
        // 1234.57

        // Modern interpolation:
        Console.WriteLine($"{1234.567:F2}");

        // OUTPUT:
        // 1234.57


        // ============================================================
        // 10. HEXADECIMAL
        // ============================================================

        Console.WriteLine("{0:X}", 255);

        // OUTPUT:
        // FF

        // Java:
        // System.out.printf("%X%n", 255);


        // ============================================================
        // 11. OCTAL AND BINARY
        // ============================================================

        Console.WriteLine(Convert.ToString(255, 8));
        Console.WriteLine(Convert.ToString(255, 2));

        // OUTPUT:
        // 377
        // 11111111

        // Java:
        // System.out.println(Integer.toOctalString(255));
        // System.out.println(Integer.toBinaryString(255));


        // ============================================================
        // 12. PERCENTAGE / CURRENCY / EXPONENTIAL
        // ============================================================

        Console.WriteLine("{0:P2}", 0.756);
        Console.WriteLine("{0:C2}", 1234.56);
        Console.WriteLine("{0:E2}", 1234.56);

        // OUTPUT:
        // 75.60%
        // $1,234.56   // depends on system culture
        // 1.23E+003


        // ============================================================
        // 13. ALIGNMENT
        // ============================================================

        Console.WriteLine("|{0,10}|", "Ali");
        Console.WriteLine("|{0,-10}|", "Ali");

        // OUTPUT:
        // |       Ali|
        // |Ali       |

        // {0,10}  → Right aligned
        // {0,-10} → Left aligned


        // ============================================================
        // 14. ZERO PADDING / THOUSANDS
        // ============================================================

        Console.WriteLine("{0:D5}", 42);
        Console.WriteLine("{0:N0}", 1234567);

        // OUTPUT:
        // 00042
        // 1,234,567


        // ============================================================
        // 15. NEWLINE
        // ============================================================

        Console.WriteLine("Hello\nWorld");

        // OUTPUT:
        // Hello
        // World


        // ============================================================
        // 16. MULTIPLE VALUES WITH WRITE()
        // ============================================================
        // Write() does not automatically add a newline.

        Console.Write("{0}{1}{2}", 1, 2, 3);
        Console.WriteLine();

        // OUTPUT:
        // 123


        // ============================================================
        // 17. REUSING FORMAT PLACEHOLDER
        // ============================================================

        Console.WriteLine(
            "{0} - {1} = {1}",
            10,
            5);

        // OUTPUT:
        // 10 - 5 = 5


        // ============================================================
        // 18. INTERPOLATION WITH FORMATTING
        // ============================================================

        double salary = 1234.567;

        Console.WriteLine(
            $"Salary: {salary:F2} USD");

        // OUTPUT:
        // Salary: 1234.57 USD


        // ============================================================
        // JAVA → C# QUICK COMPARISON
        // ============================================================
        //
        // Java                         C#
        // ------------------------------------------------------------
        // System.out.print()           Console.Write()
        // System.out.println()         Console.WriteLine()
        // System.out.printf()          Console.WriteLine("{0}", ...)
        // System.out.format()          Console.WriteLine("{0}", ...)
        // System.err.println()         Console.Error.WriteLine()
        // String.format()              string.Format()
        // "Hello " + name              $"Hello {name}"
        //
        // Java:
        // System.out.printf(
        //     "Salary: %.2f%n", salary);
        //
        // C#:
        // Console.WriteLine(
        //     $"Salary: {salary:F2}");

        // ============================================================
        // DEBUG OUTPUT
        // ============================================================

        Console.WriteLine("Done");
        Debug.WriteLine("Done");
    }
}

// ================================================================
// JAVA → C# CONSOLE / PRINT METHODS
// ================================================================

// +----------------------------+---------------------------+-----------------------------------------------------+
// | CATEGORY                   | JAVA                      |         C#                                          |
// +----------------------------+---------------------------+-----------------------------------------------------+
// | Print w/o ending LBreak    | System.out.print()        | Console.Write()                                     |
// | Print with ending LBreak   | System.out.println()      | Console.WriteLine()                                 |
// | Formatted Print w/wo LB    | System.out.printf("%n")   | Console.Write("{0} - {1} = {2}\n", 123, 23, 100);   |
// | Formatted Output w/wo LB   | System.out.format("%n")   | Console.WriteLine("{0} - {1} = {2}", 123, 23, 100); |
// | Error Print w/wo LB        | System.err.println()      | Console.Error.WriteLine()                           |
// | Exception Stack Trace      | ex.printStackTrace()      | Console.WriteLine(ex)                               |
// |                            |                           | Debug.WriteLine("")                                 |
// | String Formatting          | var f=String.format(...); | var f = string.Format("{0}-{1}={2}",3, 1, 2);       |
// |                            | System.out.println(f);    | Console.WriteLine(f);                               |
// | Interpolation in C#        | System.out.println("%n")  | var name="Saad", age = 34;                          |
// |                            |                           | Console.Write($"{name} age is {age}");              |
// +----------------------------+---------------------------+-----------------------------------------------------+


// ================================================================
// C# FORMAT SPECIFIERS - NUMBER FORMATS
// ================================================================

// +----------------------------+--------------------------------------+--------------------------------------+
// | FORMAT                     | JAVA EQUIVALENT SYNTAX               | C# EXAMPLE                           |
// +----------------------------+--------------------------------------+--------------------------------------+
// | Decimal                    | System.out.printf("%.2f", 1234.567); | Console.WriteLine("{0:F2}", 1234.567)|
// |                            | Output: 1234.57                      | Output: 1234.57                      |
// |                            | System.out.printf("%.2f %d",        | Console.WriteLine("{0} {1:F2} {2}",  |
// |                            |     14.567, 2);                       |     "Salary:", 1234.567, "USD");     |
// +----------------------------+--------------------------------------+--------------------------------------+
// | Hexadecimal                | System.out.printf("%X", 255);        | Console.WriteLine("{0:X}", 255)      |
// |                            | Output: FF                           | Output: FF                           |
// +----------------------------+--------------------------------------+--------------------------------------+
// | Octal                      | Integer.toOctalString(255);          | Convert.ToString(255, 8)             |
// |                            | Output: 377                          | Output: 377                          |
// +----------------------------+--------------------------------------+--------------------------------------+
// | Binary                     | Integer.toBinaryString(255);         | Convert.ToString(255, 2)             |
// |                            | Output: 11111111                     | Output: 11111111                     |
// +----------------------------+--------------------------------------+--------------------------------------+
// | Exponential                | System.out.printf("%e", 1234.567);   | Console.WriteLine("{0:E}", 1234.567) |
// | / Scientific               | Output: 1.234567e+03                 | Output: 1.234567E+003                |
// +----------------------------+--------------------------------------+--------------------------------------+
// | Floating Point             | System.out.printf("%.2f", 1234.567); | Console.WriteLine("{0:F2}", 1234.567)|
// |                            | Output: 1234.57                      | Output: 1234.57                      |
// +----------------------------+--------------------------------------+--------------------------------------+
// | Percentage                 | System.out.printf("%.2f%%", 0.756);  | Console.WriteLine("{0:P2}", 0.756)   |
// |                            | Output: 75.60%                       | Output: 75.60%                       |
// +----------------------------+--------------------------------------+--------------------------------------+
// | Currency                   | System.out.printf("$%,.2f", 1234.56);| Console.WriteLine("{0:C2}", 1234.56) |
// |                            | Output: $1,234.56                    | Output: $1,234.56*                   |
// +----------------------------+--------------------------------------+--------------------------------------|
//
// * C# currency symbol depends on the current culture/locale.
//   For example, en-US → $1,234.56

// +----------------------+--------------------------------------+
// | JAVA FORMAT          | DESCRIPTION                          |
// +----------------------+--------------------------------------+
// | %c                   | Character                            |
// | %s                   | String                               |
// | %b                   | Boolean                              |
// | %d                   | Decimal integer                      |
// | %o                   | Octal integer                        |
// | %x                   | Hexadecimal integer (lowercase)      |
// | %X                   | Hexadecimal integer (uppercase)      |
// | %f                   | Floating-point number                |
// | %e                   | Scientific notation (lowercase)      |
// | %E                   | Scientific notation (uppercase)      |
// | %g                   | General floating-point format        |
// | %a                   | Hexadecimal floating-point           |
// | %h                   | Hash code (hexadecimal)               |
// | %n                   | Platform-specific line separator     |
// | %%                   | Literal percent (%)                  |
// +----------------------+--------------------------------------+
// | Common Formatting:   |                                      |
// | %.2f                 | Floating point, 2 decimal places     |
// | %10s                 | String, width 10 (right-aligned)     |
// | %-10s                | String, width 10 (left-aligned)      |
// | %05d                 | Integer, padded with zeros            |
// | %,d                  | Integer with grouping separator      |
// | %+,d                 | Integer with + / - sign              |
// +----------------------+--------------------------------------+

// +----------------------+--------------------------------------+---------------------------------------+
// | FORMAT               | JAVA EQUIVALENT SYNTAX               | C# EQUIVALENT SYNTAX                  |
// +----------------------+--------------------------------------+---------------------------------------+
// | %.2f                 | System.out.printf("%.2f", 1234.567); | Console.WriteLine("{0:F2}", 1234.567) |
// |                      | Output: 1234.57                      | Output: 1234.57                       |
// +----------------------+--------------------------------------+---------------------------------------+
// | %10s                 | System.out.printf("%10s", "Ali");    | Console.WriteLine("{0,10}", "Ali");   |
// |                      | Output:        Ali                   | Output:        Ali                    |
// +----------------------+--------------------------------------+---------------------------------------+
// | %-10s                | System.out.printf("%-10s", "Ali");   | Console.WriteLine("{0,-10}", "Ali");  |
// |                      | Output: Ali                          | Output: Ali                           |
// +----------------------+--------------------------------------+---------------------------------------+
// | %05d                 | System.out.printf("%05d", 42);       | Console.WriteLine("{0:D5}", 42);      |
// |                      | Output: 00042                        | Output: 00042                         |
// +----------------------+--------------------------------------+---------------------------------------+
// | %,d                  | System.out.printf("%,d", 1234567);   | Console.WriteLine("{0:N0}", 1234567); |
// |                      | Output: 1,234,567                    | Output: 1,234,567                     |
// +----------------------+--------------------------------------+---------------------------------------+
// | %+,d                 | System.out.printf("%+,d", 1234567);  | Console.WriteLine("{0:+#,##0;-#,##0}",|
// |                      | Output: +1,234,567                   |     1234567);                         |
// |                      |                                      | Output: +1,234,567                    |
// +----------------------+--------------------------------------+---------------------------------------+
// | %+,d                 | System.out.printf("%+,d", -1234567); | Console.WriteLine("{0:+#,##0;-#,##0}",|
// |                      | Output: -1,234,567                   |     -1234567);                        |
// |                      |                                      | Output: -1,234,567                    |
// +----------------------+--------------------------------------+---------------------------------------+
//
// NOTE:
// C# alignment:
// {0,10}  → Right-aligned, width 10
// {0,-10} → Left-aligned, width 10
//
// C# numeric formatting:
// F2 → 2 decimal places
// D5 → Integer padded to 5 digits
// N0 → Number with thousands separator, 0 decimal places

// JAVA Numbers Prefix
// +----------------------+----------+------------------+----------+--------------------------+
// | NUMBER SYSTEM        | JAVA     | JAVA EXAMPLE     | C#       | C# EXAMPLE               |
// +----------------------+----------+------------------+----------+--------------------------+
// | Decimal              | None     | 255              | None     | 255                      |
// | Binary               | 0b / 0B  | 0b11111111       | 0b / 0B  | 0b11111111               |
// | Octal                | 0        | 0377             | —        | Convert.ToString(255, 8) |
// | Hexadecimal          | 0x / 0X  | 0xFF             | 0x / 0X  | 0xFF                     |
// +----------------------+----------+------------------+----------+--------------------------+
