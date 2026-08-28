using System;
using System.Collections.Generic;
using System.Linq;

namespace NullCoalecseOperatorDemo_50;

public class Employee
{
    public string? Name { get; set; }
    public string? Department { get; set; }
    public string? Email { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "Ali", Department = "IT", Email = "ali@gmail.com" },
            new Employee { Name = "Sara", Department = null, Email = "sara@gmail.com" },
            new Employee { Name = "John", Department = "HR", Email = null },
            new Employee { Name = "Ahmed", Department = "IT", Email = null }
        };

        // LINQ: Filter employees from IT department
        var itEmployees = employees.Where(e => e.Department == "IT");

        foreach (var emp in itEmployees)
        {
            Console.WriteLine($"{emp.Name} - {emp.Department ?? "Unknown"} - {emp.Email ?? "No Email"}");
        }

        // OUTPUT:
        // Ali - IT - ali@gmail.com
        // Ahmed - IT - No Email

        // ?. Null-safe access
        Console.WriteLine(employees[1].Department?.ToUpper());
        // OUTPUT: blank

        // ?? Default value when null
        Console.WriteLine(employees[1].Department ?? "Unknown");
        // OUTPUT: Unknown

        // ??= Assign only when null
        employees[2].Email ??= "john@gmail.com";
        Console.WriteLine(employees[2].Email);
        // OUTPUT: john@gmail.com

        /*
        JAVA EQUIVALENT:

        // LINQ Where() → Java Stream filter()
        employees.stream()
            .filter(e -> "IT".equals(e.department))
            .forEach(e -> System.out.println(
                e.name + " - " +
                (e.department != null ? e.department : "Unknown") +
                " - " +
                (e.email != null ? e.email : "No Email")
            ));

        // ?. → Java null check / Optional
        // ?? → Java ternary operator / Optional.orElse()
        // ??= → if (value == null) value = ...
        */
    }
}