using System;

namespace DataTypes_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 30_DataTypes ===");
            Console.WriteLine("Java vs C# Data Types");
            Console.WriteLine();


            // ============================================================
            // 1. INTEGER TYPES
            // ============================================================

            int age = 43;                 // 4 bytes
            long population = 9000000000L; // 8 bytes
            short year = 2026;            // 2 bytes
            byte percentage = 100;        // 1 byte

            Console.WriteLine($"int   : {age}");
            Console.WriteLine($"long  : {population}");
            Console.WriteLine($"short : {year}");
            Console.WriteLine($"byte  : {percentage}");

            // Java:
            // int age = 43;
            // long population = 9000000000L;
            // short year = 2026;
            // byte percentage = 100;


            // ============================================================
            // 2. FLOAT / DOUBLE / DECIMAL
            // ============================================================

            float f = 10.5F;
            double d = 1234.567;
            decimal salary = 123456.789M;

            Console.WriteLine($"float   : {f}");
            Console.WriteLine($"double  : {d}");
            Console.WriteLine($"decimal : {salary}");

            // Java:
            // float f = 10.5F;
            // double d = 1234.567;
            //
            // Java has no direct built-in decimal primitive.
            // BigDecimal is commonly used:
            //
            // BigDecimal salary =
            //     new BigDecimal("123456.789");


            // ============================================================
            // 3. CHAR / STRING / BOOLEAN
            // ============================================================

            char grade = 'A';
            string name = "Ali";
            bool active = true;

            Console.WriteLine($"char   : {grade}");
            Console.WriteLine($"string : {name}");
            Console.WriteLine($"bool   : {active}");

            // Java:
            // char grade = 'A';
            // String name = "Ali";
            // boolean active = true;


            // ============================================================
            // 4. C# ADDITIONAL TYPES
            // ============================================================
            // These have no direct Java primitive equivalent.

            sbyte signedByte = -100;
            ushort unsignedShort = 65000;
            uint unsignedInt = 4000000000U;
            ulong unsignedLong = 10000000000UL;

            Console.WriteLine($"sbyte  : {signedByte}");
            Console.WriteLine($"ushort : {unsignedShort}");
            Console.WriteLine($"uint   : {unsignedInt}");
            Console.WriteLine($"ulong  : {unsignedLong}");

            // Java:
            // No direct primitive equivalents for:
            // sbyte, ushort, uint, ulong


            // ============================================================
            // 5. VAR - IMPLICIT TYPE
            // ============================================================
            // Type is determined at compile time.

            var city = "Karachi";
            var count = 10;

            Console.WriteLine($"city  : {city}");
            Console.WriteLine($"count : {count}");

            // Java 10+:
            // var city = "Karachi";
            // var count = 10;
            //
            // Both Java and C# var are compile-time inferred types.


            // ============================================================
            // 6. DYNAMIC - C# ONLY
            // ============================================================
            // Type is resolved at runtime.

            dynamic value = 10;

            Console.WriteLine(value);

            value = "Hello";

            Console.WriteLine(value);

            // OUTPUT:
            // 10
            // Hello

            // Java:
            // No direct equivalent of C# dynamic.


            // ============================================================
            // 7. NUMBER SUFFIXES
            // ============================================================

            long l = 100L;
            uint u = 100U;
            ulong ul = 100UL;
            float fl = 100.5F;
            double db = 100.5D;
            decimal dc = 100.5M;

            Console.WriteLine(l);
            Console.WriteLine(u);
            Console.WriteLine(ul);
            Console.WriteLine(fl);
            Console.WriteLine(db);
            Console.WriteLine(dc);

            // Java:
            // long l = 100L;
            // float fl = 100.5F;
            // double db = 100.5D;
            //
            // C# additionally uses:
            // U / UL → unsigned integer types
            // M      → decimal


            // ============================================================
            // 8. TYPE INFORMATION
            // ============================================================

            Console.WriteLine(typeof(int));       // System.Int32
            Console.WriteLine(typeof(string));    // System.String
            Console.WriteLine(typeof(bool));      // System.Boolean
            Console.WriteLine(typeof(double));    // System.Double
            Console.WriteLine(typeof(decimal));   // System.Decimal

            // Java:
            // int.class
            // String.class
            // boolean.class
            // double.class


            // ============================================================
            // 9. NULLABLE TYPES
            // ============================================================
            // C# value types can explicitly allow null using ?.

            int? nullableAge = null;

            Console.WriteLine(nullableAge ?? 0);

            // OUTPUT:
            // 0

            // Java:
            // Integer nullableAge = null;
            //
            // System.out.println(
            //     nullableAge != null ? nullableAge : 0
            // );


            // ============================================================
            // 10. VAR vs DYNAMIC
            // ============================================================

            var x = 10;          // compile-time type: int
            dynamic y = 10;      // runtime type

            Console.WriteLine(x);
            Console.WriteLine(y);

            // C#:
            // var x = 10;       // x remains int
            // dynamic y = 10;   // y can later hold another type
            //
            // y = "Hello";      // valid


            Console.WriteLine();
            Console.WriteLine("Done.");
        }
    }
}

