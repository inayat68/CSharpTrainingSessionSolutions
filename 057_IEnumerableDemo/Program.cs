using System;
using System.Collections.Generic;
using System.Linq;

namespace _057_IEnumerableDemo;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("==============================================");
        Console.WriteLine("          IEnumerable<T> DEMO");
        Console.WriteLine("==============================================");

        // ------------------------------------------------------------
        // 1. IEnumerable<T> with List<T>
        // ------------------------------------------------------------

        List<string> employees = new List<string>
            {
                "Ali",
                "Ahmed",
                "John",
                "Sara",
                "David"
            };

        Console.WriteLine("\n1. List<T> implements IEnumerable<T>");

        PrintEmployees(employees);

        /*
         * JAVA EQUIVALENT
         *
         * List<String> employees = new ArrayList<>(
         *     Arrays.asList(
         *         "Ali",
         *         "Ahmed",
         *         "John",
         *         "Sara",
         *         "David"
         *     )
         * );
         *
         * System.out.println("\n1. List implements Iterable");
         *
         * printEmployees(employees);
         */


        // ------------------------------------------------------------
        // 2. IEnumerable<T> variable pointing to List<T>
        // ------------------------------------------------------------

        IEnumerable<string> employeeEnumerable = employees;

        Console.WriteLine("\n2. IEnumerable<T> variable");

        foreach (string employee in employeeEnumerable)
        {
            Console.WriteLine(employee);
        }

        /*
         * JAVA EQUIVALENT
         *
         * Iterable<String> employeeIterable = employees;
         *
         * System.out.println("\n2. Iterable variable");
         *
         * for (String employee : employeeIterable)
         * {
         *     System.out.println(employee);
         * }
         */


        // ------------------------------------------------------------
        // 3. IEnumerable<string> = new List<string>
        // ------------------------------------------------------------

        IEnumerable<string> names = new List<string>
            {
                "Ali",
                "Ahmed",
                "John",
                "Sara"
            };

        Console.WriteLine("\n3. IEnumerable<string> = new List<string>");

        foreach (string name in names)
        {
            Console.WriteLine(name);
        }

        // names.Add("David");
        // ❌ Cannot use Add() because names is declared as IEnumerable<string>.

        /*
         * JAVA EQUIVALENT
         *
         * Iterable<String> names = new ArrayList<>(
         *     Arrays.asList(
         *         "Ali",
         *         "Ahmed",
         *         "John",
         *         "Sara"
         *     )
         * );
         *
         * System.out.println("\n3. Iterable<String> = new ArrayList<String>");
         *
         * for (String name : names)
         * {
         *     System.out.println(name);
         * }
         *
         * // names.add("David");
         * // ❌ Cannot use add() because Iterable does not provide add().
         */


        // ------------------------------------------------------------
        // 4. List<T> reference can still modify the List
        // ------------------------------------------------------------

        List<string> nameList = new List<string>
            {
                "Ali",
                "Ahmed",
                "John"
            };

        IEnumerable<string> nameEnumerable = nameList;

        // nameEnumerable.Add("Sara");
        // ❌ Add() is not available through IEnumerable<T>.

        nameList.Add("Sara");
        // ✅ Add() is available through List<T>.

        Console.WriteLine("\n4. List<T> can be modified");

        foreach (string name in nameEnumerable)
        {
            Console.WriteLine(name);
        }

        /*
         * JAVA EQUIVALENT
         *
         * List<String> nameList = new ArrayList<>(
         *     Arrays.asList(
         *         "Ali",
         *         "Ahmed",
         *         "John"
         *     )
         * );
         *
         * Iterable<String> nameIterable = nameList;
         *
         * // nameIterable.add("Sara");
         * // ❌ add() is not available through Iterable.
         *
         * nameList.add("Sara");
         * // ✅ add() is available through List.
         *
         * System.out.println("\n4. List can be modified");
         *
         * for (String name : nameIterable)
         * {
         *     System.out.println(name);
         * }
         */


        // ------------------------------------------------------------
        // 5. Array also implements IEnumerable<T>
        // ------------------------------------------------------------

        string[] employeeArray =
        {
                "Ali",
                "Ahmed",
                "John",
                "Sara"
            };

        Console.WriteLine("\n5. Array implements IEnumerable<T>");

        PrintEmployees(employeeArray);

        /*
         * JAVA EQUIVALENT
         *
         * String[] employeeArray =
         * {
         *     "Ali",
         *     "Ahmed",
         *     "John",
         *     "Sara"
         * };
         *
         * System.out.println("\n5. Array implements Iterable");
         *
         * printEmployees(Arrays.asList(employeeArray));
         */


        // ------------------------------------------------------------
        // 6. HashSet<T> also implements IEnumerable<T>
        // ------------------------------------------------------------

        HashSet<string> employeeSet = new HashSet<string>
            {
                "Ali",
                "Ahmed",
                "John",
                "Ali"       // Duplicate is ignored
            };

        Console.WriteLine("\n6. HashSet<T> implements IEnumerable<T>");

        PrintEmployees(employeeSet);

        /*
         * JAVA EQUIVALENT
         *
         * Set<String> employeeSet = new HashSet<>(
         *     Arrays.asList(
         *         "Ali",
         *         "Ahmed",
         *         "John",
         *         "Ali"       // Duplicate is ignored
         *     )
         * );
         *
         * System.out.println("\n6. HashSet implements Iterable");
         *
         * printEmployees(employeeSet);
         */


        // ------------------------------------------------------------
        // 7. Dictionary<TKey, TValue>
        // ------------------------------------------------------------

        Dictionary<int, string> employeesDictionary =
            new Dictionary<int, string>
            {
                    { 101, "Ali" },
                    { 102, "Ahmed" },
                    { 103, "John" }
            };

        Console.WriteLine("\n7. Dictionary<TKey, TValue>");

        foreach (KeyValuePair<int, string> employee
                 in employeesDictionary)
        {
            Console.WriteLine(
                $"ID: {employee.Key}, Name: {employee.Value}");
        }

        /*
         * JAVA EQUIVALENT
         *
         * Map<Integer, String> employeesDictionary =
         *     new HashMap<>();
         *
         * employeesDictionary.put(101, "Ali");
         * employeesDictionary.put(102, "Ahmed");
         * employeesDictionary.put(103, "John");
         *
         * System.out.println("\n7. Map<Integer, String>");
         *
         * for (Map.Entry<Integer, String> employee :
         *      employeesDictionary.entrySet())
         * {
         *     System.out.println(
         *         "ID: " + employee.getKey()
         *         + ", Name: " + employee.getValue());
         * }
         */


        // ------------------------------------------------------------
        // 8. Dictionary Keys and Values
        // ------------------------------------------------------------

        Console.WriteLine("\n8. Dictionary Keys");

        PrintItems(employeesDictionary.Keys);

        Console.WriteLine("\nDictionary Values");

        PrintItems(employeesDictionary.Values);

        /*
         * JAVA EQUIVALENT
         *
         * System.out.println("\n8. Map Keys");
         *
         * printItems(employeesDictionary.keySet());
         *
         * System.out.println("\nMap Values");
         *
         * printItems(employeesDictionary.values());
         */


        // ------------------------------------------------------------
        // 9. IEnumerable<T> with LINQ
        // ------------------------------------------------------------

        Console.WriteLine("\n9. IEnumerable<T> + LINQ");

        IEnumerable<string> filteredEmployees =
            employees.Where(x => x.StartsWith("A"));

        PrintEmployees(filteredEmployees);

        /*
         * JAVA EQUIVALENT
         *
         * System.out.println("\n9. Iterable + Stream");
         *
         * List<String> filteredEmployees =
         *     employees.stream()
         *              .filter(x -> x.startsWith("A"))
         *              .collect(Collectors.toList());
         *
         * printEmployees(filteredEmployees);
         */


        // ------------------------------------------------------------
        // 10. Method returning IEnumerable<T>
        // ------------------------------------------------------------

        Console.WriteLine("\n10. Method returning IEnumerable<T>");

        IEnumerable<int> numbers = GetNumbers();

        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }

        /*
         * JAVA EQUIVALENT
         *
         * System.out.println("\n10. Method returning Iterable<Integer>");
         *
         * Iterable<Integer> numbers = getNumbers();
         *
         * for (int number : numbers)
         * {
         *     System.out.println(number);
         * }
         */


        // ------------------------------------------------------------
        // 11. Same method accepts different collection types
        // ------------------------------------------------------------

        Console.WriteLine("\n11. Same method accepts different collections");

        List<int> listNumbers = new List<int>
            {
                10, 20, 30
            };

        int[] arrayNumbers =
        {
                40, 50, 60
            };

        HashSet<int> setNumbers =
            new HashSet<int>
            {
                    70, 80, 90
            };

        Console.WriteLine("\nList<T>:");
        PrintItems(listNumbers);

        Console.WriteLine("\nArray:");
        PrintItems(arrayNumbers);

        Console.WriteLine("\nHashSet<T>:");
        PrintItems(setNumbers);

        /*
         * JAVA EQUIVALENT
         *
         * List<Integer> listNumbers =
         *     Arrays.asList(10, 20, 30);
         *
         * Integer[] arrayNumbers =
         *     { 40, 50, 60 };
         *
         * Set<Integer> setNumbers =
         *     new HashSet<>(Arrays.asList(70, 80, 90));
         *
         * System.out.println("\nList:");
         * printItems(listNumbers);
         *
         * System.out.println("\nArray:");
         * printItems(Arrays.asList(arrayNumbers));
         *
         * System.out.println("\nHashSet:");
         * printItems(setNumbers);
         */


        // ------------------------------------------------------------
        // 12. Convert IEnumerable<T> to List<T>
        // ------------------------------------------------------------

        Console.WriteLine("\n12. IEnumerable<T> -> List<T>");

        List<string> newEmployeeList =
            filteredEmployees.ToList();

        newEmployeeList.Add("Another Employee");

        PrintEmployees(newEmployeeList);

        /*
         * JAVA EQUIVALENT
         *
         * System.out.println("\n12. Iterable -> List");
         *
         * List<String> newEmployeeList =
         *     StreamSupport.stream(
         *         filteredEmployees.spliterator(),
         *         false)
         *     .collect(Collectors.toList());
         *
         * newEmployeeList.add("Another Employee");
         *
         * printEmployees(newEmployeeList);
         */


        // ------------------------------------------------------------
        // 13. IEnumerable<T> does not provide Add(), Remove(), Index
        // ------------------------------------------------------------

        Console.WriteLine("\n13. IEnumerable<T> provides enumeration only");

        IEnumerable<string> names2 = employees;

        foreach (string name in names2)
        {
            Console.WriteLine(name);
        }

        // The following operations are NOT available:

        // names2.Add("New Employee");
        // ❌ Add() does not exist in IEnumerable<T>

        // names2.Remove("Ali");
        // ❌ Remove() does not exist in IEnumerable<T>

        // Console.WriteLine(names2[0]);
        // ❌ Indexer [] does not exist in IEnumerable<T>

        // Console.WriteLine(names2.Count);
        // ❌ Count property does not exist in IEnumerable<T>

        // Note:
        // names2.Count() is possible because Count() is a LINQ method.

        /*
         * JAVA EQUIVALENT
         *
         * Iterable<String> names2 = employees;
         *
         * for (String name : names2)
         * {
         *     System.out.println(name);
         * }
         *
         * // names2.add("New Employee");
         * // ❌ add() does not exist in Iterable.
         *
         * // names2.remove("Ali");
         * // ❌ remove() does not exist in Iterable.
         *
         * // names2.get(0);
         * // ❌ get() does not exist in Iterable.
         *
         * // names2.size();
         * // ❌ size() does not exist in Iterable.
         */


        // ------------------------------------------------------------
        // 14. Generic method accepting IEnumerable<T>
        // ------------------------------------------------------------

        Console.WriteLine("\n14. Generic IEnumerable<T> method");

        PrintItems(new List<int> { 1, 2, 3 });

        PrintItems(new int[] { 4, 5, 6 });

        PrintItems(new HashSet<int> { 7, 8, 9 });

        /*
         * JAVA EQUIVALENT
         *
         * System.out.println("\n14. Generic Iterable method");
         *
         * printItems(Arrays.asList(1, 2, 3));
         *
         * printItems(Arrays.asList(4, 5, 6));
         *
         * printItems(new HashSet<>(
         *     Arrays.asList(7, 8, 9)
         * ));
         */


        // ------------------------------------------------------------
        // END
        // ------------------------------------------------------------

        Console.WriteLine("\n==============================================");
        Console.WriteLine("Demo completed.");
        Console.WriteLine("==============================================");

        /*
         * JAVA EQUIVALENT
         *
         * System.out.println("\n==============================================");
         * System.out.println("Demo completed.");
         * System.out.println("==============================================");
         */
    }


    // ================================================================
    // Method accepting IEnumerable<string>
    // ================================================================

    static void PrintEmployees(IEnumerable<string> employees)
    {
        foreach (string employee in employees)
        {
            Console.WriteLine(employee);
        }
    }

    /*
     * JAVA EQUIVALENT
     *
     * static void printEmployees(Iterable<String> employees)
     * {
     *     for (String employee : employees)
     *     {
     *         System.out.println(employee);
     *     }
     * }
     */


    // ================================================================
    // Generic method accepting IEnumerable<T>
    // ================================================================

    static void PrintItems<T>(IEnumerable<T> items)
    {
        foreach (T item in items)
        {
            Console.WriteLine(item);
        }
    }

    /*
     * JAVA EQUIVALENT
     *
     * static <T> void printItems(Iterable<T> items)
     * {
     *     for (T item : items)
     *     {
     *         System.out.println(item);
     *     }
     * }
     */


    // ================================================================
    // Method returning IEnumerable<T>
    // ================================================================

    static IEnumerable<int> GetNumbers()
    {
        yield return 10;
        yield return 20;
        yield return 30;
        yield return 40;
        yield return 50;
    }

    /*
     * JAVA EQUIVALENT
     *
     * static Iterable<Integer> getNumbers()
     * {
     *     return Arrays.asList(
     *         10, 20, 30, 40, 50
     *     );
     * }
     */


    // ================================================================
    // IMPORTANT REFERENCE
    // ================================================================

    // ┌──────────────────────────────────────────────────────────────┐
    // │ IMPORTANT IDEA                                              │
    // ├──────────────────────────────────────────────────────────────┤
    // │ IEnumerable<T> is the common interface used to iterate      │
    // │ through many different collection types.                   │
    // │                                                              │
    // │ This is one of the major benefits of programming against   │
    // │ an interface rather than a concrete collection.            │
    // └──────────────────────────────────────────────────────────────┘
    //
    // JAVA:
    //
    // Iterable<T> is the closest Java equivalent to IEnumerable<T>.
    //
    // It allows different collection types to be traversed using:
    //
    //     for (T item : collection)
    //
    // However, C# IEnumerable<T> is more closely integrated with
    // LINQ and supports features such as Where(), Select(), etc.


    // ┌──────────────────────────┬──────────────┬────────────────────┐
    // │ Feature                  │ List<T>      │ IEnumerable<T>     │
    // ├──────────────────────────┼──────────────┼────────────────────┤
    // │ foreach                  │     YES      │        YES         │
    // │ Add()                    │     YES      │        NO          │
    // │ Remove()                 │     YES      │        NO          │
    // │ Index [0]                │     YES      │        NO          │
    // │ Count property           │     YES      │        NO*         │
    // │ LINQ Where()             │     YES      │        YES         │
    // │ Represents a sequence    │     YES      │        YES         │
    // │ Can represent array      │     NO       │        YES         │
    // │ Can represent HashSet    │     NO       │        YES         │
    // └──────────────────────────┴──────────────┴────────────────────┘
    //
    // JAVA:
    //
    // ┌──────────────────────────┬────────────────┬──────────────────┐
    // │ Feature                  │ List<T>        │ Iterable<T>      │
    // ├──────────────────────────┼────────────────┼──────────────────┤
    // │ foreach                  │     YES        │       YES        │
    // │ add()                    │     YES        │       NO         │
    // │ remove()                 │     YES        │       NO*        │
    // │ Index [0] / get(0)       │     YES        │       NO         │
    // │ size()                   │     YES        │       NO         │
    // │ Stream filter()          │     YES        │       YES*       │
    // │ Represents a sequence    │     YES        │       YES        │
    // │ Can represent array      │     NO         │       YES**      │
    // │ Can represent Set        │     NO         │       YES        │
    // └──────────────────────────┴────────────────┴──────────────────┘
    //
    // * Depends on the concrete interface/type.
    // ** Arrays can be converted to Iterable/List using helper methods.


    // ┌──────────────────────────────────────────────────────────────┐
    // │ IMPORTANT RELATIONSHIP                                      │
    // └──────────────────────────────────────────────────────────────┘
    //
    //                         IEnumerable<T>
    //                              │
    //                 ┌────────────┼────────────┐
    //                 │            │            │
    //              List<T>       T[]       HashSet<T>
    //                 │
    //                 │
    //              Queue<T>
    //              Stack<T>
    //
    //
    // JAVA:
    //
    //                           Iterable<T>
    //                               │
    //                  ┌────────────┼────────────┐
    //                  │            │            │
    //              List<T>       Set<T>      Queue<T>
    //                  │            │            │
    //              ArrayList    HashSet     LinkedList
    //              LinkedList   TreeSet     ArrayDeque
    //
    //
    // ---------------------------------------------------------------


    // Dictionary<TKey, TValue> also provides enumerable sequences:
    //
    //
    //              Dictionary<TKey, TValue>
    //                         │
    //                         │
    //                  ┌──────┴──────┐
    //                  │             │
    //                Keys         Values
    //                  │             │
    //                  ▼             ▼
    //           IEnumerable<TKey>  IEnumerable<TValue>
    //
    //
    // JAVA:
    //
    //              Map<TKey, TValue>
    //                    │
    //              ┌─────┴─────┐
    //              │           │
    //           keySet()    values()
    //              │           │
    //              ▼           ▼
    //           Set<TKey>  Collection<TValue>


    // ┌──────────────────────────────────────────────────────────────┐
    // │ KEY CONCEPT                                                  │
    // ├──────────────────────────────────────────────────────────────┤
    // │                                                              │
    // │ List<string> list = new List<string>();                    │
    // │                                                              │
    // │ IEnumerable<string> names = list;                          │
    // │                                                              │
    // │ The actual object is still a List<string>.                 │
    // │                                                              │
    // │ But the variable "names" exposes only the operations        │
    // │ defined by IEnumerable<string>.                            │
    // │                                                              │
    // │ Therefore:                                                  │
    // │                                                              │
    // │ names.Add("Ali");       // ❌ Not available                 │
    // │ names.Remove("Ali");    // ❌ Not available                 │
    // │ names[0];               // ❌ Not available                 │
    // │                                                              │
    // │ But:                                                         │
    // │                                                              │
    // │ foreach (var name in names)                                │
    // │ {                                                            │
    // │     Console.WriteLine(name);                               │
    // │ }                                                            │
    // │                                                              │
    // │ is valid because IEnumerable<T> is designed for            │
    // │ enumeration.                                                │
    // └──────────────────────────────────────────────────────────────┘
    //
    // JAVA:
    //
    // List<String> list = new ArrayList<>();
    //
    // Iterable<String> names = list;
    //
    // The actual object is still an ArrayList.
    //
    // But the variable "names" exposes only the operations
    // defined by Iterable<String>.
    //
    // Therefore:
    //
    // names.add("Ali");       // ❌ Not available
    // names.remove("Ali");    // ❌ Not available
    // names.get(0);           // ❌ Not available
    //
    // But:
    //
    // for (String name : names)
    // {
    //     System.out.println(name);
    // }
    //
    // is valid because Iterable<T> is designed for iteration.
}
