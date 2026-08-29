using System.Text;

namespace SequentialFiles;

internal class Program
{
    // Stores information about every file found by the first extension list.
    private static readonly List<FileInformation> Files_Properties = new();

    // Stores content information for files matching the limited extension list.
    private static readonly List<ExcelListInfo> lstFileContent = new();

    // Files counter check
    private static int counter = 1;

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

        RunFoldersScanning();
    }


    public static void RunFoldersScanning()
    {
        // By default, read files from the Data folder beside the executable.
        // You can also pass another folder as the first command-line argument.
        string source_folder = Path.Combine(AppContext.BaseDirectory.Replace("\\bin\\Debug\\net10.0", ""), "Data");

        if (!Directory.Exists(source_folder))
        {
            Console.WriteLine($"Folder does not exist: {source_folder}");
            return;
        }

        string rootFolder = source_folder;

        // Extensions for collecting file properties.
        List<string> allFileExtensions = new()
        {
            "*.frm", "*.bas", "*.cls", "*.dsr", "*.ctl", "*.pag",
            "*.txt", "*.sql", "*.vb", "*.cs", "*.htm", "*.xls",
            "*.xlsx", "*.tb", "*.bat"
        };

        List<string> matchingFiles = new();

        // Extensions for collecting both properties and file content.
        List<string> limitedFileExtensions = new()
        {
            "*.cbl", "*.txt"
        };

        List<string> matchingLimitedFiles = new();

        // ------------------------------------------------------------
        // Get files for the first extension list
        // ------------------------------------------------------------
        foreach (string extension in allFileExtensions)
        {
            List<string> files = GetFilesByExtension(rootFolder, extension);
            matchingFiles.AddRange(files);
        }

        // Remove duplicates because the same file could be returned
        // by multiple extension searches.
        matchingFiles = matchingFiles.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

        // ------------------------------------------------------------
        // ALL Files Properties
        // ------------------------------------------------------------
        Log("ALL Files Properties:");

        foreach (string filePath in matchingFiles)
        {
            FileInformation file = GetFileInformation(filePath);
            Files_Properties.Add(file);

            Log($"  {file.FileName} | {file.Extension} | {file.Length:N0} bytes");
        }

        // ------------------------------------------------------------
        // Limited Files Content Grabbing
        // ------------------------------------------------------------
        Log("");
        Log("Limited Files Content:");

        foreach (string extension in limitedFileExtensions)
        {
            List<string> files = GetFilesByExtension(rootFolder, extension);
            matchingLimitedFiles.AddRange(files);
        }

        matchingLimitedFiles = matchingLimitedFiles
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        foreach (string object_file in matchingLimitedFiles)
        {
            Log("");
            Log($"File: {object_file}");

            string object_folder_path = Path.GetDirectoryName(object_file) ?? "";
            string ext = Path.GetExtension(object_file).TrimStart('.');
            string object_file_type = GetObjectFileType(ext);

            string the_file_content = GetFileContent(object_file);

            lstFileContent.Add(new ExcelListInfo
            {
                SerialNo = counter.ToString(),
                FileName = Path.GetFileName(object_file),
                FilePath = object_file,
                FolderPath = object_folder_path,
                Extension = ext,
                ObjectFileType = object_file_type,
                FileContent = the_file_content
            });

            counter++;

            Log($"  Type: {object_file_type}");
            Log($"  Extension: .{ext}");
            Log($"  Content length: {the_file_content.Length} characters");
            Log("  Content:");
            Log(the_file_content);
        }

        // ------------------------------------------------------------
        // Summary
        // ------------------------------------------------------------
        Log("");
        Log("============================================================");
        Log("SUMMARY");
        Log("============================================================");
        Log($"Root folder             : {rootFolder}");
        Log($"Files_Properties count  : {Files_Properties.Count}");
        Log($"lstFileContent count    : {lstFileContent.Count}");
        Log("============================================================");

        Console.WriteLine("");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    public static string GetFileContent(string file_path)
    {
        string readText = "";
        string path = file_path;

        try
        {
            if (File.Exists(path))
            {
                // Read the complete file as text.
                readText = File.ReadAllText(path);
            }
        }
        catch (IOException ex)
        {
            Log($"Unable to read file: {path}");
            Log($"IO error: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Log($"Access denied: {path}");
            Log($"Error: {ex.Message}");
        }

        // Preserve the behavior from the original code.
        readText = readText.Replace("~\"", "<tldq>", StringComparison.OrdinalIgnoreCase);

        return readText;
    }

    static string GetObjectFileType(string ext)
    {
        return ext.ToLowerInvariant() switch
        {
            "frm" => "Form Object",
            "cls" => "Class Object",
            "bas" => "BASIC Object",
            "dsr" => "Report Object",
            "ctl" => "Control Object",
            "pag" => "Page Object",
            "sql" => "SQL Object",
            "vb" => "Visual Basic Object",
            "cs" => "C# Object",
            "htm" => "HTML Object",
            "bat" => "Batch File",
            "txt" => "Text File",
            "cbl" => "COBOL Object",
            "tb" => "Table Object",
            "xls" => "Excel Object",
            "xlsx" => "Excel Object",
            _ => "Unknown Object"
        };
    }

    static List<string> GetFilesByExtension(string folderPath, string extension)
    {
        List<string> files = new();

        try
        {
            if (!Directory.Exists(folderPath))
                return files;

            // Get files in the current directory.
            string[] matchingFiles = Directory.GetFiles(folderPath, extension);
            files.AddRange(matchingFiles);

            // Recursively process subdirectories.
            string[] subDirectories = Directory.GetDirectories(folderPath);

            foreach (string subDir in subDirectories)
            {
                files.AddRange(GetFilesByExtension(subDir, extension));
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Log($"Unauthorized access: {folderPath} - {ex.Message}");
        }
        catch (DirectoryNotFoundException ex)
        {
            Log($"Directory not found: {folderPath} - {ex.Message}");
        }
        catch (PathTooLongException ex)
        {
            Log($"Path too long: {folderPath} - {ex.Message}");
        }
        catch (IOException ex)
        {
            Log($"IO error: {folderPath} - {ex.Message}");
        }

        return files;
    }

    static FileInformation GetFileInformation(string filePath)
    {
        FileInfo info = new(filePath);

        return new FileInformation
        {
            FileName = info.Name,
            FullPath = info.FullName,
            DirectoryName = info.DirectoryName ?? "",
            Extension = info.Extension,
            Length = info.Length,
            CreationTime = info.CreationTime,
            LastWriteTime = info.LastWriteTime
        };
    }

    static void Log(string message)
    {
        Console.WriteLine(message);
    }














}