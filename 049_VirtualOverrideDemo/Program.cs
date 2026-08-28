using System;

namespace VirtualOverrideDemo_49;

class Employee
{
    public virtual void Work()
    {
        Console.WriteLine("Employee is working.");
    }
}

class Developer : Employee
{
    // C# requires override when overriding a virtual method.
    public override void Work()
    {
        Console.WriteLine("Developer is coding.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== C# virtual + override ===");

        Employee employee = new Developer();
        employee.Work();

        // OUTPUT:
        // Developer is coding.

        /*
        ============================================================
        JAVA EQUIVALENT
        ============================================================

        class Employee {
            void work() {
                System.out.println("Employee is working.");
            }
        }

        class Developer extends Employee {

            // Java methods are virtual by default.
            @Override
            void work() {
                System.out.println("Developer is coding.");
            }
        }

        Employee employee = new Developer();
        employee.work();

        // OUTPUT:
        // Developer is coding.

        ============================================================
        C#                         JAVA
        ------------------------------------------------------------
        virtual + override        Methods virtual by default
        override required         @Override annotation recommended
        sealed override            final method
        ============================================================
        */
    }
}