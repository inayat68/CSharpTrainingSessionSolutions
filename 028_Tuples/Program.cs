using System;

namespace Tuples_22;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 22_Tuples ===");
        Console.WriteLine("Named tuples");
        Console.WriteLine();


        // ============================================================
        // TUPLE
        // ============================================================
        // C# tuple groups multiple values without creating a class.

        var employee = (
            Name: "Ali",
            Id: 1001,
            Salary: 5000
        );

        Console.WriteLine(
            $"{employee.Name} {employee.Id} {employee.Salary}"
        );

        // OUTPUT:
        // Ali 1001 5000


        // Java equivalent:
        // Java does not have a direct built-in equivalent of C# tuples.
        // Common options are record, class, or a third-party Tuple type.
        //
        // record Employee(String Name, int Id, int Salary) {}
        //
        // Employee employee =
        //     new Employee("Ali", 1001, 5000);
        //
        // System.out.println(
        //     employee.Name() + " "
        //     + employee.Id() + " "
        //     + employee.Salary()
        // );


        // ============================================================
        // RETURNING MULTIPLE VALUES
        // ============================================================
        // C# methods can easily return a tuple.

        var result = GetEmployee();

        Console.WriteLine($"{result.Name} - {result.Salary}");

        // OUTPUT:
        // Ali - 5000


        // Java:
        // Employee result = getEmployee();
        // System.out.println(
        //     result.Name() + " - " + result.Salary()
        // );


        // C# Tuple                    → Java
        // ------------------------------------------------------------
        // (Name: "Ali", Id: 1001)     → record/class
        // employee.Name              → employee.Name()
        // Multiple return values     → record/class


        Console.WriteLine();
        Console.WriteLine("Done.");
    }


    static (string Name, int Id, int Salary) GetEmployee()
    {
        return ("Ali", 1001, 5000);
    }
}