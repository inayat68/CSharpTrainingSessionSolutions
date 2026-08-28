using System;
using System.Linq;

namespace Attributes_23;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 23_Attributes ===");
        Console.WriteLine("Custom attributes");
        Console.WriteLine();

        // ============================================================
        // CUSTOM ATTRIBUTE
        // ============================================================
        // C# attributes add metadata to classes, methods, properties, etc.
        // Java has a similar feature called Annotations.

        var type = typeof(Employee);

        var attribute = type
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .Cast<DescriptionAttribute>()
            .FirstOrDefault();

        Console.WriteLine(attribute?.Text);

        // OUTPUT:
        // Employee entity

        // Java:
        // Class<Employee> type = Employee.class;
        // Description annotation =
        //     type.getAnnotation(Description.class);
        //
        // System.out.println(annotation.text());


        // ============================================================
        // C# → JAVA
        // ============================================================
        //
        // C# Attribute       → Java Annotation
        // [Description(...)] → @Description(...)
        // typeof(Employee)   → Employee.class
        // Reflection         → Reflection


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}


// C# Attribute
[Description("Employee entity")]
public class Employee
{
}


// Custom Attribute class
public class DescriptionAttribute : Attribute
{
    public string Text { get; }

    public DescriptionAttribute(string text)
    {
        Text = text;
    }
}


/*
JAVA EQUIVALENT:

import java.lang.annotation.*;

@Retention(RetentionPolicy.RUNTIME)
@interface Description {
    String text();
}

@Description(text = "Employee entity")
class Employee {
}


// Reading the annotation:

Description annotation =
    Employee.class.getAnnotation(Description.class);

System.out.println(annotation.text());

// OUTPUT:
// Employee entity
*/