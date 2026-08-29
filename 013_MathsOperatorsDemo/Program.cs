using System;

namespace MathsOperatorsDemo_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 31_OperatorsDemo ===");
            Console.WriteLine("Java vs C# Operators, Expressions and Math");
            Console.WriteLine();


            // ============================================================
            // JAVA → C# OPERATORS
            // ============================================================
            //
            // Arithmetic          +  -  *  /  %
            // Assignment          =  +=  -=  *=  /=  %=
            // Increment/Decrement ++  --
            // Comparison          ==  !=  >  <  >=  <=
            // Logical             &&  ||  !
            // Bitwise             &  |  ^  ~
            // Shift               <<  >>  >>>
            // Conditional         ?:
            //
            // C# additional:
            // Null-safe            ?.  ??  ??=
            // Type check           is
            //
            // Java:
            // instanceof
            //
            // C#:
            // is
            //
            // Java and C# share most operators.
            // C# additionally provides ??, ??= and ?. operators.


            // ============================================================
            // 1. ARITHMETIC OPERATORS
            // ============================================================

            int a = 20;
            int b = 6;

            int add = a + b;
            int sub = a - b;
            int mul = a * b;
            int div = a / b;
            int mod = a % b;

            Console.WriteLine("===== Arithmetic Operators =====");

            Console.WriteLine("Add      : " + add);
            Console.WriteLine("Subtract : " + sub);
            Console.WriteLine("Multiply : " + mul);
            Console.WriteLine("Divide   : " + div);
            Console.WriteLine("Modulus  : " + mod);

            // OUTPUT:
            // Add      : 26
            // Subtract : 14
            // Multiply : 120
            // Divide   : 3
            // Modulus  : 2

            // Java:
            // int add = a + b;
            // int sub = a - b;
            // int mul = a * b;
            // int div = a / b;
            // int mod = a % b;


            // ============================================================
            // 2. ASSIGNMENT OPERATORS
            // ============================================================

            int number = 51;

            number += 9;
            number -= 10;
            number *= 2;
            number /= 2;

            Console.WriteLine("Assignment Result: " + number);

            // Java:
            // number += 9;
            // number -= 10;
            // number *= 2;
            // number /= 2;


            // ============================================================
            // 3. STRING CONCATENATION
            // ============================================================

            string n1 = "5";
            string n2 = "10";
            string n3 = "15";

            string result = n1 + n2 + n3;

            Console.WriteLine("Concatenation: " + result);

            // OUTPUT:
            // Concatenation: 51015

            // Java:
            // String result = n1 + n2 + n3;


            // ============================================================
            // 4. OPERATOR PRECEDENCE
            // ============================================================

            int precedence = 10 + 20 * 3;

            Console.WriteLine(
                "10 + 20 * 3 = " + precedence);

            // OUTPUT:
            // 10 + 20 * 3 = 70

            // * executes before +

            int precedence2 = (10 + 20) * 3;

            Console.WriteLine(
                "(10 + 20) * 3 = " + precedence2);

            // OUTPUT:
            // (10 + 20) * 3 = 90

            // Java: Same operator precedence rules.


            // ============================================================
            // 5. INCREMENT / DECREMENT
            // ============================================================

            int x = 9;

            Console.WriteLine(x++);
            // OUTPUT: 9

            Console.WriteLine(x);
            // OUTPUT: 10

            Console.WriteLine(++x);
            // OUTPUT: 11

            Console.WriteLine(x--);
            // OUTPUT: 11

            Console.WriteLine(--x);
            // OUTPUT: 9

            // Java: Same ++ and -- behavior.


            // ============================================================
            // 6. COMPARISON OPERATORS
            // ============================================================

            Console.WriteLine(10 > 5);
            Console.WriteLine(5 == 5);
            Console.WriteLine(5 != 10);
            Console.WriteLine(10 < 20);
            Console.WriteLine(15 >= 15);

            // OUTPUT:
            // True
            // True
            // True
            // True
            // True

            // Java:
            // System.out.println(10 > 5);
            // System.out.println(5 == 5);
            // System.out.println(5 != 10);


            // ============================================================
            // 7. LOGICAL OPERATORS
            // ============================================================

            Console.WriteLine(10 > 5 && 5 < 8);
            // OUTPUT: True

            Console.WriteLine(10 > 5 || 5 > 8);
            // OUTPUT: True

            Console.WriteLine(!(10 > 5));
            // OUTPUT: False

            // Java: &&, || and ! are the same.


            // ============================================================
            // 8. CONDITIONAL / TERNARY OPERATOR
            // ============================================================

            int age = 20;

            string status = age >= 18
                ? "Adult"
                : "Minor";

            Console.WriteLine(status);

            // OUTPUT:
            // Adult

            // Java:
            // String status =
            //     age >= 18 ? "Adult" : "Minor";


            // ============================================================
            // 9. BITWISE OPERATORS
            // ============================================================

            /*
            Bitwise and shift operators are useful when working with flags, permissions, binary data, hardware-level operations, 
                    compact data storage, and performance-sensitive calculations.

                & AND → Checks whether specific bits/flags are set.
                | OR → Sets specific bits/combines multiple flags.
                << Left Shift → Moves bits left; commonly used for multiplying by powers of 2 and creating bit flags.
                >> Right Shift → Moves bits right; commonly used for dividing by powers of 2 and extracting bit information.

            Example use: File permissions, user roles, feature flags, network protocols, image/color manipulation, and low-level programming.
             */

            Console.WriteLine("AND: " + (21 & 1)); //can use in even/odd finding
            // OUTPUT: AND: 1

            Console.WriteLine("AND: " + (10 & 1));//can use in even/odd finding
            // OUTPUT: AND: 0

            Console.WriteLine("AND: " + (15 & 5));
            // OUTPUT: AND: 5

            Console.WriteLine("OR: " + (15 | 3));
            // OUTPUT: OR: 15

            Console.WriteLine("XOR: " + (15 ^ 5));
            // OUTPUT: XOR: 10

            Console.WriteLine("NOT: " + (~15));
            // OUTPUT: NOT: -16

            // Java: &, |, ^ and ~ are the same.


            // ============================================================
            // 10. SHIFT OPERATORS
            // ============================================================

            Console.WriteLine(5 << 1);  //5 x 2
            // OUTPUT: 10

            Console.WriteLine(5 << 2); //5 x 2^2
            // OUTPUT: 20

            Console.WriteLine(20 >> 1); //20 / 2
            // OUTPUT: 10

            Console.WriteLine(40 >> 2); //40 / 2^2
            // OUTPUT: 10

            // Java:
            // 5 << 1
            // 20 >> 1
            //
            // Java also has >>> for unsigned right shift.
            // C# uses >> and the behavior depends on the type.


            // ============================================================
            // 11. NULL-SAFE OPERATORS - C# FEATURE
            // ============================================================

            string? name = null;

            Console.WriteLine(name?.Length);
            // OUTPUT:
            // blank / null

            Console.WriteLine(name ?? "Default");
            // OUTPUT:
            // Default

            name ??= "Ali";

            Console.WriteLine(name);
            // OUTPUT:
            // Ali

            // Java:
            // No direct equivalent of ?. and ??.
            // Similar behavior can be implemented using
            // null checks or Java Optional.


            // ============================================================
            // 12. POWER
            // ============================================================

            double power = Math.Pow(2, 16);

            Console.WriteLine("Power: " + power);

            // OUTPUT:
            // Power: 65536

            // Java:
            // Math.pow(2, 16);


            // ============================================================
            // 13. BASIC MATH METHODS
            // ============================================================

            Console.WriteLine("Absolute: " + Math.Abs(-15));
            // OUTPUT: Absolute: 15

            Console.WriteLine("Max: " + Math.Max(10, 25));
            // OUTPUT: Max: 25

            Console.WriteLine("Min: " + Math.Min(10, 25));
            // OUTPUT: Min: 10

            Console.WriteLine("Ceiling: " + Math.Ceiling(7.2));
            // OUTPUT: Ceiling: 8

            Console.WriteLine("Floor: " + Math.Floor(7.8));
            // OUTPUT: Floor: 7

            Console.WriteLine(
                "Round: " +
                Math.Round(7.5, MidpointRounding.AwayFromZero));

            // OUTPUT:
            // Round: 8

            // Java:
            // Math.abs(-15)
            // Math.max(10, 25)
            // Math.min(10, 25)
            // Math.ceil(7.2)
            // Math.floor(7.8)
            // Math.round(7.5)


            // ============================================================
            // 14. MATH CONSTANTS
            // ============================================================

            Console.WriteLine("PI: " + Math.PI);
            // OUTPUT: 3.141592653589793

            Console.WriteLine("E: " + Math.E);
            // OUTPUT: 2.718281828459045

            // Java:
            // Math.PI
            // Math.E


            // ============================================================
            // 15. SQUARE / CUBE ROOT
            // ============================================================

            Console.WriteLine("Sqrt: " + Math.Sqrt(25));
            // OUTPUT: Sqrt: 5

            Console.WriteLine("Cbrt: " + Math.Cbrt(27));
            // OUTPUT: Cbrt: 3

            // Java:
            // Math.sqrt(25)
            // Math.cbrt(27)


            // ============================================================
            // 16. EXPONENTIAL / LOGARITHM
            // ============================================================

            Console.WriteLine("Exp(1): " + Math.Exp(1));
            // OUTPUT: 2.718281828459045

            // e raised to the power 1

            Console.WriteLine("Log(10): " + Math.Log(10));
            // Natural logarithm: ln(10)

            Console.WriteLine("Log10(100): " + Math.Log10(100));
            // OUTPUT: 2

            Console.WriteLine("Pow(2,3): " + Math.Pow(2, 3));
            // OUTPUT: 8

            // Java:
            // Math.exp(1)
            // Math.log(10)
            // Math.log10(100)
            // Math.pow(2, 3)


            // ============================================================
            // 17. LOGARITHM QUICK GRID
            // ============================================================
            //
            // C# METHOD                    BASE
            // ------------------------------------------------------------
            // Math.Log(x)                  e     Natural Log → ln(x)
            // Math.Log10(x)                10    Common Log → log10(x)
            // Math.Log(x) / Math.Log(10)   10    Common Log
            //
            // Java:
            // Math.log(x)                  → Natural log
            // Math.log10(x)                → Base-10 log


            // ============================================================
            // 18. TRIGONOMETRY
            // ============================================================
            // C# Math.Sin/Cos/Tan use RADIANS.

            double angle30 = 30 * Math.PI / 180;

            Console.WriteLine(
                "Sin(30°): " + Math.Sin(angle30));

            Console.WriteLine(
                "Cos(30°): " + Math.Cos(angle30));

            Console.WriteLine(
                "Tan(30°): " + Math.Tan(angle30));

            // OUTPUT approximately:
            // Sin(30°): 0.5
            // Cos(30°): 0.8660254
            // Tan(30°): 0.5773502

            // Java:
            // double radians = Math.toRadians(30);
            // Math.sin(radians);
            // Math.cos(radians);
            // Math.tan(radians);


            // ============================================================
            // 19. JAVA → C# TRIGONOMETRY
            // ============================================================
            //
            // +----------+---------+-----------+-----------+-----------+
            // | RADIANS  | DEGREES | sin()     | cos()     | tan()     |
            // +----------+---------+-----------+-----------+-----------+
            // | π / 6    | 30°     | 0.5       | 0.8660    | 0.5774    |
            // | π / 4    | 45°     | 0.7071    | 0.7071    | 1.0000    |
            // | π / 3    | 60°     | 0.8660    | 0.5       | 1.7321    |
            // | π / 2    | 90°     | 1.0       | 0.0       | Undefined |
            // +----------+---------+-----------+-----------+-----------+


            // ============================================================
            // 20. CIRCLE AREA
            // ============================================================

            double r = 7;
            double area = Math.PI * r * r;

            Console.WriteLine("Circle Area = " + area);

            // OUTPUT:
            // Circle Area = 153.93804002589985


            // ============================================================
            // 21. SIMPLE INTEREST
            // ============================================================

            double P = 1000;
            double R = 5;
            double T = 2;

            double SI = (P * R * T) / 100;

            Console.WriteLine("Simple Interest = " + SI);

            // OUTPUT:
            // Simple Interest = 100


            // ============================================================
            // 22. FLOOR DIVISION - JAVA vs C#
            // ============================================================
            //
            // Java:
            // Math.floorDiv(-100, 9) → -12
            //
            // C#:
            // integer division -100 / 9 → -11
            //
            // To get Java-like floor division:

            int floorDiv =
                (int)Math.Floor(-100.0 / 9.0);

            Console.WriteLine(
                "Floor Division: " + floorDiv);

            // OUTPUT:
            // Floor Division: -12

            Console.WriteLine(
                "Normal C# Division: " + (-100 / 9));

            // OUTPUT:
            // Normal C# Division: -11


            // ============================================================
            // 23. RANDOM
            // ============================================================

            Console.WriteLine(
                "Random (0-1): " +
                Random.Shared.NextDouble());

            // OUTPUT:
            // Random (0-1): <random value>


            Console.WriteLine();
            Console.WriteLine("Done.");
        }
    }
}


