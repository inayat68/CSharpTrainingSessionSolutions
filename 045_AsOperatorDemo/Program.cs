using System;

namespace AsOperatorDemo_45;
public class Program
{
    public static void Main(string[] args)
    {
        // C# — as operator
        object obj = "Hello";

        string? text = obj as string;

        if (text != null)
            Console.WriteLine(text); // Hello

        /*
        Java — No direct 'as' operator.

        Object obj = "Hello";

        String text = obj instanceof String
                ? (String) obj
                : null;

        if (text != null)
            System.out.println(text); // Hello
        */
    }
}