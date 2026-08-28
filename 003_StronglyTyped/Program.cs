using System;
using System.Collections.Generic;

namespace StronglyTyped_03;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("          C# STRONGLY TYPED EXAMPLES");
        Console.WriteLine("==============================================");

        //Strongly Typed: C# requires variables, methods, properties, parameters, and collections to have specific data types.
        //The compiler checks these types and reports incompatible assignments at compile time.

        // ============================================================
        // 1. STRONGLY TYPED VARIABLES
        // ============================================================
        // Every variable has a specific data type.

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("1. STRONGLY TYPED VARIABLES");
        Console.WriteLine("----------------------------------------------");

        int age = 43;
        double salary = 1234.50;
        string name = "Ali";
        bool active = true;

        Console.WriteLine($"Name   : {name}");
        Console.WriteLine($"Age    : {age}");
        Console.WriteLine($"Salary : {salary:F2}");
        Console.WriteLine($"Active : {active}");

        // OUTPUT:
        // Name   : Ali
        // Age    : 43
        // Salary : 1234.50
        // Active : True


        // ============================================================
        // 2. TYPE SAFETY
        // ============================================================
        // C# compiler prevents incompatible assignments.

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("2. TYPE SAFETY");
        Console.WriteLine("----------------------------------------------");

        int employeeId = 1001;

        employeeId = 1002;       // Valid

        // employeeId = "1002";  // ❌ Compile-time error
        // Cannot convert string to int

        Console.WriteLine($"Employee ID: {employeeId}");

        // OUTPUT:
        // Employee ID: 1002


        // ============================================================
        // 3. STRONGLY TYPED METHOD
        // ============================================================
        // Method parameters and return values also have types.

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("3. STRONGLY TYPED METHOD");
        Console.WriteLine("----------------------------------------------");

        int result = Add(10, 20);

        Console.WriteLine($"Result: {result}");

        // Add("10", "20");   // ❌ Compile-time error

        // OUTPUT:
        // Result: 30


        // ============================================================
        // 4. STRONGLY TYPED COLLECTION
        // ============================================================
        // List<string> can contain only strings.

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("4. STRONGLY TYPED COLLECTION");
        Console.WriteLine("----------------------------------------------");

        List<string> employees = new()
        {
            "Ali",
            "Saad",
            "Ahmed"
        };

        employees.Add("John");       // Valid

        // employees.Add(1001);      // ❌ Compile-time error

        // XXXXX employee to e
        foreach (string e in employees)
        {
            Console.WriteLine(e);
        }

        // OUTPUT:
        // Ali
        // Saad
        // Ahmed
        // John


        // ============================================================
        // 5. var IS STILL STRONGLY TYPED
        // ============================================================
        // var does NOT mean untyped.
        // The compiler determines the type at compile time.

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("5. var IS STILL STRONGLY TYPED");
        Console.WriteLine("----------------------------------------------");

        var firstName = "Ali";     // string
        var id = 1001;             // int
        var amount = 1234.50;      // double

        Console.WriteLine(firstName);
        Console.WriteLine(id);
        Console.WriteLine(amount);

        // id = "1001";            // ❌ Compile-time error

        // OUTPUT:
        // Ali
        // 1001
        // 1234.5


        // ============================================================
        // 6. STRONGLY TYPED CLASS / PROPERTIES
        // ============================================================
        // Class properties also have specific types.

        Console.WriteLine();
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("6. STRONGLY TYPED CLASS");
        Console.WriteLine("----------------------------------------------");

        Employee employee = new Employee();

        employee.Id = 1001;
        employee.Name = "Ali";
        employee.Salary = 5000.50;

        // employee.Id = "1001";       // ❌ int expected
        // employee.Salary = "5000";   // ❌ double expected

        Console.WriteLine(
            $"{employee.Id} - {employee.Name} - {employee.Salary:F2}");

        // OUTPUT:
        // 1001 - Ali - 5000.50


        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("Done.");
        Console.WriteLine("==============================================");
    }


    // Strongly typed parameters and return type
    public static int Add(int a, int b)
    {
        return a + b;
    }
}


// ================================================================
// STRONGLY TYPED CLASS
// ================================================================

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public double Salary { get; set; }
}