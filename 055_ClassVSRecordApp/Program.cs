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

        CustomerDto dto1 = new CustomerDto(1, "Ali");

        CustomerDto dto2 = dto1 with
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

//                  Object
//                    │
//           ┌────────┴────────┐
//           │                 │
//       Identity?          Just Data?
//           │                 │
//          YES                YES
//           │                 │
//           ▼                 ▼
//        CLASS              RECORD
//           │                 │
//     Customer              DTO
//     Employee              Request
//     Order                 Response
//     Account               Address
//     Cart                  Money


// =================================================================================================
// C# CLASS vs RECORD — SIMILARITIES & DIFFERENCES
// =================================================================================================
// +------------------------------------------------------------------------------------------------
// | FEATURE                    | CLASS                              | RECORD
// |----------------------------|------------------------------------|------------------------------
// | Reference type             | Yes                                | Yes
// | Strongly typed             | Yes                                | Yes
// | Properties                 | Yes                                | Yes
// | Methods                    | Yes                                | Yes
// | Constructor                | Yes                                | Yes
// | Can implement interface    | Yes                                | Yes
// | Can inherit                | Yes                                | Yes
// | Can use init               | Yes                                | Yes
// | Can use get/set            | Yes                                | Yes
// | Reference equality         | Default                            | Value-based equality
// | == comparison              | Reference comparison               | Value comparison
// | ToString()                 | Usually custom implementation      | Generated automatically
// | with expression            | No                                 | Yes
// | Deconstruction             | Not automatic                      | Supported
// | Primary constructor        | C# 12+                             | Built-in/common syntax
// +------------------------------------------------------------------------------------------------
//
// =================================================================================================
// 1. STRONGLY TYPED
// =================================================================================================
//
// Both CLASS and RECORD are strongly typed.
//
// CLASS:
//
//     Customer customer = new Customer(1, "Ali");
//
// RECORD:
//
//     CustomerDto dto = new CustomerDto(1, "Ali");
//
// Both provide compile-time type checking.
//
//     customer = "Ali";     // ❌ Compile error
//     dto = 100;            // ❌ Compile error
//
// =================================================================================================
// 2. RECORD PRIMARY CONSTRUCTOR
// =================================================================================================
//
// RECORD provides a short syntax:
//
//     public record CustomerDto(int Id, string Name);
//
// This is equivalent in concept to defining properties and
// a constructor manually.
//
// CLASS normally requires more code:
//
//     public class Customer
//     {
//         public int Id { get; }
//         public string Name { get; }
//
//         public Customer(int id, string name)
//         {
//             Id = id;
//             Name = name;
//         }
//     }
//
//
//
// =================================================================================================
// 3. `get; set;` — BOTH CAN USE IT
// =================================================================================================
//
// CLASS:
//
//     public class Customer
//     {
//         public string Name { get; set; }
//     }
//
// RECORD:
//
//     public record CustomerDto
//     {
//         public string Name { get; set; }
//     }
//
// Both properties can be changed:
//
//     customer.Name = "Ahmed";
//     dto.Name = "Ahmed";
//
// Therefore:
//
//     RECORD ≠ automatically immutable
//     RECORD automatically mutable
//
// A record CAN contain mutable properties.
//
//
// =================================================================================================
// 4. `init` — BOTH CAN USE IT
// =================================================================================================
//
// CLASS:
//
//     public class Customer
//     {
//         public int Id { get; init; }
//         public string Name { get; init; }
//     }
//
// RECORD:
//
//     public record CustomerDto
//     {
//         public int Id { get; init; }
//         public string Name { get; init; }
//     }
//
// Object creation:
//
//     Customer customer = new Customer
//     {
//         Id = 1,
//         Name = "Ali"
//     };
//
//     CustomerDto dto = new CustomerDto
//     {
//         Id = 1,
//         Name = "Ali"
//     };
//
//
// After creation:
//
//     customer.Name = "Ahmed";    // ❌ Cannot assign
//     dto.Name = "Ahmed";         // ❌ Cannot assign
//
// `init` means:
//
//     Property can be assigned during object initialization,
//     but cannot normally be changed afterward.
//
//
//
// =================================================================================================
// 5. EQUALITY — BIG DIFFERENCE
// =================================================================================================
//
// CLASS:
//
//     Customer c1 = new Customer(1, "Ali");
//     Customer c2 = new Customer(1, "Ali");
//
//     Console.WriteLine(c1 == c2);
//
//     // False
//
// By default, classes compare object references.
//
//
//
// RECORD:
//
//     CustomerDto dto1 = new CustomerDto(1, "Ali");
//     CustomerDto dto2 = new CustomerDto(1, "Ali");
//
//     Console.WriteLine(dto1 == dto2);
//
//     // True
//
// Records compare their DATA/VALUES by default.
//
//
// =================================================================================================
// 6. `with` — RECORD SPECIAL FEATURE
// =================================================================================================
//
// RECORD:
//
//     CustomerDto dto1 = new CustomerDto(1, "Ali");
//
//     CustomerDto dto2 = dto1 with
//     {
//         Name = "Ahmed"
//     };
//
// dto1 remains:
//
//     CustomerDto(1, "Ali")
//
// dto2 becomes:
//
//     CustomerDto(1, "Ahmed")
//
//
//
// CLASS:
//
//     Customer c2 = c1 with { ... };
//
//     // ❌ `with` is not normally available for a regular class.
//
//
// =================================================================================================
// 7. IMMUTABILITY
// =================================================================================================
//
// Neither CLASS nor RECORD is automatically immutable.
// Both CLASS and RECORD is automatically mutable/changeable.
//
// Mutable CLASS:
//
//     public class Customer
//     {
//         public string Name { get; set; }
//     }
//
// Mutable RECORD:
//
//     public record CustomerDto
//     {
//         public string Name { get; set; }
//     }
//
// Immutable-style CLASS (not changeable):
//
//     public class Customer
//     {
//         public int Id { get; init; }
//         public string Name { get; init; }
//     }
//
// Immutable-style RECORD (not changeable):
//
//     public record CustomerDto(int Id, string Name);
//
//
//
// =================================================================================================
// 8. TOSTRING()
// =================================================================================================
//
// CLASS:
//
//     Console.WriteLine(customer);
//
// Unless you override ToString(), the output is generally
// the type name.
//
// RECORD:
//
//     Console.WriteLine(dto);
//
// Automatically produces useful data-oriented output:
//
//     CustomerDto { Id = 1, Name = Ali }
//
//
//
// =================================================================================================
// 9. JAVA COMPARISON
// =================================================================================================
//
// C# CLASS:
//
//     public class Customer
//     {
//         public int Id { get; init; }
//         public string Name { get; init; }
//     }
//
// Java CLASS:
//
//     public class Customer
//     {
//         private int id;
//         private String name;
//
//         public Customer(int id, String name)
//         {
//             this.id = id;
//             this.name = name;
//         }
//     }
//
//
//
// C# RECORD:
//     public record CustomerDto(int Id, string Name);
//
// Java RECORD:
//     public record CustomerDto(int id, String name) {}
//
//
// =================================================================================================
// 10. QUICK MEMORY TRICK
// =================================================================================================
//
// CLASS:
//
//     Identity is usually important.
//
//     "WHO IS THIS OBJECT?"
//
//     Customer #101
//
//
// RECORD:
//
//     Data/value is usually important.
//
//     "WHAT DATA DOES THIS OBJECT CONTAIN?"
//
//     CustomerDto(101, "Ali")
//
//
// =================================================================================================
// MOST IMPORTANT DIFFERENCES
// =================================================================================================
//
// CLASS:
//
//     - Reference equality by default
//     - No automatic `with` support
//     - Usually used for entities/objects with identity
//     - Can be mutable or immutable
//
// RECORD:
//
//     - Value-based equality by default
//     - Supports `with` expressions
//     - Concise primary-constructor syntax
//     - Excellent for DTOs, requests and responses
//     - Can ALSO be mutable if using `set`
//
//
// =================================================================================================
// SIMPLE RULE
// =================================================================================================
//
//     CLASS  → Identity + Behavior + State
//
//     RECORD → Data + Value Equality + Immutability-friendly
//
// =================================================================================================