using System;

namespace ReadonlyConstDemo_41;

public class Employee
{
    // =========================================================
    // readonly
    // =========================================================
    // C# readonly:
    // Can be assigned at declaration or inside the constructor.
    // After construction, it cannot be changed.

    public readonly int EmployeeId;

    // Java equivalent:
    // final int employeeId;


    // =========================================================
    // const
    // =========================================================
    // C# const:
    // Compile-time constant.
    // Must be assigned when declared.
    // Automatically static.

    public const string CompanyName = "ABC Technologies";

    // Java equivalent:
    // static final String COMPANY_NAME = "ABC Technologies";


    public Employee(int id)
    {
        // readonly can be assigned in constructor
        EmployeeId = id;

        // EmployeeId = 2002;   // Allowed only during construction
    }

    public void Display()
    {
        Console.WriteLine($"Employee ID : {EmployeeId}");
        Console.WriteLine($"Company     : {CompanyName}");
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== C# readonly vs const ===");
        Console.WriteLine();

        Employee emp = new Employee(1001);

        emp.Display();

        Console.WriteLine();

        // =========================================================
        // readonly
        // =========================================================

        Console.WriteLine("readonly:");
        Console.WriteLine("C# -> Can be assigned at declaration or in constructor.");
        Console.WriteLine("Java -> final can also be assigned at declaration or constructor.");

        Console.WriteLine($"EmployeeId = {emp.EmployeeId}");

        // emp.EmployeeId = 2002;
        // ERROR: readonly field cannot be assigned outside constructor.


        // =========================================================
        // const
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("const:");
        Console.WriteLine("C# -> Compile-time constant and implicitly static.");
        Console.WriteLine("Java -> Common equivalent is static final.");

        Console.WriteLine($"CompanyName = {Employee.CompanyName}");

        // Employee.CompanyName = "XYZ";
        // ERROR: const value cannot be changed.


        // =========================================================
        // JAVA vs C#
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("=== JAVA vs C# ===");

        Console.WriteLine("C# readonly  -> Java final");
        Console.WriteLine("C# const     -> Java static final");
        Console.WriteLine("readonly     -> Assigned at declaration or constructor");
        Console.WriteLine("const        -> Assigned only at declaration");
        Console.WriteLine("const        -> Compile-time constant");
    }
}