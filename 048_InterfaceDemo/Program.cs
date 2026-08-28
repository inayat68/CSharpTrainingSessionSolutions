using System;

namespace InterfaceDemo_48;

interface IPrintable
{
    void Print();
}

interface IScannable
{
    void Scan();
}

// Single interface implementation
class Printer : IPrintable
{
    public void Print()
    {
        Console.WriteLine("Printing document...");
    }
}

// Multiple interface implementation
class MultiFunctionPrinter : IPrintable, IScannable
{
    public void Print()
    {
        Console.WriteLine("Printing document...");
    }

    public void Scan()
    {
        Console.WriteLine("Scanning document...");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== SINGLE INTERFACE ===");

        IPrintable printer = new Printer();
        printer.Print();

        // OUTPUT:
        // Printing document...


        Console.WriteLine();
        Console.WriteLine("=== MULTIPLE INTERFACES ===");

        MultiFunctionPrinter mfp = new MultiFunctionPrinter();
        mfp.Print();
        mfp.Scan();

        // OUTPUT:
        // Printing document...
        // Scanning document...


        // A class can also be accessed through
        // individual interface references.

        IPrintable print = mfp;
        IScannable scan = mfp;

        print.Print();
        scan.Scan();

        // OUTPUT:
        // Printing document...
        // Scanning document...


        /*
        ============================================================
        JAVA EQUIVALENT
        ============================================================

        interface IPrintable {
            void print();
        }

        interface IScannable {
            void scan();
        }

        // Single interface
        class Printer implements IPrintable {
            public void print() {
                System.out.println("Printing document...");
            }
        }

        // Multiple interfaces
        class MultiFunctionPrinter
                implements IPrintable, IScannable {

            public void print() {
                System.out.println("Printing document...");
            }

            public void scan() {
                System.out.println("Scanning document...");
            }
        }

        Printer printer = new Printer();
        printer.print();

        MultiFunctionPrinter mfp =
            new MultiFunctionPrinter();

        mfp.print();
        mfp.scan();

        ============================================================
        C#                         JAVA
        ------------------------------------------------------------
        : IPrintable              implements IPrintable
        : I1, I2                  implements I1, I2
        interface                 interface
        ============================================================
        */
    }
}