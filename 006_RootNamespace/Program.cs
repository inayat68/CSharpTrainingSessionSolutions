using System;

namespace RootNamespace_06;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== RootNamespace 06 ===");
        Console.WriteLine("System.Object and root System namespace");
        Console.WriteLine();

        // C# classes implicitly inherit from System.Object.
        // Java classes implicitly inherit from java.lang.Object.

        Employee employee = new Employee("Ali");

        Console.WriteLine(employee.ToString());

        // OUTPUT:
        // Ali

        // Java:
        // Employee employee = new Employee("Ali");
        // System.out.println(employee.toString());


        // ============================================================
        // ROOT NAMESPACE / BASE OBJECT
        // ============================================================
        //
        // C#                              Java
        // ------------------------------------------------------------
        // System                         java.lang
        // System.Object                  java.lang.Object
        // System.String                  java.lang.String
        // System.Console                 System.out
        // ToString()                     toString()
        //
        // C# classes implicitly inherit
        // from System.Object.
        //
        // Java classes implicitly inherit
        // from java.lang.Object.


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}


// C# explicitly showing System.Object inheritance
public class Employee : System.Object
{
    public string Name { get; }

    public Employee(string name)
    {
        Name = name;
    }

    // ToString() is inherited from System.Object
    // and overridden here.
    public override string ToString()
    {
        return Name;
    }
}


/*
JAVA EQUIVALENT:

public class Employee extends java.lang.Object {

    private String name;

    public Employee(String name) {
        this.name = name;
    }

    // toString() is inherited from Object
    // and overridden here.
    @Override
    public String toString() {
        return name;
    }
}


public class Program {

    public static void main(String[] args) {

        Employee employee = new Employee("Ali");

        System.out.println(employee.toString());

        // OUTPUT:
        // Ali
    }
}
*/