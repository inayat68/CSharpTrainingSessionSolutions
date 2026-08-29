using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UserManagementApi.Data;
using UserManagementApi.Middleware;
using UserManagementApi.Repositories;
using UserManagementApi.Seed;
using UserManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var dbNameWithPath = Environment.GetEnvironmentVariable("DB_PATH") ?? "Database/cs_users_db.db";
//      OR
var dbNameWithPath2 = Path.Combine(AppContext.BaseDirectory.Replace("\\bin\\Debug\\net8.0", ""), "Database", "cs_users_db.db");

var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? "dev_key";
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "api";
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "api_users";
var frontend = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:5173";

//# SQLite provider
//dotnet add package Microsoft.EntityFrameworkCore.Sqlite
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite($"Data Source={dbNameWithPath}"));

//          OR

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite(connectionString));

// ------------------------------------------------------------
// Register SQL Server DbContext
// ------------------------------------------------------------
//# SQL Server provider
//dotnet add package Microsoft.EntityFrameworkCore.SqlServer

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection")
//    ));

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("frontend", p =>
        p.WithOrigins(frontend).AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<PasswordService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementApi v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("frontend");

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    await DatabaseSeeder.SeedAsync(db);
}

bool isTraninging = false;
if (isTraninging)
{
    Chapters.chapter01();
    //Chapters.chapter02();
    //Chapters.chapter03();
    //Chapters.chapter04();
    //Chapters.chapter05();
    //Chapters.chapter06();
    //Chapters.chapter07();
    //Chapters.chapter08();


    //ChapterCollection.chapter11();
    //ChapterCollection.chapter12();
    //ChapterCollection.chapter13();
}

app.Run();


// DB Packages
//# SQLite provider
//dotnet add package Microsoft.EntityFrameworkCore.Sqlite

//# EF Core base package
//dotnet add package Microsoft.EntityFrameworkCore

//# SQL Server provider
//dotnet add package Microsoft.EntityFrameworkCore.SqlServer

//# In-Memory database provider (mainly for testing)
//dotnet add package Microsoft.EntityFrameworkCore.InMemory

//# EF Core Design package (Migrations, Scaffolding, etc.)
//dotnet add package Microsoft.EntityFrameworkCore.Design

//# EF Core CLI Tools (migrations/database commands)
//dotnet tool install --global dotnet-ef






//Basics





