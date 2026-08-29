using System;
using System.Collections.Generic;
using System.Linq;

namespace Syntax_01;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("================================================");
        Console.WriteLine("             C# BASIC SYNTAX DEMO");
        Console.WriteLine("================================================");

        // ============================================================
        // 1. DATA TYPES
        // ============================================================
        // C# primitive types are very similar to Java.

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("1. DATA TYPES");
        Console.WriteLine("----------------------------------------");

        int age = 43;
        long population = 1000000000L;
        float temperature = 36.5f;
        double salary = 12345.6789;
        decimal price = 999.99m;
        char grade = 'A';
        bool active = true;
        string name = "Ali";

        Console.WriteLine($"int     : {age}");
        Console.WriteLine($"long    : {population}");
        Console.WriteLine($"float   : {temperature}");
        Console.WriteLine($"double  : {salary}");
        Console.WriteLine($"decimal : {price}");
        Console.WriteLine($"char    : {grade}");
        Console.WriteLine($"bool    : {active}");
        Console.WriteLine($"string  : {name}");

        // OUTPUT:
        // int     : 43
        // long    : 1000000000
        // float   : 36.5
        // double  : 12345.6789
        // decimal : 999.99
        // char    : A
        // bool    : True
        // string  : Ali


        // ============================================================
        // 2. TYPE INFERENCE - var
        // ============================================================
        // Similar to Java's local variable type inference.
        // The compiler determines the type.

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("2. var");
        Console.WriteLine("----------------------------------------");

        var firstName = "Ali";     // string
        var employeeId = 1001;     // int
        var salary2 = 5000.50;     // double

        Console.WriteLine(firstName);
        Console.WriteLine(employeeId);
        Console.WriteLine(salary2);

        // OUTPUT:
        // Ali
        // 1001
        // 5000.5


        // ============================================================
        // 3. IF / ELSE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("3. IF / ELSE");
        Console.WriteLine("----------------------------------------");

        if (age >= 18)
        {
            Console.WriteLine("Adult");
        }
        else
        {
            Console.WriteLine("Minor");
        }

        // OUTPUT:
        // Adult


        // ============================================================
        // 4. ELSE IF
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("4. ELSE IF");
        Console.WriteLine("----------------------------------------");

        int marks = 85;

        if (marks >= 90)
        {
            Console.WriteLine("Grade A+");
        }
        else if (marks >= 80)
        {
            Console.WriteLine("Grade A");
        }
        else if (marks >= 70)
        {
            Console.WriteLine("Grade B");
        }
        else
        {
            Console.WriteLine("Grade C");
        }

        // OUTPUT:
        // Grade A


        // ============================================================
        // 5. SWITCH
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("5. SWITCH");
        Console.WriteLine("----------------------------------------");

        int day = 2;

        switch (day)
        {
            case 1:
                Console.WriteLine("Monday");
                break;

            case 2:
                Console.WriteLine("Tuesday");
                break;

            case 3:
                Console.WriteLine("Wednesday");
                break;

            default:
                Console.WriteLine("Invalid day");
                break;
        }

        // OUTPUT:
        // Tuesday


        // ============================================================
        // 6. FOR LOOP
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("6. FOR LOOP");
        Console.WriteLine("----------------------------------------");

        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"i = {i}");
        }

        // OUTPUT:
        // i = 1
        // i = 2
        // i = 3
        // i = 4
        // i = 5


        // ============================================================
        // 7. WHILE LOOP
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("7. WHILE LOOP");
        Console.WriteLine("----------------------------------------");

        int count = 1;

        while (count <= 3)
        {
            Console.WriteLine($"count = {count}");
            count++;
        }

        // OUTPUT:
        // count = 1
        // count = 2
        // count = 3


        // ============================================================
        // 8. DO-WHILE LOOP
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("8. DO-WHILE LOOP");
        Console.WriteLine("----------------------------------------");

        int number = 1;

        do
        {
            Console.WriteLine($"number = {number}");
            number++;
        }
        while (number <= 3);

        // OUTPUT:
        // number = 1
        // number = 2
        // number = 3


        // ============================================================
        // 9. FOREACH LOOP
        // ============================================================
        // Similar to Java enhanced for-loop.

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("9. FOREACH LOOP");
        Console.WriteLine("----------------------------------------");

        string[] names = { "Ali", "Saad", "Ahmed" };

        foreach (string item in names)
        {
            Console.WriteLine(item);
        }

        // OUTPUT:
        // Ali
        // Saad
        // Ahmed


        // ============================================================
        // 10. BREAK / CONTINUE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("10. BREAK / CONTINUE");
        Console.WriteLine("----------------------------------------");

        for (int i = 1; i <= 5; i++)
        {
            if (i == 2)
                continue;

            if (i == 5)
                break;

            Console.WriteLine(i);
        }

        // OUTPUT:
        // 1
        // 3
        // 4


        // ============================================================
        // 11. ARRAYS
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("11. ARRAYS");
        Console.WriteLine("----------------------------------------");

        int[] numbers = { 10, 20, 30, 40, 50 };

        Console.WriteLine($"First : {numbers[0]}");
        Console.WriteLine($"Length: {numbers.Length}");

        // OUTPUT:
        // First : 10
        // Length: 5


        // ============================================================
        // 12. STRING METHODS
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("12. STRING METHODS");
        Console.WriteLine("----------------------------------------");

        string text = "Hello World";

        Console.WriteLine($"Length    : {text.Length}");
        Console.WriteLine($"Upper     : {text.ToUpper()}");
        Console.WriteLine($"Lower     : {text.ToLower()}");
        Console.WriteLine($"Contains  : {text.Contains("World")}");
        Console.WriteLine($"StartsWith: {text.StartsWith("Hello")}");
        Console.WriteLine($"EndsWith  : {text.EndsWith("World")}");
        Console.WriteLine($"Replace   : {text.Replace("World", "C#")}");
        Console.WriteLine($"Substring : {text.Substring(0, 5)}");
        Console.WriteLine($"Trim      : {"  Hello  ".Trim()}");

        // OUTPUT:
        // Length    : 11
        // Upper     : HELLO WORLD
        // Lower     : hello world
        // Contains  : True
        // StartsWith: True
        // EndsWith  : True
        // Replace   : Hello C#
        // Substring : Hello
        // Trim      : Hello


        // ============================================================
        // 13. STRING INTERPOLATION
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("13. STRING INTERPOLATION");
        Console.WriteLine("----------------------------------------");

        string employeeName = "Ali";
        // XXXXXX int employeeId = 1001;

        Console.WriteLine(
            $"Employee: {employeeName}, ID: {employeeId}");

        // OUTPUT:
        // Employee: Ali, ID: 1001


        // ============================================================
        // 14. NUMERIC METHODS
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("14. NUMERIC METHODS");
        Console.WriteLine("----------------------------------------");

        double value = -25.75;

        Console.WriteLine($"Abs   : {Math.Abs(value)}");
        Console.WriteLine($"Round : {Math.Round(value)}");
        Console.WriteLine($"Floor : {Math.Floor(value)}");
        Console.WriteLine($"Ceil  : {Math.Ceiling(value)}");
        Console.WriteLine($"Trunc  : {Math.Truncate(value)}");

        // OUTPUT:
        // Abs   : 25.75
        // Round : -26
        // Floor : -26
        // Ceil  : -25
        // Trunc : -25


        // ============================================================
        // 15. MIN / MAX
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("15. MIN / MAX");
        Console.WriteLine("----------------------------------------");

        int a = 10;
        int b = 20;

        Console.WriteLine($"Min: {Math.Min(a, b)}");
        Console.WriteLine($"Max: {Math.Max(a, b)}");

        // OUTPUT:
        // Min: 10
        // Max: 20


        // ============================================================
        // 16. POWER / SQRT
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("16. POWER / SQRT");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine($"Pow : {Math.Pow(2, 3)}");
        Console.WriteLine($"Sqrt: {Math.Sqrt(25)}");

        // OUTPUT:
        // Pow : 8
        // Sqrt: 5


        // ============================================================
        // 17. TRIGONOMETRIC METHODS
        // ============================================================
        // C# Math.Sin(), Math.Cos(), Math.Tan()
        // use RADIANS, just like Java Math.sin(), Math.cos(), Math.tan().

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("17. TRIGONOMETRIC METHODS");
        Console.WriteLine("----------------------------------------");

        double radians = 30 * Math.PI / 180;

        Console.WriteLine($"Sin 30°: {Math.Sin(radians):F2}");
        Console.WriteLine($"Cos 30°: {Math.Cos(radians):F2}");
        Console.WriteLine($"Tan 30°: {Math.Tan(radians):F2}");

        // OUTPUT:
        // Sin 30°: 0.50
        // Cos 30°: 0.87
        // Tan 30°: 0.58


        // ============================================================
        // 18. NUMBER FORMATTING
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("18. NUMBER FORMATTING");
        Console.WriteLine("----------------------------------------");

        double amount = 12345.6789;

        Console.WriteLine($"F2 : {amount:F2}");
        Console.WriteLine($"N2 : {amount:N2}");
        Console.WriteLine($"C2 : {amount:C2}");
        Console.WriteLine($"E2 : {amount:E2}");

        // OUTPUT:
        // F2 : 12345.68
        // N2 : 12,345.68
        // C2 : $12,345.68
        // E2 : 1.23E+004


        // ============================================================
        // 19. HEX / OCTAL / BINARY
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("19. HEX / OCTAL / BINARY");
        Console.WriteLine("----------------------------------------");

        int num = 255;

        Console.WriteLine($"Decimal: {num}");
        Console.WriteLine($"Hex    : {num:X}");
        Console.WriteLine($"Octal  : {Convert.ToString(num, 8)}");
        Console.WriteLine($"Binary : {Convert.ToString(num, 2)}");

        // OUTPUT:
        // Decimal: 255
        // Hex    : FF
        // Octal  : 377
        // Binary : 11111111


        // ============================================================
        // 20. TYPE CONVERSION
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("20. TYPE CONVERSION");
        Console.WriteLine("----------------------------------------");

        string numberText = "100";

        int converted = int.Parse(numberText);

        Console.WriteLine($"String: {numberText}");
        Console.WriteLine($"Int   : {converted}");

        // OUTPUT:
        // String: 100
        // Int   : 100


        // ============================================================
        // 21. PARSE / TRY PARSE
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("21. PARSE / TRY PARSE");
        Console.WriteLine("----------------------------------------");

        string input = "123";

        int parsed = int.Parse(input);

        Console.WriteLine($"Parse: {parsed}");

        if (int.TryParse(input, out int result))
        {
            Console.WriteLine($"TryParse: {result}");
        }

        // OUTPUT:
        // Parse: 123
        // TryParse: 123


        // ============================================================
        // 22. NULL-COALESCING OPERATOR
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("22. NULL-COALESCING OPERATOR");
        Console.WriteLine("----------------------------------------");

        string? userName = null;

        string finalName = userName ?? "Guest";

        Console.WriteLine(finalName);

        // OUTPUT:
        // Guest


        // ============================================================
        // 23. TERNARY OPERATOR
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("23. TERNARY OPERATOR");
        Console.WriteLine("----------------------------------------");

        string status = age >= 18 ? "Adult" : "Minor";

        Console.WriteLine(status);

        // OUTPUT:
        // Adult


        // ============================================================
        // 24. NULL-CONDITIONAL OPERATOR
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("24. NULL-CONDITIONAL OPERATOR");
        Console.WriteLine("----------------------------------------");

        string? message = null;

        Console.WriteLine(message?.Length);

        // OUTPUT:
        //
        // <empty line>


        // ============================================================
        // 25. COLLECTION - LIST
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("25. LIST");
        Console.WriteLine("----------------------------------------");

        List<string> employees = new()
        {
            "Ali",
            "Saad",
            "Ahmed"
        };

        employees.Add("John");

        foreach (string employee in employees)
        {
            Console.WriteLine(employee);
        }

        // OUTPUT:
        // Ali
        // Saad
        // Ahmed
        // John


        // ============================================================
        // END
        // ============================================================

        Console.WriteLine();
        Console.WriteLine("Done.");

        Console.WriteLine("");
        Console.WriteLine("==============================================");
        Console.WriteLine("Press any key to exit...");
        Console.WriteLine("==============================================");
        Console.ReadKey();

    }
}

//using pg2 = AsyncAwait_18;
//string s = await pg2.Program.GetRemoteData();
//Console.WriteLine("Remote Data: " + s);
