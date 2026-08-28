using System;

namespace RefOutInParamDemo_42;

public class Program
{
    // ref: modifies the caller's variable.
    // Java: No direct equivalent.
    static void AddTen(ref int number) => number += 10;

    // out: returns an additional value.
    // Java: No direct equivalent.
    static void GetName(out string name) => name = "Ali";

    // in: passes by reference but cannot be modified.
    // Java equivalent:
    //
    // static void displayNames(String... names)
    // {
    //     for (String name : names)
    //     {
    //         System.out.println("Name: " + name);
    //     }
    // }

    // C# usage:
    // DisplayNames("Ali", "Ahmed", "Sara");
    // DisplayNames();

    // Java usage:
    // displayNames("Ali", "Ahmed", "Sara");
    // displayNames();
    static void Display(in int number)
    {
        // number = 200;  // ❌ Cannot modify an 'in' parameter.
        Console.WriteLine($"Number: {number}");
    }

    // params: allows zero or more arguments.
    // Java: Similar concept to varargs (...).
    static void DisplayNames(params string[] names)
    {
        foreach (string name in names)
        {
            Console.WriteLine($"Name: {name}");
        }
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("=== C# ref / out / in / params ===");
        
        // ref
        int value = 10;
        AddTen(ref value);
        Console.WriteLine($"ref: {value}");       // 20
        
        // out
        GetName(out string name);
        Console.WriteLine($"out: {name}");        // Ali
        
        // in
        int number = 100;
        Display(in number);                       // Number: 100
        
        // params
        DisplayNames("Ali", "Ahmed", "Sara");
        
        // You can also pass no arguments.
        DisplayNames();
    
        // Or pass an existing array.
        string[] names = ["John", "Mary", "David"];
        DisplayNames();
        DisplayNames(name);

        // Summary
        Console.WriteLine();
        Console.WriteLine("C# ref    -> Modify caller's variable.");
        Console.WriteLine("C# out    -> Return an additional value.");
        Console.WriteLine("C# in     -> Read-only reference parameter.");
        Console.WriteLine("C# params -> Accept zero or more arguments.");
        Console.WriteLine("Java      -> No direct ref, out or in parameter modifiers.");
        Console.WriteLine("Java      -> params is similar to varargs (...).");
    }
}