public class Chapters
{
    //Chapter 01: Print / Display BigInteger / BigDecimal
    public static void chapter01()
    {
        // ================================================================
        // JAVA → C# CONSOLE / PRINT METHODS
        // ================================================================

        // +----------------------------+---------------------------+-----------------------------------------------------+
        // | CATEGORY                   | JAVA                      |         C#                                          |
        // +----------------------------+---------------------------+-----------------------------------------------------+
        // | Print w/o ending LBreak    | System.out.print()        | Console.Write()                                     |
        // | Print with ending LBreak   | System.out.println()      | Console.WriteLine()                                 |
        // | Formatted Print w/wo LB    | System.out.printf("%n")   | Console.Write("{0} - {1} = {2}\n", 123, 23, 100);   |
        // | Formatted Output w/wo LB   | System.out.format("%n")   | Console.WriteLine("{0} - {1} = {2}", 123, 23, 100); |
        // | Error Print w/wo LB        | System.err.println()      | Console.Error.WriteLine()                           |
        // | Exception Stack Trace      | ex.printStackTrace()      | Console.WriteLine(ex)                               |
        // |                            |                           | Debug.WriteLine("")                                 |
        // | String Formatting          | var f=String.format(...); | var f = string.Format("{0}-{1}={2}",3, 1, 2);       |
        // |                            | System.out.println(f);    | Console.WriteLine(f);                               |
        // | Interpolation in C#        | System.out.println("%n")  | var name="Saad", age = 34;                          |
        // |                            |                           | Console.Write($"{name} age is {age}");              |
        // +----------------------------+---------------------------+-----------------------------------------------------+


        // ================================================================
        // C# FORMAT SPECIFIERS - NUMBER FORMATS
        // ================================================================

        // +----------------------------+--------------------------------------+--------------------------------------+
        // | FORMAT                     | JAVA EQUIVALENT SYNTAX               | C# EXAMPLE                           |
        // +----------------------------+--------------------------------------+--------------------------------------+
        // | Decimal                    | System.out.printf("%.2f", 1234.567); | Console.WriteLine("{0:F2}", 1234.567)|
        // |                            | Output: 1234.57                      | Output: 1234.57                      |
        // |                            | System.out.printf("%.2f %d",        | Console.WriteLine("{0} {1:F2} {2}",  |
        // |                            |     14.567, 2);                       |     "Salary:", 1234.567, "USD");     |
        // +----------------------------+--------------------------------------+--------------------------------------+
        // | Hexadecimal                | System.out.printf("%X", 255);        | Console.WriteLine("{0:X}", 255)      |
        // |                            | Output: FF                           | Output: FF                           |
        // +----------------------------+--------------------------------------+--------------------------------------+
        // | Octal                      | Integer.toOctalString(255);          | Convert.ToString(255, 8)             |
        // |                            | Output: 377                          | Output: 377                          |
        // +----------------------------+--------------------------------------+--------------------------------------+
        // | Binary                     | Integer.toBinaryString(255);         | Convert.ToString(255, 2)             |
        // |                            | Output: 11111111                     | Output: 11111111                     |
        // +----------------------------+--------------------------------------+--------------------------------------+
        // | Exponential                | System.out.printf("%e", 1234.567);   | Console.WriteLine("{0:E}", 1234.567) |
        // | / Scientific               | Output: 1.234567e+03                 | Output: 1.234567E+003                |
        // +----------------------------+--------------------------------------+--------------------------------------+
        // | Floating Point             | System.out.printf("%.2f", 1234.567); | Console.WriteLine("{0:F2}", 1234.567)|
        // |                            | Output: 1234.57                      | Output: 1234.57                      |
        // +----------------------------+--------------------------------------+--------------------------------------+
        // | Percentage                 | System.out.printf("%.2f%%", 0.756);  | Console.WriteLine("{0:P2}", 0.756)   |
        // |                            | Output: 75.60%                       | Output: 75.60%                       |
        // +----------------------------+--------------------------------------+--------------------------------------+
        // | Currency                   | System.out.printf("$%,.2f", 1234.56);| Console.WriteLine("{0:C2}", 1234.56) |
        // |                            | Output: $1,234.56                    | Output: $1,234.56*                   |
        // +----------------------------+--------------------------------------+--------------------------------------|
        //
        // * C# currency symbol depends on the current culture/locale.
        //   For example, en-US → $1,234.56

        // +----------------------+--------------------------------------+
        // | JAVA FORMAT          | DESCRIPTION                          |
        // +----------------------+--------------------------------------+
        // | %c                   | Character                            |
        // | %s                   | String                               |
        // | %b                   | Boolean                              |
        // | %d                   | Decimal integer                      |
        // | %o                   | Octal integer                        |
        // | %x                   | Hexadecimal integer (lowercase)      |
        // | %X                   | Hexadecimal integer (uppercase)      |
        // | %f                   | Floating-point number                |
        // | %e                   | Scientific notation (lowercase)      |
        // | %E                   | Scientific notation (uppercase)      |
        // | %g                   | General floating-point format        |
        // | %a                   | Hexadecimal floating-point           |
        // | %h                   | Hash code (hexadecimal)               |
        // | %n                   | Platform-specific line separator     |
        // | %%                   | Literal percent (%)                  |
        // +----------------------+--------------------------------------+
        // | Common Formatting:   |                                      |
        // | %.2f                 | Floating point, 2 decimal places     |
        // | %10s                 | String, width 10 (right-aligned)     |
        // | %-10s                | String, width 10 (left-aligned)      |
        // | %05d                 | Integer, padded with zeros            |
        // | %,d                  | Integer with grouping separator      |
        // | %+,d                 | Integer with + / - sign              |
        // +----------------------+--------------------------------------+

        // +----------------------+--------------------------------------+---------------------------------------+
        // | FORMAT               | JAVA EQUIVALENT SYNTAX               | C# EQUIVALENT SYNTAX                  |
        // +----------------------+--------------------------------------+---------------------------------------+
        // | %.2f                 | System.out.printf("%.2f", 1234.567); | Console.WriteLine("{0:F2}", 1234.567) |
        // |                      | Output: 1234.57                      | Output: 1234.57                       |
        // +----------------------+--------------------------------------+---------------------------------------+
        // | %10s                 | System.out.printf("%10s", "Ali");    | Console.WriteLine("{0,10}", "Ali");   |
        // |                      | Output:        Ali                   | Output:        Ali                    |
        // +----------------------+--------------------------------------+---------------------------------------+
        // | %-10s                | System.out.printf("%-10s", "Ali");   | Console.WriteLine("{0,-10}", "Ali");  |
        // |                      | Output: Ali                          | Output: Ali                           |
        // +----------------------+--------------------------------------+---------------------------------------+
        // | %05d                 | System.out.printf("%05d", 42);       | Console.WriteLine("{0:D5}", 42);      |
        // |                      | Output: 00042                        | Output: 00042                         |
        // +----------------------+--------------------------------------+---------------------------------------+
        // | %,d                  | System.out.printf("%,d", 1234567);   | Console.WriteLine("{0:N0}", 1234567); |
        // |                      | Output: 1,234,567                    | Output: 1,234,567                     |
        // +----------------------+--------------------------------------+---------------------------------------+
        // | %+,d                 | System.out.printf("%+,d", 1234567);  | Console.WriteLine("{0:+#,##0;-#,##0}",|
        // |                      | Output: +1,234,567                   |     1234567);                         |
        // |                      |                                      | Output: +1,234,567                    |
        // +----------------------+--------------------------------------+---------------------------------------+
        // | %+,d                 | System.out.printf("%+,d", -1234567); | Console.WriteLine("{0:+#,##0;-#,##0}",|
        // |                      | Output: -1,234,567                   |     -1234567);                        |
        // |                      |                                      | Output: -1,234,567                    |
        // +----------------------+--------------------------------------+---------------------------------------+
        //
        // NOTE:
        // C# alignment:
        // {0,10}  → Right-aligned, width 10
        // {0,-10} → Left-aligned, width 10
        //
        // C# numeric formatting:
        // F2 → 2 decimal places
        // D5 → Integer padded to 5 digits
        // N0 → Number with thousands separator, 0 decimal places

        // JAVA Numbers Prefix
        // +----------------------+----------+------------------+----------+--------------------------+
        // | NUMBER SYSTEM        | JAVA     | JAVA EXAMPLE     | C#       | C# EXAMPLE               |
        // +----------------------+----------+------------------+----------+--------------------------+
        // | Decimal              | None     | 255              | None     | 255                      |
        // | Binary               | 0b / 0B  | 0b11111111       | 0b / 0B  | 0b11111111               |
        // | Octal                | 0        | 0377             | —        | Convert.ToString(255, 8) |
        // | Hexadecimal          | 0x / 0X  | 0xFF             | 0x / 0X  | 0xFF                     |
        // +----------------------+----------+------------------+----------+--------------------------+


        // New Line
        Console.WriteLine();

        //ERROR: No Overlod
        //Console.Write();

        Console.Write("Chapter");
        Console.WriteLine('1');     //'' for single-char
        Console.Write("Started");

        // ---------------- NUMBER ----------------
        Console.WriteLine(1);
        Console.WriteLine(2);
        Console.WriteLine(3);
        // OUTPUT: (each in different line)
        // 1
        // 2
        // 3

        Console.Write(1);
        Console.Write(2);
        Console.Write(3);
        // OUTPUT: 123 (all in 1 line)

        Console.WriteLine(1 + " " + 9);
        // OUTPUT: 1 9

        Console.WriteLine(1 + 9);
        // OUTPUT: 10

        /*
          SUMMARY - C# FORMAT SPECIFIERS
          ================================================================

          // +----------------------+--------------------------------------+----------------------+
          // | FORMAT               | C# EXAMPLE                           | OUTPUT               |
          // +----------------------+--------------------------------------+----------------------+
          // | {0}                  | Console.WriteLine("{0}", 123);       | 123                  |
          // | Integer / Object     |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0:F}                | Console.WriteLine("{0:F}", 1234.567);| 1234.57              |
          // | Floating Point       |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0:F2}               |Console.WriteLine("{0:F2}", 1234.567);| 1234.57             |
          // | 2 Decimal Places     |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0}                  | Console.WriteLine("{0}", "Ali");     | Ali                  |
          // | String / Object      |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0}                  | Console.WriteLine("{0}", 'A');       | A                    |
          // | Character            |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0}                  | Console.WriteLine("{0}", true);      | True                 |
          // | Boolean              |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0:X}                | Console.WriteLine("{0:X}", 255);     | FF                   |
          // | Hexadecimal          |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | Convert.ToString()   | Console.WriteLine(                   | 377                  |
          // | Octal                |     Convert.ToString(255, 8));       |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | Convert.ToString()   | Console.WriteLine(                   | 11111111             |
          // | Binary               |     Convert.ToString(255, 2));       |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0:P2}               | Console.WriteLine("{0:P2}", 0.756);  | 75.60%               |
          // | Percentage           |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0:C2}               | Console.WriteLine("{0:C2}", 1234.56);| $1,234.56*           |
          // | Currency             |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0:E2}               | Console.WriteLine("{0:E2}", 1234.56);| 1.23E+003            |
          // | Exponential          |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0,10}               | Console.WriteLine("{0,10}", "Ali");  |        Ali           |
          // | Right Alignment      |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0,-10}              | Console.WriteLine("{0,-10}", "Ali"); | Ali                  |
          // | Left Alignment       |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0:D5}               | Console.WriteLine("{0:D5}", 42);     | 00042                |
          // | Zero Padding         |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | {0:N0}               | Console.WriteLine("{0:N0}", 1234567);| 1,234,567            |
          // | Thousands Separator  |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          // | \n                   | Console.WriteLine("Hello\nWorld");   | Hello                |
          // | Newline              |                                      | World                |
          // +----------------------+--------------------------------------+----------------------+
          // | %                    | Console.WriteLine("75%");            | 75%                  |
          // | Percent Sign         |                                      |                      |
          // +----------------------+--------------------------------------+----------------------+
          //
          // * Currency symbol depends on the current system culture.
          //
          // IMPORTANT:
          // C# does NOT require Java-style %s, %d, %f, etc.
          //
          // Java:
          // System.out.printf("%.2f", 1234.567);
          //
          // C#:
          // Console.WriteLine("{0:F2}", 1234.567);
          //
          // Modern C# alternative:
          // Console.WriteLine($"{1234.567:F2}");
      */

        Console.Write("{0}", 2);
        Console.Write("{0}", 2);

        Console.Write("{0}", 2);
        // OUTPUT: 222

        //Time view Format
        Console.Write("{0}:{1}pm", 10, 15);
        //OUTPUT: 10:15pm

        //Date view Format - day mon yer
        Console.Write("{0}th {1}, {2}", 20, "AUG", 2026);
        //OUTPUT: 20th AUG, 2026

        //Stop Watch view Timer - h:m:s.ms
        Console.Write("{0}:{1}:{2}.{3}", 3, 15, 32, 897);
        //OUTPUT: 3:15:32.897

        // No need for {2} as 3rd item
        // XXX Console.Write("{0} - {1} = {2}", 10, 5, 5); XXX

        //Can simply do
        Console.Write("{0} - {1} = {1}", 10, 5);
        // OUTPUT: 10 - 5 = 5

        var diff = string.Format("{0} - {1} = {2}", 123, 23, 100);
        Console.WriteLine(diff);

        Console.WriteLine("{0}\n{1}\n{2}", 1, 2, 3);
        // OUTPUT: (each in different line)
        // 1
        // 2
        // 3

        //System.out.format("Name: %s, Age: %d%n", name, age);
        //System.out.printf("Name: %s | Age: %d | Salary: %.2f%n", name, age, salary);

        Console.Write("Name = {0}, Salary={1}, Age= {2}", "Saad", 92500.25, 36);
        // OUTPUT: Name = Saad, Salary=92500.25, Age= 36 (all in 1 line)

        Console.Write("{0}{1}{2}", 1, 2, 3);
        // OUTPUT: 123 (all in 1 line)

        //With Interpolation
        Console.Write($"{111}-{"aaa"}, {3334}");
        // OUTPUT: 111-aaa, 3334

        Console.WriteLine("{0:F2}", 1234.567);
        // OUTPUT: 1234.57 (rounded to 2 decimals)

        Console.WriteLine("{0} is {1:F2} in {2}", "Salary:", 1234.567, "USD");
        // OUTPUT: Salary is 1234.567 in USD

        //With Interpolation
        Console.WriteLine(
                $"{"Salary:"} is {1234.567:F2} in {"USD"}"
            );

        //check formatting in different lines
        Console.WriteLine(
            "Id={0} Name={1} Salary={2:F2}",
            101,
            "Ali",
            50000.75);
        // OUTPUT: Id=101 Name=Ali Salary=50000.75

        //@ is Verbatim Literal for Multiline text like """" in Java
        Console.Write(@"{0} latest version is {1}.
            Its widely used for 
                        enterprise application development using .NET.", "C#", 14);
        /*  //Java Sample:
            System.out.printf(
                """
                %s latest version is %d.
                Its widely used for
                enterprise application development using .NET.
                """,
                "C#", 14
            );
       */

        Console.WriteLine(6.05);
        // OUTPUT: 6.05

        Console.WriteLine(-8);
        // OUTPUT: -8

        Console.WriteLine(-5.009);
        // OUTPUT: -5.009

        Console.WriteLine(11.03 + " " + (-4.91) + " " + 0.077);
        // OUTPUT: 11.03 -4.91 0.077

        Console.WriteLine(5e20);
        // OUTPUT: 5E+20 (scientific notation)

        Console.WriteLine(5e25);
        // OUTPUT: 5E+25

        Console.WriteLine(5.1e-6);
        // OUTPUT: 5.1E-06

        // ---------------- STRING ----------------

        Console.WriteLine("A");
        // OUTPUT: A

        Console.WriteLine('A');
        // OUTPUT: A

        string strObj = new string("Account");
        Console.WriteLine(strObj);
        // OUTPUT: Account

        Console.WriteLine(Convert.ToString(21));
        // OUTPUT: 21


        // Java Text Block - Multiline Text
        //Console.WriteLine("""
        //    Hello,
        //    World!
        //""");
        // OUTPUT:
        // Hello,
        // World!

        Console.WriteLine("A star ".GetType().Name);
        // OUTPUT: String alias is string

        Console.WriteLine('z'.GetType().Name);
        // OUTPUT: Char alias is char

        Console.WriteLine(5.GetType().Name);
        // OUTPUT: Int32 alias is int

        Console.WriteLine(5.25.GetType().Name);
        // OUTPUT: Double       --alias is double

        Console.WriteLine(true.GetType().Name);
        // OUTPUT: Boolean      --alias is bool

        Console.WriteLine(5.25f.GetType().Name);
        // OUTPUT: Single       --alias is float

        Console.WriteLine(5.25m.GetType().Name);
        // OUTPUT: Decimal      --alias is decimal

        Console.WriteLine("Text with\nDouble\nquotes");
        // OUTPUT:
        // Text with
        // Double
        // quotes

        Console.WriteLine("Text with\tSingle 'quotes");
        // OUTPUT: Text with    Single 'quotes

        Console.WriteLine("Apple" + ", " + " Mango" + " " + " Banana");
        // OUTPUT: Apple, Mango Banana

        Console.WriteLine("I am a \"Frontend Developer\"");
        // OUTPUT: I am a "Frontend Developer"


        // ---------------- BOOLEAN ----------------

        Console.WriteLine(true);
        // OUTPUT: True

        Console.WriteLine(false);
        // OUTPUT: False


    }


    // =========================
    // FUNCTION / METHOD
    // =========================

    // Java:
    //
    // class Demo {
    //     static int add(int a, int b) {
    //         return a + b;
    //     }
    // }

    // C# equivalent:
    public static int Add(int a, int b)
    {
        return a + b;
    }


    // =========================
    // INTEGER BIT COUNT
    // =========================

    // Java:
    //
    // Integer.bitCount(7);
    //
    // C# equivalent helper method.
    public static int BitCount(int value)
    {
        int count = 0;

        while (value != 0)
        {
            count += value & 1;
            value >>= 1;
        }

        return count;
    }



    //Chapter 02: Data Types -  Number, String, Char, Boolean, Var, Init, Numbers Parsing and String Methods
    public static void chapter02()
    {
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


        Console.WriteLine("Chapter 02");

        int x;
        // x declared but NOT initialized yet (local variable must be initialized before use)

        int y = 10, z = 20;
        // y = 10, z = 20 stored in memory

        Console.WriteLine(x = 5);
        // x assigned value 5 → output: 5

        Console.WriteLine(y + z);
        // 10 + 20 = 30 → output: 30


        // CONSTANTS
        // PI is constant (cannot be changed)
        const double PI = 3.1415;
        Console.WriteLine(PI);
        // output: 3.1415

        // fname is constant string = "John"
        const string fname = "John";
        Console.WriteLine(fname);
        // output: John


        // +------------+------------------+----------+---------------------------+
        // | C# TYPE    | .NET TYPE        | SIZE     | MAX PRECISION             |
        // +------------+------------------+----------+---------------------------+
        // | float      | System.Single    | 4 bytes  | ~6–9 significant digits   |
        // | double     | System.Double    | 8 bytes  | ~15–17 significant digits |
        // | decimal    | System.Decimal   | 16 bytes | 28–29 significant digits  |
        // +------------+------------------+----------+---------------------------+
        //
        // Simple Rule:
        //
        // float    → ~7 digits of precision
        // double   → ~16 digits of precision
        // decimal  → ~29 digits of precision
        //
        // NOTE:
        // Precision means significant digits, NOT simply the number of
        // digits after the decimal point.
        //
        // float   → Good for general floating-point calculations
        // double  → Default choice for most scientific/general calculations
        // decimal → Recommended for financial/money calculations


        //precision places count			
        float ft = 11.397243323f;
        double db = 11.34238943734234;
        decimal dc = 11.34238943783242342343434m;

        // BOOLEAN EXAMPLE

        bool isLoggedIn = true;
        // user is logged in

        bool isAdmin = false;
        // user is NOT admin

        Console.WriteLine("LoggedIn: " + isLoggedIn);
        // output: LoggedIn: True

        Console.WriteLine("Admin: " + isAdmin);
        // output: Admin: False

        bool result = (10 < 20);
        // 10 < 20 is true

        Console.WriteLine("10 < 20 = " + result);
        // output: 10 < 20 = True

        // int -> double (implicit widening)
        int numInt = 50;
        double numDouble = numInt;
        Console.WriteLine("int -> double: " + numDouble);
        // output: int -> double: 50

        // double -> int (explicit narrowing)
        double dd = 50.55;
        int i = (int)dd;
        Console.WriteLine("double -> int: " + i);
        // output: double -> int: 50

        // int -> String
        int num = 100;
        string strFromInt = num.ToString();
        Console.WriteLine("int -> String: " + strFromInt);
        // output: int -> String: 100

        // String -> int
        string str = "200";
        int intFromStr = int.Parse(str);
        Console.WriteLine("String -> int: " + intFromStr);
        // output: String -> int: 200

        // double -> String
        double price = 99.99;
        string strFromDouble = price.ToString();
        Console.WriteLine("double -> String: " + strFromDouble);
        // output: double -> String: 99.99

        // JAVA Numbers Prefix
        // +----------------------+----------+------------------+----------+--------------------------+
        // | NUMBER SYSTEM        | JAVA     | JAVA EXAMPLE     | C#       | C# EXAMPLE               |
        // +----------------------+----------+------------------+----------+--------------------------+
        // | Decimal              | None     | 255              | None     | 255                      |
        // | Binary               | 0b / 0B  | 0b11111111       | 0b / 0B  | 0b11111111               |
        // | Octal                | 0        | 0377             | —        | Convert.ToString(255, 8) |
        // | Hexadecimal          | 0x / 0X  | 0xFF             | 0x / 0X  | 0xFF                     |
        // +----------------------+----------+------------------+----------+--------------------------+

        //Java Octal
        //var num = 0100;
        //OUTPUT: 64

        // IMPLICIT conversion: C# converts automatically.
        // EXPLICIT conversion: requires a cast.

        // String -> double
        string strDouble = "123.45";
        double doubleFromStr = double.Parse(strDouble);
        Console.WriteLine("String -> double: " + doubleFromStr);
        // output: String -> double: 123.45


        // char -> int (ASCII conversion)
        char ch = 'A';
        int ascii = ch;
        Console.WriteLine("char -> int (ASCII): " + ascii);
        // output: char -> int (ASCII): 65

        // int -> char
        int asciiValue = 66;
        char charFromInt = (char)asciiValue;
        Console.WriteLine("int -> char: " + charFromInt);
        // output: int -> char: B

        // long -> int (explicit narrowing)
        long largeNum = 100000L;
        int fromLong = (int)largeNum;
        Console.WriteLine("long -> int: " + fromLong);
        // output: long -> int: 100000


        // int -> long (implicit widening)
        int small = 25;
        long longValue = small;
        Console.WriteLine("int -> long: " + longValue);
        // output: int -> long: 25


        // IMPLICIT conversion: C# converts automatically.
        // float -> double (implicit)
        float ff = 10.5f;
        double fromFloat = ff;
        Console.WriteLine("float -> double: " + fromFloat);
        // output: float -> double: 10.5


        // double -> float (explicit)
        double d12 = 55.99;
        float fromDoubleToFloat = (float)d12;
        Console.WriteLine("double -> float: " + fromDoubleToFloat);
        // output: double -> float: 55.99



        // =========================
        // STRING CLASS FUNCTIONS
        // =========================

        string s = "Hello World";

        Console.WriteLine(s.Length);
        // OUTPUT: 11

        Console.WriteLine(s[0]);
        // OUTPUT: H

        Console.WriteLine(s.ToUpper());
        // OUTPUT: HELLO WORLD

        Console.WriteLine(s.ToLower());
        // OUTPUT: hello world

        Console.WriteLine(s.Substring(0, 5));
        // OUTPUT: Hello

        Console.WriteLine(s.Contains("World"));
        // OUTPUT: True

        Console.WriteLine(s.IndexOf("o"));
        // OUTPUT: 4

        Console.WriteLine(s.LastIndexOf("o"));
        // OUTPUT: 7

        Console.WriteLine(s.Replace("World", "Java"));
        // OUTPUT: Hello Java

        Console.WriteLine(s.StartsWith("He"));
        // OUTPUT: True

        Console.WriteLine(s.EndsWith("ld"));
        // OUTPUT: True

        Console.WriteLine("   trim test   ".Trim());
        // OUTPUT: "trim test"

        string[] arr0 = s.Split(' ');

        Console.WriteLine(arr0[0]);
        // OUTPUT: Hello

        Console.WriteLine(Convert.ToString(100));
        // OUTPUT: "100"

        Console.WriteLine(new string('A', 5));
        // OUTPUT: AAAAA


        // =========================
        // STRING COMPARISON
        // =========================

        string a = "Java";
        string b = "java";

        Console.WriteLine(a == b);
        // OUTPUT: False

        Console.WriteLine(
            string.Equals(
                a,
                b,
                StringComparison.OrdinalIgnoreCase));
        // OUTPUT: True

        Console.WriteLine(
            string.Compare(
                a,
                b,
                StringComparison.Ordinal));
        // OUTPUT: negative number (case difference)

        //Interpolation + @ multi-line text -> $@""
        string language = "C#", dotnet = ".NET";
        int version = 14;
        Console.Write($@"{language} latest version is {version}.
            Its widely used for
                enterprise application development using {dotnet}.");

        Console.WriteLine("=== BASIC STRING REPLACE & REGEX DEMO ===");
        // output: === BASIC STRING REPLACE & REGEX DEMO ===

        string text1 = "one two three two one";

        // replaceAll
        string replaceAll = text1.Replace("two", "FIVE");

        Console.WriteLine("replaceAll: " + replaceAll);

        // output:
        // replaceAll: one FIVE three FIVE one


        // =========================
        // STRING BUILDER (PERFORMANCE)
        // =========================
        //System.Text Namespace having StringBuilder
        //String is Immutable = Unchangeable ~ Cannot change existing object
        //StringBuilder is Mutable = Changeable ~ Can change Existing object content

        StringBuilder sb = new StringBuilder("Hi");
        sb.Append(" Java");
        Console.WriteLine(sb.ToString());
        // OUTPUT: Hi Java
        sb.Insert(0, "Hello ");
        Console.WriteLine(sb.ToString());
        // OUTPUT: Hello Hi Java

        /////////sb.Reverse();

        Console.WriteLine(sb.ToString());
        // OUTPUT: reversed string


        string one = "Hello";
        string two = " World";
        string three = "of C#";
        string four = new StringBuilder(one)
            .Append(two)
            .Append(three)
            .ToString();
        Console.WriteLine("Final: " + four);
        // output: Final: Hello World of C#


        // ---------------- ARRAYS ----------------

        int[] arr1 = { 10, 20, 30 };

        Console.WriteLine(
            "[" + string.Join(", ", arr1) + "]");

        // OUTPUT: [10, 20, 30]


        // ---------------- NULL ----------------

        string empty = null;

        Console.WriteLine(empty);
        // OUTPUT: null


        // ---------------- UNDEFINED ----------------

        // C# local variables must be definitely assigned
        // before they are used.
        //
        // Example:
        //
        // string undefined;
        // Console.WriteLine(undefined);
        //
        // This will produce a compile-time error.


        // +-------------------+----------------------------+--------------------------------+
        // | OPERATION         | JAVA                       | C#                             |
        // +-------------------+----------------------------+--------------------------------+
        // | Literal Replace   | text.replace("a", "@")     | text.Replace("a", "@")         |
        // | Regex Replace All | text.replaceAll("a", "@")  | Regex.Replace(text,"a","@")    |
        // | Regex First       | text.replaceFirst("a","@") | Regex.Replace(text,"a","@",1,0)|
        // +-------------------+----------------------------+--------------------------------+
        //
        // "." Example:
        //
        // Java:
        // text.replace(".", "@")       → a@b@c   // Literal dot
        // text.replaceAll(".", "@")    → @@@@@   // "." = any character
        //
        // C#:
        // text.Replace(".", "@")       → a@b@c   // Literal dot
        // Regex.Replace(text, ".", "@") → @@@@@   // "." = any character
        //
        // KEY:
        // replace()      → Literal
        // replaceAll()   → Regex, all matches
        // replaceFirst() → Regex, first match

        // replace (char replace)
        string text2 = "a123abc";

        string replacedChar =
            text2.Replace('a', '@');

        Console.WriteLine(
            "replace char: " + replacedChar);
        // output: replace char: 123@bc

        Console.WriteLine("=== STRING CONVERSIONS ===");

        // int -> String
        int num12 = 100;
        string s1 = num12.ToString();
        Console.WriteLine(
            "int -> String: " + s1);
        // output: int -> String: 100

        // String -> int
        string s2 = "200";
        int num2 = int.Parse(s2);
        Console.WriteLine("String -> int: " + num2);
        // output: String -> int: 200


        // double -> String
        double d2 = 99.99;
        string s3 = d2.ToString();
        Console.WriteLine("double -> String: " + s3);
        // output: double -> String: 99.99


        // String -> double
        string s4 = "123.45";
        double d22 = double.Parse(s4);
        Console.WriteLine("String -> double: " + d22);
        // output: String -> double: 123.45

        Console.WriteLine("=== EXTRA USEFUL STRING OPERATIONS ===");

        // toUpperCase / toLowerCase
        string caseText = "C# String API";
        Console.WriteLine("upper: " + caseText.ToUpper());
        // output: upper: C# STRING API

        Console.WriteLine("lower: " + caseText.ToLower());
        // output: lower: c# string api


        // trim
        string spaced = "   C#/Java is awesome   ";
        Console.WriteLine("trim: '" + spaced.Trim() + "'");
        // output: trim: 'C#/Java is awesome'


        // substring
        string sub = "HelloWorld".Substring(0, 5);
        Console.WriteLine("substring: " + sub);
        // output: substring: Hello


        // split
        string fruits = "apple,banana,grapes";
        string[] arr = fruits.Split(',');
        Console.WriteLine("split:");
        // output: split:

        foreach (string f in arr)
        {
            Console.WriteLine(" - " + f);
        }

        // output:
        //  - apple
        //  - banana
        //  - grapes


        // charAt
        string word = "Jenkov";
        Console.WriteLine("charAt(0): " + word[0]);
        // output:
        // charAt(0): J


        // indexOf
        Console.WriteLine("indexOf 'k': " + word.IndexOf("k"));
        // output: indexOf 'k': 3


        // ---------------- BIGDECIMAL ----------------

        // Java BigDecimal has arbitrary precision.
        // C# decimal has fixed precision (28-29 significant digits).
        // decimal is the closest built-in C# equivalent.

        decimal bigd1 = decimal.Parse("12345678901234567890.123456789");

        decimal bigd2 = decimal.Parse("12345678901234567890.123456789");

        Console.WriteLine(
            "B1={0}, B2={1}",
            bigd1,
            bigd2);

        // OUTPUT:
        // B1=12345678901234567890.123456789,
        // B2=12345678901234567890.123456789


        // BIGINTEGER EXAMPLES

        BigInteger value1 = new BigInteger(10);
        BigInteger value2 = new BigInteger(5);

        Console.WriteLine(value1);
        // OUTPUT: 10

        Console.WriteLine(value2);
        // OUTPUT: 5

        Console.WriteLine(value1 + value2);
        // OUTPUT: 15

        Console.WriteLine(value1 - value2);
        // OUTPUT: 5

        Console.WriteLine(value1 * value2);
        // OUTPUT: 50

        Console.WriteLine(value1 / value2);
        // OUTPUT: 2

        Console.WriteLine(value1 % value2);
        // OUTPUT: 0

        Console.WriteLine(BigInteger.Pow(value1, 2));
        // OUTPUT: 100

        Console.WriteLine(value1.CompareTo(value2));
        // OUTPUT: 1 (10 > 5)

        Console.WriteLine(
            value1 > value2 ? value1 : value2);
        // OUTPUT: 10

        Console.WriteLine(
            value1 < value2 ? value1 : value2);
        // OUTPUT: 5


        // BIGDECIMAL EXAMPLES

        decimal _d1 = decimal.Parse("10.5");
        decimal _d2 = decimal.Parse("2.0");

        Console.WriteLine(_d1);
        // OUTPUT: 10.5

        Console.WriteLine(_d2);
        // OUTPUT: 2.0

        Console.WriteLine(_d1 + _d2);
        // OUTPUT: 12.5

        Console.WriteLine(_d1 - _d2);
        // OUTPUT: 8.5

        Console.WriteLine(_d1 * _d2);
        // OUTPUT: 21.00

        Console.WriteLine(
            decimal.Round(
                _d1 / _d2,
                2,
                MidpointRounding.AwayFromZero));
        // OUTPUT: 5.25

        Console.WriteLine(decimal.Compare(_d1, _d2));
        // OUTPUT: 1 (10.5 > 2.0)

        decimal val = decimal.Parse("10.56789");

        Console.WriteLine(
            decimal.Round(
                val,
                2,
                MidpointRounding.AwayFromZero));
        // OUTPUT: 10.57

        Console.WriteLine(decimal.ToInt32(_d1));
        // OUTPUT: 10

        Console.WriteLine((double)_d1);
        // OUTPUT: 10.5


        // =========================
        // INTEGER CLASS FUNCTIONS
        // =========================

        Console.WriteLine(int.Parse("123"));
        // OUTPUT: 123

        Console.WriteLine(int.Parse("456"));
        // OUTPUT: 456

        Console.WriteLine(789.ToString());
        // OUTPUT: "789"

        Console.WriteLine(
            Convert.ToInt32("1010", 2));
        // OUTPUT: 10 (binary to decimal)

        Console.WriteLine(
            Convert.ToString(10, 2));
        // OUTPUT: 1010

        Console.WriteLine(
            Convert.ToString(255, 16));
        // OUTPUT: ff

        Console.WriteLine(
            Convert.ToString(64, 8));
        // OUTPUT: 100


        // Bit Count
        Console.WriteLine(
            BitCount(7));
        // OUTPUT: 3 (111 has 3 bits)


        // REMAINDER (MODULO)

        BigInteger value21 = new BigInteger(6);

        BigInteger remainder = value1 % value21;

        Console.WriteLine(remainder);
        // OUTPUT: 4 (10 % 6 = 4)


        // GCD (GREATEST COMMON DIVISOR)

        BigInteger num_1 = new BigInteger(12);
        BigInteger num_2 = new BigInteger(18);

        Console.WriteLine(
            BigInteger.GreatestCommonDivisor(num_1, num_2));
        // OUTPUT: 6


        BigInteger ba = new BigInteger(10);  // 1010 (binary)
        BigInteger bb = new BigInteger(6);   // 0110 (binary)


        // =========================
        // BITWISE AND
        // =========================

        Console.WriteLine(ba & bb);
        // OUTPUT: 2 (1010 & 0110 = 0010)


        // =========================
        // BITWISE OR
        // =========================

        Console.WriteLine(ba | bb);
        // OUTPUT: 14 (1010 | 0110 = 1110)


        // =========================
        // BITWISE XOR
        // =========================

        Console.WriteLine(ba ^ bb);
        // OUTPUT: 12 (1010 ^ 0110 = 1100)


        // =========================
        // BITWISE NOT
        // =========================

        Console.WriteLine(~ba);
        // OUTPUT: -11 (two's complement result)


        // =========================
        // AND NOT (a & ~b)
        // =========================

        Console.WriteLine(ba & ~bb);
        // OUTPUT: 8 (1010 & ~0110 = 1000)


        // =========================
        // SHIFT LEFT
        // =========================

        Console.WriteLine(ba << 2);
        // OUTPUT: 40 (10 << 2 = 40)


        // =========================
        // SHIFT RIGHT
        // =========================

        Console.WriteLine(ba >> 1);
        // OUTPUT: 5 (10 >> 1 = 5)



        // ---------------- BOOLEAN OPERATIONS ----------------

        Console.WriteLine(10 > 5 && 5 < 8);
        // OUTPUT: True

        Console.WriteLine(10 > 5 || 5 > 8);
        // OUTPUT: True

        Console.WriteLine(5 == 5);
        // OUTPUT: True

        Console.WriteLine(5 != 10);
        // OUTPUT: True

        Console.WriteLine(10 < 20);
        // OUTPUT: True

        Console.WriteLine(15 >= 15);
        // OUTPUT: True


        // ---------------- STRING CONCAT ----------------

        int a1 = 3, b1 = 11;

        Console.WriteLine(
            "Number#1 = " + a1 +
            " and Number#2 = " + b1);
        // OUTPUT: Number#1 = 3 and Number#2 = 11

        Console.WriteLine(
            "Number#1 = {0} and Number#2 = {1}",
            a1,
            b1);
        // OUTPUT: Number#1 = 3 and Number#2 = 11

        int _id = 101;
        string _name = "Ali";
        double _salary = 50000.75;
        Console.WriteLine(
            "Id={0} Name={1} Salary={2:F2}",
            _id,
            _name,
            _salary);
        // OUTPUT: Id=101 Name=Ali Salary=50000.75

        // ---------------- ERROR / WARN ----------------

        Console.Error.WriteLine("Error message");
        // OUTPUT (stderr): Error message

        Console.WriteLine("Warning message (no native warn type in C#)");
        // OUTPUT: Warning message (no native warn type in C#)


        // ---------------- TABLE OUTPUT (ALTERNATIVE) ----------------

        Console.WriteLine("Name\tAge");
        // OUTPUT: Name    Age

        Console.WriteLine("Ali\t25");
        // OUTPUT: Ali     25

        Console.WriteLine("Ahmed\t30");
        // OUTPUT: Ahmed   30


        // ---------------- INPUT (PROMPT EQUIVALENT) ----------------

        // C# Console input example:
        //
        // Console.Write("Enter name: ");
        // string name = Console.ReadLine();
        // Console.WriteLine("Hello " + name);


        // ---------------- NUMBER (PRIMITIVE vs OBJECT) ----------------

        int numPrim1 = 100;

        int numObj1 = 100;

        Console.WriteLine(numPrim1 + " -> primitive int (recommended)");
        // OUTPUT: 100 -> primitive int (recommended)

        Console.WriteLine(numObj1 + " -> Integer object");
        // OUTPUT: 100 -> Integer object


        // ---------------- STRING ----------------

        string strPrim1 = "Ali";

        string strObj1 = new string("Ali");

        Console.WriteLine(strPrim1 + " -> String literal (recommended)");

        // OUTPUT: Ali -> String literal (recommended)

        Console.WriteLine(
            strObj1 +
            " -> String object");

        // OUTPUT: Ali -> String object


        // ---------------- BOOLEAN ----------------

        bool flag = true;

        bool flagObj = true;

        Console.WriteLine(
            flag +
            " -> primitive boolean (recommended)");
        // OUTPUT: True -> primitive boolean (recommended)

        Console.WriteLine(
            flagObj +
            " -> Boolean object");
        // OUTPUT: True -> Boolean object
    }


    // Chapter 03: OPERATORS & EXPRESSIONS and MATH LIBRARY
    public static void chapter03()
    {
        // ================================================================
        // JAVA → C# OPERATORS & EXPRESSIONS
        // ================================================================

        // +----------------------+-----------------------------+----------------------------+
        // | CATEGORY             | JAVA                        | C#                         |
        // +----------------------+------------------------------+---------------------------+
        // | Arithmetic           | +  -  *  /  %               | +  -  *  /  %              |
        // | Assignment           | =  +=  -=  *=  /=  %=       | =  +=  -=  *=  /=  %=      |
        // | Increment/Decrement  | ++  --                      | ++  --                     |
        // | Comparison           | ==  !=  >  <  >=  <=        | ==  !=  >  <  >=  <=       |
        // | Logical              | &&  ||  !                   | &&  ||  !                  |
        // | Bitwise              | &  |  ^  ~                  | &  |  ^  ~                 |
        // | Shift                | <<  >>  >>>                 | <<  >>  >>>                |
        // | Conditional          | ?:                          | ?:                         |
        // | Null                 | null                        | null                       |
        // | Type Check           | instanceof                  | is                         |
        // | Type Cast            | (int)value                  | (int)value                 |
        // | Null-safe Access     | No direct equivalent        | ?.  ??  ??=                |
        // | Pattern Matching     | Limited                     | is pattern matching        |
        // +----------------------+-----------------------------+----------------------------+

        // ---------------- SUM OF NUMBERS ----------------
        int number_1 = 5, number_2 = 10;
        int sum_numbers = number_1 + number_2;
        Console.WriteLine("Sum of " + number_1 + ", " + number_2 + " is: " + sum_numbers);
        // OUTPUT: Sum of 5, 10 is: 15


        // ---------------- ADD & ASSIGN ----------------
        int num_51 = 51;
        num_51 = num_51 + 9;
        num_51 += 9;
        Console.WriteLine("Final Value after additions = " + num_51);
        // OUTPUT: Final Value after additions = 69


        // ---------------- STRING CONCAT NUMBERS ----------------
        string strNumber_1 = "5", strNumber_2 = "10", strNumber_3 = "15";
        string concat_numbers = strNumber_1 + strNumber_2 + strNumber_3;
        Console.WriteLine("Concatenationof " + strNumber_1 + ", " + strNumber_2 + " and " + strNumber_3 + " is: " + concat_numbers);
        // OUTPUT: Concatenationof 5, 10 and 15 is: 51015


        // ---------------- DIFFERENCE ----------------
        double number01 = 15.08, number02 = 10.11;
        double difference_numbers = number01 - number02;
        Console.WriteLine("Difference of " + number01 + " and " + number02 + " is: " + difference_numbers);
        // OUTPUT: Difference of 15.08 and 10.11 is: 4.969999999999999
        // NOTE: Due to floating-point precision, C# may display 4.969999999999999.
        //       The mathematical result is approximately 4.97.


        // STRING TO NUMBER SUBTRACTION (JAVA EXPLICIT CONVERSION)
        string strNumber01 = "15.08";
        double nNumber02 = 10.11;
        double str_num_diff = double.Parse(strNumber01) - nNumber02;
        Console.WriteLine("Difference is: " + str_num_diff);
        // OUTPUT: Difference is: 4.969999999999999
        // NOTE: The mathematical result is approximately 4.97.


        // ---------------- SUBTRACT & ASSIGN ----------------
        int num_57 = 57, num_7 = 7;
        num_57 = num_57 - num_7;
        num_57 -= num_7;
        Console.WriteLine("Final Value after subtraction= " + num_57);
        // OUTPUT: Final Value after subtraction= 43


        // ---------------- MULTIPLICATION ----------------
        // Java variable names containing '$' are legal.
        // C# identifiers cannot contain '$', so number$1/number$2
        // are converted to number_1_mul/number_2_mul.
        double number_1_mul = 5.25, number_2_mul = 11;
        double product_numbers = number_1_mul * number_2_mul;
        Console.WriteLine("Product = " + product_numbers);
        // OUTPUT: Product = 57.75


        // ---------------- MULTIPLY & ASSIGN ----------------
        int num_10 = 10;
        num_10 = num_10 * 5;
        num_10 *= 5;
        Console.WriteLine("Final Value after multiplication= " + num_10);
        // OUTPUT: Final Value after multiplication= 250


        // ---------------- STRING MULTIPLY ----------------
        double strNum01 = 5.8, strNum02 = 2.9;
        double str_num_multiply = strNum01 * strNum02;
        Console.WriteLine("Multiplication= " + str_num_multiply);
        // OUTPUT: Multiplication= 16.82


        // ---------------- DIVISION ----------------
        int number1 = 99, number2 = 11;
        int division_numbers = number1 / number2;
        Console.WriteLine("Division = " + division_numbers);
        // OUTPUT: Division = 9


        // STRING DIVISION (EXPLICIT)
        int strNum1 = int.Parse("20");
        int strNum2 = int.Parse("5");
        int str_num_division = strNum1 / strNum2;
        Console.WriteLine("Division = " + str_num_division);
        // OUTPUT: Division = 4


        // ---------------- DIVIDE & ASSIGN ----------------
        int num_99 = 99, num_9 = 9;
        num_99 = num_99 / num_9;
        num_99 /= num_9;
        Console.WriteLine("Final Division Value = " + num_99);
        // OUTPUT: Final Division Value = 1


        // ---------------- REMAINDER ----------------
        int r1 = 99, r2 = 11;
        int remainder = r1 % r2;
        Console.WriteLine("Remainder = " + remainder);
        // OUTPUT: Remainder = 0


        // ---------------- POWER ----------------
        int baseNumber = 2;
        double power = Math.Pow(baseNumber, 16);
        Console.WriteLine("Power = " + power);
        // OUTPUT: Power = 65536


        // ---------------- INCREMENT / DECREMENT ----------------
        int N = 9;

        Console.WriteLine(N++);
        // OUTPUT: 9

        Console.WriteLine(N);
        // OUTPUT: 10

        int X = 9;

        Console.WriteLine(++X);
        // OUTPUT: 10


        // ---------------- BITWISE ----------------
        Console.WriteLine("Bitwise AND: " + (15 & 5));
        // OUTPUT: Bitwise AND: 5

        Console.WriteLine("Bitwise OR: " + (15 | 3));
        // OUTPUT: Bitwise OR: 15

        Console.WriteLine("Even/Odd check: " + (12 & 1));
        // OUTPUT: Even/Odd check: 0

        Console.WriteLine("Even/Odd check: " + (7 & 1));
        // OUTPUT: Even/Odd check: 1


        // ---------------- SHIFT OPERATORS ----------------
        Console.WriteLine(5 << 1);
        // OUTPUT: 10

        Console.WriteLine(3 << 2);
        // OUTPUT: 12

        Console.WriteLine(20 >> 1);
        // OUTPUT: 10

        Console.WriteLine(20 >> 2);
        // OUTPUT: 5


        /* CHAPTER 4: MATH LIBRARY */

        Console.WriteLine(Math.PI);
        // OUTPUT: 3.141592653589793

        //Euler's Constant - Base of Natural Log
        Console.WriteLine(Math.E);
        // OUTPUT: 2.718281828459045

        Console.WriteLine(Math.Sqrt(25));
        // OUTPUT: 5

        // Java Math.cbrt(27)
        // C# equivalent using Math.Pow()
        Console.WriteLine(Math.Pow(27, 1.0 / 3.0));
        // OUTPUT: approximately 3
        // NOTE: Floating-point calculation may display 3 or 3.0000000000000004.

        Console.WriteLine(Math.Pow(3, 3));
        // OUTPUT: 27


        // CONSTANTS

        // Java:
        // System.out.println(Math.LN2);
        // System.out.println(Math.LN10);

        // C# does not have Math.LN2 / Math.LN10 constants.
        // Equivalent:
        Console.WriteLine(Math.Log(2));
        // OUTPUT: 0.6931471805599453

        Console.WriteLine(Math.Log(10));
        // OUTPUT: 2.302585092994046

        Console.WriteLine(Math.Log10(Math.E));
        // OUTPUT: 0.43429448190325176


        // AREA OF CIRCLE

        double r = 7;
        double area = Math.PI * r * r;
        Console.WriteLine("Circle Area = " + area);
        // OUTPUT: Circle Area = 153.93804002589985


        // SIMPLE INTEREST

        double P = 1000, R = 5, T = 2;
        double SI = (P * R * T) / 100;
        Console.WriteLine("Simple Interest = " + SI);
        // OUTPUT: Simple Interest = 100

        //==============================================================
        //JAVA → C# TRIGONOMETRY QUICK GRID
        //==============================================================
        /*
            // +----------+---------+-----------+-----------+-----------+
            // | RADIANS  | DEGREES | sin()     | cos()     | tan()     |
            // +----------+---------+-----------+-----------+-----------+
            // | π / 6    | 30°     | 0.5       | 0.8660    | 0.5774    |
            // | π / 4    | 45°     | 0.7071    | 0.7071    | 1.0000    |
            // | π / 3    | 60°     | 0.8660    | 0.5       | 1.7321    |
            // | π / 2    | 90°     | 1.0       | 0.0       | Undefined |
            // +----------+---------+-----------+-----------+-----------+
        */

        // TRIGONOMETRIC Ratios

        double angle = Math.PI / 6;
        Console.WriteLine("Sin = " + Math.Sin(angle));
        // OUTPUT: Sin = 0.49999999999999994

        Console.WriteLine("Cos = " + Math.Cos(angle));
        // OUTPUT: Cos = 0.8660254037844387


        /*********** Java equivalent **************
         *
         * double pi = Math.sqrt(12) *
         *     IntStream.rangeClosed(0, 100)
         *     .mapToDouble(k ->
         *         Math.pow(-3, -1 * k) / (2 * k + 1))
         *     .sum();
         *
         * System.out.println(Math.PI);
         * System.out.println(pi);
         *
         */

        //C# Code
        double pi =
            Math.Sqrt(12) *
            Enumerable.Range(0, 101)
                .Select(k =>
                    Math.Pow(-3, -1 * k) /
                    (2 * k + 1))
                .Sum();

        // pi is calculated using Madhava-Leibniz type series approximation
        // output will be very close to Math.PI
        Console.WriteLine(pi);
        // output: approx 3.141592653589793 (computed approximation)


        Console.WriteLine(
            Math.Sign(10 - 20));
        // OUTPUT: -1



        // =========================================================
        // 1. BASIC JAVA MATH OPERATORS
        // =========================================================

        int a = 20, b = 6;

        int add = a + b;        // Addition
        int sub = a - b;        // Subtraction
        int mul = a * b;        // Multiplication
        int div = a / b;        // Division (integer)
        int mod = a % b;        // Remainder (modulus)

        Console.WriteLine("===== Arithmetic Operators =====");
        // OUTPUT: ===== Arithmetic Operators =====

        Console.WriteLine("Add      : " + add);
        // OUTPUT: Add      : 26

        Console.WriteLine("Subtract : " + sub);
        // OUTPUT: Subtract : 14

        Console.WriteLine("Multiply : " + mul);
        // OUTPUT: Multiply : 120

        Console.WriteLine("Divide   : " + div);
        // OUTPUT: Divide   : 3

        Console.WriteLine("Modulus  : " + mod);
        // OUTPUT: Modulus  : 2


        // Operator precedence example

        int precedence = 10 + 20 * 3;
        // * executes before +

        Console.WriteLine(
            "\nPrecedence Result (10 + 20 * 3): " +
            precedence);

        // OUTPUT: Precedence Result (10 + 20 * 3): 70


        int precedence2 = (10 + 20) * 3;
        // parentheses override precedence

        Console.WriteLine(
            "With Parentheses ((10 + 20) * 3): " +
            precedence2);

        // OUTPUT: With Parentheses ((10 + 20) * 3): 90


        // =========================================================
        // 2. JAVA Math CLASS - BASIC FUNCTIONS
        // =========================================================

        Console.WriteLine(
            "\n===== Math Class Basic Functions =====");

        // OUTPUT:
        // ===== Math Class Basic Functions =====

        Console.WriteLine(
            "Absolute (-15): " + Math.Abs(-15));
        // OUTPUT: Absolute (-15): 15

        Console.WriteLine(
            "Max (10, 25): " + Math.Max(10, 25));
        // OUTPUT: Max (10, 25): 25

        Console.WriteLine(
            "Min (10, 25): " + Math.Min(10, 25));
        // OUTPUT: Min (10, 25): 10

        Console.WriteLine(
            "Ceil (7.2): " + Math.Ceiling(7.2));
        // OUTPUT: Ceil (7.2): 8

        Console.WriteLine(
            "Floor (7.8): " + Math.Floor(7.8));
        // OUTPUT: Floor (7.8): 7

        // Java Math.round(7.5) returns 8.
        // C# Math.Round(7.5) normally uses banker's rounding,
        // therefore MidpointRounding.AwayFromZero is used
        // to match Java Math.round() behavior for positive values.

        Console.WriteLine(
            "Round (7.5): " +
            Math.Round(7.5, MidpointRounding.AwayFromZero));

        // OUTPUT: Round (7.5): 8

        Console.WriteLine(
            "Random (0-1): " + Random.Shared.NextDouble());

        // OUTPUT:
        // Random (0-1): <random value between 0 and 1>


        // =========================================================
        // 3. EXPONENTIAL & POWER FUNCTIONS
        // =========================================================

        // ================================================================
        // JAVA → C# LOGARITHM METHODS
        // ================================================================

        // +------------------------------+-------------+-------------------------------------+-----------------------+
        // | C# METHOD                    | BASE        | TYPE                                | MATHEMATICAL FORM     |
        // +------------------------------+-------------+-------------------------------------+-----------------------+
        // | Math.Log(x)                  | e ≈ 2.71828 | Natural Log                         | ln(x)                 |
        // | Math.Log10(x)                | 10          | Common Log                          | log₁₀(x)              |
        // | Math.Log(x) / Math.Log(10)   | 10          | Common Log using Natural Log        | log₁₀(x)              |
        // +------------------------------+----------+----------------------------------------+-----------------------+

        Console.WriteLine("\n===== Exponential & Power =====");

        // OUTPUT:
        // ===== Exponential & Power =====

        Console.WriteLine("Exp(1): " + Math.Exp(1));
        // OUTPUT: Exp(1): 2.718281828459045 -
        // //Returns 'e' rainsed to the power Euler's number 

        Console.WriteLine("Log(10): " + Math.Log(10));
        // OUTPUT: Log(10): 2.302585092994046

        Console.WriteLine("Log10(100): " + Math.Log10(100));
        // OUTPUT: Log10(100): 2

        Console.WriteLine("Pow(2, 3): " + Math.Pow(2, 3));
        // OUTPUT: Pow(2, 3): 8

        Console.WriteLine("Sqrt(25): " + Math.Sqrt(25));
        // OUTPUT: Sqrt(25): 5

        Console.WriteLine("Sqrt(27): " + Math.Cbrt(27));
        // OUTPUT: Cbrt(25): 3

        // =========================================================
        // 4. TRIGONOMETRIC FUNCTIONS
        // =========================================================

        Console.WriteLine(
            "\n===== Trigonometry =====");

        // OUTPUT:
        // ===== Trigonometry =====

        //double ang = Math.PI / 6;//means 30 degree
        double ang = Math.PI / 2;//means 45 degree
        //double ang = Math.PI / 3;//means 60 degree

        Console.WriteLine("PI: " + Math.PI);
        // OUTPUT: PI: 3.141592653589793

        double toRadians = Math.PI / 180;
        double radians30 = 30 * toRadians;
        double result30 = Math.Sin(radians30);
        Console.WriteLine(result30);  // 0.5

        Console.WriteLine("Sin(PI/2): " + Math.Sin(ang));
        // OUTPUT: Sin(PI/2): 1

        Console.WriteLine("Cos(PI/2): " + Math.Cos(ang));
        // OUTPUT: Cos(PI/2): 6.123233995736766E-17
        // NOTE: Mathematically this is 0, but floating-point
        // calculation produces a very small value close to zero.

        Console.WriteLine("Tan(PI/2): " + Math.Tan(ang));
        // OUTPUT: Tan(PI/2): 16331239353195370
        // NOTE: Mathematically tan(PI/2) is undefined.
        // Floating-point calculation produces a very large number.

        Console.WriteLine("Degrees (PI): " + (180.0 / Math.PI * Math.PI));

        // OUTPUT: Degrees (PI): 180
        // C# equivalent of Java Math.toDegrees(Math.PI)

        Console.WriteLine(
            "Radians (180): " +
            (180 * Math.PI / 180));

        // OUTPUT: Radians (180): 3.141592653589793
        // C# equivalent of Java Math.toRadians(180)


        // =========================================================
        // 5. FLOOR DIV EXAMPLE (IMPORTANT DIFFERENCE)
        // =========================================================

        Console.WriteLine(
            "\n===== Floor Division =====");

        // Java:
        // Math.floorDiv(-100, 9)
        //
        // C# integer division truncates toward zero:
        // -100 / 9 = -11
        //
        // Java floorDiv returns -12 because it rounds toward
        // negative infinity.
        //
        // C# equivalent:

        int floorDiv = (int)Math.Floor(-100.0 / 9.0);

        Console.WriteLine(
            "Math.floorDiv(-100, 9): " + floorDiv);

        // OUTPUT: Math.floorDiv(-100, 9): -12


        Console.WriteLine(
            "Normal division (-100/9): " +
            (-100 / 9));

        // OUTPUT: Normal division (-100/9): -11


        // =========================================================
        // 6. COMBINED REAL-LIFE STYLE CALCULATION
        // =========================================================

        Console.WriteLine(
            "\n===== Combined Example =====");

        // OUTPUT:
        // ===== Combined Example =====

        double price = 99.99;
        double taxRate = 0.17;

        double tax = price * taxRate;
        double total = price + tax;

        Console.WriteLine("Price     : " + price);
        // OUTPUT: Price     : 99.99

        Console.WriteLine("Tax       : " + tax);
        // OUTPUT: Tax       : 16.9983

        Console.WriteLine("Total Bill: " + total);
        // OUTPUT: Total Bill: 116.9883


        // Rounding final bill

        // Java Math.round(total) rounds to nearest long.
        // C# equivalent for positive numbers:
        Console.WriteLine("Rounded Total: " + Math.Round(total, MidpointRounding.AwayFromZero));
        // OUTPUT: Rounded Total: 117


        // TEMPERATURE CONVERSION
        double celsius = 25;
        double fahrenheit = (celsius * 9 / 5) + 32;
        Console.WriteLine("Fahrenheit = " + fahrenheit);

        // OUTPUT: Fahrenheit = 77
    }


    // Chapter 04: NUMBER vs BIGINTEGER
    public static void chapter04()
    {
        // ================================================================
        // JAVA → C# BIGINTEGER QUICK GRID
        // ================================================================

        // +----------------+--------------------------------+--------------------------------+
        // | OPERATION      | JAVA                           | C#                             |
        // +----------------+--------------------------------+--------------------------------+
        // | Type           | BigInteger                     | BigInteger                     |
        // | Namespace      | java.math                      | System.Numerics                |
        // | Import         | import java.math.BigInteger;   | using System.Numerics;         |
        // | Create         | new BigInteger("123")          | BigInteger.Parse("123")        |
        // | Add            | a.add(b)                       | a + b                          |
        // | Subtract       | a.subtract(b)                  | a - b                          |
        // | Multiply       | a.multiply(b)                  | a * b                          |
        // | Divide         | a.divide(b)                    | a / b                          |
        // | Remainder      | a.remainder(b)                 | a % b                          |
        // | Power          | a.pow(5)                       | BigInteger.Pow(a, 5)           |
        // | GCD            | a.gcd(b)                       | BigInteger.GreatestCommonDivisor(a, b) |
        // | Absolute       | a.abs()                        | BigInteger.Abs(a)              |
        // | Maximum        | a.max(b)                       | BigInteger.Max(a, b)           |
        // | Minimum        | a.min(b)                       | BigInteger.Min(a, b)           |
        // | Compare        | a.compareTo(b)                 | a.CompareTo(b)                 |
        // | Equal          | a.equals(b)                    | a == b                         |
        // | Zero           | BigInteger.ZERO                | BigInteger.Zero                |
        // | One            | BigInteger.ONE                 | BigInteger.One                 |
        // | String         | a.toString()                   | a.ToString()                   |
        // | Negate         | a.negate()                     | -a                             |
        // | Bitwise AND    | a.and(b)                       | a & b                          |
        // | Bitwise OR     | a.or(b)                        | a | b                          |
        // | Bitwise XOR    | a.xor(b)                       | a ^ b                          |
        // | Bitwise NOT    | a.not()                        | ~a                             |
        // | Shift Left     | a.shiftLeft(2)                 | a << 2                         |
        // | Shift Right    | a.shiftRight(2)                | a >> 2                         |
        // +----------------+--------------------------------+--------------------------------+

        double earth = 5.972e24;
        double jupiter = 1.898e27;

        Console.WriteLine("Earth = " + earth);
        Console.WriteLine("Jupiter = " + jupiter);
        // OUTPUT:
        // Earth = 5.972E+24
        // Jupiter = 1.898E+27


        // BIGINTEGER (EXACT VALUES)

        BigInteger earthBig =
            BigInteger.Parse("5972000000000000000000000");

        BigInteger jupiterBig =
            BigInteger.Parse("1898000000000000000000000000");

        Console.WriteLine("Earth Big = " + earthBig);
        Console.WriteLine("Jupiter Big = " + jupiterBig);
        // OUTPUT:
        // Earth Big = 5972000000000000000000000
        // Jupiter Big = 1898000000000000000000000000


        // BIGINTEGER DIFFERENCE

        Console.WriteLine(
            "Difference = " + (jupiterBig - earthBig));
        // OUTPUT:
        // Difference = 1300800000000000000000000000


        // BEST PRACTICE NOTE:
        // C# uses double for scientific numbers (approx)
        // BigInteger for exact integer precision
        //
        // Java:
        // BigInteger earthBig = new BigInteger("...");
        //
        // C#:
        // BigInteger earthBig = BigInteger.Parse("...");
        // BigInteger is available in: System.Numerics
    }


    // Date & Time and its Manipulation Functions
    public static void chapter05()
    {
        // ===============================================================================================================
        // JAVA → C# DATE & TIME
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA ITEM / METHOD                   | C# EQUIVALENT                        | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | LocalDate                            | DateTime.Today                       | Java: date only; C#: DateTime         |
        // | LocalDateTime                        | DateTime.Now                         | Java: date + time; C#: DateTime       |
        // | LocalTime                            | DateTime.Now.TimeOfDay               | Java: time; C#: TimeSpan              |
        // | ZonedDateTime                        | DateTimeOffset                       | Java: date/time + zone; C#: offset    |
        // | OffsetDateTime                       | DateTimeOffset                       | Same date/time + offset concept       |
        // | Instant                              | DateTimeOffset.UtcNow                | Java: UTC timestamp; C#: UTC + offset |
        // | Duration                             | TimeSpan                              | Both represent duration                |
        // | Period                               | AddDays/AddMonths/AddYears           | C#: no direct Period type              |
        // | ZoneId                               | TimeZoneInfo                         | Java: named zone; C#: TimeZoneInfo     |
        // | ZoneOffset                           | TimeSpan                             | Java: UTC offset; C#: TimeSpan         |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // CURRENT DATE / TIME
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | LocalDate.now()                      | DateTime.Today                       | Current date                           |
        // | LocalDateTime.now()                  | DateTime.Now                         | Current local date/time                |
        // | Instant.now()                        | DateTimeOffset.UtcNow                | Current UTC time                       |
        // | LocalTime.now()                      | DateTime.Now.TimeOfDay               | Current local time                     |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // CREATE DATE / DATE-TIME
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | LocalDate.of(2026,8,12)              | new DateTime(2026,8,12)              | Creates date                            |
        // | LocalDateTime.of(2026,8,12,15,30)    | new DateTime(2026,8,12,15,30,0)      | Creates date + time                     |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // GET DATE COMPONENTS
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | date.getYear()                       | date.Year                            | Java: method; C#: property             |
        // | date.getMonthValue()                 | date.Month                           | Java: method; C#: property             |
        // | date.getDayOfMonth()                 | date.Day                             | Java: method; C#: property             |
        // | date.getDayOfWeek()                  | date.DayOfWeek                       | Java: method; C#: property             |
        // | date.getDayOfYear()                  | date.DayOfYear                       | Java: method; C#: property             |
        // | dateTime.getHour()                   | dateTime.Hour                        | Java: method; C#: property             |
        // | dateTime.getMinute()                 | dateTime.Minute                      | Java: method; C#: property             |
        // | dateTime.getSecond()                 | dateTime.Second                      | Java: method; C#: property             |
        // | dateTime.getNano()                   | dateTime.Ticks                       | Java: nanoseconds; C#: 100-ns ticks    |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // ADD / SUBTRACT DATE VALUES
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | date.plusDays(5)                    | date.AddDays(5)                      | Adds days                              |
        // | date.minusDays(5)                   | date.AddDays(-5)                     | C#: negative value                     |
        // | date.plusMonths(2)                  | date.AddMonths(2)                    | Adds months                            |
        // | date.minusMonths(2)                 | date.AddMonths(-2)                   | C#: negative value                     |
        // | date.plusYears(1)                   | date.AddYears(1)                     | Adds years                             |
        // | date.minusYears(1)                  | date.AddYears(-1)                    | C#: negative value                     |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // DATE COMPARISON
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | date1.isBefore(date2)               | date1 < date2                        | Java: method; C#: operator             |
        // | date1.isAfter(date2)                | date1 > date2                        | Java: method; C#: operator             |
        // | date1.isEqual(date2)                | date1 == date2                       | Java: method; C#: operator             |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // MONTH / LEAP YEAR
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | date.lengthOfMonth()                | DateTime.DaysInMonth(date.Year,     | Java: instance method; C#: static      |
        // |                                      | date.Month)                          | method                                 |
        // | date.isLeapYear()                   | DateTime.IsLeapYear(date.Year)       | Java: instance; C#: static method     |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // DATE FORMATTING
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | date.format(formatter)              | date.ToString("yyyy-MM-dd")         | Java: formatter; C#: format string     |
        // | DateTimeFormatter.ofPattern(...)    | "yyyy-MM-dd"                         | Java: formatter object; C#: string     |
        // | DateTimeFormatter.ISO_DATE          | "yyyy-MM-dd"                         | Predefined formatter vs format string  |
        // | DateTimeFormatter.ISO_DATE_TIME     | "yyyy-MM-ddTHH:mm:ss"                | Predefined formatter vs format string  |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // PARSING
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | LocalDate.parse("2026-08-12")        | DateTime.Parse("2026-08-12")        | Parses date string                     |
        // | LocalDate.parse(...)                 | DateTime.ParseExact(...)             | Custom format                          |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // STRING CONVERSION
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | date.toString()                     | date.ToString("yyyy-MM-dd")          | C#: explicit format recommended        |
        // | dateTime.toString()                 | dateTime.ToString()                  | Converts date/time to string            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // DURATION
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | Duration.between(start,end)          | end - start                          | Java: Duration; C#: TimeSpan            |
        // | duration.getSeconds()               | timeSpan.TotalSeconds                | Java: method; C#: property              |
        // | duration.toMinutes()                | timeSpan.TotalMinutes                | Java: method; C#: property              |
        // | duration.toHours()                  | timeSpan.TotalHours                  | Java: method; C#: property              |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // COMBINE DATE + TIME
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | date.atStartOfDay()                 | date.Date                            | Midnight / 00:00:00                    |
        // | date.atTime(15,30)                  | date.Date.AddHours(15)               | Combines date + time                    |
        // |                                      | .AddMinutes(30)                      | C#: uses Add methods                    |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // TIME ZONES
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | ZoneId.of("UTC")                    | TimeZoneInfo                         | Named time zone                        |
        // | ZonedDateTime.now(ZoneId.of(...))   | TimeZoneInfo.ConvertTimeBySystemTime | Different API approach                 |
        // | ZonedDateTime.now(ZoneId.of("UTC")) | DateTime.UtcNow                      | UTC current time                       |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // ENUMS
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | DIFFERENCE                            |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | DayOfWeek.MONDAY                    | DayOfWeek.Monday                     | Both use enum                          |
        // | DayOfWeek.FRIDAY                    | DayOfWeek.Friday                     | Both use enum                          |
        // | Month.JANUARY                       | 1                                    | Java: Month enum; C#: int              |
        // | Month.DECEMBER                      | 12                                   | Java: Month enum; C#: int              |
        // +--------------------------------------+--------------------------------------+--------------------------------------+


        // ===============================================================================================================
        // IMPORTANT DIFFERENCES
        // ===============================================================================================================

        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | JAVA                                 | C#                                   | KEY DIFFERENCE                         |
        // +--------------------------------------+--------------------------------------+--------------------------------------+
        // | LocalDate                            | DateTime                             | Java dedicated date type               |
        // | LocalTime                            | TimeSpan                             | Java time; C# duration/time            |
        // | LocalDateTime                        | DateTime                             | Both date + time                       |
        // | ZonedDateTime                        | DateTimeOffset + TimeZoneInfo        | C# separates offset/time zone          |
        // | Duration                             | TimeSpan                             | Both elapsed-time types                |
        // | Period                               | DateTime Add methods                 | No direct C# Period equivalent          |
        // | DateTimeFormatter                    | Format strings                       | Different formatting approach          |
        // | getYear()                            | Year                                 | Java method vs C# property              |
        // | plusDays()                           | AddDays()                            | Different method naming                |
        // | minusDays()                          | AddDays(-N)                          | C# uses negative value                 |
        // | isBefore()                           | <                                    | Java method vs C# operator             |
        // | isAfter()                            | >                                    | Java method vs C# operator             |
        // | isEqual()                            | ==                                   | Java method vs C# operator             |
        // | Month enum                           | DateTime.Month                       | Java enum vs C# int                    |
        // | DayOfWeek enum                       | DayOfWeek enum                       | Very similar                           |
        // | Nanoseconds                          | Ticks                                | Java: nanoseconds; C#: 100-ns ticks    |
        // +--------------------------------------+--------------------------------------+--------------------------------------+

        // ---------------- DATE (JAVA) Obsolete ----------------

        DateTime _now = DateTime.Now;

        Console.WriteLine(_now);
        // OUTPUT: current system date/time


        DateTime _date =
            new DateTime(2026, 5, 8);

        Console.WriteLine(
            _date.ToString("yyyy-MM-dd"));

        // OUTPUT: 2026-05-08


        DateTime _today = DateTime.Today;

        Console.WriteLine(
            _today.ToString("yyyy-MM-dd"));

        // OUTPUT: current date

        // DATE FORMATTING

        DateTime _d = DateTime.Now;
        // current system date fetched (example: 2026-06-05)

        string formatted =
            _d.Day + "-" +
            _d.Month + "-" +
            _d.Year;
        // example format: 5-6-2026

        Console.WriteLine(formatted);
        // output: today's date in d-m-yyyy format (e.g., 5-6-2026)

        // 1. CURRENT DATE & TIME
        DateTime now = DateTime.Now;

        Console.WriteLine("Current DateTime: " + now);
        // OUTPUT: Current DateTime: 2026-06-05 13:45:30.123
        // NOTE: Actual output depends on the current system date/time.


        DateTime today = DateTime.Today;

        Console.WriteLine("Current Date: " + today.ToString("yyyy-MM-dd"));
        // OUTPUT: Current Date: 2026-06-05
        // NOTE: Actual output depends on the current system date.


        // 2. DATE PARTS (YEAR / MONTH / DAY)

        Console.WriteLine("Year: " + today.Year);
        // OUTPUT: Year: 2026

        Console.WriteLine("Month: " + today.Month);
        // OUTPUT: Month: 6

        Console.WriteLine("Day: " + today.Day);
        // OUTPUT: Day: 5


        // 3. ADD DAYS

        DateTime plus10Days = today.AddDays(10);

        Console.WriteLine(" +10 Days: " +
                          plus10Days.ToString("yyyy-MM-dd"));
        // OUTPUT: +10 Days: 2026-06-15


        // 4. ADD MONTHS

        DateTime plus2Months = today.AddMonths(2);

        Console.WriteLine(" +2 Months: " +
                          plus2Months.ToString("yyyy-MM-dd"));
        // OUTPUT: +2 Months: 2026-08-05


        // 5. ADD YEARS

        DateTime plus1Year = today.AddYears(1);

        Console.WriteLine(" +1 Year: " +
                          plus1Year.ToString("yyyy-MM-dd"));
        // OUTPUT: +1 Year: 2027-06-05


        // 6. CUSTOM DATE CREATION

        DateTime custom = new DateTime(2026, 4, 24);

        Console.WriteLine("Custom Date: " +
                          custom.ToString("yyyy-MM-dd"));
        // OUTPUT: Custom Date: 2026-04-24


        // 7. FORMAT DATE -> "Mon, 5 April 2026"

        string formattedDate =
            today.ToString("ddd, d MMMM yyyy");

        Console.WriteLine("Formatted: " + formattedDate);
        // OUTPUT: Formatted: Fri, 5 June 2026
        // NOTE: Day/month names depend on the current culture.


        // 8. YYYY-MM-DD FORMAT

        Console.WriteLine(
            "ISO Format: " +
            today.ToString("yyyy-MM-dd"));

        // OUTPUT: ISO Format: 2026-06-05


        // 9. DATE DIFFERENCE

        DateTime d1 = new DateTime(2026, 4, 22);
        DateTime d2 = new DateTime(2026, 6, 10);

        long diffDays = (d2 - d1).Days;

        Console.WriteLine(
            "Difference in Days: " + diffDays);

        // OUTPUT: Difference in Days: 49


        // 10. ADD / SUBTRACT DAYS

        Console.WriteLine(
            "Add 1 Month: " +
            d1.AddMonths(1).ToString("yyyy-MM-dd"));

        // OUTPUT: Add 1 Month: 2026-05-22


        Console.WriteLine(
            "Subtract 10 Days: " +
            d1.AddDays(-10).ToString("yyyy-MM-dd"));

        // OUTPUT: Subtract 10 Days: 2026-04-12


        // 11. MODIFY DATE

        DateTime modified =
            new DateTime(d1.Year, d1.Month, 1);

        Console.WriteLine(
            "Modified Date: " +
            modified.ToString("yyyy-MM-dd"));

        // OUTPUT: Modified Date: 2026-04-01


        // 12. COMPARISON

        Console.WriteLine(
            "d1 > d2 ? " + (d1 > d2));

        // OUTPUT: d1 > d2 ? False

        Console.WriteLine(
            "d1 < d2 ? " + (d1 < d2));

        // OUTPUT: d1 < d2 ? True

        Console.WriteLine(
            "Equal ? " +
            (d1 == new DateTime(2026, 4, 22)));

        // OUTPUT: Equal ? True


        // LOCALDATETIME FULL FUNCTIONS

        DateTime dt = DateTime.Now;

        Console.WriteLine(dt);
        // OUTPUT: 2026-06-05T14:30:00.123
        // NOTE: Actual output depends on the current system date/time.


        // --------------------- BASIC ADDITIONS ---------------------

        DateTime plusDays = dt.AddDays(5);

        Console.WriteLine(plusDays);
        // OUTPUT: 2026-06-10T14:30:00.123


        DateTime plusWeeks = dt.AddDays(2 * 7);

        Console.WriteLine(plusWeeks);
        // OUTPUT: +2 weeks date-time


        DateTime plusMonths = dt.AddMonths(3);

        Console.WriteLine(plusMonths);
        // OUTPUT: +3 months date-time


        DateTime plusYears = dt.AddYears(1);

        Console.WriteLine(plusYears);
        // OUTPUT: +1 year date-time


        // --------------------- TIME ADDITIONS ---------------------

        DateTime plusHours = dt.AddHours(4);

        Console.WriteLine(plusHours);
        // OUTPUT: +4 hours date-time


        DateTime plusMinutes = dt.AddMinutes(30);

        Console.WriteLine(plusMinutes);
        // OUTPUT: +30 minutes date-time


        DateTime plusSeconds = dt.AddSeconds(45);

        Console.WriteLine(plusSeconds);
        // OUTPUT: +45 seconds date-time


        // Java:
        // LocalDateTime plusNanos = dt.plusNanos(1_000_000);

        // C# DateTime uses ticks.
        // 1 tick = 100 nanoseconds.
        // 1 millisecond = 10,000 ticks.

        DateTime plusNanos =
            dt.AddTicks(1_000_000 / 100);

        Console.WriteLine(plusNanos);
        // OUTPUT: +1 millisecond approx (nanoseconds added)


        // --------------------- SUBTRACTION ---------------------

        DateTime minusDays = dt.AddDays(-10);

        Console.WriteLine(minusDays);
        // OUTPUT: -10 days date-time


        DateTime minusMonths = dt.AddMonths(-2);

        Console.WriteLine(minusMonths);
        // OUTPUT: -2 months date-time


        DateTime minusHours = dt.AddHours(-5);

        Console.WriteLine(minusHours);
        // OUTPUT: -5 hours date-time


        // --------------------- GET PARTS ---------------------

        Console.WriteLine(dt.Year);
        // OUTPUT: 2026


        Console.WriteLine(dt.Month);
        // OUTPUT: 6
        // Java getMonth() returns JUNE.
        // C# DateTime.Month returns numeric month.


        Console.WriteLine(dt.Month);
        // OUTPUT: 6


        Console.WriteLine(dt.Day);
        // OUTPUT: 5


        Console.WriteLine(dt.DayOfWeek);
        // OUTPUT: Friday
        // Java example: FRIDAY


        Console.WriteLine(dt.Hour);
        // OUTPUT: current hour


        Console.WriteLine(dt.Minute);
        // OUTPUT: current minute


        Console.WriteLine(dt.Second);
        // OUTPUT: current second


        // --------------------- MODIFY DATE ---------------------

        DateTime withDay =
            new DateTime(
                dt.Year,
                dt.Month,
                1,
                dt.Hour,
                dt.Minute,
                dt.Second,
                dt.Millisecond);

        Console.WriteLine(withDay);
        // OUTPUT: first day of current month


        DateTime withMonth =
            new DateTime(
                dt.Year,
                12,
                dt.Day,
                dt.Hour,
                dt.Minute,
                dt.Second,
                dt.Millisecond);

        Console.WriteLine(withMonth);
        // OUTPUT: December same time


        DateTime withYear =
            new DateTime(
                2030,
                dt.Month,
                dt.Day,
                dt.Hour,
                dt.Minute,
                dt.Second,
                dt.Millisecond);

        Console.WriteLine(withYear);
        // OUTPUT: year changed to 2030


        // --------------------- COMPARISON ---------------------

        DateTime dt1 = DateTime.Now;

        DateTime dt2 = dt1.AddDays(2);

        Console.WriteLine(dt1 < dt2);
        // OUTPUT: True


        Console.WriteLine(dt2 > dt1);
        // OUTPUT: True


        Console.WriteLine(dt1 == dt1);
        // OUTPUT: True


        // --------------------- FORMAT DATE TIME ---------------------

        string formatter =
            dt.ToString("yyyy-MM-dd HH:mm:ss");

        Console.WriteLine(formatter);
        // OUTPUT: 2026-06-05 14:30:00


        // --------------------- CONVERT TO DATE ---------------------

        DateTime onlyDate = dt.Date;

        Console.WriteLine(
            onlyDate.ToString("yyyy-MM-dd"));

        // OUTPUT: 2026-06-05


        // --------------------- CONVERT TO TIME ---------------------

        TimeSpan onlyTime = dt.TimeOfDay;

        Console.WriteLine(onlyTime);
        // OUTPUT: 14:30:00.123


        // --------------------- START / END USAGE ---------------------

        DateTime startOfDay = dt.Date;

        Console.WriteLine(startOfDay);
        // OUTPUT: 2026-06-05 00:00:00


        DateTime endOfDay =
            dt.Date.AddHours(23)
                   .AddMinutes(59)
                   .AddSeconds(59);

        Console.WriteLine(endOfDay);
        // OUTPUT: 2026-06-05 23:59:59
    }


    // TRAFFIC LIGHT (BOOLEAN FLAGS)
    public static void chapter06()
    {
        // C# does not support Java-style local classes.
        // Therefore, the Java methods are represented below
        // as local functions inside chapter09().


        /******** BOOLEAN LOGIC ********/


        // BOOLEAN FLAG VERSION

        void ShowLight(bool red, bool yellow, bool green)
        {
            if (red)
            {
                Console.WriteLine("STOP (Red Light ON)");
                // OUTPUT: STOP (Red Light ON)
            }
            else if (yellow)
            {
                Console.WriteLine("READY (Yellow Light ON)");
                // OUTPUT: READY (Yellow Light ON)
            }
            else if (green)
            {
                Console.WriteLine("GO (Green Light ON)");
                // OUTPUT: GO (Green Light ON)
            }
            else
            {
                Console.WriteLine("NO LIGHT ACTIVE");
                // OUTPUT: NO LIGHT ACTIVE
            }
        }


        // CLEAN VERSION (STRING BASED - RECOMMENDED IN C#)

        void ShowTrafficLight(string light)
        {
            switch (light.ToLower())
            {
                case "red":
                    Console.WriteLine("STOP");
                    // OUTPUT: STOP
                    break;

                case "yellow":
                    Console.WriteLine("READY");
                    // OUTPUT: READY
                    break;

                case "green":
                    Console.WriteLine("GO");
                    // OUTPUT: GO
                    break;

                default:
                    Console.WriteLine("INVALID LIGHT");
                    // OUTPUT: INVALID LIGHT
                    break;
            }
        }


        // BOOLEAN ARRAY VERSION

        void ShowLight2(bool[] lights)
        {
            if (lights[0])
            {
                Console.WriteLine("STOP (Red Light)");
                // OUTPUT: STOP (Red Light)
            }
            else if (lights[1])
            {
                Console.WriteLine("READY (Yellow Light)");
                // OUTPUT: READY (Yellow Light)
            }
            else if (lights[2])
            {
                Console.WriteLine("GO (Green Light)");
                // OUTPUT: GO (Green Light)
            }
        }


        bool[] NextLight(bool[] lights)
        {
            if (lights[0])
            {
                return new bool[]
                {
                false, true, false
                };
                // OUTPUT STATE: RED → YELLOW
            }

            if (lights[1])
            {
                return new bool[]
                {
                false, false, true
                };
                // OUTPUT STATE: YELLOW → GREEN
            }

            return new bool[]
            {
            true, false, false
            };
            // OUTPUT STATE: GREEN → RED
        }


        ShowLight(true, false, false);
        // INPUT: red=true
        // OUTPUT: STOP (Red Light ON)


        ShowTrafficLight("red");
        // INPUT: "red"
        // OUTPUT: STOP


        bool[] lights =
        {
        true, false, false
    };

        // STATE: RED light active


        ShowLight2(lights);
        // INPUT: {true,false,false}
        // OUTPUT: STOP (Red Light)


        lights = NextLight(lights);
        // STATE CHANGE: RED → YELLOW


        ShowLight2(lights);
        // INPUT: {false,true,false}
        // OUTPUT: READY (Yellow Light)
    }




    // 5: Selection Structures and Control Structures
    public static void chapter07()
    {
        // ================================================================
        // QUICK COMPARISON GRID
        // ================================================================

        // +----------------------+------------------------------+------------------------------+
        // | CONTROL FLOW         | JAVA                         | C#                           |
        // +----------------------+------------------------------+------------------------------+
        // | if                   | if (x > 0)                   | if (x > 0)                   |
        // | else                 | else                         | else                         |
        // | else-if              | else if                      | else if                      |
        // | ternary              | ? :                          | ? :                          |
        // | switch               | switch / case / break        | switch / case / break        |
        // | switch expression    | switch expression            | switch expression            |
        // | for                  | for (init; condition; inc)   | for (init; condition; inc)   |
        // | foreach              | for (Type x : collection)    | foreach (Type x in collection)|
        // | while                | while (condition)            | while (condition)            |
        // | do-while             | do { } while (condition)     | do { } while (condition)     |
        // | break                | break                        | break                        |
        // | continue             | continue                     | continue                     |
        // | lambda               | x -> expression              | x => expression              |
        // | output               | System.out.println()         | Console.WriteLine()           |
        // +----------------------+------------------------------+------------------------------+

        /*
         * ================================================================
         * JAVA → C# MAPPING
         * ================================================================
         *
         * JAVA                              C#
         * ----------------------------------------------------------------
         * for-each loop                  -> foreach loop
         * Iterator                      -> IEnumerator / foreach
         * Stream API                    -> LINQ
         * HashSet                       -> HashSet<T>
         * HashMap                       -> Dictionary<TKey, TValue>
         *
         * ================================================================
         */

        // 1. SPEED CHECKER
        int speed = 120;
        if (speed > 100)
        {
            Console.WriteLine("Overspeeding");
        }
        else if (speed >= 60)
        {
            Console.WriteLine("Normal Speed ");
        }
        else
        {
            Console.WriteLine("Too Slow ");
        }
        // OUTPUT: Overspeeding


        // 2. GRADE CALCULATOR
        int marks = 85;
        if (marks >= 90)
        {
            Console.WriteLine("A");
        }
        else if (marks >= 80)
        {
            Console.WriteLine("B");
        }
        else if (marks >= 70)
        {
            Console.WriteLine("C");
        }
        else if (marks >= 60)
        {
            Console.WriteLine("D");
        }
        else
        {
            Console.WriteLine("Fail");
        }
        // OUTPUT: B


        // 3. CIRCLE CALCULATOR
        double PI = 3.14159;
        double radius = 5;
        string type = "area";

        if (type == "area")
        {
            Console.WriteLine(PI * radius * radius);
        }
        else if (type == "circumference")
        {
            Console.WriteLine(2 * PI * radius);
        }
        else
        {
            Console.WriteLine("Invalid type");
        }
        // OUTPUT: 78.53975


        // 4. RECTANGLE CALCULATOR
        int length = 10, width = 5;
        type = "area";

        if (type == "area")
        {
            Console.WriteLine(length * width);
        }
        else if (type == "perimeter")
        {
            Console.WriteLine(2 * (length + width));
        }
        else
        {
            Console.WriteLine("Invalid type");
        }
        // OUTPUT: 50


        // 5. TRIANGLE TYPE CHECKER
        int aa = 10, bb = 10, cc = 10;

        if (aa == bb && bb == cc)
        {
            Console.WriteLine("Equilateral");
        }
        else if (aa == bb || bb == cc || aa == cc)
        {
            Console.WriteLine("Isosceles");
        }
        else
        {
            Console.WriteLine("Scalene");
        }
        // OUTPUT: Equilateral


        // 6. BILL CALCULATOR
        double amount = 1200;

        if (amount >= 1000)
        {
            Console.WriteLine(amount * 0.8);
        }
        else if (amount >= 500)
        {
            Console.WriteLine(amount * 0.9);
        }
        else
        {
            Console.WriteLine(amount);
        }
        // OUTPUT: 960


        // 7. LOGIN SYSTEM
        string user = "admin";
        string pass = "1234";

        if (user == "admin" && pass == "1234")
        {
            Console.WriteLine("Admin Login");
        }
        else if (user == "user" && pass == "1111")
        {
            Console.WriteLine("User Login");
        }
        else
        {
            Console.WriteLine("Invalid Credentials");
        }
        // OUTPUT: Admin Login


        // 8. WEATHER ADVICE
        int temp = 40;

        if (temp > 35)
        {
            Console.WriteLine("Very Hot");
        }
        else if (temp >= 20)
        {
            Console.WriteLine("Pleasant");
        }
        else
        {
            Console.WriteLine("Cold");
        }
        // OUTPUT: Very Hot


        // 9. MULTIPLICATION TABLE
        int tableNumber = 17;

        for (int ia = 1; ia <= 10; ia++)
        {
            Console.WriteLine(
                tableNumber + " x " + ia + " = " +
                (tableNumber * ia));
        }

        // OUTPUT:
        // 17 x 1 = 17
        // 17 x 2 = 34
        // 17 x 3 = 51
        // 17 x 4 = 68
        // 17 x 5 = 85
        // 17 x 6 = 102
        // 17 x 7 = 119
        // 17 x 8 = 136
        // 17 x 9 = 153
        // 17 x 10 = 170


        // 10. FORMULA LOOP (x² + y/3)
        for (int x = 1, y = 3; x <= 5; x++, y += 3)
        {
            double z = (x * x) + (y / 3.0);

            Console.WriteLine(
                "x=" + x +
                " y=" + y +
                " z=" + z);
        }

        // OUTPUT:
        // x=1 y=3 z=2
        // x=2 y=6 z=6
        // x=3 y=9 z=12
        // x=4 y=12 z=20
        // x=5 y=15 z=30


        // 11. FIND MAX IN ARRAY
        int[] arr = { 10, 50, 30, 20 };

        int max = arr[0];

        for (int ib = 1; ib < arr.Length; ib++)
        {
            if (arr[ib] > max)
            {
                max = arr[ib];
            }
        }

        Console.WriteLine("Max: " + max);
        // OUTPUT: Max: 50


        // 12. WHILE LOOP TABLE
        int num = 5;
        int i = 1;

        while (i <= 10)
        {
            Console.WriteLine(num * i);
            i++;
        }

        // OUTPUT:
        // 5
        // 10
        // 15
        // 20
        // 25
        // 30
        // 35
        // 40
        // 45
        // 50


        // 13. VOTING ELIGIBILITY
        int age = 18;

        if (age >= 18)
        {
            Console.WriteLine("Eligible to Vote");
        }
        else
        {
            Console.WriteLine("Not Eligible");
        }
        // OUTPUT: Eligible to Vote


        // 14. LARGEST OF THREE NUMBERS
        int a1 = 10, b1 = 25, c1 = 15;

        if (a1 > b1 && a1 > c1)
        {
            Console.WriteLine("A is largest");
        }
        else if (b1 > a1 && b1 > c1)
        {
            Console.WriteLine("B is largest");
        }
        else
        {
            Console.WriteLine("C is largest");
        }
        // OUTPUT: B is largest


        // 15. PRINT 1 TO N
        int n = 5;

        for (i = 1; i <= n; i++)
        {
            Console.WriteLine(i);
        }

        // OUTPUT:
        // 1
        // 2
        // 3
        // 4
        // 5


        // 16. ODD NUMBERS
        for (i = 1; i <= 20; i++)
        {
            if (i % 2 != 0)
            {
                Console.Write(i + " ");
            }
        }

        Console.WriteLine("");
        // OUTPUT: 1 3 5 7 9 11 13 15 17 19


        // 17. REVERSE NUMBER
        int numRev = 123;
        int rev = 0;

        while (numRev > 0)
        {
            int digit = numRev % 10;
            rev = rev * 10 + digit;
            numRev = numRev / 10;
        }

        Console.WriteLine("Reversed: " + rev);
        // OUTPUT: Reversed: 321


        // 18. COUNT DIGITS
        int numCount = 12345;
        int count = 0;

        while (numCount > 0)
        {
            numCount = numCount / 10;
            count++;
        }

        Console.WriteLine("Digits: " + count);
        // OUTPUT: Digits: 5


        // 19. POWER USING LOOP
        int base4 = 4;
        int result = 1;

        for (i = 1; i <= 4; i++)
        {
            result *= base4;
        }

        Console.WriteLine("Result: " + result);
        // OUTPUT: Result: 256
        // NOTE: 4 x 4 x 4 x 4 = 256.
        // The original Java comment said 1024, but that output is incorrect.


        // 20. EVEN / ODD INDEX IN STRING
        string str = "HELLO";

        for (i = 0; i < str.Length; i++)
        {
            if (i % 2 == 0)
            {
                Console.WriteLine(
                    str[i] + " -> Even Index");
            }
            else
            {
                Console.WriteLine(
                    str[i] + " -> Odd Index");
            }
        }

        // OUTPUT:
        // H -> Even Index
        // E -> Odd Index
        // L -> Even Index
        // L -> Odd Index
        // O -> Even Index


        // 21. ASCII EVEN / ODD
        string str2 = "ABC";

        for (i = 0; i < str2.Length; i++)
        {
            int code = str2[i];

            if (code % 2 == 0)
            {
                Console.WriteLine(
                    str2[i] + " EVEN ASCII");
            }
            else
            {
                Console.WriteLine(
                    str2[i] + " ODD ASCII");
            }
        }

        // OUTPUT:
        // A ODD ASCII
        // B EVEN ASCII
        // C ODD ASCII


        // NOTE (JAVA DIFFERENCE)
        // - Java does NOT support truthy/falsy checks.
        // - C# also requires a Boolean expression for if/while conditions.
        // - Both Java and C# use explicit conditions (==, !=, >, <).
        // - C# does NOT automatically treat numbers or strings as true/false.
        // - Java String comparison:
        //       user.equals("admin")
        //   C# String comparison:
        //       user == "admin"
        // - Java String length:
        //       str.length()
        //   C# String length:
        //       str.Length
        // - Java charAt(i):
        //       str.charAt(i)
        //   C# equivalent:
        //       str[i]
        // - Java array length:
        //       arr.length
        //   C# array length:
        //       arr.Length
    }



    // FUNCTIONS (JAVA METHODS)
    public static void chapter08()
    {
        // C# supports local functions.
        // They provide a simple equivalent to the Java
        // static methods defined inside FunctionChapter.


        // MultiplicationTable

        void GenerateTable(int number)
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine(
                    number + " x " +
                    i + " = " +
                    (number * i));

                // OUTPUT: prints multiplication table step-by-step
                // Example for 5:
                // 5 x 1 = 5
                // 5 x 2 = 10
                // ...
                // 5 x 10 = 50
            }
        }


        // WEATHER ADVICE

        string WeatherAdvice(int temp)
        {
            if (temp > 35)
            {
                return "Very Hot";
                // OUTPUT: if temp > 35 → "Very Hot"
            }
            else if (temp >= 20)
            {
                return "Pleasant";
                // OUTPUT: if 20–35 → "Pleasant"
            }
            else
            {
                return "Cold";
                // OUTPUT: if < 20 → "Cold"
            }
        }


        // CIRCLE CALCULATOR

        double CircleCalculator(
            double radius,
            string type)
        {
            double PI = 3.14159;

            if (type == "area")
            {
                return PI * radius * radius;
                // OUTPUT: area = πr²
            }
            else if (type == "circumference")
            {
                return 2 * PI * radius;
                // OUTPUT: circumference = 2πr
            }

            return -1;
            // OUTPUT: invalid type → -1
        }


        // RECTANGLE CALCULATOR

        int RectangleCalculator(
            int length,
            int width,
            string type)
        {
            if (type == "area")
            {
                return length * width;
                // OUTPUT: area = length × width
            }

            if (type == "perimeter")
            {
                return 2 * (length + width);
                // OUTPUT: perimeter = 2(l + w)
            }

            return -1;
            // OUTPUT: invalid type
        }


        // TRIANGLE TYPE

        string TriangleType(
            int a,
            int b,
            int c)
        {
            if (a == b && b == c)
            {
                return "Equilateral";
                // OUTPUT: all sides equal
            }

            if (a == b || b == c || a == c)
            {
                return "Isosceles";
                // OUTPUT: two sides equal
            }

            return "Scalene";
            // OUTPUT: all sides different
        }


        // BILL CALCULATOR

        double CalculateBill(double amount)
        {
            if (amount >= 1000)
            {
                return amount * 0.8;
                // OUTPUT: 20% discount
            }

            if (amount >= 500)
            {
                return amount * 0.9;
                // OUTPUT: 10% discount
            }

            return amount;
            // OUTPUT: no discount
        }


        // SPEED CHECK

        string CheckSpeed(int speed)
        {
            if (speed > 100)
            {
                return "Overspeeding";
                // OUTPUT: >100 → Overspeeding
            }

            if (speed >= 60)
            {
                return "Normal Speed";
                // OUTPUT: 60–100 → Normal
            }

            return "Too Slow";
            // OUTPUT: <60 → Too Slow
        }


        // GRADE CALCULATOR (RANGE STYLE)

        string GetGrade(int marks)
        {
            if (marks >= 90 && marks <= 100)
            {
                return "A";
                // OUTPUT: 90–100 → A
            }

            if (marks >= 80)
            {
                return "B";
                // OUTPUT: 80–89 → B
            }

            if (marks >= 70)
            {
                return "C";
                // OUTPUT: 70–79 → C
            }

            if (marks >= 60)
            {
                return "D";
                // OUTPUT: 60–69 → D
            }

            if (marks >= 0)
            {
                return "Fail";
                // OUTPUT: 0–59 → Fail
            }

            return "Invalid Marks";
            // OUTPUT: negative or >100 invalid
        }


        // LOGIN SYSTEM

        string Login(
            string user,
            string pass)
        {
            if (user == "admin" && pass == "1234")
            {
                return "Admin Login";
                // OUTPUT: correct admin credentials
            }

            if (user == "user" && pass == "1111")
            {
                return "User Login";
                // OUTPUT: correct user credentials
            }

            return "Invalid Credentials";
            // OUTPUT: wrong login
        }


        /******** FUNCTIONS ********/


        GenerateTable(5);
        // INPUT: 5
        // OUTPUT:
        // 5 x 1 = 5
        // 5 x 2 = 10
        // 5 x 3 = 15
        // 5 x 4 = 20
        // 5 x 5 = 25
        // 5 x 6 = 30
        // 5 x 7 = 35
        // 5 x 8 = 40
        // 5 x 9 = 45
        // 5 x 10 = 50


        Console.WriteLine(
            WeatherAdvice(40));

        // INPUT: 40
        // OUTPUT: Very Hot


        Console.WriteLine(
            CircleCalculator(5, "area"));

        // INPUT: r=5
        // OUTPUT: 78.53975


        Console.WriteLine(
            RectangleCalculator(10, 5, "area"));

        // INPUT: 10,5
        // OUTPUT: 50


        Console.WriteLine(
            TriangleType(5, 5, 3));

        // INPUT: 5,5,3
        // OUTPUT: Isosceles


        Console.WriteLine(
            CalculateBill(1200));

        // INPUT: 1200
        // OUTPUT: 960.0
        // 20% discount applied


        Console.WriteLine(
            CheckSpeed(120));

        // INPUT: 120
        // OUTPUT: Overspeeding


        Console.WriteLine(
            GetGrade(85));

        // INPUT: 85
        // OUTPUT: B


        Console.WriteLine(
            Login("admin", "1234"));

        // INPUT: valid credentials
        // OUTPUT: Admin Login
    }




}

// Chapter 11-13: LIST & SET - USER MODEL (POJO)
public class ChapterCollection
{