// ================================================================
// JAVA → C# DATA TYPES WITH MEMORY SIZE & RANGES
// ================================================================

// +----------+--------------------+----------------------+--------------------------------------+----------------------+--------------------------------------+
// | JAVA     | C#                 | JAVA MEMORY          | JAVA RANGE / VALUE                   | C# MEMORY            | C# RANGE / VALUE                     |
// +----------+--------------------+----------------------+--------------------------------------+----------------------+--------------------------------------+
// | int      | int    /Int32      | 4 bytes              | -2,147,483,648 to 2,147,483,647      | 4 bytes              | -2,147,483,648 to 2,147,483,647      |
// | long     | long   /Int64      | 8 bytes              | -9.22E18 to 9.22E18                  | 8 bytes              | -9.22E18 to 9.22E18                  |
// | byte     | byte   /Byte       | 1 byte               | -128 to 127                          | 1 byte               | -128 to 127                          |
// | short    | short  /Int16      | 2 bytes              | -32,768 to 32,767                    | 2 bytes              | -32,768 to 32,767                    |
// | float    | float  /Single     | 4 bytes              | ~±1.4E-45 to ±3.4E38                 | 4 bytes              | ~±1.5E-45 to ±3.4E38                 |
// | double   | double /Double     | 8 bytes              | ~±4.9E-324 to ±1.8E308               | 8 bytes              | ~±5.0E-324 to ±1.7E308               |
// | char     | char   /Char       | 2 bytes              | 0 to 65,535 (UTF-16)                 | 2 bytes              | 0 to 65,535 (UTF-16)                 |
// | String   | string /String     | Reference + variable | String object                        | Reference + variable | System.String object                 |
// | boolean  | bool   /Boolean    | JVM-dependent*       | true / false                         | 1 byte               | true / false                         |
// +----------+--------------------+----------------------+--------------------------------------+----------------------+--------------------------------------+


// ================================================================
// C# ADDITIONAL DATA TYPES
// ================================================================

// +----------+----------+-------------------------------+--------------------------------------+
// | JAVA     | C#                | C# MEMORY            | C# RANGE / VALUE                     |
// +----------+-------------------+----------------------+--------------------------------------+
// | —        | sbyte/SByte       | 1 byte               | -128 to 127                          |
// | —        | ushort/UInt16     | 2 bytes              | 0 to 65,535                          |
// | —        | uint/UInt32       | 4 bytes              | 0 to 4,294,967,295                   |
// | —        | ulong/UInt64      | 8 bytes              | 0 to 18,446,744,073,709,551,615      |
// | —        | decimal/Decimal   | 16 bytes             | ±1.0E-28 to ±7.9E28                  |
// | —        | dynamic/Dynamic   | Variable/reference   | Depends on runtime type/value        |
// +----------+----------+-------------------------------+--------------------------------------+

// +----------------+----------------------+--------------------------------------+
// | SUFFIX         | TYPE                 | EXAMPLE                              |
// +----------------+----------------------+--------------------------------------+
// | none           | int                  | 100                                  |
// | l / L          | long                 | 100L                                 |
// | u / U          | uint                 | 100U                                 |
// | ul / UL        | ulong                | 100UL                                |
// | lu / LU        | ulong                | 100LU                                |
// | f / F          | float                | 100.5F                               |
// | d / D          | double               | 100.5D                               |
// | m / M          | decimal              | 100.5M                               |
// +----------------+----------------------+--------------------------------------+


