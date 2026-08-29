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
        // Read JSON Formatted String
        // --------------------------------------------------

        string jsonString = """
                    {
                        "fullName": "John Alex",
                        "departmentName": "IT",
                        "dateOfJoining": "2026-01-01",
                        "skills": [
                            "C#",
                            ".NET",
                            "SQL Server",
                            "Git"
                        ]
                    }
                    """;
        var data = JsonSerializer.Deserialize<JsonElement>(jsonString);

        Console.WriteLine($"Full Name: {data.GetProperty("fullName")}");
        Console.WriteLine($"Department Name: {data.GetProperty("departmentName")}");
        Console.WriteLine($"Date of Joining: {data.GetProperty("dateOfJoining")}");

        Console.WriteLine("Skills:");

        foreach (var skill in data.GetProperty("skills").EnumerateArray())
        {
            Console.WriteLine($"  - {skill}");
        }

        Console.ReadKey();

        // --------------------------------------------------
        // Create multiple Employee objects
        // --------------------------------------------------

        List<Employee> employees =
        [
            new Employee
            {
                Id = 101,
                Name = "Ali",
                DepartmentName = "IT",
                Salary = 150000,
                Skills =
                [
                    "C#",
                    ".NET",
                    "SQL Server"
                ],
                Age = 40
            },

            new Employee
            {
                Id = 102,
                Name = "Ahmed",
                DepartmentName = "HR",
                Salary = 120000,
                Skills =
                [
                    "Recruitment",
                    "Communication",
                    "Employee Management"
                ]
            },

            new Employee
            {
                Id = 103,
                Name = "Sara",
                DepartmentName = "Finance",
                Salary = 135000,
                Skills =
                [
                    "Accounting",
                    "Excel",
                    "Financial Analysis"
                ],
                Age = 21
            }
        ];

        // --------------------------------------------------
        // JSON options
        // --------------------------------------------------

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // --------------------------------------------------
        // Serialize objects to JSON
        // --------------------------------------------------

        string json = JsonSerializer.Serialize(employees, options);

        Console.WriteLine();
        Console.WriteLine("1. Objects → JSON");
        Console.WriteLine("--------------------------------");

        Console.WriteLine(json);

        // --------------------------------------------------
        // Save JSON file
        // --------------------------------------------------

        string folder = Path.Combine(AppContext.BaseDirectory.Replace("\\bin\\Debug\\net10.0", ""), "Data");

        Directory.CreateDirectory(folder);

        string filePath = Path.Combine(folder, "employees.json");

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
        Console.WriteLine("3. JSON → Objects");
        Console.WriteLine("--------------------------------");

        List<Employee>? employeesFromJson = JsonSerializer.Deserialize<List<Employee>>(jsonFromFile, options);

        if (employeesFromJson != null)
        {
            // --------------------------------------------------
            // Iterate through all employees
            // --------------------------------------------------

            foreach (Employee employee in employeesFromJson)
            {
                Console.WriteLine($"ID: {employee.Id}");

                Console.WriteLine($"Name: {employee.Name}");

                Console.WriteLine($"Department: {employee.DepartmentName}");

                Console.WriteLine($"Salary: {employee.Salary:N2}");

                Console.WriteLine("Skills:");

                foreach (string skill in employee.Skills)
                {
                    Console.WriteLine($"  - {skill}");
                }

                Console.WriteLine("--------------------------------");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Completed.");
    }
}