    // ================================================================
    // JAVA → C# GENERIC & NON-GENERIC COLLECTIONS
    // ================================================================
    //
    // COLLECTION TYPE          JAVA                              C#
    // -------------------------------------------------------------------------------
    //
    // GENERIC COLLECTIONS
    //
    // List                     ArrayList<T>                      List<T>
    //                         // java.util.ArrayList             // System.Collections.Generic
    //
    // Linked List              LinkedList<T>                     LinkedList<T>
    //                         // java.util.LinkedList            // System.Collections.Generic
    //
    // Dictionary / Map         HashMap<K,V>                      Dictionary<K,V>
    //                         // java.util.HashMap                // System.Collections.Generic
    //
    // Sorted Dictionary        TreeMap<K,V>                      SortedDictionary<K,V>
    //                         // java.util.TreeMap                // System.Collections.Generic
    //
    // Sorted Map               TreeMap<K,V>                      SortedList<K,V>
    //                         // Sorted key-value collection      // Alternative C# collection
    //
    // Set                      HashSet<T>                        HashSet<T>
    //                         // java.util.HashSet                 // System.Collections.Generic
    //
    // Sorted Set               TreeSet<T>                        SortedSet<T>
    //                         // java.util.TreeSet                 // System.Collections.Generic
    //
    // Queue                    Queue<T>                          Queue<T>
    //                         // java.util.Queue                   // System.Collections.Generic
    //
    // Priority Queue           PriorityQueue<T>                  PriorityQueue<TElement,TPriority>
    //                         // Java 17+                           // .NET 6+
    //
    // Stack                    Stack<T>                           Stack<T>
    //                         // java.util.Stack                    // System.Collections.Generic
    //
    // ================================================================
    // NON-GENERIC COLLECTIONS
    // ================================================================
    //
    // ArrayList                ArrayList                         ArrayList
    //                         // java.util.ArrayList                // System.Collections
    //
    // Hashtable                Hashtable                         Hashtable
    //                         // java.util.Hashtable                // System.Collections
    //
    // Stack                    Stack                            Stack
    //                         // java.util.Stack                    // System.Collections
    //
    // Queue                    Queue                            Queue
    //                         // java.util.Queue is GENERIC           // System.Collections.Queue
    //
    // LinkedList                LinkedList                        No direct non-generic equivalent
    //                         // java.util.LinkedList               // Use LinkedList<T>
    //
    // HashMap                  HashMap                           No direct equivalent
    //                         // Java HashMap<K,V>                  // Use Hashtable or Dictionary<K,V>
    //
    // HashSet                  HashSet                           No direct non-generic equivalent
    //                         // Java HashSet<T>                    // Use HashSet<T>
    //
    // TreeMap                  TreeMap                           No direct equivalent
    //                         // Java TreeMap<K,V>                  // Use SortedDictionary<K,V>
    //
    // TreeSet                  TreeSet                           No direct equivalent
    //                         // Java TreeSet<T>                    // Use SortedSet<T>
    //
    // ================================================================
    // IMPORTANT DIFFERENCE
    // ================================================================
    //
    // Java Generic List       List<String>                       List<string>
    //                         // Strongly typed                    // Strongly typed
    //
    // Java Non-Generic        ArrayList                          ArrayList
    //                         // Object-based                      // Object-based
    //
    // Java Generics           ArrayList<String>                  List<string>
    //                         // Type-safe                         // Type-safe
    //
    // Java Object Collection  ArrayList                         ArrayList
    //                         // Can store different types         // Can store different types
    //
    // ================================================================
    // GENERIC EXAMPLES
    // ================================================================
    //
    // Java                     C#
    // -------------------------------------------------------------------------------
    //
    // ArrayList<String>         List<string>
    // list.add("Ali");          list.Add("Ali");
    //
    // HashMap<Integer,String>   Dictionary<int,string>
    // map.put(1,"Ali");         map[1] = "Ali";
    //
    // HashSet<String>           HashSet<string>
    // set.add("Ali");           set.Add("Ali");
    //
    // Queue<String>             Queue<string>
    // queue.offer("Ali");       queue.Enqueue("Ali");
    //
    // Stack<String>             Stack<string>
    // stack.push("Ali");        stack.Push("Ali");
    //
    // TreeSet<Integer>          SortedSet<int>
    // set.add(10);              set.Add(10);
    //
    // TreeMap<Integer,String>   SortedDictionary<int,string>
    // map.put(1,"Ali");         map[1] = "Ali";
    //
    // ================================================================
    // MAIN DIFFERENCE
    // ================================================================
    //
    // Java Collection      → C# Equivalent
    // ArrayList<T>         → List<T>
    // LinkedList<T>        → LinkedList<T>
    // HashMap<K,V>         → Dictionary<K,V>
    // TreeMap<K,V>         → SortedDictionary<K,V>
    // HashSet<T>           → HashSet<T>
    // TreeSet<T>           → SortedSet<T>
    // Queue<T>             → Queue<T>
    // Stack<T>             → Stack<T>
    // PriorityQueue<T>     → PriorityQueue<TElement,TPriority>
    // ArrayList            → ArrayList
    // Hashtable            → Hashtable
    //
    // ================================================================
    // NAMESPACE DIFFERENCE — JAVA vs C#
    // ================================================================
    //
    // JAVA GENERIC COLLECTIONS                    C# GENERIC COLLECTIONS
    // -------------------------------------------------------------------------------
    // java.util.ArrayList                         System.Collections.Generic.List<T>
    // java.util.LinkedList                        System.Collections.Generic.LinkedList<T>
    // java.util.HashMap                           System.Collections.Generic.Dictionary<TKey,TValue>
    // java.util.HashSet                           System.Collections.Generic.HashSet<T>
    // java.util.TreeMap                           System.Collections.Generic.SortedDictionary<TKey,TValue>
    // java.util.TreeSet                           System.Collections.Generic.SortedSet<T>
    // java.util.Queue                             System.Collections.Generic.Queue<T>
    // java.util.Stack                             System.Collections.Generic.Stack<T>
    //
    // -------------------------------------------------------------------------------
    //
    // JAVA NON-GENERIC COLLECTIONS                 C# NON-GENERIC COLLECTIONS
    // -------------------------------------------------------------------------------
    // java.util.ArrayList                         System.Collections.ArrayList
    // java.util.Hashtable                         System.Collections.Hashtable
    // java.util.Stack                             System.Collections.Stack
    // java.util.Queue                             System.Collections.Queue
    //
    // ================================================================
    // NAMESPACE IMPORT / USING
    // ================================================================
    //
    // Java:
    // import java.util.ArrayList;
    // import java.util.HashMap;
    // import java.util.HashSet;
    // import java.util.LinkedList;
    //
    // C#:
    // using System.Collections.Generic;
    //
    // ================================================================
    // IMPORTANT:
    // ================================================================
    //
    // Java:  import java.util.*;
    // C#:    using System.Collections.Generic;
    //
    // Java uses individual classes under java.util.
    // C# generic collections are mainly under System.Collections.Generic.
    //
    // ================================================================

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public User(string name, int age, string city)
        {
            Name = name;
            Age = age;
            City = city;
        }