// ================================================================
// JAVA → C# OPERATORS & EXPRESSIONS
// ================================================================

// +----------------------+-----------------------------+----------------------------+
// | CATEGORY             | JAVA                        | C#                         |
// +----------------------+------------------------------+---------------------------+
// | Arithmetic           | +  -  *  /  %               | +  -  *  /  %              |
// | Assignment           | =  +=  -=  *=  /=  %=       | =  +=  -=  *=  /=  %=      |
// | Increment/Decrement  | ++  --                      | ++  --                     |
// | Comparison           | ==  !=  >  <  >=  <=        | ==  !=  >  <  >=  <=       |
// | Logical              | &&  ||  !                   | &&  ||  !                  |
// | Bitwise              | &  |  ^  ~                  | &  |  ^  ~                 |
// | Shift                | <<  >>  >>>                 | <<  >>  >>>                |
// | Conditional          | ?:                          | ?:                         |
// | Null                 | null                        | null                       |
// | Type Check           | instanceof                  | is                         |
// | Type Cast            | (int)value                  | (int)value                 |
// | Null-safe Access     | No direct equivalent        | ?.  ??  ??=                |
// | Pattern Matching     | Limited                     | is pattern matching        |
// +----------------------+-----------------------------+----------------------------+


//==============================================================
//JAVA → C# TRIGONOMETRY QUICK GRID
//==============================================================
/*
    // +----------+---------+-----------+-----------+-----------+
    // | RADIANS  | DEGREES | sin()     | cos()     | tan()     |
    // +----------+---------+-----------+-----------+-----------+
    // | π / 6    | 30°     | 0.5       | 0.8660    | 0.5774    |
    // | π / 4    | 45°     | 0.7071    | 0.7071    | 1.0000    |
    // | π / 3    | 60°     | 0.8660    | 0.5       | 1.7321    |
    // | π / 2    | 90°     | 1.0       | 0.0       | Undefined |
    // +----------+---------+-----------+-----------+-----------+
*/



// =========================================================
// 3. EXPONENTIAL & POWER FUNCTIONS
// =========================================================

// ================================================================
// JAVA → C# LOGARITHM METHODS
// ================================================================

// +------------------------------+-------------+-------------------------------------+-----------------------+
// | C# METHOD                    | BASE        | TYPE                                | MATHEMATICAL FORM     |
// +------------------------------+-------------+-------------------------------------+-----------------------+
// | Math.Log(x)                  | e ≈ 2.71828 | Natural Log                         | ln(x)                 |
// | Math.Log10(x)                | 10          | Common Log                          | log₁₀(x)              |
// | Math.Log(x) / Math.Log(10)   | 10          | Common Log using Natural Log        | log₁₀(x)              |
// +------------------------------+----------+----------------------------------------+-----------------------+


