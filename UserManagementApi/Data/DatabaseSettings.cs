namespace UserManagementApi.Data;

public class DatabaseSettings
{
    public string ConnectionString { get; }

    // Due to Service Startup or Lifetime - NOT all services are possible to use at any place
    // X - IServiceCollection sc, 
    // X - string[] args,
    // X - IHostBuilder hb, HostBuilderContext hbc
    public DatabaseSettings(IConfiguration configuration, IWebHostEnvironment environment, IServiceProvider sp)
    {
        string appSettingConnectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string not found.");

        string databaseFolder = Path.Combine(environment.ContentRootPath, "Database");

        Directory.CreateDirectory(databaseFolder);

        string databasePath = Path.Combine(databaseFolder, "CustomerOrders.db");

        ConnectionString = appSettingConnectionString.Replace("<FOLDER_PATH>", databasePath);

        string[]? strTest1 = null;

        if (strTest1 is not null && strTest1.Length == 0)
        {
            Console.WriteLine("Test1 is empty.");
        }

        string[] strTest2 =
        {
            "CompanyName=MyCompany",
            "Environment=Development",
            "Port=5001"
        };
    }
}

// ┌─────────────────────────────────────────────────────────────────────┐
// │                         DI CONTAINER                                │
// └─────────────────────────────────────────────────────────────────────┘
//                                  │
//              ┌───────────────────┼───────────────────┐
//              │                   │                   │
//              ▼                   ▼                   ▼
// ┌─────────────────────┐ ┌─────────────────────┐ ┌─────────────────────┐
// │ Framework Services  │ │   Your Services     │ │ Third-party         │
// │                     │ │                     │ │ Services            │
// ├─────────────────────┤ ├─────────────────────┤ ├─────────────────────┤
// │ IConfiguration      │ │ DatabaseSettings    │ │ DbContext           │
// │ IWebHostEnvironment │ │ DbHelper            │ │ HttpClient          │
// │ ILogger<T>          │ │ InvoiceService      │ │ Redis Client        │
// │ IServiceProvider    │ │ EmailService        │ │ ...                 │
// │ IOptions<T>         │ │ ...                 │ │                     │
// │ ...                 │ │                     │ │                     │
// └─────────────────────┘ └─────────────────────┘ └─────────────────────┘


// =================================================================================================
//                         COMMON ASP.NET CORE DI SERVICES
// =================================================================================================
//
// ┌─────────────────────────────┬──────────────────────────────────────┬──────────────────────────┐
// │ DI Service                  │ Purpose                              │ Example Usage            │
// ├─────────────────────────────┼──────────────────────────────────────┼──────────────────────────┤
// │ IConfiguration              │ Read application configuration       │ appsettings.json         │
// │                             │                                      │ connection strings       │
// │                             │                                      │ API URLs, custom values  │
// ├─────────────────────────────┼──────────────────────────────────────┼──────────────────────────┤
// │ IWebHostEnvironment         │ Get application/environment info     │ Root folder, wwwroot,    │
// │                             │                                      │ Development/Production   │
// ├─────────────────────────────┼──────────────────────────────────────┼──────────────────────────┤
// │ IServiceProvider            │ Resolve services from DI container   │ GetRequiredService<T>()  │
// │                             │                                      │ Create DI scopes         │
// └─────────────────────────────┴──────────────────────────────────────┴──────────────────────────┘