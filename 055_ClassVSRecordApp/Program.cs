// =================================================================================================
// C# CLASS vs RECORD — SIMPLE BEGINNER EXAMPLE
// =================================================================================================
// | CLASS                         | RECORD                              |
// |-------------------------------|-------------------------------------|
// | Represents an object/entity   | Represents data/value               |
// | Reference equality by default | Value equality by default           |
// | Usually mutable               | Usually immutable                   |
// | Identity is important         | Values are important                |
// | Customer, Order, Employee     | DTO, Request, Response              |
// | Java: class                   | Java: record                        |
// =================================================================================================
// Simple rule:
//
//     CLASS  -> Entity / Identity / Changing State
//     RECORD -> Data / Value / Immutable Data
//
// =================================================================================================


// =================================================================================================
// 1. CLASS
// =================================================================================================

public class Customer
{
    public int Id { get; }

    public string Name { get; private set; }

    public Customer(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void ChangeName(string newName)
    {
        Name = newName;
    }

    public override string ToString()
    {
        return $"Customer: {Id}, {Name}";
    }
}


// =================================================================================================
// 2. RECORD
// =================================================================================================
//
// Record is useful for DTOs / API data.
//
// =================================================================================================

public record CustomerDto(int Id, string Name);


// Java equivalent:
//
// public record CustomerDto(int id, String name) {}


// =================================================================================================
// 3. PROGRAM
// =================================================================================================

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("===== CLASS vs RECORD =====");

        ClassExample();

        RecordExample();

        WithExample();
    }


    // =============================================================================================
    // CLASS EXAMPLE
    // =============================================================================================

    private static void ClassExample()
    {
        Console.WriteLine("\n--- CLASS ---");

        Customer c1 = new Customer(1, "Ali");

        Customer c2 = new Customer(1, "Ali");

        // Class uses reference equality by default.
        Console.WriteLine($"c1 == c2 : {c1 == c2}");
        // False


        // Class object can change its state.
        c1.ChangeName("Ahmed");

        Console.WriteLine(c1);
        // Customer: 1, Ahmed


        // Java:
        //
        // Customer c1 = new Customer(1, "Ali");
        // Customer c2 = new Customer(1, "Ali");
        //
        // System.out.println(c1 == c2);
        // false
    }


    // =============================================================================================
    // RECORD EXAMPLE
    // =============================================================================================

    private static void RecordExample()
    {
        Console.WriteLine("\n--- RECORD ---");

        CustomerDto dto1 = new CustomerDto(1, "Ali");

        CustomerDto dto2 = new CustomerDto(1, "Ali");


        // Record uses value equality.
        Console.WriteLine($"dto1 == dto2 : {dto1 == dto2}");
        // True


        Console.WriteLine(dto1);
        // CustomerDto { Id = 1, Name = Ali }


        // Java:
        //
        // CustomerDto dto1 = new CustomerDto(1, "Ali");
        // CustomerDto dto2 = new CustomerDto(1, "Ali");
        //
        // System.out.println(dto1.equals(dto2));
        // true
    }


    // =============================================================================================
    // RECORD `with` EXAMPLE
    // =============================================================================================
    //
    // `with` = create a copy of the record with specified changes.
    //
    // Original record is not changed.
    //
    // =============================================================================================

    private static void WithExample()
    {
        Console.WriteLine("\n--- RECORD WITH ---");

        CustomerDto dto1 =
            new CustomerDto(1, "Ali");


        CustomerDto dto2 =
            dto1 with
            {
                Name = "Ahmed"
            };


        Console.WriteLine($"Original: {dto1}");
        Console.WriteLine($"New     : {dto2}");

        // Original: CustomerDto { Id = 1, Name = Ali }
        // New     : CustomerDto { Id = 1, Name = Ahmed }
    }
}


// =================================================================================================
// MEMORY TRICK
// =================================================================================================
//
// CLASS
//     "WHO IS THIS OBJECT?"
//     Customer #1
//
// RECORD
//     "WHAT DATA DOES THIS OBJECT CONTAIN?"
//     CustomerDto(1, "Ali")
//
// =================================================================================================
//
// Real-world ASP.NET Core example:
//
//     Customer          -> CLASS
//     CustomerDto       -> RECORD
//     CreateCustomerRequest -> RECORD
//     CustomerResponse  -> RECORD
//
// =================================================================================================