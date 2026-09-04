using System;
using System.Globalization;
using System.Reflection;
using _058_ModelAttributeDemo.Models;

namespace _058_ModelAttributeDemo;

class Program
{
    static void Main()
    {
        Console.WriteLine("Employee Date Validation Demo");
        Console.WriteLine("==============================");
        Console.WriteLine();

        // --------------------------------------------------------
        // Valid Employee
        // --------------------------------------------------------

        Employee employee1 = new Employee
        {
            JoiningDate = new DateTime(2024, 05, 10),
            LeavingDate = new DateTime(2026, 08, 20)
        };

        Console.WriteLine("Employee 1");
        Console.WriteLine("----------");

        bool result1 = EmployeeValidator.Validate(employee1);

        Console.WriteLine();
        Console.WriteLine($"Validation Result: {result1}");
        Console.WriteLine();

        // --------------------------------------------------------
        // Invalid Employee
        // LeavingDate is before JoiningDate
        // --------------------------------------------------------

        Employee employee2 = new Employee
        {
            JoiningDate = new DateTime(2026, 08, 20),
            LeavingDate = new DateTime(2025, 05, 10)
        };

        Console.WriteLine("Employee 2");
        Console.WriteLine("----------");

        bool result2 = EmployeeValidator.Validate(employee2);

        Console.WriteLine();
        Console.WriteLine($"Validation Result: {result2}");

        Console.ReadKey();
    }
}