namespace CustomerManagement.Core;

/// <summary>
/// Strongly typed representation of the ConnectionStrings section
/// from appsettings.json.
/// This class lives in the shared Class Library so all applications
/// can use the same settings class.
/// </summary>
public class ConnectionStringSetup
{
    public string ConnectionString { get; set; } = string.Empty;
}
