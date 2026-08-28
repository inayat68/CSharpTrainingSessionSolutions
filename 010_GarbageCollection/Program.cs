using System;

namespace GarbageCollection_07;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 07_GarbageCollection ===");
        Console.WriteLine("Automatic Garbage Collection");
        Console.WriteLine();

        // C# and Java both use Automatic Garbage Collection.

        // C#
        var employee = new Employee("Ali");
        Console.WriteLine(employee.Name);

        // Java equivalent:
        // Employee employee = new Employee("Ali");
        // System.out.println(employee.getName());

        // Remove object reference
        employee = null!;

        // C# - Request Garbage Collection
        GC.Collect();

        // Java equivalent:
        // System.gc();

        Console.WriteLine("GC requested.");

        // OUTPUT:
        // Ali
        // GC requested.

        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}

public class Employee
{
    public string Name { get; }

    public Employee(string name)
    {
        Name = name;
    }
}


/*
JAVA EQUIVALENT:

public class Employee {

    private final String name;

    public Employee(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }
}

public class Main {

    public static void main(String[] args) {

        Employee employee = new Employee("Ali");

        System.out.println(employee.getName());

        employee = null;

        System.gc();

        System.out.println("GC requested.");
    }
}
*/


// C#                  → Java
// ------------------------------------------------
// GC.Collect()        → System.gc()
// employee = null     → employee = null
// Automatic GC        → Automatic GC