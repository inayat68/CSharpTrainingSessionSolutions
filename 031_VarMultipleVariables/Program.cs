using System;

namespace VarMultipleVariables_27;

public class Program
{
    /*
     * In C#, var is used for implicit local variable typing. 
     * The compiler determines the variable's type at compile time from the value on the right side.
        
        var age = 43;
        age = 50;       // ✅
        age = "Ali";    // ❌ Compile error
     */
    public static void Main(string[] args)
    {
        Console.WriteLine("=== 27_VarMultipleVariables ===");
        Console.WriteLine("C# var declaration");
        Console.WriteLine();

        // C# var infers the type from the assigned value.
        // Each var variable requires its own declaration.

        var fname = "";
        var lname = "";

        fname = "Ali";
        lname = "Rehman";

        Console.WriteLine($"{fname} {lname}");

        // OUTPUT:
        // Ali Rehman

        // Java:
        // Java also supports var (Java 10+).
        //
        // var fname = "";
        // var lname = "";
        //
        // fname = "Ali";
        // lname = "Rehman";
        //
        // System.out.println(fname + " " + lname);


        // ============================================================
        // MULTIPLE VARIABLES
        // ============================================================
        // C# does NOT allow multiple variables with one 'var':
        //
        // var fname = "", lname = "";   // ❌ Not allowed
        //
        // Declare them separately:
        //
        // var fname = "";
        // var lname = "";
        //
        // Java:
        // String fname = "", lname = ""; // ✅
        // var cannot be used for multiple declaration in Java either.


        // C#                         Java
        // ------------------------------------------------------------
        // var fname = "";            var fname = "";
        // var lname = "";            var lname = "";
        // var → inferred type        var → inferred type
        // Separate var declarations  Separate var declarations


        Console.WriteLine();
        Console.WriteLine("Done.");
    }
}