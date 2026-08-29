using System;

class ComplexNumber
{
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // + Operator
    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    // - Operator
    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }

    // * Operator
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
    {
        return new ComplexNumber(a.Real * b.Real - a.Imaginary * b.Imaginary,
                                    a.Real * b.Imaginary + a.Imaginary * b.Real);
    }

    // / Operator
    public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
    {
        double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;

        return new ComplexNumber((a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator,
                                                 (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator);
    }

    public override string ToString()
    {
        return $"{Real} {(Imaginary >= 0 ? "+" : "-")} {Math.Abs(Imaginary)}i";
    }
}

class Program
{
    static void Main()
    {
        //a+bi where i^2 = -1
        ComplexNumber c1 = new ComplexNumber(4.0, 3);//4+3i
        ComplexNumber c2 = new ComplexNumber(2, 1.0);//2+i

        Console.WriteLine($"C1 = {c1}");
        Console.WriteLine($"C2 = {c2}");

        Console.WriteLine($"C1 + C2 = {c1 + c2}");
        Console.WriteLine($"C1 - C2 = {c1 - c2}");
        Console.WriteLine($"C1 * C2 = {c1 * c2}");
        Console.WriteLine($"C1 / C2 = {c1 / c2}");
    }
}

// ┌─────────────────────────────────────────────────────────────┐
// │ Complex Number Multiplication                               │
// │                                                             │
// │ (a + bi) × (c + di)                                         │
// │ = (ac - bd) + (ad + bc)i                                    │
// │                                                             │
// │ Example:                                                    │
// │   (4 + 3i) × (2 + i)                                        │
// │ = (4×2 - 3×1) + (4×1 + 3×2)i                                │
// │ = 5 + 10i                                                   │
// └─────────────────────────────────────────────────────────────┘

// ┌─────────────────────────────────────────────────────────────┐
// │ Complex Number Division                                     │
// │                                                             │
// │ (a + bi) ÷ (c + di)                                         │
// │ = [(ac + bd) + (bc - ad)i] / (c² + d²)                      │
// │                                                             │
// │ Example:                                                    │
// │   (4 + 3i) ÷ (2 + i)                                        │
// │ = [(4×2 + 3×1) + (3×2 - 4×1)i] / (2² + 1²)                  │
// │ = (11 + 2i) / 5                                             │
// │ = 2.2 + 0.4i                                                │
// └─────────────────────────────────────────────────────────────┘