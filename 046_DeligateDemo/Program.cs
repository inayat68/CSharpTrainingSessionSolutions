using System;

public class Program
{
    // Delegate definition
    delegate int Calculate(int a, int b);

    static int Add(int a, int b)
    {
        return a + b;
    }

    static int Multiply(int a, int b)
    {
        return a * b;
    }

    // Delegate passed as a method parameter
    static void CalculateAndPrint(int a, int b, Calculate operation)
    {
        int result = operation(a, b);
        Console.WriteLine("Result: " + result);
    }

    public static void Main(string[] args)
    {
        CalculateAndPrint(10, 20, Add);
        // Result: 30

        CalculateAndPrint(10, 20, Multiply);
        // Result: 200

        /*
        Java equivalent using functional interface:

        interface Calculate {
            int calculate(int a, int b);
        }

        static void calculateAndPrint(
                int a, int b, Calculate operation) {

            int result = operation.calculate(a, b);
            System.out.println("Result: " + result);
        }

        calculateAndPrint(10, 20, Program::add);
        calculateAndPrint(10, 20, Program::multiply);
        */
    }
}