        public override string ToString()
        {
            return $"{{name={Name}, age={Age}, city={City}}}";
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }

        public Product(string name, int price, string brand)
        {
            Name = name;
            Price = price;
            Brand = brand;
        }

        public override string ToString()
        {
            return $"{{name={Name}, price={Price}, brand={Brand}}}";
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public Item(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }

    // SUPPORT CLASS FOR SET OBJECT EXAMPLE
    public class SetUser
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SetUser(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{{id={Id}, name='{Name}'}}";
        }

        // C# NOTE:
        // For proper duplicate detection in HashSet,
        // you should override Equals() and GetHashCode()
        // when logical equality is required.
    }


    // =========================================================
    // CHAPTER 11: LIST & SET
    // =========================================================

    public static void chapter11()
    {
        /*
              ===============================================================
              JAVA vs C# LIST / ARRAYLIST — QUICK COMPARISON GRID
              ===============================================================

              // +---------------------------+---------------------------+---------------------------+
              // | OPERATION                 | JAVA                      | C#                        |
              // +---------------------------+---------------------------+---------------------------+
              // | List<T>                   | List<T>                   | List<T>                   |
              // | ArrayList<T>              | ArrayList<T>              | List<T>                   |
              // | Add                       | list.add(x)               | list.Add(x)               |
              // | Insert                    | list.add(i, x)            | list.Insert(i, x)         |
              // | Get                       | list.get(i)               | list[i]                   |
              // | Set                       | list.set(i, x)            | list[i] = x               |
              // | Remove by Index           | list.remove(i)            | list.RemoveAt(i)          |
              // | Remove by Value           | list.remove(x)            | list.Remove(x)            |
              // | Size                      | list.size()               | list.Count                |
              // | Is Empty                  | list.isEmpty()            | list.Count == 0           |
              // | Clear                     | list.clear()              | list.Clear()              |
              // | Contains                  | list.contains(x)          | list.Contains(x)          |
              // | Index Of                  | list.indexOf(x)           | list.IndexOf(x)           |
              // | Last Index Of             | list.lastIndexOf(x)       | list.LastIndexOf(x)       |
              // | Add All                   | list.addAll(list2)        | list.AddRange(list2)      |
              // | Remove All                | list.removeAll(list2)     | list.RemoveAll(...)       |
              // | Sub List                  | list.subList(a, b)        | list.GetRange(a, b-a)     |
              // | To Array                  | list.toArray()            | list.ToArray()            |
              // | Sort                      | Collections.sort(list)    | list.Sort()               |
              // | Reverse                   | Collections.reverse(list) | list.Reverse()            |
              // | Shuffle                   | Collections.shuffle(list) | Random / Shuffle()        |
              // | For Each                  | list.forEach(x -> ...)    | list.ForEach(x => ...)    |
              // | Foreach Loop              | for (T x : list)          | foreach (T x in list)     |
              // | Stream / LINQ             | list.stream()             | list / LINQ               |
              // | Map / Select              | stream.map()               | Select()                 |
              // | Filter / Where            | stream.filter()            | Where()                  |
              // | Count                     | stream.count()             | Count()                  |
              // | First                     | stream.findFirst()         | First()                  |
              // | Any Match                 | stream.anyMatch()          | Any()                    |
              // | All Match                 | stream.allMatch()          | All()                    |
              // | Find First / Default      | stream.findFirst()         | FirstOrDefault()         |
              // | Create Empty List         | new ArrayList<>()          | new List<T>()            |
              // | Create With Values        | List.of(a,b,c)             | new List<T> { a,b,c }    |
              // | Arrays.asList             | Arrays.asList(a,b,c)       | new List<T> { a,b,c }    |
              // | To String                 | list.toString()            | string.Join(", ", list)  |
              // +---------------------------+---------------------------+---------------------------+

              ===============================================================
              IMPORTANT DIFFERENCES
              ===============================================================

              // Java List interface       → C# List<T> class
              // Java ArrayList<T>          → C# List<T>
              // Java get(i)                → C# [i]
              // Java set(i,x)              → C# [i] = x
              // Java size()                → C# Count
              // Java add()                 → C# Add()
              // Java remove(i)             → C# RemoveAt(i)
              // Java remove(x)             → C# Remove(x)
              // Java subList(a,b)          → C# GetRange(a,b-a)
              // Java Streams               → C# LINQ
              // Java stream.map()          → C# Select()
              // Java stream.filter()       → C# Where()
              // Java stream.anyMatch()     → C# Any()
              // Java stream.allMatch()     → C# All()
              // Java stream.findFirst()    → C# FirstOrDefault()
              // Java Collections.sort()    → C# List.Sort()

              ===============================================================
 */

        // JAVA:
        // List<String> list = new ArrayList<>();

        // C# equivalent:
        List<string> list = new List<string>();

        // add(T type)
        list.Add("A");
        list.Add("B");
        list.Add("C");
        list.Add("B");

        Console.WriteLine("After add(): " + FormatList(list));
        // OUTPUT:
        // After add(): [A, B, C, B]

        // =========================
        // add(int index, T type)
        // =========================

        list.Insert(2, "X");

        Console.WriteLine("After add(index, value): " + FormatList(list));
        // OUTPUT:
        // After add(index, value): [A, B, X, C, B]

        // =========================
        // remove(Object o)
        // =========================

        // Removes FIRST occurrence
        list.Remove("B");

        Console.WriteLine("After remove(Object): " + FormatList(list));
        // OUTPUT:
        // After remove(Object): [A, X, C, B]

        // =========================
        // remove(int index)
        // =========================

        list.RemoveAt(1);

        Console.WriteLine("After remove(index): " + FormatList(list));
        // OUTPUT:
        // After remove(index): [A, C, B]

        // =========================
        // get(int index)
        // =========================

        Console.WriteLine("Get index 1: " + list[1]);
        // OUTPUT:
        // Get index 1: C

        // =========================
        // set(int index, E element)
        // =========================

        list[1] = "Z";

        Console.WriteLine("After set(): " + FormatList(list));
        // OUTPUT:
        // After set(): [A, Z, B]

        // =========================
        // indexOf(Object o)
        // =========================

        Console.WriteLine("indexOf B: " + list.IndexOf("B"));
        // OUTPUT:
        // indexOf B: 2

        // =========================
        // lastIndexOf(Object o)
        // =========================

        list.Add("B"); // add duplicate

        Console.WriteLine("List now: " + FormatList(list));
        // OUTPUT:
        // List now: [A, Z, B, B]

        Console.WriteLine("lastIndexOf B: " + list.LastIndexOf("B"));
        // OUTPUT:
        // lastIndexOf B: 3


        // =========================================================
        // 1. ARRAYLIST
        // =========================================================
        // - Resizable array implementation
        // - Fast random access (get by index)
        // - Slow insert/remove in middle
        //
        // JAVA:
        // List<String> arrayList = new ArrayList<>();
        //
        // C#:
        // List<T> is the closest equivalent.

        List<string> arrayList = new List<string>();

        arrayList.Add("Apple");
        arrayList.Add("Banana");
        arrayList.Add("Mango");

        Console.WriteLine("ArrayList: " + FormatList(arrayList));
        // OUTPUT:
        // ArrayList: [Apple, Banana, Mango]

        Console.WriteLine("Get index 1: " + arrayList[1]);
        // OUTPUT:
        // Get index 1: Banana


        // =========================================================
        // 2. LINKEDLIST
        // =========================================================
        // - Doubly linked list implementation
        // - Fast insert/remove when node is known
        // - Slow random access
        //
        // JAVA:
        // List<String> linkedList = new LinkedList<>();
        //
        // C#:
        // LinkedList<T>

        LinkedList<string> linkedList = new LinkedList<string>();

        linkedList.AddLast("Apple");
        linkedList.AddLast("Banana");
        linkedList.AddLast("Mango");

        // Insert in middle
        LinkedListNode<string> bananaNode = linkedList.Find("Banana");
        linkedList.AddBefore(bananaNode, "Orange");

        Console.WriteLine("LinkedList: " + FormatLinkedList(linkedList));
        // OUTPUT:
        // LinkedList: [Apple, Orange, Banana, Mango]

        linkedList.Remove("Banana");

        Console.WriteLine("After remove: " + FormatLinkedList(linkedList));
        // OUTPUT:
        // After remove: [Apple, Orange, Mango]


        // =========================================================
        // 3. VECTOR
        // =========================================================
        // - Java Vector is synchronized/thread-safe
        // - Slower than ArrayList
        // - Legacy Java class
        //
        // C# does not have a direct Vector equivalent.
        // List<T> is the normal C# collection.
        // System.Collections.Vector does not exist.

        List<string> vector = new List<string>();

        vector.Add("A");
        vector.Add("B");
        vector.Add("C");

        Console.WriteLine("Vector: " + FormatList(vector));
        // OUTPUT:
        // Vector: [A, B, C]

        vector[1] = "Z";

        Console.WriteLine("After set(): " + FormatList(vector));
        // OUTPUT:
        // After set(): [A, Z, C]


        // =========================================================
        // 4. STACK
        // =========================================================
        // - LIFO (Last In First Out)
        // - Push, Pop, Peek operations
        //
        // JAVA:
        // Stack<Integer> stack = new Stack<>();
        //
        // C#:
        // Stack<int>

        Stack<int> stack = new Stack<int>();

        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        Console.WriteLine("Stack: " + FormatStack(stack));
        // OUTPUT:
        // Stack: [10, 20, 30]

        Console.WriteLine("Pop: " + stack.Pop());
        // OUTPUT:
        // Pop: 30

        Console.WriteLine("After pop: " + FormatStack(stack));
        // OUTPUT:
        // After pop: [10, 20]

        Console.WriteLine("Peek: " + stack.Peek());
        // OUTPUT:
        // Peek: 20


        // =========================================================
        // COMPARISON EXAMPLE
        // =========================================================

        List<int> numbers = new List<int>();

        numbers.Add(1);
        numbers.Add(2);
        numbers.Add(3);

        Console.WriteLine("ArrayList Numbers: " + FormatList(numbers));
        // OUTPUT:
        // ArrayList Numbers: [1, 2, 3]

        LinkedList<int> linkedNumbers = new LinkedList<int>(numbers);

        Console.WriteLine("LinkedList Numbers: " + FormatLinkedList(linkedNumbers));
        // OUTPUT:
        // LinkedList Numbers: [1, 2, 3]


        // =========================================================
        // 1. HASHSET
        // =========================================================
        // - Stores unique values
        // - No ordering guarantee
        // - Fast lookup
        //
        // JAVA:
        // Set<String> hashSet = new HashSet<>();
        //
        // C#:
        // HashSet<string>

        HashSet<string> hashSet = new HashSet<string>();

        hashSet.Add("Banana");
        hashSet.Add("Apple");
        hashSet.Add("Mango");
        hashSet.Add("Apple"); // duplicate ignored

        Console.WriteLine("HashSet: " + FormatSet(hashSet));
        // OUTPUT:
        // HashSet: [Apple, Mango, Banana]
        // NOTE: C# HashSet order is NOT guaranteed.


        // =========================================================
        // 2. LINKEDHASHSET
        // =========================================================
        // Java LinkedHashSet maintains insertion order.
        //
        // C# does not have a direct LinkedHashSet<T>.
        // List<T> + Contains() can be used when insertion order
        // and uniqueness are both required.

        List<string> linkedHashSet = new List<string>();

        AddUnique(linkedHashSet, "Banana");
        AddUnique(linkedHashSet, "Apple");
        AddUnique(linkedHashSet, "Mango");
        AddUnique(linkedHashSet, "Apple"); // duplicate ignored

        Console.WriteLine("LinkedHashSet: " + FormatList(linkedHashSet));
        // OUTPUT (in insertion order):
        // LinkedHashSet: [Banana, Apple, Mango]


        // =========================================================
        // 3. TREESET
        // =========================================================
        // - Automatically sorted
        // - No duplicate values
        //
        // JAVA:
        // Set<String> treeSet = new TreeSet<>();
        //
        // C#:
        // SortedSet<string>

        SortedSet<string> treeSet = new SortedSet<string>();

        treeSet.Add("Banana");
        treeSet.Add("Apple");
        treeSet.Add("Mango");
        treeSet.Add("Apple"); // duplicate ignored

        Console.WriteLine("TreeSet: " + FormatSet(treeSet));
        // OUTPUT (sorted order):
        // TreeSet: [Apple, Banana, Mango]


        // =========================================================
        // COMPARISON EXAMPLE (NUMBERS)
        // =========================================================

        HashSet<int> numberS = new HashSet<int>();

        numberS.Add(30);
        numberS.Add(10);
        numberS.Add(20);

        Console.WriteLine("HashSet Numbers: " + FormatSet(numberS));
        // OUTPUT (order not guaranteed):
        // HashSet Numbers: [20, 10, 30]

        SortedSet<int> sortedNumbers = new SortedSet<int>(numberS);

        Console.WriteLine("TreeSet Numbers: " + FormatSet(sortedNumbers));
        // OUTPUT (sorted):
        // TreeSet Numbers: [10, 20, 30]


        // =========================================================
        // USER MODEL
        // =========================================================

        User u = new User("Ali", 25, "Karachi");

        Console.WriteLine(u.Name);
        Console.WriteLine(u.City);
        // OUTPUT:
        // Ali
        // Karachi


        // =========================================================
        // USING DICTIONARY AS JS-LIKE OBJECT ALTERNATIVE
        // =========================================================

        Dictionary<string, object> userMap = new Dictionary<string, object>();

        userMap["name"] = "Ali";
        userMap["age"] = 25;

        Console.WriteLine(FormatDictionary(userMap));
        // OUTPUT:
        // {name=Ali, age=25}


        // =========================================================
        // OBJECT OPERATIONS
        // =========================================================

        Product p = new Product("Laptop", 50000, "Dell");

        Dictionary<string, object> productMap = new Dictionary<string, object>();

        productMap["name"] = p.Name;
        productMap["price"] = p.Price;
        productMap["brand"] = p.Brand;

        Console.WriteLine(FormatDictionary(productMap));
        // OUTPUT:
        // {name=Laptop, price=50000, brand=Dell}


        // =========================================================
        // ARRAY OPERATIONS
        // JS map/filter equivalent
        // =========================================================

        List<Product> cart = new List<Product>
        {
            new Product("Laptop", 50000, "Dell"),
            new Product("Mouse", 1000, "Logitech")
        };

        int total = cart.Sum(item => item.Price);

        List<string> names = cart
            .Select(item => item.Name)
            .ToList();

        Console.WriteLine("Total: " + total);
        Console.WriteLine("Names: " + FormatList(names));
        // OUTPUT:
        // Total: 51000
        // Names: [Laptop, Mouse]


        // =========================================================
        // FILTER (ACTIVE USERS)
        // =========================================================

        List<User> users = new List<User>
        {
            new User("Ali", 25, "Karachi"),
            new User("Sara", 22, "Lahore")
        };

        List<User> filtered = users
            .Where(u1 => u1.Age > 23)
            .ToList();

        Console.WriteLine(filtered.Count);
        // OUTPUT:
        // 1
    }


