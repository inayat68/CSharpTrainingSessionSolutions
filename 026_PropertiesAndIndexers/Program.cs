using System;

namespace PropertiesAndIndexers_20;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 20_PropertiesAndIndexers ===");
        Console.WriteLine("Properties and indexers");
        Console.WriteLine();

        // ============================================================
        // 1. PROPERTY
        // ============================================================
        // C# property provides get/set access to a field.
        //
        // Java usually uses getter/setter methods.

        var employee = new Employee();

        employee.Name = "Ali";

        Console.WriteLine(employee.Name);

        // OUTPUT:
        // Ali

        // Java:
        // Employee employee = new Employee();
        // employee.setName("Ali");
        // System.out.println(employee.getName());


        // ============================================================
        // 2. INDEXER
        // ============================================================
        // C# indexer allows an object to be accessed like an array.

        employee[0] = "Developer";
        employee[1] = "C#";

        Console.WriteLine(employee[0]);
        Console.WriteLine(employee[1]);

        // OUTPUT:
        // Developer
        // C#

        // Java has no direct indexer equivalent.
        // Usually an array/List with getter/setter methods is used:
        //
        // employee.setSkill(0, "Developer");
        // System.out.println(employee.getSkill(0));


        // C#                         Java
        // ------------------------------------------------------------
        // employee.Name              employee.getName()
        // employee.Name = "Ali"      employee.setName("Ali")
        // employee[0]                employee.getSkill(0)
        // employee[0] = "C#"         employee.setSkill(0, "C#")


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}


// ================================================================
// EMPLOYEE
// ================================================================

public class Employee
{
    // C# Property
    public string Name { get; set; } = "";

    private readonly string[] skills = new string[3];


    // C# Indexer
    // Allows: employee[0]
    public string this[int index]
    {
        get => skills[index];
        set => skills[index] = value;
    }
}


/*
JAVA EQUIVALENT:

public class Employee {

    private String name;
    private String[] skills = new String[3];

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getSkill(int index) {
        return skills[index];
    }

    public void setSkill(int index, String value) {
        skills[index] = value;
    }
}

Usage:

Employee employee = new Employee();

employee.setName("Ali");
employee.setSkill(0, "Developer");

System.out.println(employee.getName());
System.out.println(employee.getSkill(0));

*/