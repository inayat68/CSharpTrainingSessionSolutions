using System;
using System.Linq;

namespace LINQ_16;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 16_LINQ ===");
        Console.WriteLine("LINQ query and transformation");
        Console.WriteLine();

        // ============================================================
        // LINQ
        // ============================================================
        // LINQ (Language Integrated Query) is used to filter,
        // sort, transform and query collections.
        //
        // Java equivalent: Stream API.

        var employees = new[]
        {
            new Employee("Ali", 5000),
            new Employee("Saad", 7000),
            new Employee("Ahmed", 4000)
        };


        // Where  → filter
        // OrderBy → sort
        // Select → transform/project

        var highPaid = employees
            .Where(e => e.Salary >= 5000)
            .OrderBy(e => e.Name)
            .Select(e => e.Name);

        Console.WriteLine(string.Join(", ", highPaid));

        // OUTPUT:
        // Ali, Saad


        // Java equivalent:
        //
        // List<String> highPaid = employees.stream()
        //     .filter(e -> e.getSalary() >= 5000)
        //     .sorted(Comparator.comparing(Employee::getName))
        //     .map(Employee::getName)
        //     .collect(Collectors.toList());
        //
        // System.out.println(String.join(", ", highPaid));


        // ============================================================
        // C# LINQ → Java Stream
        // ============================================================
        //
        // C# LINQ                    Java Stream
        // ------------------------------------------------------------
        // Where()                    filter()
        // OrderBy()                  sorted()
        // Select()                   map()
        // First()                    findFirst()
        // Any()                      anyMatch()
        // Count()                    count()
        // Sum()                      sum()
        // Average()                 average()
        //
        // LINQ is built into C#/.NET and works with many
        // collection types.


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}


public class Employee
{
    public string Name { get; }
    public int Salary { get; }

    public Employee(string name, int salary)
    {
        Name = name;
        Salary = salary;
    }
}


/*
JAVA EMPLOYEE:

public class Employee {

    private String name;
    private int salary;

    public Employee(String name, int salary) {
        this.name = name;
        this.salary = salary;
    }

    public String getName() {
        return name;
    }

    public int getSalary() {
        return salary;
    }
}
*/