    // =========================================================
    // CHAPTER 12: MAP IN JAVA
    // =========================================================

    public static void chapter12()
    {
        /*
         ================================================================
         JAVA vs C# MAP / HASHMAP — QUICK COMPARISON GRID
         ================================================================

         // +---------------------------+-----------------------------+-----------------------------+
         // | OPERATION                 | JAVA                        | C#                          |
         // +---------------------------+-----------------------------+-----------------------------+
         // | Map<K,V>                  | Map<K,V>                    | Dictionary<K,V>             |
         // | HashMap<K,V>              | HashMap<K,V>                | Dictionary<K,V>             |
         // | TreeMap<K,V>              | TreeMap<K,V>                | SortedDictionary<K,V>       |
         // | Add / Update              | map.put(k,v)                | map[k] = v                  |
         // | Get                       | map.get(k)                  | map[k]                      |
         // | Remove                    | map.remove(k)               | map.Remove(k)               |
         // | Contains Key              | map.containsKey(k)          | map.ContainsKey(k)          |
         // | Contains Value            | map.containsValue(v)        | map.ContainsValue(v)        |
         // | Size                      | map.size()                  | map.Count                   |
         // | Empty Check               | map.isEmpty()               | map.Count == 0              |
         // | Clear                     | map.clear()                 | map.Clear()                 |
         // | Keys                      | map.keySet()                | map.Keys                    |
         // | Values                    | map.values()                | map.Values                  |
         // | Entries                   | map.entrySet()              | map                         |
         // | Entry Key                 | entry.getKey()              | entry.Key                   |
         // | Entry Value               | entry.getValue()            | entry.Value                 |
         // | Add If Absent             | map.putIfAbsent(k,v)        | map.TryAdd(k,v)             |
         // | Get Default               | map.getOrDefault(k,v)       | TryGetValue()               |
         // | Replace                   | map.replace(k,v)            | map[k] = v                  |
         // | Compute If Absent         | map.computeIfAbsent()       | ContainsKey() + assignment  |
         // | Merge                     | map.merge()                 | lookup + update             |
         // | Loop                      | for (Entry e : map.entrySet())| foreach (var e in map)    |
         // | Null Key                  | null key                    | null key*                   |
         // +---------------------------+-----------------------------+-----------------------------+
         //
         // * Depends on C# key type and nullable reference type settings.
         //================================================================
         */

        Dictionary<object, object> map = new Dictionary<object, object>();

        map["name"] = "Ali";
        map["age"] = 25;
        map[1] = "Number Key";
        map[true] = "Boolean Key";

        Console.WriteLine("Initial Map: " + FormatDictionary(map));
        // OUTPUT:
        // Initial Map: {name=Ali, age=25, 1=Number Key, True=Boolean Key}
        // NOTE: Dictionary order should not be relied upon.


        // =========================================================
        // 2. put() - ADD / UPDATE VALUES
        // =========================================================

        map["city"] = "Karachi";
        map["age"] = 30;

        Console.WriteLine("After put(): " + FormatDictionary(map));
        // OUTPUT:
        // After put(): {name=Ali, age=30, city=Karachi, 1=Number Key, True=Boolean Key}


        // =========================================================
        // 3. get() - FETCH VALUE
        // =========================================================

        Console.WriteLine(
            "Name: " + map["name"] +
            ", City: " + map["city"]);
        // OUTPUT:
        // Name: Ali, City: Karachi


        // =========================================================
        // 4. containsKey() - CHECK KEY
        // =========================================================

        Console.WriteLine(
            "Has age? " + map.ContainsKey("age") +
            ", Has salary? " + map.ContainsKey("salary"));
        // OUTPUT:
        // Has age? True, Has salary? False


        // =========================================================
        // 5. remove() - DELETE KEY
        // =========================================================

        map.Remove("age");

        Console.WriteLine("After remove age: " + FormatDictionary(map));
        // OUTPUT:
        // After remove age: map without age key


        // =========================================================
        // 6. size - TOTAL ELEMENTS
        // =========================================================

        Console.WriteLine("Map size: " + map.Count);
        // OUTPUT:
        // Map size: current size of map


        // =========================================================
        // 7. LOOP (forEach)
        // =========================================================

        foreach (KeyValuePair<object, object> entry in map)
        {
            Console.WriteLine(entry.Key + " => " + entry.Value);
        }
        // OUTPUT:
        // all key-value pairs printed


        // =========================================================
        // 8. ITERATE USING ENTRY SET
        // =========================================================

        foreach (KeyValuePair<object, object> entry in map)
        {
            Console.WriteLine(entry.Key + " : " + entry.Value);
        }
        // OUTPUT:
        // all entries printed


        // =========================================================
        // 9. KEY / VALUE COLLECTIONS
        // =========================================================

        Console.WriteLine(
            "Keys: " + FormatCollection(map.Keys) +
            ", Values: " + FormatCollection(map.Values));
        // OUTPUT:
        // keys + values lists


        // =========================================================
        // 10. CLEAR MAP
        // =========================================================

        Dictionary<string, int> tempMap =
            new Dictionary<string, int>();

        tempMap["a"] = 1;
        tempMap["b"] = 2;

        tempMap.Clear();

        Console.WriteLine("After clear size: " + tempMap.Count);
        // OUTPUT:
        // 0


        // =========================================================
        // 11. STUDENT DATABASE
        // =========================================================

        Dictionary<int, string> students =
            new Dictionary<int, string>();

        students[101] = "Ahmed";
        students[102] = "Sara";
        students[103] = "Ali";

        Console.WriteLine("Student 102: " + students[102]);
        // OUTPUT:
        // Student 102: Sara


        // =========================================================
        // 12. SHOPPING CART
        // =========================================================

        Dictionary<string, int> cart =
            new Dictionary<string, int>();

        cart["Apple"] = 3;

        Console.WriteLine("Apple qty: " + cart["Apple"]);
        // OUTPUT:
        // Apple qty: 3


        // =========================================================
        // 13. OBJECT AS KEY
        // =========================================================

        object user = new object();

        Dictionary<object, string> userMap =
            new Dictionary<object, string>();

        userMap[user] = "User Profile Data";

        Console.WriteLine(
            "Object Key Value: " + userMap[user]);
        // OUTPUT:
        // Object Key Value: User Profile Data


        // =========================================================
        // CHAPTER 13: ARRAYS
        // =========================================================


        // =========================================================
        // 1. ARRAY DECLARATIONS
        // =========================================================

        int[] intArray = { 1, 2, 3, 4, 5 };

        char[] charArray = { 'a', 'b', 'c' };

        string[] strArray =
        {
            "red",
            "blue",
            "green"
        };

        List<bool> traffic = new List<bool>();

        traffic.Add(true);
        traffic.Add(true);
        traffic.Add(false);


        // =========================================================
        // 2. LOOPING ARRAYS
        // =========================================================

        int[] numArray = { 3, 5, 7 };

        for (int i = 0; i < numArray.Length; i++)
        {
            Console.WriteLine(i);
            // OUTPUT:
            // index values
        }

        foreach (int n in numArray)
        {
            Console.WriteLine(n);
            // OUTPUT:
            // 3
            // 5
            // 7
        }


        // =========================================================
        // 3. MULTIPLICATION LOOP
        // =========================================================

        int[] myArray = { 1, 2, 3, 4 };

        for (int i = 0; i < myArray.Length; i++)
        {
            Console.WriteLine(
                "2 * value is: " + (myArray[i] * 2));
            // OUTPUT:
            // multiplied values
        }


        // =========================================================
        // 4. ASCII GENERATION
        // =========================================================

        System.Text.StringBuilder sb =
            new System.Text.StringBuilder();

        for (int i = 65; i <= 122; i++)
        {
            sb.Append((char)i);
        }

        Console.WriteLine(sb.ToString());
        // OUTPUT:
        // ASCII range string


        // =========================================================
        // 5. ARRAY CREATION METHODS
        // =========================================================

        List<string> fruits = new List<string>();

        fruits.Add("Apple");
        fruits.Add("Banana");
        fruits.Add("Mango");

        Console.WriteLine(FormatList(fruits));
        // OUTPUT:
        // [Apple, Banana, Mango]


        // =========================================================
        // 6. PUSH / ADD
        // =========================================================

        fruits.Add("Orange");
        // OUTPUT:
        // updated list


        // =========================================================
        // 7. REMOVE LAST
        // =========================================================

        fruits.RemoveAt(fruits.Count - 1);
        // OUTPUT:
        // last element removed


        // =========================================================
        // 8. UPDATE ELEMENT
        // =========================================================

        fruits[1] = "Grapes";
        // OUTPUT:
        // updated index 1


        // =========================================================
        // 9. FOREACH
        // =========================================================

        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
        }

