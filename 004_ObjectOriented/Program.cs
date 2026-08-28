using System;

namespace ObjectOriented_04;

// ================================================================
// BASE CLASS
// ================================================================

public class Employee
{
    public string Name { get; set; }
    public int Id { get; set; }

    public Employee(string name, int id)
    {
        Name = name;
        Id = id;
    }

    public virtual void Display()
    {
        Console.WriteLine($"Employee: {Name}, ID: {Id}");
    }

    public override string ToString()
    {
        return $"{Name} ({Id})";
    }
}


// ================================================================
// DERIVED CLASS
// ================================================================

public class Manager : Employee
{
    public string Department { get; set; }

    public Manager(string name, int id, string department) : base(name, id)
    {
        // Java: super(name, id)

        Department = department;
    }

    public override void Display()
    {
        Console.WriteLine(
            $"Manager: {Name}, ID: {Id}, Department: {Department}");
    }
}


// ================================================================
// ENCAPSULATION
// ================================================================

public class BankAccount
{
    private decimal balance;

    public void Deposit(decimal amount)
    {
        if (amount > 0)
            balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= balance)
            balance -= amount;
    }

    public decimal GetBalance() => balance;
}


// ================================================================
// ABSTRACTION
// ================================================================

public abstract class Shape
{
    public abstract double CalculateArea();
}

public class Circle : Shape
{
    private double radius;

    public Circle(double radius)
    {
        this.radius = radius;
    }

    public override double CalculateArea()
    {
        return Math.PI * radius * radius;
    }
}


// ================================================================
// INTERFACE
// ================================================================

public interface IPrintable
{
    void Print();
}

public class Report : IPrintable
{
    public void Print()
    {
        Console.WriteLine("Printing Report...");
    }
}


// ================================================================
// METHOD OVERLOADING
// ================================================================

public class Calculator
{
    public int Add(int a, int b) => a + b;

    public int Add(int a, int b, int c) => a + b + c;
}


// ================================================================
// MAIN
// ================================================================

public class Program
{
    public static void Main(string[] args)
    {
        // =========================================================
        // 1. CLASS & OBJECT
        // =========================================================

        Employee emp = new Employee("Ali", 1001);

        Console.WriteLine(emp.Name);
        Console.WriteLine(emp.Id);

        // OUTPUT:
        // Ali
        // 1001


        // =========================================================
        // 2. ENCAPSULATION
        // =========================================================

        BankAccount account = new BankAccount();

        account.Deposit(5000);
        account.Withdraw(1500);

        Console.WriteLine($"Balance: {account.GetBalance()}");

        // OUTPUT:
        // Balance: 3500


        // =========================================================
        // 3. INHERITANCE + base() / Java super()
        // =========================================================

        Manager manager = new Manager("Saad", 1002, "IT");

        manager.Display();

        Console.WriteLine($"Department: {manager.Department}");

        // OUTPUT:
        // Manager: Saad, ID: 1002, Department: IT
        // Department: IT


        // =========================================================
        // 4. POLYMORPHISM + METHOD OVERRIDING
        // =========================================================

        Employee employee = new Manager("Ahmed", 1003, "HR");

        employee.Display();

        // OUTPUT:
        // Manager: Ahmed, ID: 1003, Department: HR


        // =========================================================
        // 5. ABSTRACTION
        // =========================================================

        Shape shape = new Circle(10);

        Console.WriteLine(
            $"Area: {shape.CalculateArea():F2}");

        // OUTPUT:
        // Area: 314.16


        // =========================================================
        // 6. INTERFACE
        // =========================================================

        IPrintable report = new Report();

        report.Print();

        // OUTPUT:
        // Printing Report...


        // =========================================================
        // 7. METHOD OVERLOADING
        // =========================================================

        Calculator calc = new Calculator();

        Console.WriteLine(calc.Add(10, 20));
        Console.WriteLine(calc.Add(10, 20, 30));

        // OUTPUT:
        // 30
        // 60


        // =========================================================
        // 8. ToString() OVERRIDING
        // =========================================================

        Employee employee2 = new Employee("David", 1005);

        Console.WriteLine(employee2);

        // OUTPUT:
        // David (1005)
    }
}


// ================================================================
// JAVA EQUIVALENT
// ================================================================

/*

// ---------------------------------------------------------------
// BASE CLASS
// ---------------------------------------------------------------

class Employee {

    String name;
    int id;

    Employee(String name, int id) {
        this.name = name;
        this.id = id;
    }

    void display() {
        System.out.println(
            "Employee: " + name + ", ID: " + id);
    }

    @Override
    public String toString() {
        return name + " (" + id + ")";
    }
}


// ---------------------------------------------------------------
// DERIVED CLASS
// ---------------------------------------------------------------

class Manager extends Employee {

    String department;

    Manager(String name, int id, String department) {

        super(name, id);       // C#: base(name, id)

        this.department = department;
    }

    @Override
    void display() {
        System.out.println("Manager: " + name +  ", ID: " + id + ", Department: " + department);
    }
}


// ---------------------------------------------------------------
// MAIN
// ---------------------------------------------------------------

public class Main {

    public static void main(String[] args) {

        Manager manager =
            new Manager("Saad", 1002, "IT");

        System.out.println(manager.name);
        System.out.println(manager.id);
        System.out.println(manager.department);

        manager.display();
    }
}

*/


// ================================================================
// C# vs JAVA — IMPORTANT
// ================================================================
//
// C#                              JAVA
// ---------------------------------------------------------------
// class Manager : Employee        class Manager extends Employee
//
// base(name, id)                  super(name, id)
//
// base.Property                   super.property
//
// base.Method()                   super.method()
//
// public string Department        String department
//
// Console.WriteLine()             System.out.println()
//
// public override void Display()  @Override + void display()
//
// Property { get; set; }          Field + getter/setter
//
// virtual                         Method can be overridden
//
// override                        @Override
//
// abstract class                  abstract class
//
// interface                       interface
//
// Dictionary<K,V>                 HashMap<K,V>
//
// List<T>                         ArrayList<T>
// ================================================================