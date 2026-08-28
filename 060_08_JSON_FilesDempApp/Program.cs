using _60_JSON_FilesDempApp.Models;
using System.Text.Json;

namespace JsonFiles;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("         8. JSON File Demonstration");
        Console.WriteLine("========================================");

        // --------------------------------------------------
        // Create object
        // --------------------------------------------------

        Employee employee = new Employee
        {
            Id = 101,
            Name = "Ali",
            Department = "IT",
            Salary = 150000,
            Skills =
            [
                "C#",
                ".NET",
                "SQL Server"
            ]
        };

        // --------------------------------------------------
        // JSON options
        // --------------------------------------------------

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // --------------------------------------------------
        // Serialize object to JSON
        // --------------------------------------------------

        string json = JsonSerializer.Serialize(employee, options);

        Console.WriteLine();
        Console.WriteLine("1. Object → JSON");
        Console.WriteLine("--------------------------------");

        Console.WriteLine(json);

        // --------------------------------------------------
        // Save JSON file
        // --------------------------------------------------

        string folder = Path.Combine(AppContext.BaseDirectory, "Data");

        Directory.CreateDirectory(folder);

        string filePath = Path.Combine(folder, "employee.json");

        File.WriteAllText(filePath, json);

        Console.WriteLine();
        Console.WriteLine($"JSON saved to:\n{filePath}");

        // --------------------------------------------------
        // Read JSON file
        // --------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("2. Reading JSON file");
        Console.WriteLine("--------------------------------");

        string jsonFromFile = File.ReadAllText(filePath);

        Console.WriteLine(jsonFromFile);

        // --------------------------------------------------
        // Deserialize JSON
        // --------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("3. JSON → Object");
        Console.WriteLine("--------------------------------");

        Employee? employeeFromJson = JsonSerializer.Deserialize<Employee>(jsonFromFile, options);

        if (employeeFromJson != null)
        {
            Console.WriteLine($"ID: {employeeFromJson.Id}");

            Console.WriteLine($"Name: {employeeFromJson.Name}");

            Console.WriteLine($"Department: {employeeFromJson.Department}");

            Console.WriteLine($"Salary: {employeeFromJson.Salary:N2}");

            Console.WriteLine("Skills:");

            foreach (string skill in employeeFromJson.Skills)
            {
                Console.WriteLine($"  - {skill}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Completed.");
    }
}