// ================================================================
// NOTE
// ================================================================
//
// * Java boolean:
//   Java does not define an exact memory size for boolean.
//   The JVM implementation determines its actual storage.
//
// String / string:
//   Both are reference types with variable-size objects;
//   actual memory usage depends on the string contents and runtime.
//
// decimal:
//   C# decimal provides approximately 28–29 significant digits.
//
// dynamic:
//   C# dynamic is resolved at runtime; its actual storage depends
//   on the value/type assigned to it.

// +----------------------+----------------------+----------------------+----------------------------+
// | TYPE                 | JAVA                 | C#                   | C# DESCRIPTION             |
// +----------------------+----------------------+----------------------+----------------------------+
// | String               | String               | string / Strin       | Text / sequence of chars   |
// |                      |                      |                      | Example: "Hello"           |
// +----------------------+----------------------+----------------------+----------------------------+
// | Character            | char                 | char / Char          | Single character           |
// |                      |                      |                      | Example: 'A'               |
// +----------------------+----------------------+----------------------+----------------------------+
// | Boolean              | boolean              | bool / Boolean       | true or false              |
// |                      |                      |                      | Example: true              |
// +----------------------+----------------------+----------------------+----------------------------+
// | Date / Time          | LocalDateTime        | DateTime / DateOnly  | Date and time              |
// |                      | java.time.LocalDateTime| System.DateTime    | Example: DateTime.Now      |
// +----------------------+----------------------+----------------------+----------------------------+
// | Dynamic Type         | —                    | dynamic              | Runtime type resolution    |
// |                      |                      |                      | Example: dynamic x = 10;   |
// +----------------------+----------------------+----------------------+----------------------------+
// | Implicit Type        | var                  | var                  | Compile-time type inferred |
// |                      |                      |                      | Example: var x = 10;       |
// +----------------------+----------------------+----------------------+----------------------------+
//
// NOTE:
// Java:
//   String  → java.lang.String
//   char    → primitive character type
//   boolean → primitive Boolean type
//
// C#:
//   string  → System.String
//   char    → System.Char
//   bool    → System.Boolean
//   dynamic → System.Object at runtime with dynamic binding
//   var     → compile-time inferred type; NOT a runtime type
//
// IMPORTANT:
// `var` and `dynamic` are different:
//
//   var x = 10;       // x is statically typed as int
//   dynamic y = 10;  // y is dynamically resolved at runtime

// +-----------------------+--------------------------------------+--------------------------------------+--------------------------------------+
// | SCENARIO              | JAVA CODE                            | C# CODE                              | RESULT                               |
// +-----------------------+--------------------------------------+--------------------------------------+--------------------------------------+
// | Multiple variables    | var name = "Saad", fname = "ali";    | var name = "Saad", fname = "ali";    | Java: ✅ Valid                       |
// | with var              | int age = 34;                        | int age = 34;                        | C#:   ❌ CS0819                      |
// +-----------------------+--------------------------------------+--------------------------------------+--------------------------------------+
// | Separate var          | var name = "Saad";                   | var name = "Saad";                   | Java: ✅ Valid                       |
// | declarations          | var fname = "ali";                   | var fname = "ali";                   | C#:   ✅ Valid                       |
// |                       | int age = 34;                        | int age = 34;                        |                                      |
// +-----------------------+--------------------------------------+--------------------------------------+--------------------------------------+
// | Explicit type with    | String name = "Saad",                | string name = "Saad",                | Java: ✅ Valid                       |
// | multiple variables    |        fname = "ali";                |        fname = "ali";                | C#:   ✅ Valid                       |
// |                       | int age = 34;                        | int age = 34;                        |                                      |
// +-----------------------+--------------------------------------+--------------------------------------+--------------------------------------+
//
// IMPORTANT:
//
// JAVA:
// var name = "Saad", fname = "ali";
// → Allowed because Java's `var` can be used with multiple declarators.
//
// C#:
// var name = "Saad", fname = "ali";
// → ❌ Error CS0819
// → An implicitly typed local variable declaration cannot include multiple declarators.
//
// C# solution:
// var name = "Saad";
// var fname = "ali";
//
// OR use an explicit type:
// string name = "Saad", fname = "ali";
