using FluentFTP;
using System.Net;
using System.Net.NetworkInformation;

namespace FtpApis;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("        2. FTP API Demonstration");
        Console.WriteLine("========================================");

        // --------------------------------------------------
        // FTP configuration
        // --------------------------------------------------

        string host = "ftp.example.com";
        string username = "your-username";
        string password = "your-password";

        using FtpClient client = new FtpClient(
            host,
            new NetworkCredential(username, password));

        try
        {
            // --------------------------------------------------
            // Connect
            // --------------------------------------------------

            Console.WriteLine();
            Console.WriteLine("Connecting to FTP server...");

            client.Connect();

            Console.WriteLine("Connected.");

            // --------------------------------------------------
            // List files
            // --------------------------------------------------

            Console.WriteLine();
            Console.WriteLine("Files on FTP server:");
            Console.WriteLine("--------------------------------");

            foreach (FtpListItem item in client.GetListing("/"))
            {
                Console.WriteLine(
                    $"{item.Type,-10} {item.Name}");
            }

            // --------------------------------------------------
            // Upload
            // --------------------------------------------------

            string localFile = Path.Combine(
                AppContext.BaseDirectory,
                "upload.txt");

            File.WriteAllText(
                localFile,
                "Hello from C# FTP application.");

            Console.WriteLine();
            Console.WriteLine("Uploading file...");

            FtpStatus uploadStatus = client.UploadFile(
                localFile,
                "/upload.txt",
                FtpRemoteExists.Overwrite);

            Console.WriteLine(
                $"Upload status: {uploadStatus}");

            // --------------------------------------------------
            // Download
            // --------------------------------------------------

            string downloadFile = Path.Combine(
                AppContext.BaseDirectory,
                "download.txt");

            Console.WriteLine();
            Console.WriteLine("Downloading file...");

            FtpStatus downloadSuccess = client.DownloadFile(
                downloadFile,
                "/upload.txt",
                FtpLocalExists.Overwrite);

            Console.WriteLine(
                $"Download successful: {downloadSuccess}");

            // --------------------------------------------------
            // Disconnect
            // --------------------------------------------------

            client.Disconnect();

            Console.WriteLine();
            Console.WriteLine("Disconnected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("FTP Error:");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine();
        Console.WriteLine("Completed.");
    }
}