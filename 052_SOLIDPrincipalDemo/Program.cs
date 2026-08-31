using System;

namespace SOLIDPrincipalDemo_52;
// ============================================================
// S — Single Responsibility Principle
// A class should have one responsibility.
// ============================================================

class Invoice
{
    public void CalculateTotal()
    {
        Console.WriteLine("Calculating invoice total...");
    }
}

class InvoicePrinter
{
    public void Print()
    {
        Console.WriteLine("Printing invoice...");
    }
}

// ============================================================
// O — Open/Closed Principle
// Open for extension, closed for modification.
// ============================================================

interface IDiscount
{
    double Apply(double price);
}

class RegularDiscount : IDiscount
{
    public double Apply(double price)
    {
        return price * 0.90;
    }
}

class PremiumDiscount : IDiscount
{
    public double Apply(double price)
    {
        return price * 0.80;
    }
}

// ============================================================
// L — Liskov Substitution Principle
// Derived class Object should be usable wherever an object of the base class is expected,
// without breaking the correctness of the program.
// ============================================================

class Employee
{
    public virtual void Work()
    {
        Console.WriteLine("Employee is working.");
    }
}

class Developer : Employee
{
    public override void Work()
    {
        Console.WriteLine("Developer is coding.");
    }
}

class TeamLead : Employee
{
    public override void Work()
    {
        Console.WriteLine("Team Lead is reviewing the code.");
    }
}


// ============================================================
// I — Interface Segregation Principle
// Don't force a class to implement methods it doesn't need.
// ============================================================

interface IPrinter
{
    void Print();
}

interface IScanner
{
    void Scan();
}

class Printer : IPrinter
{
    public void Print()
    {
        Console.WriteLine("Printing...");
    }
}

// ============================================================
// D — Dependency Inversion Principle
// Depend on abstractions, not concrete classes.
// ============================================================

interface IMessageService
{
    void Send(string message);
}

class EmailService : IMessageService
{
    public void Send(string message)
    {
        Console.WriteLine("Email: " + message);
    }
}

class Notification
{
    private readonly IMessageService service;

    public Notification(IMessageService service)
    {
        this.service = service;
    }

    public void Notify()
    {
        service.Send("Order completed.");
    }
}

// ============================================================
// MAIN
// ============================================================

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== SOLID PRINCIPLES ===");

        // S — Single Responsibility
        Invoice invoice = new Invoice();
        invoice.CalculateTotal();

        InvoicePrinter printer = new InvoicePrinter();
        printer.Print();

        // O — Open/Closed
        IDiscount discount = new PremiumDiscount();
        Console.WriteLine("Discounted Price: " + discount.Apply(100));

        // L — Liskov Substitution
        Employee employee = new Developer();
        employee.Work();

        // I — Interface Segregation
        IPrinter simplePrinter = new Printer();
        simplePrinter.Print();

        // D — Dependency Inversion
        IMessageService email = new EmailService();
        Notification notification = new Notification(email);
        notification.Notify();

        /*
        ============================================================
        JAVA vs C# — BASIC EQUIVALENTS

        C# interface       -> Java interface
        C# class           -> Java class
        C# : BaseClass     -> Java extends BaseClass
        C# : IInterface    -> Java implements IInterface
        C# override         -> Java @Override
        C# constructor DI   -> Java constructor DI

        SOLID principles are fundamentally the same in
        Java and C#; the syntax differs.
        ============================================================
        */
    }
}
