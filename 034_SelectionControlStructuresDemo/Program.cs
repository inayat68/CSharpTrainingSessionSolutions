using System;

namespace SelectionControlStructuresDemo_34;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== 07_SelectionStructures ===");
        Console.WriteLine("Java vs C# Selection & Control Structures");
        Console.WriteLine();


        // ================================================================
        // QUICK JAVA → C# COMPARISON
        // ================================================================

        // JAVA                              C#
        // ----------------------------------------------------------------
        // if (x > 0)                       if (x > 0)
        // else                             else
        // else if                          else if
        // ? :                              ? :
        // switch / case                   switch / case
        // for (...)                       for (...)
        // for (Type x : list)             foreach (Type x in list)
        // while (...)                     while (...)
        // do { } while (...)              do { } while (...)
        // break                           break
        // continue                        continue
        // x -> expression                 x => expression
        // System.out.println()            Console.WriteLine()
        // System.out.print()              Console.Write()


        // ================================================================
        // 1. IF / ELSE-IF / ELSE
        // ================================================================

        int speed = 120;

        if (speed > 100)
        {
            Console.WriteLine("Overspeeding");
        }
        else if (speed >= 60)
        {
            Console.WriteLine("Normal Speed");
        }
        else
        {
            Console.WriteLine("Too Slow");
        }

        // OUTPUT: Overspeeding

        // Java:
        // if (speed > 100) {
        //     System.out.println("Overspeeding");
        // } else if (speed >= 60) {
        //     System.out.println("Normal Speed");
        // } else {
        //     System.out.println("Too Slow");
        // }


        // ================================================================
        // 2. GRADE CALCULATOR
        // ================================================================

        int marks = 85;

        if (marks >= 90)
            Console.WriteLine("A");
        else if (marks >= 80)
            Console.WriteLine("B");
        else if (marks >= 70)
            Console.WriteLine("C");
        else if (marks >= 60)
            Console.WriteLine("D");
        else
            Console.WriteLine("Fail");

        // OUTPUT: B

        // Java:
        // if (marks >= 90)
        //     System.out.println("A");
        // else if (marks >= 80)
        //     System.out.println("B");


        // ================================================================
        // 3. TERNARY OPERATOR
        // ================================================================

        int age = 20;

        string status =
            age >= 18 ? "Adult" : "Minor";

        Console.WriteLine(status);

        // OUTPUT: Adult

        // Java:
        // String status =
        //     age >= 18 ? "Adult" : "Minor";

        // Difference:
        // Ternary operator is almost identical in Java and C#.


        // ================================================================
        // 4. LOGICAL OPERATORS
        // ================================================================

        string user = "admin";
        string pass = "1234";

        if (user == "admin" && pass == "1234")
        {
            Console.WriteLine("Admin Login");
        }

        // OUTPUT: Admin Login

        // Java:
        // if (user.equals("admin") && pass.equals("1234")) {
        //     System.out.println("Admin Login");
        // }

        // Difference:
        // Java String comparison → user.equals("admin")
        // C# String comparison   → user == "admin"


        // ================================================================
        // 5. SWITCH
        // ================================================================

        int day = 2;

        switch (day)
        {
            case 1:
                Console.WriteLine("Monday");
                break;

            case 2:
                Console.WriteLine("Tuesday");
                break;

            default:
                Console.WriteLine("Other Day");
                break;
        }

        // OUTPUT: Tuesday

        // Java:
        // switch (day) {
        //     case 1:
        //         System.out.println("Monday");
        //         break;
        //     case 2:
        //         System.out.println("Tuesday");
        //         break;
        //     default:
        //         System.out.println("Other Day");
        // }


        // ================================================================
        // 6. SWITCH EXPRESSION — C# FEATURE
        // ================================================================

        int number = 10;

        string result = number switch
        {
            1 => "One",
            2 => "Two",
            10 => "Ten",
            _ => "Other"
        };

        Console.WriteLine(result);

        // OUTPUT: Ten

        // Java:
        // Traditional switch can be used.
        //
        // Java also has newer switch expressions:
        // String result = switch (number) {
        //     case 1 -> "One";
        //     case 2 -> "Two";
        //     case 10 -> "Ten";
        //     default -> "Other";
        // };


        // ================================================================
        // 7. FOR LOOP
        // ================================================================

        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine(i);
        }

        // OUTPUT:
        // 1
        // 2
        // 3
        // 4
        // 5

        // Java:
        // for (int i = 1; i <= 5; i++) {
        //     System.out.println(i);
        // }

        // Difference:
        // Almost identical syntax.


        // ================================================================
        // 8. MULTIPLICATION TABLE
        // ================================================================

        int tableNumber = 5;

        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine(
                $"{tableNumber} x {i} = {tableNumber * i}");
        }

        // OUTPUT:
        // 5 x 1 = 5
        // 5 x 2 = 10
        // ...
        // 5 x 10 = 50

        // Java:
        // System.out.println(
        //     tableNumber + " x " + i +
        //     " = " + (tableNumber * i));


        // ================================================================
        // 9. FOREACH
        // ================================================================

        string[] names =
        {
                "Ali",
                "Saad",
                "Ahmed"
            };

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        // OUTPUT:
        // Ali
        // Saad
        // Ahmed

        // Java:
        // for (String name : names) {
        //     System.out.println(name);
        // }

        // Difference:
        // Java → for-each
        // C#   → foreach


        // ================================================================
        // 10. WHILE LOOP
        // ================================================================

        int count = 1;

        while (count <= 5)
        {
            Console.WriteLine(count);
            count++;
        }

        // OUTPUT:
        // 1
        // 2
        // 3
        // 4
        // 5

        // Java:
        // int count = 1;
        // while (count <= 5) {
        //     System.out.println(count);
        //     count++;
        // }


        // ================================================================
        // 11. DO-WHILE
        // ================================================================

        int x = 1;

        do
        {
            Console.WriteLine(x);
            x++;
        }
        while (x <= 3);

        // OUTPUT:
        // 1
        // 2
        // 3

        // Java:
        // int x = 1;
        // do {
        //     System.out.println(x);
        //     x++;
        // } while (x <= 3);

        // Same syntax and behavior.


        // ================================================================
        // 12. BREAK
        // ================================================================

        for (int i = 1; i <= 10; i++)
        {
            if (i == 5)
                break;

            Console.WriteLine(i);
        }

        // OUTPUT:
        // 1
        // 2
        // 3
        // 4

        // Java:
        // for (int i = 1; i <= 10; i++) {
        //     if (i == 5)
        //         break;
        //     System.out.println(i);
        // }


        // ================================================================
        // 13. CONTINUE
        // ================================================================

        for (int i = 1; i <= 5; i++)
        {
            if (i == 3)
                continue;

            Console.WriteLine(i);
        }

        // OUTPUT:
        // 1
        // 2
        // 4
        // 5

        // Java:
        // for (int i = 1; i <= 5; i++) {
        //     if (i == 3)
        //         continue;
        //     System.out.println(i);
        // }


        // ================================================================
        // 14. FIND MAXIMUM IN ARRAY
        // ================================================================

        int[] numbers =
        {
                10, 50, 30, 20
            };

        int max = numbers[0];

        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] > max)
            {
                max = numbers[i];
            }
        }

        Console.WriteLine("Max: " + max);

        // OUTPUT: Max: 50

        // Java:
        // int[] numbers = {10, 50, 30, 20};
        // int max = numbers[0];
        //
        // for (int i = 1; i < numbers.length; i++) {
        //     if (numbers[i] > max)
        //         max = numbers[i];
        // }
        //
        // System.out.println("Max: " + max);

        // Difference:
        // Java array length → numbers.length
        // C# array length   → numbers.Length


        // ================================================================
        // 15. ODD NUMBERS
        // ================================================================

        for (int i = 1; i <= 20; i++)
        {
            if (i % 2 != 0)
            {
                Console.Write(i + " ");
            }
        }

        Console.WriteLine();

        // OUTPUT:
        // 1 3 5 7 9 11 13 15 17 19

        // Java:
        // for (int i = 1; i <= 20; i++) {
        //     if (i % 2 != 0)
        //         System.out.print(i + " ");
        // }
        // System.out.println();


        // ================================================================
        // 16. REVERSE NUMBER
        // ================================================================

        int num = 123;
        int reversed = 0;

        while (num > 0)
        {
            int digit = num % 10;
            reversed = reversed * 10 + digit;
            num /= 10;
        }

        Console.WriteLine("Reversed: " + reversed);

        // OUTPUT: Reversed: 321

        // Java:
        // int num = 123;
        // int reversed = 0;
        //
        // while (num > 0) {
        //     int digit = num % 10;
        //     reversed = reversed * 10 + digit;
        //     num /= 10;
        // }
        //
        // System.out.println("Reversed: " + reversed);


        // ================================================================
        // 17. COUNT DIGITS
        // ================================================================

        int value = 12345;
        int digitCount = 0;

        while (value > 0)
        {
            value /= 10;
            digitCount++;
        }

        Console.WriteLine("Digits: " + digitCount);

        // OUTPUT: Digits: 5

        // Java:
        // value /= 10;
        // digitCount++;


        // ================================================================
        // 18. POWER USING LOOP
        // ================================================================

        int baseNumber = 4;
        int power = 4;
        int answer = 1;

        for (int i = 1; i <= power; i++)
        {
            answer *= baseNumber;
        }

        Console.WriteLine("Result: " + answer);

        // OUTPUT: Result: 256
        // 4 × 4 × 4 × 4 = 256

        // Java:
        // int baseNumber = 4;
        // int power = 4;
        // int answer = 1;
        //
        // for (int i = 1; i <= power; i++)
        //     answer *= baseNumber;


        // ================================================================
        // 19. STRING INDEX
        // ================================================================

        string text = "HELLO";

        for (int i = 0; i < text.Length; i++)
        {
            if (i % 2 == 0)
                Console.WriteLine(
                    text[i] + " -> Even Index");
            else
                Console.WriteLine(
                    text[i] + " -> Odd Index");
        }

        // OUTPUT:
        // H -> Even Index
        // E -> Odd Index
        // L -> Even Index
        // L -> Odd Index
        // O -> Even Index

        // Java:
        // for (int i = 0; i < text.length(); i++) {
        //     if (i % 2 == 0)
        //         System.out.println(
        //             text.charAt(i) + " -> Even Index");
        // }

        // Difference:
        // Java String length → text.length()
        // C# String length   → text.Length
        //
        // Java character     → text.charAt(i)
        // C# character       → text[i]


        // ================================================================
        // 20. ASCII EVEN / ODD
        // ================================================================

        string letters = "ABC";

        for (int i = 0; i < letters.Length; i++)
        {
            int code = letters[i];

            if (code % 2 == 0)
                Console.WriteLine(
                    $"{letters[i]} EVEN ASCII");
            else
                Console.WriteLine(
                    $"{letters[i]} ODD ASCII");
        }

        // OUTPUT:
        // A ODD ASCII
        // B EVEN ASCII
        // C ODD ASCII

        // Java:
        // int code = letters.charAt(i);
        //
        // if (code % 2 == 0)
        //     System.out.println(
        //         letters.charAt(i) + " EVEN ASCII");


        // ================================================================
        // 21. LAMBDA EXPRESSION
        // ================================================================

        int[] values = { 1, 2, 3, 4, 5 };

        var evenNumbers =
            Array.FindAll(values, n => n % 2 == 0);

        Console.WriteLine(
            string.Join(", ", evenNumbers));

        // OUTPUT:
        // 2, 4

        // Java:
        // Arrays.stream(values)
        //       .filter(n -> n % 2 == 0)
        //       .forEach(System.out::println);

        // Difference:
        // Java lambda:
        //     n -> n % 2 == 0
        //
        // C# lambda:
        //     n => n % 2 == 0


        // ================================================================
        // IMPORTANT JAVA vs C# DIFFERENCES
        // ================================================================

        // 1. Boolean conditions
        // Java and C# both require a Boolean expression.
        //
        // Java:
        // if (age >= 18)
        //
        // C#:
        // if (age >= 18)
        //
        // Neither language allows:
        // if (age)          // invalid
        // if ("hello")       // invalid


        // 2. String comparison
        // Java:
        // user.equals("admin")
        //
        // C#:
        // user == "admin"


        // 3. String length
        // Java:
        // text.length()
        //
        // C#:
        // text.Length


        // 4. Character access
        // Java:
        // text.charAt(i)
        //
        // C#:
        // text[i]


        // 5. Array length
        // Java:
        // numbers.length
        //
        // C#:
        // numbers.Length


        // 6. Output
        // Java:
        // System.out.print("Hello");
        // System.out.println("Hello");
        //
        // C#:
        // Console.Write("Hello");
        // Console.WriteLine("Hello");


        // 7. Foreach
        // Java:
        // for (String name : names)
        //
        // C#:
        // foreach (string name in names)


        // 8. Lambda
        // Java:
        // n -> n * 2
        //
        // C#:
        // n => n * 2


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}
