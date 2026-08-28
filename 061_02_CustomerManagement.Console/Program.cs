using CustomerManagement.Core;
using Microsoft.Extensions.Configuration;
using System.Configuration;
//dotnet add CustomerManagement.Console package Microsoft.Extensions.Configuration.Json
//dotnet add CustomerManagement.Console package Microsoft.Extensions.Configuration.FileExtensions
//dotnet add CustomerManagement.Console package Microsoft.Extensions.Configuration.Binder

// ------------------------------------------------------------
// Load appsettings.json
// ------------------------------------------------------------
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile(
        "appsettings.json",
        optional: false,
        reloadOnChange: false)
    .Build();


// ------------------------------------------------------------
// Read the ConnectionStrings section
// into our shared ConnectionStringSettings class.
// ------------------------------------------------------------
//var connectionSettings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringSettings>() ?? new ConnectionStringSettings();

var connectionSettings = new ConnectionStringSettings
{
    ConnectionString    =
        configuration.GetConnectionString("DefaultConnection")
        ?? string.Empty
};
Console.WriteLine(connectionSettings.ConnectionString);


Console.WriteLine("Customer Management Console");
Console.WriteLine("===========================");
Console.WriteLine();

Console.WriteLine("Connection String:");
Console.WriteLine(connectionSettings.ConnectionString);
Console.WriteLine();

var customerService = new CustomerService();

foreach (var customer in customerService.GetCustomers())
{
    Console.WriteLine(
        $"{customer.Id} - {customer.Name} - {customer.Email}");
}