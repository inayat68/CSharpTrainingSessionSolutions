using System;

namespace Records_22;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 21_Records ===");
        Console.WriteLine("Records and value-based equality");
        Console.WriteLine();

        // ============================================================
        // RECORD
        // ============================================================
        // Records are designed mainly for storing data.
        // Unlike classes, records use value-based equality by default.

        var a = new Employee("Ali", 1001);
        var b = new Employee("Ali", 1001);

        Console.WriteLine(a == b);

        // OUTPUT:
        // True

        // Java:
        // Java classes normally use reference equality with ==.
        //
        // Employee a = new Employee("Ali", 1001);
        // Employee b = new Employee("Ali", 1001);
        //
        // System.out.println(a == b);       // false
        // System.out.println(a.equals(b));  // depends on equals() implementation


        // ============================================================
        // RECORD vs CLASS
        // ============================================================
        //
        // record:
        // - Value-based equality
        // - Designed for data objects
        // - Concise syntax
        // - Supports with-expressions
        //
        // class:
        // - Reference-based equality by default
        // - General-purpose object behavior
        // - Usually used for entities/services/behavior
        //
        // C#:
        // record Employee(string Name, int Id);
        //
        // class Employee
        // {
        //     public string Name { get; set; }
        //     public int Id { get; set; }
        // }


        // C# record                  → Java record
        // C# class                   → Java class
        // == on record               → equals() for value comparison
        // == on class                → reference comparison


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}


// C# record
public record Employee(string Name, int Id);


/*
JAVA EQUIVALENT:

// Java 16+ record

public record Employee(String name, int id);


// Usage:

Employee a = new Employee("Ali", 1001);
Employee b = new Employee("Ali", 1001);

System.out.println(a.equals(b));

// OUTPUT:
// true
*/