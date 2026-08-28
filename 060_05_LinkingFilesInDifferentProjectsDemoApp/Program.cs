namespace FileLinks;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("       5. Files in Different Folders");
        Console.WriteLine("========================================");

        // --------------------------------------------------
        // Application root
        // --------------------------------------------------

        string root =
            AppContext.BaseDirectory;

        Console.WriteLine();
        Console.WriteLine($"Application Root:");
        Console.WriteLine(root);

        // --------------------------------------------------
        // Create folders
        // --------------------------------------------------

        string sourceFolder =
            Path.Combine(root, "Source");

        string destinationFolder =
            Path.Combine(root, "Destination");

        Directory.CreateDirectory(sourceFolder);
        Directory.CreateDirectory(destinationFolder);

        // --------------------------------------------------
        // Create source file
        // --------------------------------------------------

        string sourceFile =
            Path.Combine(
                sourceFolder,
                "Customer.txt");

        File.WriteAllText(
            sourceFile,
            "Customer information");

        Console.WriteLine();
        Console.WriteLine($"Source File:");
        Console.WriteLine(sourceFile);

        // --------------------------------------------------
        // Destination file
        // --------------------------------------------------

        string destinationFile =
            Path.Combine(
                destinationFolder,
                "Customer.txt");

        // --------------------------------------------------
        // Copy file
        // --------------------------------------------------

        File.Copy(
            sourceFile,
            destinationFile,
            overwrite: true);

        Console.WriteLine();
        Console.WriteLine(
            $"File copied to:\n{destinationFile}");

        // --------------------------------------------------
        // Read destination
        // --------------------------------------------------

        string content =
            File.ReadAllText(destinationFile);

        Console.WriteLine();
        Console.WriteLine("Destination content:");
        Console.WriteLine(content);

        // --------------------------------------------------
        // Demonstrate relative path
        // --------------------------------------------------

        string relativePath =
            Path.GetRelativePath(
                root,
                destinationFile);

        Console.WriteLine();
        Console.WriteLine(
            $"Relative path: {relativePath}");

        // --------------------------------------------------
        // File information
        // --------------------------------------------------

        FileInfo fileInfo =
            new FileInfo(destinationFile);

        Console.WriteLine();
        Console.WriteLine("File Information");
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Name: {fileInfo.Name}");
        Console.WriteLine($"Directory: {fileInfo.DirectoryName}");
        Console.WriteLine($"Size: {fileInfo.Length} bytes");
        Console.WriteLine($"Created: {fileInfo.CreationTime}");
        Console.WriteLine($"Modified: {fileInfo.LastWriteTime}");

        Console.WriteLine();
        Console.WriteLine("Completed.");
    }
}