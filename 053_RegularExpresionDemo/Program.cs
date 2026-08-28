using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RegularExpresionDemo_53;

class Program
{
    static void Main()
    {
        string folderPath = @"D:\Data\Inayat Rehman\mCloud\UserManagementApp\053_RegularExpresionDemo\Data";

        // Read all .log files from the folder
        string[] files = Directory.GetFiles(folderPath, "*.txt");

        // Output file
        string outputFile = Path.Combine(folderPath, "UnknownKeyword_Errors.txt");

        // Regex:
        // :\s+                  -> Colon followed by whitespace
        // CREATE_PROCEDURE_     -> Procedure name
        // [^\r\n]+              -> Rest of filename on same line
        // [\s\S]*?              -> Capture everything, non-greedy
        // Mlogica\.             -> Stop at "Mlogica."
        string pattern =@":\s+CREATE_PROCEDURE_[^\r\n]+[\s\S]*?Mlogica\.";

        Regex regex = new Regex(
            pattern,
            RegexOptions.Multiline | RegexOptions.IgnoreCase
        );

        foreach (string file in files)
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("FILE: " + file);
            Console.WriteLine("========================================");

            string text = File.ReadAllText(file);

            MatchCollection matches = regex.Matches(text);

            Console.WriteLine("Matches Found: " + matches.Count);

            int count = 1;


            StringBuilder result = new StringBuilder();

            foreach (Match match in matches)
            {
                if (match.Value.Contains("Unknown keyword"))
                {
                    result.AppendLine($"----- Match {count} -----");
                    result.AppendLine(match.Value);
                    result.AppendLine();
                    result.AppendLine(new string('-', 50));
                    result.AppendLine();

                    count++;
                }
            }

            using (StreamWriter writer = new StreamWriter(outputFile, false))
            {
                writer.Write(result.ToString());
            }
        }
    }
}