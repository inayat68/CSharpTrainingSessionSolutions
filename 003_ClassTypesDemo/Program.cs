//namespace _41_ClassTypesDemo_41;

using System;

// ============================================================
// C# CLASS TYPES — JAVA COMPARISON
// ============================================================

// ------------------------------------------------------------
// 1. PUBLIC CLASS
// ------------------------------------------------------------
// Accessible from any assembly that can access the class.
//
// Java equivalent:
// public class Employee { }

public class Employee
{
    public string Name { get; set; }

    public Employee(string name)
    {
        Name = name;
    }
}


// ------------------------------------------------------------
// 2. INTERNAL CLASS
// ------------------------------------------------------------
// Accessible only within the same C# assembly/project.
//
// Java has no direct equivalent.
// Java package-private class is somewhat similar.
//
// Java:
// class Helper { }

internal class Helper
{
    public void Show()
    {
        Console.WriteLine("Internal class");
    }
}


// ------------------------------------------------------------
// 3. PRIVATE NESTED CLASS
// ------------------------------------------------------------
// A class can be private only when nested inside another class.
// Accessible only inside the containing class.
//
// Java:
// class Outer {
//     private static class Inner { }
// }

public class Container
{
    private class PrivateClass
    {
        public void Show()
        {
            Console.WriteLine("Private nested class");
        }
    }

    public void TestPrivateClass()
    {
        PrivateClass obj = new PrivateClass();
        obj.Show();
    }
}


// ------------------------------------------------------------
// 4. PROTECTED NESTED CLASS
// ------------------------------------------------------------
// Accessible inside the containing class and derived classes.
//
// Java:
// class Employee {
//     protected static class Details { }
// }

public class Parent
{
    protected class ProtectedClass
    {
        public void Show()
        {
            Console.WriteLine("Protected nested class");
        }
    }
}

public class Child : Parent
{
    public void TestProtectedClass()
    {
        ProtectedClass obj = new ProtectedClass();
        obj.Show();
    }
}


// ------------------------------------------------------------
// 5. SEALED CLASS
// ------------------------------------------------------------
// Cannot be inherited.
//
// Java has no direct "sealed class" equivalent historically.
// Modern Java supports:
// final class Employee { }

public sealed class FinalEmployee
{
    public void Show()
    {
        Console.WriteLine("Sealed class");
    }
}

// ERROR:
// class Manager : FinalEmployee { }


// ------------------------------------------------------------
// 6. STATIC CLASS
// ------------------------------------------------------------
// Cannot be instantiated.
// Contains only static members.
//
// Java has no direct static class.
// Java commonly uses a class with static methods.

public static class Utility
{
    public static void Show()
    {
        Console.WriteLine("Static class");
    }
}


// ------------------------------------------------------------
// 7. ABSTRACT CLASS
// ------------------------------------------------------------
// Cannot be instantiated directly.
// Used as a base class.
//
// Java:
// abstract class Shape { }

public abstract class Shape
{
    public abstract void Draw();
}

public class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Circle");
    }
}


// ------------------------------------------------------------
// 8. PARTIAL CLASS
// ------------------------------------------------------------
// Allows one class to be split across multiple files.
//
// Java has no direct equivalent.

public partial class Customer
{
    public string Name { get; set; }
}

// Another file can contain:
// public partial class Customer
// {
//     public void Show() { }
// }


// ------------------------------------------------------------
// 9. RECORD
// ------------------------------------------------------------
// Modern C# type designed primarily for data/value-based objects.
//
// Java equivalent is a record (Java 16+).

public record Person(string Name, int Age);


// ============================================================
// MAIN
// ============================================================

public class Program
{
    public static void Main(string[] args)
    {
        // ========================================================
        // 1. PUBLIC CLASS
        // ========================================================

        Employee emp = new Employee("Ali");

        Console.WriteLine(
            $"Public Class: {emp.Name}");

        // OUTPUT:
        // Public Class: Ali


        // ========================================================
        // 2. INTERNAL CLASS
        // ========================================================

        Helper helper = new Helper();
        helper.Show();

        // OUTPUT:
        // Internal class


        // ========================================================
        // 3. PRIVATE NESTED CLASS
        // ========================================================

        Container container = new Container();

        // PrivateClass cannot be accessed directly here.
        // container.PrivateClass -> ERROR

        container.TestPrivateClass();

        // OUTPUT:
        // Private nested class


        // ========================================================
        // 4. PROTECTED NESTED CLASS
        // ========================================================

        Child child = new Child();

        // ProtectedClass cannot be accessed directly here.
        // child.ProtectedClass -> ERROR

        child.TestProtectedClass();

        // OUTPUT:
        // Protected nested class


        // ========================================================
        // 5. SEALED CLASS
        // ========================================================

        FinalEmployee employee = new FinalEmployee();

        employee.Show();

        // OUTPUT:
        // Sealed class


        // ========================================================
        // 6. STATIC CLASS
        // ========================================================

        // No object required.

        Utility.Show();

        // OUTPUT:
        // Static class


        // ========================================================
        // 7. ABSTRACT CLASS
        // ========================================================

        // Shape shape = new Shape();   // ERROR

        Shape shape = new Circle();

        shape.Draw();

        // OUTPUT:
        // Circle


        // ========================================================
        // 8. PARTIAL CLASS
        // ========================================================

        Customer customer = new Customer();

        customer.Name = "Ahmed";

        Console.WriteLine(
            $"Partial Class: {customer.Name}");

        // OUTPUT:
        // Partial Class: Ahmed


        // ========================================================
        // 9. RECORD
        // ========================================================

        Person person = new Person("Sara", 25);

        Console.WriteLine(
            $"Record: {person.Name}, {person.Age}");

        // OUTPUT:
        // Record: Sara, 25

        Console.WriteLine("");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

    }
}


// ============================================================
// QUICK C# vs JAVA
// ============================================================

/*

+------------------+-------------------------+--------------------------+
| C# CLASS TYPE    | C#                      | JAVA                     |
+------------------+-------------------------+--------------------------+
| Public           | public class            | public class             |
| Internal         | internal class          | No direct equivalent     |
| Private          | private nested class   | private nested class     |
| Protected        | protected nested class | protected nested class   |
| Sealed           | sealed class            | final class              |
| Static           | static class            | No direct equivalent     |
| Abstract         | abstract class          | abstract class           |
| Partial          | partial class           | No direct equivalent     |
| Record           | record                  | record (Java 16+)        |
+------------------+-------------------------+--------------------------+

IMPORTANT:

C#:
    class Manager : Employee

Java:
    class Manager extends Employee

C#:
    public class Manager : Employee, IPrintable

Java:
    public class Manager extends Employee implements IPrintable

C#:
    : base(name, id)

Java:
    super(name, id)

*/