        fruits.ForEach(x => Console.WriteLine(x));

        foreach (string fruit in fruits)
        {
            Console.WriteLine(fruit);
            // OUTPUT:
            // all fruits printed
        }


        // =========================================================
        // 10. MAP (TRANSFORM)
        // =========================================================

        List<string> upper = new List<string>();

        foreach (string f in fruits)
        {
            upper.Add(f.ToUpper());
        }

        Console.WriteLine(FormatList(upper));
        // OUTPUT:
        // uppercase list


        // =========================================================
        // 11. FILTER
        // =========================================================

        List<string> longNames = new List<string>();

        foreach (string f in fruits)
        {
            if (f.Length > 5)
            {
                longNames.Add(f);
            }
        }

        Console.WriteLine(FormatList(longNames));
        // OUTPUT:
        // filtered list


        // =========================================================
        // 12. FIND
        // =========================================================

        string found = null;

        foreach (string f in fruits)
        {
            if (f.StartsWith("M"))
            {
                found = f;
                break;
            }
        }

        Console.WriteLine(found);
        // OUTPUT:
        // first match or null


        // =========================================================
        // 13. INCLUDES
        // =========================================================

        Console.WriteLine(fruits.Contains("Apple"));
        // OUTPUT:
        // true/false


        // =========================================================
        // 14. INDEX OF
        // =========================================================

        Console.WriteLine(fruits.IndexOf("Mango"));
        // OUTPUT:
        // index or -1


        // =========================================================
        // 15. SORT
        // =========================================================

        List<int> numbers =
            new List<int> { 40, 10, 100, 5 };

        numbers.Sort();

        Console.WriteLine(FormatList(numbers));
        // OUTPUT:
        // [5, 10, 40, 100]


        // =========================================================
        // 16. REVERSE
        // =========================================================

        numbers.Reverse();

        Console.WriteLine(FormatList(numbers));
        // OUTPUT:
        // [100, 40, 10, 5]


        // =========================================================
        // 17. SLICE
        // =========================================================

        List<string> some =
            fruits.GetRange(0, 2);

        Console.WriteLine(FormatList(some));
        // OUTPUT:
        // sliced list


        // =========================================================
        // 18. JOIN
        // =========================================================

        Console.WriteLine(
            string.Join(", ", fruits));
        // OUTPUT:
        // joined string


        // =========================================================
        // 19. CONCAT
        // =========================================================

        List<int> arr1 =
            new List<int> { 1, 2 };

        List<int> arr2 =
            new List<int> { 3, 4 };

        List<int> merged =
            new List<int>();

        merged.AddRange(arr1);
        merged.AddRange(arr2);

        Console.WriteLine(FormatList(merged));
        // OUTPUT:
        // [1, 2, 3, 4]


        // =========================================================
        // 20. REDUCE (SUM)
        // =========================================================

        int sum = 0;

        foreach (int n in numbers)
        {
            sum += n;
        }

        Console.WriteLine(sum);
        // OUTPUT:
        // sum of numbers


        // =========================================================
        // 21. SHOPPING CART ANALYSIS
        // =========================================================

        List<Item> cart2 = new List<Item>();

        cart2.Add(
            new Item("Laptop", 50000));

        cart2.Add(
            new Item("Mouse", 1000));

        int total = 0;

        foreach (Item item in cart2)
        {
            total += item.Price;
        }

        Console.WriteLine("Total: " + total);
        // OUTPUT:
        // Total: 51000


        // =========================================================
        // 22. IMMUTABLE ARRAY NOTE
        // =========================================================

        // Java does NOT support toReversed/toSorted/toSpliced.
        //
        // C# also does not have direct equivalents with these
        // exact JavaScript method names.
        //
        // LINQ can be used to create transformed copies.


        // =========================================================
        // 23. MATH OPERATIONS
        // =========================================================

        int[] nums =
        {
            10, 50, 20, 80, 30
        };

        int max = nums.Max();

        int min = nums.Min();

        int totalSum = nums.Sum();

        double avg =
            totalSum / (double)nums.Length;

        Console.WriteLine(
            "Max: " + max +
            ", Min: " + min +
            ", Avg: " + avg);
        // OUTPUT:
        // Max: 80, Min: 10, Avg: 38


        // =========================================================
        // 24. OBJECT ARRAY ANALYSIS
        // =========================================================

        Product[] products =
        {
            new Product("Laptop", 50000, "Dell"),
            new Product("Mouse", 1000, "Logitech"),
            new Product("Keyboard", 3000, "Dell")
        };

        int maxPrice = int.MinValue;

        Product maxProduct = null;

        foreach (Product p in products)
        {
            if (p.Price > maxPrice)
            {
                maxPrice = p.Price;
                maxProduct = p;
            }
        }

        Console.WriteLine(
            "Max Product: " +
            maxProduct.Name +
            " " +
            maxProduct.Price);
        // OUTPUT:
        // Max Product: Laptop 50000


        // =========================================================
        // 1. HASHMAP
        // =========================================================
        // - No ordering guarantee
        // - Java HashMap allows one null key
        // - C# Dictionary does NOT allow a null key
        // - Fast general-purpose dictionary
        //
        // Java:
        // Map<Integer, String> hashMap = new HashMap<>();
        //
        // C#:
        // Dictionary<int, string>

        Dictionary<int, string> hashMap =
            new Dictionary<int, string>();

        hashMap[3] = "Mango";
        hashMap[1] = "Apple";
        hashMap[2] = "Banana";

        Console.WriteLine(
            "HashMap: " +
            FormatDictionary(hashMap));
        // OUTPUT:
        // HashMap: {3=Mango, 1=Apple, 2=Banana}
        // NOTE: Dictionary ordering should not be relied upon.


        // =========================================================
        // 2. LINKEDHASHMAP
        // =========================================================
        // - Maintains insertion order
        // - C# Dictionary preserves insertion order in
        //   modern .NET implementations, but ordering should
        //   generally not be used as the primary contract.
        //
        // For explicit insertion-order semantics, use a
        // collection designed for ordered dictionaries.

        Dictionary<int, string> linkedHashMap =
            new Dictionary<int, string>();

        linkedHashMap[3] = "Mango";
        linkedHashMap[1] = "Apple";
        linkedHashMap[2] = "Banana";

        Console.WriteLine(
            "LinkedHashMap: " +
            FormatDictionary(linkedHashMap));
        // OUTPUT (in insertion order):
        // LinkedHashMap: {3=Mango, 1=Apple, 2=Banana}


        // =========================================================
        // 3. TREEMAP
        // =========================================================
        // - Sorted by key
        // - Based on a balanced tree
        //
        // Java TreeMap -> C# SortedDictionary

        SortedDictionary<int, string> treeMap =
            new SortedDictionary<int, string>();

        treeMap[3] = "Mango";
        treeMap[1] = "Apple";
        treeMap[2] = "Banana";

        Console.WriteLine(
            "TreeMap: " +
            FormatDictionary(treeMap));
        // OUTPUT (sorted by key):
        // TreeMap: {1=Apple, 2=Banana, 3=Mango}


        // =========================================================
        // 4. HASHTABLE
        // =========================================================
        // - Thread-safe in Java
        // - No null keys or null values
        // - Legacy Java class
        //
        // C# also has System.Collections.Hashtable.
        // It is a non-generic legacy collection.

        Hashtable hashTable =
            new Hashtable();

        hashTable[1] = "One";
        hashTable[2] = "Two";
        hashTable[3] = "Three";

        Console.WriteLine(
            "Hashtable: " +
            FormatHashtable(hashTable));
        // OUTPUT (order not guaranteed):
        // Hashtable: {3=Three, 2=Two, 1=One}


        // =========================================================
        // 5. COMPARISON EXAMPLE
        // =========================================================

        Dictionary<string, int> scores =
            new Dictionary<string, int>();

        scores["Ali"] = 90;
        scores["Sara"] = 85;
        scores["John"] = 95;

        Console.WriteLine(
            "HashMap Scores: " +
            FormatDictionary(scores));
        // OUTPUT:
        // HashMap Scores: {Ali=90, Sara=85, John=95}


        SortedDictionary<string, int> sortedScores =
            new SortedDictionary<string, int>(scores);

        Console.WriteLine(
            "TreeMap Scores: " +
            FormatDictionary(sortedScores));
        // OUTPUT:
        // TreeMap Scores: {Ali=90, John=95, Sara=85}


        Dictionary<string, int> orderedScores =
            new Dictionary<string, int>();

        orderedScores["Ali"] = 90;
        orderedScores["Sara"] = 85;
        orderedScores["John"] = 95;

        Console.WriteLine(
            "LinkedHashMap Scores: " +
            FormatDictionary(orderedScores));
        // OUTPUT:
        // LinkedHashMap Scores: {Ali=90, Sara=85, John=95}
    }


    // =========================================================
    // CHAPTER 13: SET
    // =========================================================

    public static void chapter13()
    {
        // =========================
        // LIST EXAMPLE
        // =========================

        Console.WriteLine("List example .....");

        List<string> list =
            new List<string>();

        list.Add("1");
        list.Add("2");
        list.Add("3");
        list.Add("4");
        list.Add("1"); // duplicate allowed in List

        foreach (string temp in list)
        {
            Console.WriteLine(temp);
        }

        // OUTPUT:
        // 1
        // 2
        // 3
        // 4
        // 1


        // =========================
        // SET EXAMPLE
        // =========================
        // removes duplicates

        Console.WriteLine("\nSet example .....");

        HashSet<string> set =
            new HashSet<string>();

        set.Add("1");
        set.Add("2");
        set.Add("3");
        set.Add("4");
        set.Add("1"); // duplicate ignored

        foreach (string temp in set)
        {
            Console.WriteLine(temp);
        }
        // OUTPUT:
        // 1
        // 2
        // 3
        // 4
        // NOTE: HashSet order is not guaranteed.


        // =========================
        // MAP EXAMPLE
        // =========================

        Console.WriteLine("\nMap example .....");

        Dictionary<int, string> map2 =
            new Dictionary<int, string>();

        map2[1] = "A";
        map2[2] = "B";
        map2[3] = "C";
        map2[1] = "D"; // overwrite key 1

        foreach (
            KeyValuePair<int, string> entry
            in map2)
        {
            Console.WriteLine(
                entry.Key +
                " -> " +
                entry.Value);
        }

        // OUTPUT:
        // 1 -> D
        // 2 -> B
        // 3 -> C


        // =========================================================
        // 1. CREATE A SET
        // =========================================================
        // HashSet stores unique values only.
        // Duplicates are ignored.

        HashSet<int> numbersSet =
            new HashSet<int>(
                new int[] { 1, 2, 3, 3, 4, 4, 5 });

        Console.WriteLine(
            "Initial Set: " +
            FormatSet(numbersSet));
        // OUTPUT:
        // Initial Set: [1, 2, 3, 4, 5]


        // =========================================================
        // 2. ADD VALUES (Add)
        // =========================================================
        // Inserts elements; duplicates ignored automatically

        HashSet<string> fruits =
            new HashSet<string>();

        fruits.Add("Apple");
        fruits.Add("Banana");
        fruits.Add("Mango");
        fruits.Add("Apple");

        Console.WriteLine(
            "After add(): " +
            FormatSet(fruits));
        // OUTPUT:
        // After add(): [Apple, Banana, Mango]
        // NOTE: HashSet order is not guaranteed.


        // =========================================================
        // 3. DIFFERENT WAYS TO INITIALIZE SET
        // =========================================================

        HashSet<string> set1 =
            new HashSet<string>(
                new string[]
                {
                    "Apple",
                    "Banana",
                    "Mango"
                });

        Console.WriteLine(
            "From Array: " +
            FormatSet(set1));


        HashSet<string> set2 =
            new HashSet<string>();

        set2.Add("Apple");
        set2.Add("Banana");
        set2.Add("Mango");

        Console.WriteLine(
            "Using Add(): " +
            FormatSet(set2));


        HashSet<string> set3 =
            new HashSet<string>(
                new string[]
                {
                    "A",
                    "B",
                    "A",
                    "C"
                });

        Console.WriteLine(
            "Duplicates Removed: " +
            FormatSet(set3));


        HashSet<char> set4 =
            new HashSet<char>();

        foreach (char c in "ABCA")
        {
            set4.Add(c);
        }

        Console.WriteLine(
            "From String: " +
            FormatSet(set4));
        // OUTPUT:
        // unique characters


        // =========================================================
        // 4. CHECK VALUE EXISTS (Contains)
        // =========================================================

        Console.WriteLine(
            "Has Banana? " +
            fruits.Contains("Banana"));

        Console.WriteLine(
            "Has Grapes? " +
            fruits.Contains("Grapes"));
        // OUTPUT:
        // Has Banana? True
        // Has Grapes? False


        // =========================================================
        // 5. DELETE VALUE (Remove)
        // =========================================================

        fruits.Remove("Banana");

        Console.WriteLine(
            "After remove: " +
            FormatSet(fruits));
        // OUTPUT:
        // Banana removed


        // =========================================================
        // 6. SIZE OF SET
        // =========================================================

        Console.WriteLine(
            "Set size: " +
            fruits.Count);
        // OUTPUT:
        // Set size: 2


        // =========================================================
        // 7. LOOP THROUGH SET
        // =========================================================

        // Java LinkedHashSet equivalent:
        // C# does not have a direct LinkedHashSet<T>.
        // List<T> can be used when insertion order is important.

        List<string> colors =
            new List<string>
            {
                "Red",
                "Green",
                "Blue"
            };

        foreach (string color in colors)
        {
            Console.WriteLine(color);
        }

        colors.ForEach(
            c => Console.WriteLine("Color: " + c));

        // OUTPUT:
        // Red
        // Green
        // Blue
        // Color: Red
        // Color: Green
        // Color: Blue


        // =========================================================
        // 8. CONVERT SET TO ARRAY
        // =========================================================

        HashSet<int> setNumbers =
            new HashSet<int>(
                new int[] { 10, 20, 30 });

        int[] arrayNumbers =
            setNumbers.ToArray();

        Console.WriteLine(
            FormatArray(arrayNumbers));
        // OUTPUT:
        // [10, 20, 30]


        // =========================================================
        // 9. REMOVE DUPLICATES FROM ARRAY
        // =========================================================

        int[] arr =
        {
            1, 2, 2, 3, 4, 4, 5
        };

        // LinkedHashSet equivalent:
        // use List + Contains to preserve insertion order.

        List<int> unique =
            new List<int>();

        foreach (int n in arr)
        {
            if (!unique.Contains(n))
            {
                unique.Add(n);
            }
        }

        Console.WriteLine(
            "Unique Array: " +
            FormatList(unique));
        // OUTPUT:
        // Unique Array: [1, 2, 3, 4, 5]


        // =========================================================
        // 10. CLEAR SET
        // =========================================================

        HashSet<int> tempSet =
            new HashSet<int>(
                new int[] { 1, 2, 3 });

        tempSet.Clear();

        Console.WriteLine(
            "After clear: " +
            FormatSet(tempSet));
        // OUTPUT:
        // After clear: []


        // =========================================================
        // 11. SET WITH OBJECTS
        // =========================================================

        HashSet<SetUser> userSet =
            new HashSet<SetUser>();

        SetUser u1 =
            new SetUser(1, "Ali");

        SetUser u2 =
            new SetUser(2, "Sara");

        userSet.Add(u1);
        userSet.Add(u2);
        userSet.Add(u1);

        Console.WriteLine(
            "User Set: " +
            FormatSet(userSet));
        // OUTPUT:
        // User Set:
        // {id=1, name='Ali'}, {id=2, name='Sara'}

        // NOTE:
        // C# HashSet<T> uses Equals() and GetHashCode()
        // for logical duplicate detection.
        //
        // The current SetUser class does not override them,
        // therefore duplicate detection is based on object
        // reference equality.
        //
        // To make C# behave like a Java User class with
        // equals() and hashCode(), override:
        //
        // Equals()
        // GetHashCode()


        // =========================================================
        // SUMMARY (C# SET METHODS)
        // =========================================================

        /*
            Add(value)       -> add element
            Remove(value)    -> delete element
            Contains()       -> check existence
            Count            -> number of elements
            Clear()          -> remove all elements
            foreach          -> iterate elements
        */
    }


