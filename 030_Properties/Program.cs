using System;

namespace Properties_26;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 26_Properties ===");
        Console.WriteLine("C# Properties vs Java Getters/Setters");
        Console.WriteLine();

        // C# property provides direct get/set access.
        // Java normally uses getter/setter methods.

        var employee = new Employee(1001, "Saad");

        Console.WriteLine($"{employee.RollId} - {employee.Name}");

        // OUTPUT:
        // 1001 - Saad

        employee.Name = "Ali";

        Console.WriteLine(employee.Name);

        // OUTPUT:
        // Ali

        // Java:
        // Employee employee = new Employee(1001, "Saad");
        //
        // System.out.println(
        //     employee.getRollId() + " - " + employee.getName()
        // );
        //
        // employee.setName("Ali");
        // System.out.println(employee.getName());


        // ============================================================
        // C# Property → Java Getter/Setter
        // ============================================================
        //
        // C#:
        // public string Name { get; set; }
        //
        // Java:
        // public String getName() { return name; }
        // public void setName(String name) { this.name = name; }
        //
        // C# allows:
        // employee.Name = "Ali";
        //
        // Java:
        // employee.setName("Ali");


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}


public class Employee
{
    // Auto-property
    public int RollId { get; set; }

    public string Name { get; set; } = "";

    public Employee(int rollId, string name)
    {
        RollId = rollId;
        Name = name;
    }
}


/*
JAVA EQUIVALENT:

public class Employee {

    private int rollId;
    private String name;

    public Employee(int rollId, String name) {
        this.rollId = rollId;
        this.name = name;
    }

    public int getRollId() {
        return rollId;
    }

    public void setRollId(int rollId) {
        this.rollId = rollId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
*/