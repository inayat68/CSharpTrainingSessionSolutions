using System;

namespace CrossPlatform_01;

public class Program
{
    //Roadmap for Java developers learning C# and Python
    //.NET > Java: https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tips-for-java-developers
    //.NET > Python: https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/tips-for-python-developers
    public static async Task Main(string[] args)
    {
        System.Console.WriteLine("=== CrossPlatform 01 ===");
        Console.WriteLine("Like Java, Modern .NET applications can run on Windows, Linux and macOS");
        Console.WriteLine();


        ///////////////////////////////////////////////////
        Console.WriteLine("=== JAVA vs C# — SIMILARITIES ===");
        Console.WriteLine();

        Console.WriteLine( "01. Both Java and C# are case-sensitive.");
        Console.WriteLine(@"02. Both allow identifiers/variables to start with _ and alphabets 
                                but not with a number; 
                                special characters are not allowed.");
        //go to Project 05
        //go to Project 08
        //go to Project 03

        Console.WriteLine("03. Both support classes, objects, inheritance, interfaces, abstract classes, constructors and methods.");
        Console.WriteLine("04. Both support method overloading and method overriding.");
        //go to Project 04

        Console.WriteLine("05. C# and Java both use Automatic Garbage Collection.");
        //go to Project 10

        Console.WriteLine("06. C# and Java both support Text Block style even C# support Verbatim Literal @ for \\ escape.");
        //go to Project 12
        //go to Project 19

        ///////////////////////////////////////////////////

        Console.WriteLine("=== Dependencies: Analyzers, Frameworks & Packages Section ===");
        Console.WriteLine();

        Console.WriteLine("Analyzers:");
        Console.WriteLine("Analyzers check C# code during development and compilation.");
        Console.WriteLine("They detect coding errors, warnings, style issues and possible problems.");
        Console.WriteLine("Some analyzers can also generate source code automatically.");
        Console.WriteLine("Example: Microsoft.CodeAnalysis.NetAnalyzers");
        Console.WriteLine();

        Console.WriteLine("Frameworks:");
        Console.WriteLine("Frameworks show the .NET framework/API that the project targets.");
        Console.WriteLine("Microsoft.NETCore.App provides the core .NET APIs used by the application.");
        Console.WriteLine("Examples: System, System.Collections.Generic, System.Text, System.IO");
        Console.WriteLine();

        Console.WriteLine("Packages: To be added in .csproj file");
        Console.WriteLine("Use to add Nuget Package(s)");
        Console.WriteLine("Use to add Inner Folder(s)");
        Console.WriteLine("Use to add Other Project Reference: It allow references of other projects and assemblies (DLLs).");
        Console.WriteLine();

        Console.WriteLine("Java Comparison:");
        Console.WriteLine("C# Analyzers  -> Similar to Java compiler checks, IDE inspections and static analysis tools.");
        Console.WriteLine("C# Frameworks  -> Similar to Java JDK/JRE libraries available to the application.");
        Console.WriteLine();

        Console.WriteLine("Dependencies:");
        Console.WriteLine("Analyzers  -> Help analyze and improve the source code.");
        Console.WriteLine("Frameworks  -> Provide the .NET APIs required to run/build the application.");

        ///////////////////////////////////////////////////

        Console.WriteLine();
        Console.WriteLine("=== JAVA vs C# — SYNTAX DIFFERENCES ===");

        Console.WriteLine("01. C# uses namespace; Java uses package.");
        Console.WriteLine("02. C# has extension *.cs; Java uses *.java extension files.");
        Console.WriteLine("03. C# uses 'using' for namespaces; Java uses 'import' for packages/types.");
        //go to Project 02

        Console.WriteLine("04. Console Input/Output: C# uses Console.WriteLine(), Console.ReadLine(), and Console.ReadKey();" +
                            "Java uses System.out.println(), Scanner for input, and " +
                            "System.in.read() / System.console() for key/input handling.");
        //got to Project 05

        Console.WriteLine("05. C# commonly uses PascalCase for methods like GetName(); Java commonly uses camelCase like getName().");
        Console.WriteLine("06. C# uses string interpolation $\"Hello {name}\"; Java commonly uses concatenation or String.format().");
        //got to Project 15

        Console.WriteLine("07. C# uses string/String; Java uses String.");
        //go to Project 11

        Console.WriteLine("08. C# uses ':' for class inheritance; Java uses 'extends'.");
        Console.WriteLine("08. C# uses ':' for interface implementation; Java uses 'implements'.");
        Console.WriteLine("09. C# uses base() to call a parent constructor; Java uses super().");
        Console.WriteLine("10. C# uses properties like Name { get; set; }; Java commonly uses getName() and setName().");
        Console.WriteLine("11. C# uses sealed to prevent inheritance; Java uses final for a class that cannot be extended.");
        //go to Project 04
        //go to Project 48 - Interface

        Console.WriteLine("12. C# uses virtual + override for overriding methods; Java methods are virtual by default unless restricted.");
        //go to Project 49

        Console.WriteLine("13. C# uses List<T>; Java commonly uses ArrayList<T>.");
        //go to Project 36

        Console.WriteLine("14. C# uses Dictionary<TKey,TValue>; Java commonly uses HashMap<K,V>.");
        Console.WriteLine("15. C# uses foreach (var item in objects); Java uses enhanced for (Object item : objects).");
        //go to Project 37

        Console.WriteLine("16. C# uses LINQ Select()/Where(); Java commonly uses Stream map()/filter().");
        //go to Project 18

        Console.WriteLine("17. C# supports additional class types such as sealed, partial, static and record.");

        Console.WriteLine("18. C# uses Math.Pow(2, 3); Java uses Math.pow(2, 3).");
        //goto to Project 13

        Console.WriteLine("19. C# uses try/catch/finally but does not have Java's checked exceptions.");
        //go to Project 09
        //go to Project 17

        Console.WriteLine("20. C# supports operator overloading for user-defined types; Java does not.");
        //go to Project 40

        Console.WriteLine("21. C# uses readonly for a field that can be assigned only during declaration or construction; Java commonly uses final.");
        Console.WriteLine("22. C# uses const for compile-time constants; Java commonly uses static final.");
        //go to Project 41

        Console.WriteLine("23. C# uses ref/out/in parameters; Java does not have equivalent parameter modifiers.");
        //go to Project 42

        Console.WriteLine("24. C# supports properties directly; Java represents object state commonly through fields plus getter/setter methods.");
        //go to Project 26

        Console.WriteLine("25. C# supports delegates and events; Java commonly uses interfaces/lambdas and listener patterns.");
        //go to Project 16

        Console.WriteLine("26. C# uses 'var' for implicitly typed local variable (one at a time); Java also supports 'var' for local variables in modern Java (more than 1 at a time init).");
        //go to Project 14

        Console.WriteLine("27. C# supports 'struct' value types; Java primarily uses classes as reference types with separate primitive types.");
        Console.WriteLine("28. C# supports nullable reference type annotations such as string?; Java reference types can directly contain null.");
        //go to Project 43

        Console.WriteLine("29. C# uses async/await with Task; Java commonly uses CompletableFuture.");
        //go to Project 24
        //go to Project Ollama

        Console.WriteLine("30. C# uses Properties, Tuple and Record; Java has no compatibility of such same syntax.");
        //go to Project 27
        //go to Project 28
        //go to Project 29

        Console.WriteLine("31. C# and Java has different Array maniputation Library and Syntax methods.");
        //go to Project 38

        Console.WriteLine("32. C# and Java has different DataTime maniputation Library and Syntax methods.");
        //go to Project 33

        Console.WriteLine("33. C# uses the 'is' and 'as' operator for type checking and pattern matching. " +
                                 "Java uses 'instanceof' for type checking and pattern matching.");
        //go to Project 44
        //go to Project 45

        Console.WriteLine("34. C# delegates allow a method to be passed as a parameter and invoked later; " +
                                "Java has no direct delegate type but provides similar functionality using functional interfaces, " +
                                    "lambdas, and method references.");
        //go to Project 46

        Console.WriteLine("35. C# struct is a value type used for small data structures." +
                            "Java has no direct struct equivalent and typically uses a class (reference type) for similar purposes.");
        //go to Project 47

        Console.WriteLine("36. Factory Pattern Implemenation in .NET");
        //go to Project 51

        Console.WriteLine("37. SOLID Principal Implemenation in .NET");
        //go to Project 52

        Console.WriteLine("38. C# Regex: Uses a verbatim string (@\"...\") to match CREATE_PROCEDURE error blocks containing Unknown keyword; Java uses the same regex pattern but requires escaped backslashes (\"\\\\...\").");
        //go to Project 53
        //https://regexr.com/

        Console.WriteLine("39. C# – Release Mode with Command-Line Arguments: Build and run the application in Release mode, " +
                            "                   passing folder path and file name as command-line arguments to generate a text file; " +
                            "Java equivalent: compile and run the application with javac/java, passing " +
                            "       the same values through the String[] args parameter.");
        //go to Project 54

        Console.WriteLine("40. .NET commonly integrates with 'Microsoft SQL Server'; Java applications commonly use databases such as 'Oracle', 'PostgreSQL', or 'MySQL'.");

        ///////////////////////////////////////////////////

        Console.WriteLine();
        Console.WriteLine("=== C# CLASS TYPES ===");

        Console.WriteLine("public class    -> Accessible from other classes and assemblies.");
        Console.WriteLine("private class   -> Accessible only within its containing type.");
        Console.WriteLine("protected class -> Accessible within its containing type and derived types.");
        Console.WriteLine("internal class  -> Accessible within the same assembly/project.");
        Console.WriteLine("sealed class    -> Cannot be inherited by another class.");
        Console.WriteLine("static class    -> Cannot be instantiated and contains only static members.");
        Console.WriteLine("partial class   -> Allows one class to be split across multiple files.");
        Console.WriteLine("abstract class  -> Cannot be instantiated and is designed to be inherited.");

        Console.WriteLine();
        Console.WriteLine("Done.");

        Console.WriteLine("");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

    }

    public static void Test1()
    {

    }
    public static void Test3()
    {

    }
    public static void Test2()
    {

    }
}