    // =========================================================
    // HELPER METHODS
    // These methods are used only to make C# console output
    // look similar to Java collection output.
    // =========================================================

    private static string FormatList<T>(
        IEnumerable<T> collection)
    {
        return "[" +
               string.Join(", ", collection) +
               "]";
    }


    private static string FormatCollection<T>(
        IEnumerable<T> collection)
    {
        return "[" +
               string.Join(", ", collection) +
               "]";
    }


    private static string FormatSet<T>(
        IEnumerable<T> collection)
    {
        return "[" +
               string.Join(", ", collection) +
               "]";
    }


    private static string FormatArray<T>(
        IEnumerable<T> collection)
    {
        return "[" +
               string.Join(", ", collection) +
               "]";
    }


    private static string FormatLinkedList<T>(
        LinkedList<T> collection)
    {
        return "[" +
               string.Join(", ", collection) +
               "]";
    }


    private static string FormatStack<T>(
        Stack<T> stack)
    {
        // Stack<T> enumerates from top to bottom.
        // Reverse it here so the display resembles Java Stack.
        return "[" +
               string.Join(", ", stack.Reverse()) +
               "]";
    }


    private static string FormatDictionary<TKey, TValue>(
        IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
    {
        return "{" +
               string.Join(
                   ", ",
                   dictionary.Select(
                       x => $"{x.Key}={x.Value}")) +
               "}";
    }


    private static string FormatHashtable(
        Hashtable table)
    {
        List<string> entries =
            new List<string>();

        foreach (DictionaryEntry entry in table)
        {
            entries.Add(
                $"{entry.Key}={entry.Value}");
        }

        return "{" +
               string.Join(", ", entries) +
               "}";
    }


    // =========================================================
    // ADD UNIQUE
    // C# equivalent helper for Java LinkedHashSet behavior.
    // =========================================================

    private static void AddUnique<T>(
        List<T> list,
        T value)
    {
        if (!list.Contains(value))
        {
            list.Add(value);
        }
    }



}

//Objects Collections
public class ChapterListing
{
    class Person
    {
        public string name = "Ali";
        public int age = 25;
        public string city = "Karachi";
    }

    class Student
    {
        public string firstName;
        public string lastName;
        public int rollNum;
        public string emailAddress;
        public DateTime dateOfBirth;

        public Student(
            string firstName,
            string lastName,
            int rollNum,
            string emailAddress,
            DateTime dateOfBirth)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.rollNum = rollNum;
            this.emailAddress = emailAddress;
            this.dateOfBirth = dateOfBirth;
        }
    }

    // JAVA OBJECT
    public static void chapter14()
    {
        // 1. JAVA OBJECT (Equivalent of JS Object)

        Person person = new Person();
        Console.WriteLine("JAVA OBJECT EXAMPLE: " + person.name); // OUTPUT: Ali


        // 2. JSON STRING
        string jsonData = "{\"name\":\"Ali\",\"age\":25,\"city\":\"Karachi\"}";
        Console.WriteLine("JSON STRING: " + jsonData);
        // OUTPUT: {"name":"Ali","age":25,"city":"Karachi"}


        // 3. JSON → OBJECT (Parsing)
        string jsonData1 = "{\"name\":\"Sara\",\"grade\":\"A\"}";

        try
        {
            // C# equivalent of Java JSONObject requires System.Text.Json
            //using System.Text.Json;

            JsonDocument obj = JsonDocument.Parse(jsonData1);

            Console.WriteLine("PARSED NAME: " +
                obj.RootElement.GetProperty("name").GetString());

            Console.WriteLine("PARSED GRADE: " +
                obj.RootElement.GetProperty("grade").GetString());
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex.Message);
        }

        // OUTPUT:
        // PARSED NAME: Sara
        // PARSED GRADE: A


        // 4. OBJECT → JSON (Serialization)
        try
        {
            //using System.Text.Json;

            var student = new Dictionary<string, object>
        {
            { "name", "Sara" },
            { "grade", "A" }
        };

            string jsonString = JsonSerializer.Serialize(student);
            Console.WriteLine("STRINGIFIED JSON: " + jsonString);
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex.Message);
        }
        // OUTPUT: {"name":"Sara","grade":"A"}


        // 5. TYPE CHECK (C# Equivalent)
        Console.WriteLine(person.GetType().Name); // OUTPUT: Person
        Console.WriteLine(((object)jsonData).GetType().Name); // OUTPUT: String
    }


    // ============================================================
    // C# COLLECTIONS → JAVA
    // Array, List, LinkedList, HashMap, HashSet...
    // ============================================================

    public static void chapter15()
    {
        // =========================================================
        // ARRAY
        // =========================================================
        // Array stores fixed-size data.
        // Best when size is known and fast index access is needed.
        // Example Use:
        // Store months, days, fixed exam subjects etc.
        // =========================================================

        Console.WriteLine("========== ARRAY EXAMPLE ==========");

        // Fixed size array of 3 elements
        string[] fruits = { "Apple", "Banana", "Mango" };

        // Access by index
        Console.WriteLine("First Fruit : " + fruits[0]);
        // OUTPUT: First Fruit : Apple

        // Update element
        fruits[1] = "Orange";

        // Loop through array
        for (int i = 0; i < fruits.Length; i++)
        {
            Console.WriteLine(fruits[i]);
        }
        // OUTPUT:
        // Apple
        // Orange
        // Mango


        // =========================================================
        // ARRAYLIST
        // =========================================================
        // Java ArrayList equivalent in C# is List<T>.
        // List<T> is a dynamic resizable array.
        // Best for frequent searching and random access.
        // Fast reading using list[index].
        // Example Use:
        // Student list, product list, employee list etc.
        // =========================================================

        Console.WriteLine("\n========== ARRAYLIST EXAMPLE ==========");

        List<string> students = new List<string>();

        // Add elements
        students.Add("Ali");
        students.Add("Ahmed");
        students.Add("Sara");

        // Insert at specific position
        students.Insert(1, "Bilal");

        // Access element
        Console.WriteLine("Student at index 0 : " + students[0]);
        // OUTPUT: Student at index 0 : Ali

        // Update value
        students[2] = "Usman";

        // Remove element
        students.Remove("Sara");

        // Total size
        Console.WriteLine("Total Students : " + students.Count);
        // OUTPUT: Total Students : 3

        // Loop through list
        for (int i = 0; i < students.Count; i++)
        {
            Console.WriteLine(students[i]);
        }
        // OUTPUT:
        // Ali
        // Bilal
        // Usman


        // =========================================================
        // LINKEDLIST
        // =========================================================
        // LinkedList<T> stores data with node connections.
        // Best when frequent insertion/removal is needed.
        // Slow random access compared to List<T>.
        // Example Use:
        // Music playlist, browser history, queue systems.
        // =========================================================

        Console.WriteLine("\n========== LINKEDLIST EXAMPLE ==========");

        LinkedList<string> cities = new LinkedList<string>();

        cities.AddLast("Karachi");
        cities.AddLast("Lahore");
        cities.AddLast("Islamabad");

        // Add at beginning
        cities.AddFirst("Peshawar");

        // Add at end
        cities.AddLast("Quetta");

        // Remove first item
        cities.RemoveFirst();

        // Get first element
        Console.WriteLine("First City : " + cities.First.Value);
        // OUTPUT: First City : Lahore

        // Loop
        foreach (string city in cities)
        {
            Console.WriteLine(city);
        }
        // OUTPUT:
        // Lahore
        // Islamabad
        // Quetta


        // =========================================================
        // VECTOR
        // =========================================================
        // Java Vector equivalent in modern C#:
        // List<T> is normally preferred.
        //
        // If thread-safe collection behavior is required,
        // use System.Collections.Concurrent collections.
        // =========================================================

        Console.WriteLine("\n========== VECTOR EXAMPLE ==========");

        List<int> marks = new List<int>();

        marks.Add(80);
        marks.Add(90);
        marks.Add(70);

        // Insert value
        marks.Insert(1, 85);

        // Remove value at index 2
        marks.RemoveAt(2);

        // Check existence
        Console.WriteLine("Contains 90 ? " + marks.Contains(90));
        // OUTPUT: Contains 90 ? True

        // Loop
        foreach (int mark in marks)
        {
            Console.WriteLine(mark);
        }
        // OUTPUT:
        // 80
        // 85
        // 90


        // =========================================================
        // HASHMAP
        // =========================================================
        // Java HashMap equivalent in C# is Dictionary<TKey,TValue>.
        // Stores key-value pairs.
        // Fast searching using key.
        // No duplicate keys allowed.
        // Example Use:
        // StudentID -> StudentName
        // BookID -> BookName
        // =========================================================

        Console.WriteLine("\n========== HASHMAP EXAMPLE ==========");

        Dictionary<int, string> books = new Dictionary<int, string>();

        // Add key-value pairs
        books.Add(101, "Java");
        books.Add(102, "C#");
        books.Add(103, "Python");

        // Access by key
        Console.WriteLine("Book 101 : " + books[101]);
        // OUTPUT: Book 101 : Java

        // Update value
        books[102] = "Advanced C#";

        // Remove by key
        books.Remove(103);

        // Check key existence
        Console.WriteLine("Contains 101 ? " + books.ContainsKey(101));
        // OUTPUT: Contains 101 ? True

        // Loop through HashMap
        foreach (int key in books.Keys)
        {
            Console.WriteLine(key + " => " + books[key]);
        }
        // OUTPUT:
        // 101 => Java
        // 102 => Advanced C#


        // =========================================================
        // HASHSET
        // =========================================================
        // HashSet<T> stores unique values only.
        // Duplicate data automatically ignored.
        // Example Use:
        // Unique emails, unique usernames, unique tags.
        // =========================================================

        Console.WriteLine("\n========== HASHSET EXAMPLE ==========");

        HashSet<string> emails = new HashSet<string>();

        emails.Add("a@gmail.com");
        emails.Add("b@gmail.com");
        emails.Add("a@gmail.com");

        // Duplicate value ignored automatically

        foreach (string email in emails)
        {
            Console.WriteLine(email);
        }
        // OUTPUT:
        // a@gmail.com
        // b@gmail.com
        // (Order is not guaranteed)


        // =========================================================
        // STACK
        // =========================================================
        // Stack follows LIFO:
        // Last In First Out
        // Example Use:
        // Undo system, browser back button.
        // =========================================================

        Console.WriteLine("\n========== STACK EXAMPLE ==========");

        Stack<string> stack = new Stack<string>();

        // Add items
        stack.Push("First");
        stack.Push("Second");
        stack.Push("Third");

        // Get top item
        Console.WriteLine("Top : " + stack.Peek());
        // OUTPUT: Top : Third

        // Remove top item
        stack.Pop();

        // Loop
        foreach (string item in stack)
        {
            Console.WriteLine(item);
        }
        // OUTPUT:
        // Second
        // First


        // =========================================================
        // QUEUE
        // =========================================================
        // Queue follows FIFO:
        // First In First Out
        // Example Use:
        // Customer service queue, printing queue.
        // =========================================================

        Console.WriteLine("\n========== QUEUE EXAMPLE ==========");

        Queue<string> queue = new Queue<string>();

        // Add items
        queue.Enqueue("Customer 1");
        queue.Enqueue("Customer 2");
        queue.Enqueue("Customer 3");

        // Get first item
        Console.WriteLine("First : " + queue.Peek());
        // OUTPUT: First : Customer 1

        // Remove first item
        queue.Dequeue();

        // Loop
        foreach (string item in queue)
        {
            Console.WriteLine(item);
        }
        // OUTPUT:
        // Customer 2
        // Customer 3


        // =========================================================
        // TREEMAP
        // =========================================================
        // Java TreeMap equivalent in C# is SortedDictionary<TKey,TValue>.
        // Stores sorted key-value pairs.
        // Automatically sorts keys.
        // Slower than Dictionary but keeps sorted order.
        // Example Use:
        // Ranking system, sorted reports.
        // =========================================================

        Console.WriteLine("\n========== TREEMAP EXAMPLE ==========");

        SortedDictionary<int, string> employees =
            new SortedDictionary<int, string>();

        employees.Add(3, "Ali");
        employees.Add(1, "Ahmed");
        employees.Add(2, "Sara");

        // Automatically sorted by keys

        foreach (int id in employees.Keys)
        {
            Console.WriteLine(id + " => " + employees[id]);
        }
        // OUTPUT:
        // 1 => Ahmed
        // 2 => Sara
        // 3 => Ali


        // =========================================================
        // PRIORITYQUEUE
        // =========================================================
        // C# PriorityQueue<TElement,TPriority> is available in
        // modern .NET versions.
        //
        // PriorityQueue processes the lowest priority value first.
        // Example Use:
        // Task scheduler, hospital emergency system.
        // =========================================================

        Console.WriteLine("\n========== PRIORITY QUEUE EXAMPLE ==========");

        PriorityQueue<int, int> numbers =
            new PriorityQueue<int, int>();

        numbers.Enqueue(30, 30);
        numbers.Enqueue(10, 10);
        numbers.Enqueue(20, 20);

        // Remove in priority order

        while (numbers.Count > 0)
        {
            Console.WriteLine(numbers.Dequeue());
        }
        // OUTPUT:
        // 10
        // 20
        // 30


        // =========================================================
        // ITERATOR
        // =========================================================
        // C# equivalent of Java Iterator<T> is IEnumerator<T>.
        // However, IEnumerator<T> does NOT support remove().
        //
        // For safe removal while iterating a List<T>, use
        // RemoveAll(), or iterate backwards and use RemoveAt().
        // =========================================================

        Console.WriteLine("\n========== ITERATOR EXAMPLE ==========");

        List<string> names = new List<string>();

        names.Add("Ali");
        names.Add("");
        names.Add("Ahmed");

        // C# IEnumerator equivalent
        IEnumerator<string> it = names.GetEnumerator();

        while (it.MoveNext())
        {
            string value = it.Current;

            Console.WriteLine(value);
        }

        // Safe removal from List<T>
        names.RemoveAll(value => value == "");

        // Print final values
        foreach (string name in names)
        {
            Console.WriteLine(name);
        }
        // OUTPUT:
        // Ali
        // Ahmed
    }


    // ============================================================
    // C# LINQ → JAVA STREAM EQUIVALENT
    // ============================================================

    public static void chapter16()
    {


        // Student class defines structure for each student object

        List<Student> students = new List<Student>
    {
        new Student(
            "Ali",
            "Khan",
            1,
            "ali@gmail.com",
            new DateTime(2000, 1, 10)),

        new Student(
            "Sara",
            "Ahmed",
            12,
            "sara@yahoo.com",
            new DateTime(1999, 5, 21)),

        new Student(
            "John",
            "Doe",
            15,
            "john@gmail.com",
            new DateTime(2001, 3, 15))
    };

        // OUTPUT DATA:
        // 3 students created in list
        // Ali (roll 1)
        // Sara (roll 12)
        // John (roll 15)


        // 1. Enhanced for-each loop (most common)
        foreach (Student s in students)
        {
            Console.WriteLine(
                s.firstName + " (roll " + s.rollNum + ")");
        }

        // OUTPUT:
        // Ali (roll 1)
        // Sara (roll 12)
        // John (roll 15)


        // 2. Java forEach() equivalent in C# LINQ
        students.ForEach(s =>
            Console.WriteLine(
                s.firstName + " (roll " + s.rollNum + ")")
        );

        // OUTPUT:
        // Ali (roll 1)
        // Sara (roll 12)
        // John (roll 15)


        // 3. Java stream().map().forEach() equivalent
        students
            .Select(s => s.firstName + " (roll " + s.rollNum + ")")
            .ToList()
            .ForEach(Console.WriteLine);

        // OUTPUT:
        // Ali (roll 1)
        // Sara (roll 12)
        // John (roll 15)


        // ================================
        // 1. Select() equivalent of map() in Java
        // ================================

        List<string> emails = students
            .Select(s => s.emailAddress)
            .ToList();

        // OUTPUT:
        // ["ali@gmail.com", "sara@yahoo.com", "john@gmail.com"]
        // Explanation: extracts only emailAddress from each Student


        // ====================================================
        // 2. Where() equivalent of filter() in Java
        // ====================================================

        List<Student> filtered = students
            .Where(s => s.rollNum > 10)
            .ToList();

        // OUTPUT:
        // [Sara, John]
        // Explanation: only students with rollNum > 10


        // ====================================================
        // 3. Any() equivalent of anyMatch() in Java
        // ====================================================

        bool exists = students
            .Any(s => s.emailAddress.Contains("gmail"));

        // OUTPUT:
        // true
        // Explanation: at least one student has gmail account


        // ====================================================
        // 4. FirstOrDefault() equivalent of findFirst() in Java
        // ====================================================

        Student student = students
            .Where(s => s.rollNum == 12)
            .FirstOrDefault();

        // OUTPUT:
        // Sara Ahmed (roll 12)
        // Explanation: first match where rollNum == 12


        // ====================================================
        // 5. GroupBy() equivalent of groupingBy() in Java
        // ====================================================

        var grouped = students
            .GroupBy(s => s.lastName)
            .ToDictionary(
                g => g.Key,
                g => g.ToList()
            );

        // OUTPUT:
        // {
        //   Khan=[Ali],
        //   Ahmed=[Sara],
        //   Doe=[John]
        // }

        // Explanation: grouped students by lastName


        // ====================================================
        // 6. Take() equivalent of limit() in Java
        // ====================================================

        List<Student> top2 = students
            .Take(2)
            .ToList();

        // OUTPUT:
        // [Ali, Sara]
        // Explanation: first 2 students only


        // ====================================================
        // 7. Complex pipeline (filter + sort + map) in Java
        //    equivalent of C# LINQ
        // ====================================================

        List<string> result = students
            .Where(s => s.rollNum > 5)
            .OrderBy(s => s.firstName)
            .Select(s => s.emailAddress)
            .ToList();

        // OUTPUT:
        // ["john@gmail.com", "sara@yahoo.com"]
        // Explanation:
        // Step 1: filter rollNum > 5 → Sara, John
        // Step 2: sort by firstName → John, Sara
        // Step 3: map/select to email → emails list


        Console.WriteLine(emails);
        // NOTE:
        // C# List<T>.ToString() does not print elements like Java List.
        // For actual element output use string.Join().
        // OUTPUT:
        // System.Collections.Generic.List`1[System.String]

        Console.WriteLine(string.Join(", ", emails));
        // OUTPUT:
        // ali@gmail.com, sara@yahoo.com, john@gmail.com


        Console.WriteLine(exists);
        // OUTPUT:
        // True


        // Print grouped values
        foreach (var group in grouped)
        {
            Console.WriteLine(
                group.Key + "=[" +
                string.Join(", ", group.Value.Select(s => s.firstName)) +
                "]");
        }

        // OUTPUT:
        // Khan=[Ali]
        // Ahmed=[Sara]
        // Doe=[John]


        Console.WriteLine(
            "[" + string.Join(", ", result) + "]");

        // OUTPUT:
        // [john@gmail.com, sara@yahoo.com]
    }





}

//Complex Objcts
public class ChapterComplex
{
    class Mark
    {
        public string Subject;
        public int Score;

        public Mark(string subject, int score)
        {
            Subject = subject;
            Score = score;
        }
    }


    class Student
    {
        public string FirstName;
        public string LastName;
        public int RollNum;
        public List<string> EmailAddresses;
        public List<Mark> Marks;

        public Student(
            string firstName,
            string lastName,
            int rollNum,
            List<string> emailAddresses,
            List<Mark> marks)
        {
            FirstName = firstName;
            LastName = lastName;
            RollNum = rollNum;
            EmailAddresses = emailAddresses;
            Marks = marks;
        }
    }

