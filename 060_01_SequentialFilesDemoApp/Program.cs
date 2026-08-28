using System.Text;

namespace SequentialFiles;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("   1. Sequential Files Demonstration");
        Console.WriteLine("========================================");

        string folder = Path.Combine(
            AppContext.BaseDirectory,
            "Data");

        Directory.CreateDirectory(folder);

        string filePath = Path.Combine(
            folder,
            "employees.txt");

        Console.WriteLine();
        Console.WriteLine($"File: {filePath}");

        // --------------------------------------------------
        // 1. Write a new sequential file
        // --------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("1. Writing file...");

        using (StreamWriter writer = new StreamWriter(
            filePath,
            append: false,
            encoding: Encoding.UTF8))
        {
            writer.WriteLine("101,Ali,Developer");
            writer.WriteLine("102,Ahmed,Manager");
            writer.WriteLine("103,Sara,Tester");
        }

        Console.WriteLine("File written successfully.");

        // --------------------------------------------------
        // 2. Append records
        // --------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("2. Appending records...");

        using (StreamWriter writer = new StreamWriter(
            filePath,
            append: true,
            encoding: Encoding.UTF8))
        {
            writer.WriteLine("104,Usman,Architect");
            writer.WriteLine("105,Fatima,Designer");
        }

        Console.WriteLine("Records appended.");

        // --------------------------------------------------
        // 3. Read sequentially
        // --------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("3. Reading sequentially...");
        Console.WriteLine("--------------------------------");

        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        // --------------------------------------------------
        // 4. Read using File.ReadLines()
        // --------------------------------------------------

        Console.WriteLine();
        Console.WriteLine("4. Reading using File.ReadLines()");
        Console.WriteLine("--------------------------------");

        foreach (string line in File.ReadLines(filePath))
        {
            string[] fields = line.Split(',');

            Console.WriteLine(
                $"ID={fields[0]}, " +
                $"Name={fields[1]}, " +
                $"Designation={fields[2]}");
        }

        Console.WriteLine();
        Console.WriteLine("Completed.");
    }
}