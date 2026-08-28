using System.Diagnostics;

namespace ObjectInheritSample;

// Every C# class implicitly inherits from System.Object
public class Employee : System.Object
{
    //property accessor: get; set; init;
    //C# Property
    // │
    // ├── get   → Get Accessor    → Read the value
    // ├── set   → Set Accessor    → Change the value
    // └── init  → Init Accessor   → Set only during initialization
    int RollId { get; set; } = 1001;

    //init means - set value only for init level and no update again
    //public string RollId { get; init; } = 1001;
    public string Name { get; set; } = "saad";

    public Employee(int rollId, string name)
    {
        RollId = rollId;
        Name = name;
    }

    // System.Object defines:
    //
    // public virtual string ToString()
    //
    // Since ToString() is virtual in System.Object,
    // Employee can override it.
    public override string ToString()
    {
        return Name + $" ({RollId})";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        System.Object obj = new object();
        Console.WriteLine(obj.ToString());

        Employee emp = new Employee(1002, "LastName");

        // Console.WriteLine(object) internally calls ToString()
        Console.WriteLine(emp);

        //emp.RollId = 1002;
        emp.Name = "";

        // Explicitly calling ToString()
        Console.WriteLine(emp.ToString());

        // Both produce:
        // Ali
        // Ali

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Done");
        Debug.WriteLine("Done");

        
    }
}