    // =========================================================
    // STUDENT COMPLEX ITERATION
    // Java -> C# Equivalent
    // =========================================================
    public static void ChapterStudentComplexIteration()
    {



        ///////////////////////////////////////////////

        List<Student> students = new List<Student>();

        students.Add(new Student(
                "Ali",
                "Khan",
                1,
                new List<string>
                {
                "ali@gmail.com",
                "abc@gmail.com"
                },
                new List<Mark>
                {
                new Mark("english", 78),
                new Mark("maths", 90),
                new Mark("science", 80)
                }
            ));

        students.Add(new Student(
            "Sara",
            "Ahmed",
            12,
            new List<string>
            {
                "sara@gmail.com",
                "pqr@gmail.com"
            },
            new List<Mark>
            {
                new Mark("english", 98),
                new Mark("maths", 56),
                new Mark("science", 68)
            }
        ));

        students.Add(new Student(
            "John",
            "Doe",
            15,
            new List<string>
            {
                "john@gmail.com",
                "xyz@gmail.com"
            },
            new List<Mark>
            {
                new Mark("english", 55),
                new Mark("maths", 99),
                new Mark("science", 22)
            }
        ));

        //Iteration using for-each loop (Most common)
        /// ////////////////////////////////////

        foreach (Student s in students)
        {
            Console.WriteLine(
                "Student: " + s.FirstName + " " + s.LastName);

            foreach (string email in s.EmailAddresses)
            {
                Console.WriteLine("  Email: " + email);
            }

            foreach (Mark m in s.Marks)
            {
                Console.WriteLine(
                    "  " + m.Subject + " = " + m.Score);
            }
        }

        // OUTPUT:
        // Student: Ali Khan
        //   Email: ali@gmail.com
        //   Email: abc@gmail.com
        //   english = 78
        //   maths = 90
        //   science = 80
        // Student: Sara Ahmed
        //   Email: sara@gmail.com
        //   Email: pqr@gmail.com
        //   english = 98
        //   maths = 56
        //   science = 68
        // Student: John Doe
        //   Email: john@gmail.com
        //   Email: xyz@gmail.com
        //   english = 55
        //   maths = 99
        //   science = 22


        //Iteration using classic for loop
        /// /////////////////////////////////////

        for (int ai = 0; ai < students.Count; ai++)
        {
            Student s = students[ai];

            Console.WriteLine(
                "Student: " + s.FirstName);

            for (int aj = 0; aj < s.Marks.Count; aj++)
            {
                Mark m = s.Marks[aj];

                Console.WriteLine(
                    "  " + m.Subject + ": " + m.Score);
            }
        }

        // OUTPUT:
        // Student: Ali
        //   english: 78
        //   maths: 90
        //   science: 80
        // Student: Sara
        //   english: 98
        //   maths: 56
        //   science: 68
        // Student: John
        //   english: 55
        //   maths: 99
        //   science: 22


        //Iteration using while loop
        /// //////////////////////////////////////////

        int i = 0;

        while (i < students.Count)
        {
            Student s = students[i];

            Console.WriteLine(
                "Student: " + s.FirstName);

            int j = 0;

            while (j < s.EmailAddresses.Count)
            {
                Console.WriteLine(
                    "  Email: " + s.EmailAddresses[j]);

                j++;
            }

            i++;
        }

        // OUTPUT:
        // Student: Ali
        //   Email: ali@gmail.com
        //   Email: abc@gmail.com
        // Student: Sara
        //   Email: sara@gmail.com
        //   Email: pqr@gmail.com
        // Student: John
        //   Email: john@gmail.com
        //   Email: xyz@gmail.com


        //Calculate average marks per student (complex map)
        /// ////////////////////////////////////

        students
            .Select(s =>
            {
                double avg = s.Marks
                    .Select(m => m.Score)
                    .DefaultIfEmpty(0)
                    .Average();

                return s.FirstName + " avg = " + avg;
            })
            .ToList()
            .ForEach(Console.WriteLine);

        // OUTPUT:
        // Ali avg = 82.66666666666667
        // Sara avg = 74
        // John avg = 58.666666666666664
    }


    // =========================================================
    // STUDENT COURSE SCORE SYSTEM
    // Nested Dictionary + LINQ Analytics
    // =========================================================
    public static void Chapter17()
    {
        /*
            TITLE:
            Student Course Score System using Nested Dictionary + LINQ Analytics

            DESCRIPTION:
            This program demonstrates advanced usage of Dictionary in C# including:
            - Nested Dictionary (Student -> Subject -> Marks)
            - LINQ for calculations
            - Average marks per student
            - Finding topper using LINQ
            - Subject-wise highest score

            Java equivalent:
            HashMap<String, HashMap<String, Integer>>
        */


        // Student -> (Subject -> Score)
        Dictionary<string, Dictionary<string, int>> studentMarks =
            new Dictionary<string, Dictionary<string, int>>();

        // OUTPUT: Empty outer dictionary created
        // Structure: { }


        Dictionary<string, int> alice =
            new Dictionary<string, int>();

        // OUTPUT: Alice subject-score dictionary created

        alice["Math"] = 85;
        // OUTPUT: Alice -> Math = 85

        alice["English"] = 78;
        // OUTPUT: Alice -> English = 78

        alice["Science"] = 92;
        // OUTPUT: Alice -> Science = 92


        Dictionary<string, int> bob =
            new Dictionary<string, int>();

        // OUTPUT: Bob subject-score dictionary created

        bob["Math"] = 65;
        // OUTPUT: Bob -> Math = 65

        bob["English"] = 72;
        // OUTPUT: Bob -> English = 72

        bob["Science"] = 60;
        // OUTPUT: Bob -> Science = 60


        Dictionary<string, int> charlie =
            new Dictionary<string, int>();

        // OUTPUT: Charlie subject-score dictionary created

        charlie["Math"] = 95;
        // OUTPUT: Charlie -> Math = 95

        charlie["English"] = 88;
        // OUTPUT: Charlie -> English = 88

        charlie["Science"] = 91;
        // OUTPUT: Charlie -> Science = 91


        studentMarks["Alice"] = alice;
        // OUTPUT: Alice added to main dictionary

        studentMarks["Bob"] = bob;
        // OUTPUT: Bob added to main dictionary

        studentMarks["Charlie"] = charlie;
        // OUTPUT: Charlie added to main dictionary


        // Print all students
        Console.WriteLine("All Student Data:");
        // OUTPUT: All Student Data:


        foreach (var item in studentMarks)
        {
            Console.WriteLine(
                item.Key + " -> {" +
                string.Join(
                    ", ",
                    item.Value.Select(x =>
                        x.Key + "=" + x.Value)) +
                "}");
        }

        // OUTPUT:
        // Alice -> {Math=85, English=78, Science=92}
        // Bob -> {Math=65, English=72, Science=60}
        // Charlie -> {Math=95, English=88, Science=91}


        // Average marks per student
        Dictionary<string, double> avgMarks =
            studentMarks.ToDictionary(
                entry => entry.Key,
                entry => entry.Value.Values
                    .DefaultIfEmpty(0)
                    .Average());

        // OUTPUT LOGIC:
        // Alice average = (85 + 78 + 92) / 3 = 85.0
        // Bob average = (65 + 72 + 60) / 3 = 65.67
        // Charlie average = (95 + 88 + 91) / 3 = 91.33


        Console.WriteLine("\nAverage Marks:");
        // OUTPUT: Average Marks:


        foreach (var item in avgMarks)
        {
            Console.WriteLine(
                item.Key + " -> " + item.Value);
        }

        // OUTPUT:
        // Alice -> 85
        // Bob -> 65.66666666666667
        // Charlie -> 91.33333333333333


        // Topper
        string topper = avgMarks
            .OrderByDescending(x => x.Value)
            .First()
            .Key;

        // OUTPUT LOGIC:
        // Highest average = Charlie (91.33)

        Console.WriteLine("\nTopper: " + topper);
        // OUTPUT: Topper: Charlie


        // Subject-wise highest score
        Dictionary<string, int> subjectTopScores =
            new Dictionary<string, int>();

        // OUTPUT: Empty dictionary for storing subject-wise max scores


        foreach (var student in studentMarks.Values)
        {
            foreach (var subject in student)
            {
                if (!subjectTopScores.ContainsKey(subject.Key) ||
                    subjectTopScores[subject.Key] < subject.Value)
                {
                    subjectTopScores[subject.Key] =
                        subject.Value;
                }
            }
        }

        // OUTPUT LOGIC:
        // Math    -> max(85, 65, 95) = 95
        // English -> max(78, 72, 88) = 88
        // Science -> max(92, 60, 91) = 92


        Console.WriteLine("\nSubject-wise Top Scores:");
        // OUTPUT: Subject-wise Top Scores:


        foreach (var item in subjectTopScores)
        {
            Console.WriteLine(
                item.Key + " -> " + item.Value);
        }

        // OUTPUT:
        // Math -> 95
        // English -> 88
        // Science -> 92
    }


    // =========================================================
    // JAVASCRIPT -> JAVA -> C# DIFFERENCES
    // =========================================================
    public static void Chapter18()
    {
        /*
         *******************************************************
         FINAL SUMMARY

         JAVASCRIPT -> JAVA -> C# DIFFERENCES:

         - Number:
           Java -> Integer / Double / Long
           C#   -> int / double / long / decimal

         - Math:
           Java -> java.lang.Math
           C#   -> System.Math

         - String:
           Java -> java.lang.String
           C#   -> System.String / string

         - Array:
           Java -> int[] / ArrayList<>
           C#   -> int[] / List<int>

         - Object:
           Java -> HashMap / POJO class
           C#   -> Dictionary / class

         - Map:
           Java -> HashMap / TreeMap
           C#   -> Dictionary / SortedDictionary

         - Set:
           Java -> HashSet / TreeSet
           C#   -> HashSet / SortedSet

         - Date:
           Java -> java.time
           C#   -> System.DateTime / DateOnly / TimeOnly

         - RegExp:
           Java -> java.util.regex.Pattern
           C#   -> System.Text.RegularExpressions.Regex

         - TypedArray:
           Java -> ByteBuffer + primitive arrays
           C#   -> byte[] / Memory<T> / Span<T>

         - JSON:
           Java -> Jackson / Gson
           C#   -> System.Text.Json / Newtonsoft.Json

         - Promise:
           Java -> CompletableFuture
           C#   -> Task / Task<T>

         - async/await:
           Java -> CompletableFuture / reactive
           C#   -> async / await / Task

         - console.log():
           Java -> System.out.println()
           C#   -> Console.WriteLine()

         - typeof:
           Java -> instanceof / getClass()
           C#   -> is / GetType() / typeof()

         - undefined:
           Java -> null (no direct equivalent)
           C#   -> null

         - null:
           Java -> null
           C#   -> null

         - function:
           Java -> method
           C#   -> method

         - arrow function:
           Java -> lambda expression
           C#   -> lambda expression

         - class:
           Java -> class
           C#   -> class

         - prototype:
           Java -> inheritance via extends
           C#   -> inheritance via :

         - module:
           Java -> package + import
           C#   -> namespace + using

         - fetch API:
           Java -> HttpClient
           C#   -> HttpClient

         - setTimeout():
           Java -> ScheduledExecutorService
           C#   -> Task.Delay / Timer

         - localStorage:
           Java -> No direct equivalent
           C#   -> File / DB / Cache

         - DOM manipulation:
           Java -> Swing / JavaFX / Web
           C#   -> WinForms / WPF / ASP.NET

         - event loop:
           Java -> Thread / ExecutorService
           C#   -> Task / ThreadPool

         - error handling:
           Java -> try/catch + checked exceptions
           C#   -> try/catch

         JAVASCRIPT FEATURES NOT DIRECTLY AVAILABLE IN JAVA/C#:

         - map/filter/reduce:
           Java -> Stream API
           C#   -> LINQ

         - dynamic typing:
           Java -> static typing
           C#   -> static typing / dynamic available

         - hoisting:
           Java -> not supported
           C#   -> not supported

         - prototype chaining:
           Java -> class inheritance
           C#   -> class inheritance

         - loosely typed comparisons:
           Java -> strict typing
           C#   -> strict typing

         - browser APIs:
           Java -> not available in core Java
           C#   -> not available in core .NET

         - automatic type coercion:
           Java -> explicit conversion
           C#   -> explicit conversion

         - JSON dynamic objects:
           Java -> POJOs / Map / Jackson / Gson
           C#   -> classes / Dictionary / JsonDocument

         - flexible object structure:
           Java -> fixed class structure
           C#   -> fixed class structure
         */


        // =====================================================
        // NUMBER
        // Java -> Integer, Double, Long
        // C#   -> int, double, long
        // =====================================================

        int x = 10;
        double y = 10.5;

        // Java:
        // Object xo = 123;
        // System.out.println(xo instanceof Integer);

        // C# equivalent:
        object xo = 123;

        Console.WriteLine(xo is int);
        // OUTPUT: True


        Console.WriteLine(double.IsNaN(y));
        // OUTPUT: False


        Console.WriteLine(int.Parse("123"));
        // OUTPUT: 123


        Console.WriteLine(double.Parse("12.5"));
        // OUTPUT: 12.5


        Console.WriteLine(int.MaxValue);
        // OUTPUT: 2147483647


        Console.WriteLine(int.MinValue);
        // OUTPUT: -2147483648


        Console.WriteLine(100.ToString());
        // OUTPUT: 100


        Console.WriteLine(
            Convert.ToString(10, 2));
        // OUTPUT: 1010


        Console.WriteLine(
            Convert.ToString(255, 16));
        // OUTPUT: ff


        Console.WriteLine(
            12.3456.ToString("F2"));
        // OUTPUT: 12.35


        Console.WriteLine(
            1234.5678.ToString("G4"));
        // OUTPUT: 1235


        // NOTE:
        // Java:
        // num.toFixed()
        // num.toPrecision()
        // num.toExponential()
        //
        // C# alternatives:
        // ToString("F2")
        // ToString("G4")
        // ToString("E2")


        // =====================================================
        // MATH API
        // Java -> java.lang.Math
        // C#   -> System.Math
        // =====================================================

        // =========================
        // INPUT VARIABLES
        // =========================

        int a = -10;
        int b = 2;
        int c = 3;
        int d = 16;
        int e = 27;
        double xx = 10.6;
        double yy = 10.9;
        double zz = 10.1;
        int m = 10;
        int n = 20;
        int p = 5;
        int q = 3;
        int r = 4;


        // =========================
        // MATH OPERATIONS
        // =========================

        Console.WriteLine(Math.Abs(a));
        // OUTPUT: 10


        Console.WriteLine(Math.Pow(b, c));
        // OUTPUT: 8


        Console.WriteLine(Math.Sqrt(d));
        // OUTPUT: 4


        Console.WriteLine(
            Math.Round(Math.Pow(e, 1.0 / 3.0)));
        // OUTPUT: 3


        Console.WriteLine(
            Math.Round(xx));
        // OUTPUT: 11


        Console.WriteLine(
            Math.Floor(yy));
        // OUTPUT: 10


        Console.WriteLine(
            Math.Ceiling(zz));
        // OUTPUT: 11


        Console.WriteLine(
            new Random().NextDouble());
        // OUTPUT:
        // Random value between 0.0 and 1.0


        Console.WriteLine(Math.Max(m, n));
        // OUTPUT: 20


        Console.WriteLine(Math.Min(m, n));
        // OUTPUT: 10


        Console.WriteLine(Math.PI);
        // OUTPUT: 3.141592653589793


        Console.WriteLine(Math.E);
        // OUTPUT: 2.718281828459045


        Console.WriteLine(Math.Sign(a));
        // OUTPUT: -1


        Console.WriteLine(Math.Log(m));
        // OUTPUT: 2.302585092994046


        Console.WriteLine(Math.Log10(m));
        // OUTPUT: 1


        // =========================
        // INPUT VARIABLES
        // =========================

        int angleSin = 90;
        int angleCos = 0;
        int angleTan = 45;

        int pp = 3;
        int rr = 4;


        // =========================
        // STEP 1: SIN (90°)
        // =========================

        double sinRadians =
            angleSin * Math.PI / 180.0;

        double sinResult =
            Math.Sin(sinRadians);

        Console.WriteLine(sinResult);
        // OUTPUT: 1


        // =========================
        // STEP 2: COS (0°)
        // =========================

        double cosRadians =
            angleCos * Math.PI / 180.0;

        double cosResult =
            Math.Cos(cosRadians);

        Console.WriteLine(cosResult);
        // OUTPUT: 1


        // =========================
        // STEP 3: TAN (45°)
        // =========================

        double tanRadians =
            angleTan * Math.PI / 180.0;

        double tanResult =
            Math.Tan(tanRadians);

        Console.WriteLine(tanResult);
        // OUTPUT: 0.9999999999999999
        // (~1.0)


        // =========================
        // STEP 4: HYPOTENUSE (3,4)
        // =========================

        double hypotenuse =
            Math.Sqrt(
                (pp * pp) +
                (rr * rr));

        Console.WriteLine(hypotenuse);
        // OUTPUT: 5


        // =====================================================
        // STRING
        // Java -> String
        // C#   -> string
        // =====================================================

        string str = " Hello Java ";


        Console.WriteLine(str.Length);
        // OUTPUT: 12


        Console.WriteLine(str.ToUpper());
        // OUTPUT:  HELLO JAVA 


        Console.WriteLine(str.ToLower());
        // OUTPUT:  hello java 


        Console.WriteLine(str.Trim());
        // OUTPUT: Hello Java


        Console.WriteLine(
            str.Contains("Hello"));
        // OUTPUT: True


        Console.WriteLine(
            str.IndexOf("H"));
        // OUTPUT: 1


        Console.WriteLine(
            str.LastIndexOf("l"));
        // OUTPUT: 9


        Console.WriteLine(
            str.Replace("Java", "World"));
        // OUTPUT:  Hello World 


        Console.WriteLine(
            str.Substring(2, 3));
        // OUTPUT: ell


        Console.WriteLine(
            str.StartsWith(" H"));
        // OUTPUT: True


        Console.WriteLine(
            str.EndsWith(" "));
        // OUTPUT: True


        // Java:
        // str.repeat(2)

        Console.WriteLine(
            string.Concat(
                Enumerable.Repeat(str, 2)));
        // OUTPUT:  Hello Java  Hello Java 


        string[] parts =
            str.Trim().Split(" ");

        Console.WriteLine(
            "[" + string.Join(", ", parts) + "]");
        // OUTPUT: [Hello, Java]


        Console.WriteLine(
            Convert.ToString(65));
        // OUTPUT: 65


        Console.WriteLine(
            (char)65);
        // OUTPUT: A


        // NOTE:
        // Java does NOT have some JavaScript string methods directly.
        //
        // C# alternatives include:
        // PadLeft()
        // PadRight()
        // Substring()
        // Replace()
        // String.Format()
        // StringBuilder


        // =====================================================
        // ARRAY
        // C# fixed-size arrays
        // =====================================================

        int[] arr =
        {
            1,
            2,
            3
        };

        Console.WriteLine(
            "[" + string.Join(", ", arr) + "]");
        // OUTPUT: [1, 2, 3]


        List<int> list =
            new List<int>();

        list.Add(1);
        list.Add(2);

        list.Remove(1);

        Console.WriteLine(
            "[" + string.Join(", ", list) + "]");
        // OUTPUT: [2]


        list.ForEach(
            value => Console.WriteLine(value));

        // OUTPUT:
        // 2


        List<int> mapped =
            list
                .Select(nn => nn * 2)
                .ToList();

        Console.WriteLine(
            "[" + string.Join(", ", mapped) + "]");

        // OUTPUT:
        // [4]


        list.Sort();

        list.Reverse();

        Console.WriteLine(
            "[" + string.Join(", ", list) + "]");

        // OUTPUT:
        // [2]


        // NOTE:
        // Java:
        // Stream API
        //
        // C#:
        // LINQ
        //
        // map()    -> Select()
        // filter() -> Where()
        // reduce() -> Aggregate()


        // =====================================================
        // DATE & TIME
        // Java -> java.time
        // C#   -> System.DateTime
        // =====================================================

        DateTime date =
            DateTime.Now.Date;

        Console.WriteLine(date);
        // OUTPUT:
        // Current date
        // Example: 2026-08-12 00:00:00


        DateTime dt =
            DateTime.Now;

        Console.WriteLine(dt);
        // OUTPUT:
        // Current date-time


        Console.WriteLine(
            date.Day);
        // OUTPUT:
        // Current day of month


        Console.WriteLine(
            date.DayOfWeek);
        // OUTPUT:
        // Current day of week


        Console.WriteLine(
            date.Month);
        // OUTPUT:
        // Current month


        Console.WriteLine(
            date.Year);
        // OUTPUT:
        // Current year


        Console.WriteLine(
            dt.Hour);
        // OUTPUT:
        // Current hour


        Console.WriteLine(
            dt.Minute);
        // OUTPUT:
        // Current minute


        DateTime newDate =
            date.AddDays(5);

        Console.WriteLine(newDate);
        // OUTPUT:
        // Current date + 5 days


        Console.WriteLine(
            date.ToString("yyyy-MM-dd"));
        // OUTPUT:
        // 2026-08-12


        // NOTE:
        // Java replaces JavaScript Date with java.time.
        //
        // C# equivalent:
        // DateTime
        // DateOnly
        // TimeOnly
        // DateTimeOffset


        // =====================================================
        // OBJECT OPERATIONS
        // Java Map -> C# Dictionary
        // =====================================================

        Dictionary<string, int> obj =
            new Dictionary<string, int>();

        obj["a"] = 1;
        obj["b"] = 2;


        Console.WriteLine(
            "[" + string.Join(", ", obj.Keys) + "]");
        // OUTPUT:
        // [a, b]


        Console.WriteLine(
            "[" + string.Join(", ", obj.Values) + "]");
        // OUTPUT:
        // [1, 2]


        foreach (
            KeyValuePair<string, int> ee
            in obj)
        {
            Console.WriteLine(
                ee.Key + " = " + ee.Value);
        }

        // OUTPUT:
        // a = 1
        // b = 2


        // =====================================================
        // MAP
        // Java HashMap -> C# Dictionary
        // =====================================================

        Dictionary<string, string> map =
            new Dictionary<string, string>();

        map["id"] = "1";
        map["name"] = "Ali";


        Console.WriteLine(
            map["name"]);
        // OUTPUT: Ali


        Console.WriteLine(
            map.ContainsKey("id"));
        // OUTPUT: True


        Console.WriteLine(
            map.Count);
        // OUTPUT: 2


        foreach (string k in map.Keys)
        {
            Console.WriteLine(k);
        }

        // OUTPUT:
        // id
        // name


        // =====================================================
        // SET
        // Java HashSet -> C# HashSet
        // =====================================================

        HashSet<int> set =
            new HashSet<int>();

        set.Add(1);
        set.Add(2);
        set.Add(2);


        Console.WriteLine(
            "[" + string.Join(", ", set) + "]");
        // OUTPUT:
        // [1, 2]


        Console.WriteLine(
            set.Contains(1));
        // OUTPUT: True


        set.Remove(1);


        Console.WriteLine(
            set.Count);
        // OUTPUT: 1


        foreach (int v in set)
        {
            Console.WriteLine(v);
        }

        // OUTPUT:
        // 2


        // NOTE:
        // Java Set = automatically unique values
        // C# HashSet = automatically unique values


        // =====================================================
        // REGEX
        // Java -> Pattern / Matcher
        // C#   -> Regex / Match
        // =====================================================

        string text =
            "abc123abc";


        Regex pattern =
            new Regex("abc");


        MatchCollection matches =
            pattern.Matches(text);


        foreach (Match match in matches)
        {
            Console.WriteLine(
                "Match at: " + match.Index);

            // OUTPUT:
            // Match at: 0
            // Match at: 6
        }


        Console.WriteLine(
            Regex.Replace(
                text,
                "abc",
                "X"));

        // OUTPUT:
        // X123X


        Console.WriteLine(
            "[" +
            string.Join(
                ", ",
                text.Split("123")) +
            "]");

        // OUTPUT:
        // [abc, abc]


        // =====================================================
        // TYPED ARRAYS
        // Java -> ByteBuffer
        // C#   -> byte[] / Memory<T>
        // =====================================================

        ArrayBufferDemo();
    }


    // =========================================================
    // TYPED ARRAY DEMO
    // Java ByteBuffer -> C# byte[]
    // =========================================================
    static void ArrayBufferDemo()
    {
        byte[] buffer =
            new byte[8];

        BitConverter.GetBytes(10)
            .CopyTo(buffer, 0);

        BitConverter.GetBytes(20)
            .CopyTo(buffer, 4);


        Console.WriteLine(
            BitConverter.ToInt32(buffer, 0));

        // OUTPUT:
        // 10


        Console.WriteLine(
            BitConverter.ToInt32(buffer, 4));

        // OUTPUT:
        // 20


        // NOTE:
        // Java equivalent:
        // ByteBuffer buffer = ByteBuffer.allocate(16);
        //
        // C# alternatives:
        // byte[]
        // Memory<byte>
        // Span<byte>
    }



}