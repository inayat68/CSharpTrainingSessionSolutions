using System.Numerics;

namespace _32_NumbersMathLibraryDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ================================================================
            // JAVA → C# BIGINTEGER QUICK GRID
            // ================================================================

            // +----------------+--------------------------------+--------------------------------+
            // | OPERATION      | JAVA                           | C#                             |
            // +----------------+--------------------------------+--------------------------------+
            // | Type           | BigInteger                     | BigInteger                     |
            // | Namespace      | java.math                      | System.Numerics                |
            // | Import         | import java.math.BigInteger;   | using System.Numerics;         |
            // | Create         | new BigInteger("123")          | BigInteger.Parse("123")        |
            // | Add            | a.add(b)                       | a + b                          |
            // | Subtract       | a.subtract(b)                  | a - b                          |
            // | Multiply       | a.multiply(b)                  | a * b                          |
            // | Divide         | a.divide(b)                    | a / b                          |
            // | Remainder      | a.remainder(b)                 | a % b                          |
            // | Power          | a.pow(5)                       | BigInteger.Pow(a, 5)           |
            // | GCD            | a.gcd(b)                       | BigInteger.GreatestCommonDivisor(a, b) |
            // | Absolute       | a.abs()                        | BigInteger.Abs(a)              |
            // | Maximum        | a.max(b)                       | BigInteger.Max(a, b)           |
            // | Minimum        | a.min(b)                       | BigInteger.Min(a, b)           |
            // | Compare        | a.compareTo(b)                 | a.CompareTo(b)                 |
            // | Equal          | a.equals(b)                    | a == b                         |
            // | Zero           | BigInteger.ZERO                | BigInteger.Zero                |
            // | One            | BigInteger.ONE                 | BigInteger.One                 |
            // | String         | a.toString()                   | a.ToString()                   |
            // | Negate         | a.negate()                     | -a                             |
            // | Bitwise AND    | a.and(b)                       | a & b                          |
            // | Bitwise OR     | a.or(b)                        | a | b                          |
            // | Bitwise XOR    | a.xor(b)                       | a ^ b                          |
            // | Bitwise NOT    | a.not()                        | ~a                             |
            // | Shift Left     | a.shiftLeft(2)                 | a << 2                         |
            // | Shift Right    | a.shiftRight(2)                | a >> 2                         |
            // +----------------+--------------------------------+--------------------------------+

            // ================================================================
            // 1. DOUBLE — APPROXIMATE SCIENTIFIC VALUES
            // ================================================================

            // C#:
            double earth = 5.972e24;
            double jupiter = 1.898e27;

            Console.WriteLine("Earth = " + earth);
            Console.WriteLine("Jupiter = " + jupiter);

            // OUTPUT:
            // Earth = 5.972E+24
            // Jupiter = 1.898E+27

            // Java:
            // double earth = 5.972e24;
            // double jupiter = 1.898e27;
            //
            // System.out.println("Earth = " + earth);
            // System.out.println("Jupiter = " + jupiter);


            // ================================================================
            // 2. BIGINTEGER — EXACT LARGE INTEGER VALUES
            // ================================================================
            // C# BigInteger can store integers larger than long.

            BigInteger earthBig =
                BigInteger.Parse("5972000000000000000000000");

            BigInteger jupiterBig =
                BigInteger.Parse("1898000000000000000000000000");

            Console.WriteLine("Earth Big = " + earthBig);
            Console.WriteLine("Jupiter Big = " + jupiterBig);

            // OUTPUT:
            // Earth Big = 5972000000000000000000000
            // Jupiter Big = 1898000000000000000000000000

            // Java:
            // BigInteger earthBig =
            //     new BigInteger("5972000000000000000000000");
            //
            // BigInteger jupiterBig =
            //     new BigInteger("1898000000000000000000000000");


            // ================================================================
            // 3. BIGINTEGER ARITHMETIC
            // ================================================================
            // Unlike Java, C# supports arithmetic operators directly.

            BigInteger sum = earthBig + jupiterBig;
            BigInteger difference = jupiterBig - earthBig;
            BigInteger product = earthBig * 2;
            BigInteger quotient = jupiterBig / earthBig;
            BigInteger remainder = jupiterBig % earthBig;

            Console.WriteLine("Sum        = " + sum);
            Console.WriteLine("Difference = " + difference);
            Console.WriteLine("Product    = " + product);
            Console.WriteLine("Quotient   = " + quotient);
            Console.WriteLine("Remainder  = " + remainder);

            // Java:
            // BigInteger sum = earthBig.add(jupiterBig);
            // BigInteger difference = jupiterBig.subtract(earthBig);
            // BigInteger product = earthBig.multiply(BigInteger.valueOf(2));
            // BigInteger quotient = jupiterBig.divide(earthBig);
            // BigInteger remainder = jupiterBig.remainder(earthBig);


            // ================================================================
            // 4. POWER
            // ================================================================

            BigInteger power = BigInteger.Pow(2, 100);

            Console.WriteLine("2^100 = " + power);

            // Java:
            // BigInteger power = BigInteger.valueOf(2).pow(100);


            // ================================================================
            // 5. GCD / ABS / MIN / MAX
            // ================================================================

            BigInteger a = BigInteger.Parse("48");
            BigInteger b = BigInteger.Parse("18");

            Console.WriteLine("GCD = " +
                BigInteger.GreatestCommonDivisor(a, b));

            Console.WriteLine("ABS = " +
                BigInteger.Abs(-a));

            Console.WriteLine("MAX = " +
                BigInteger.Max(a, b));

            Console.WriteLine("MIN = " +
                BigInteger.Min(a, b));

            // Java:
            // a.gcd(b)
            // a.abs()
            // a.max(b)
            // a.min(b)


            // ================================================================
            // 6. COMPARE / EQUAL
            // ================================================================

            Console.WriteLine("Compare = " +
                a.CompareTo(b));

            Console.WriteLine("Equal = " +
                (a == b));

            // Java:
            // a.compareTo(b)
            // a.equals(b)

            // C#:
            // a == b

            // Java:
            // a.equals(b)


            // ================================================================
            // 7. NEGATE / BITWISE / SHIFT
            // ================================================================

            Console.WriteLine("Negate = " + (-a));
            Console.WriteLine("AND    = " + (a & b));
            Console.WriteLine("OR     = " + (a | b));
            Console.WriteLine("XOR    = " + (a ^ b));
            Console.WriteLine("NOT    = " + (~a));
            Console.WriteLine("Left   = " + (a << 2));
            Console.WriteLine("Right  = " + (a >> 2));

            // Java:
            // a.negate()
            // a.and(b)
            // a.or(b)
            // a.xor(b)
            // a.not()
            // a.shiftLeft(2)
            // a.shiftRight(2)


            // ================================================================
            // 8. CONSTANTS
            // ================================================================

            Console.WriteLine("Zero = " + BigInteger.Zero);
            Console.WriteLine("One  = " + BigInteger.One);

            // Java:
            // BigInteger.ZERO
            // BigInteger.ONE


            // ================================================================
            // 9. STRING CONVERSION
            // ================================================================

            string text = earthBig.ToString();

            Console.WriteLine("String = " + text);

            // Java:
            // String text = earthBig.toString();


            // ================================================================
            // 10. IMPORTANT DIFFERENCE
            // ================================================================

            // Java BigInteger:
            // Operations use methods:
            //
            // a.add(b)
            // a.subtract(b)
            // a.multiply(b)
            // a.divide(b)

            // C# BigInteger:
            // Operators can be used directly:
            //
            // a + b
            // a - b
            // a * b
            // a / b
            //
            // This makes C# BigInteger syntax look more like
            // normal numeric types.


            // ================================================================
            // 11. BEST PRACTICE
            // ================================================================

            // double:
            // Approximate floating-point value.
            // Suitable for scientific/measurement calculations.

            // BigInteger:
            // Exact integer value with no fixed upper numeric limit
            // other than available memory.

            // C#:
            // using System.Numerics;
            //
            // BigInteger value =
            //     BigInteger.Parse("123456789012345678901234567890");

            // Java:
            // import java.math.BigInteger;
            //
            // BigInteger value =
            //     new BigInteger("123456789012345678901234567890");
        }
    }
}
