using CustomerManagementWebApp.Data;
using CustomerManagementWebApp.Database;
using CustomerManagementWebApp.Services;
using System.Security.Cryptography.Xml;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using CustomerManagementWebApp.Middleware;

namespace CustomerManagementWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Singleton: Create ONE instance for the entire application lifetime.
            //    The same DatabaseSettings object is reused everywhere.
            builder.Services.AddSingleton<DatabaseSettings>();

            // Register the complete command-line argument array
            builder.Services.AddSingleton<string[]>(args);
            //builder.Services.AddSingleton(args);

            // 2. Scoped: Create ONE instance per HTTP request (scope).
            //    A new DbHelper is created for each web request.
            builder.Services.AddScoped<DbHelper>();

            // 3. Transient
            // A new instance every time the service is requested.
            builder.Services.AddTransient<ICustomerService, CustomerService>();

            // Register MVC services for Controllers + Views.
            // This enables the application to use MVC Controllers and Razor Views.
            builder.Services.AddControllersWithViews();


            // Build the application and create the Dependency Injection (DI) container.
            var app = builder.Build();


            // Create SQLite database tables when the application starts.
            //
            // CreateScope() creates a temporary DI scope.
            // GetRequiredService<T>() asks DI to provide an instance of T.
            // Here, DI creates DatabaseSettings and automatically supplies
            // IConfiguration and IWebHostEnvironment to its constructor.
            using (var scope = app.Services.CreateScope())
            {
                var databaseSettings = scope.ServiceProvider.GetRequiredService<DatabaseSettings>();

                DatabaseInitializer.CreateTables(databaseSettings);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                //It enables HSTS(HTTP Strict Transport Security) in your ASP.NET Core application.
                //It tells the browser: "Always use HTTPS for this website. Don't use HTTP."
            }

            app.UseMiddleware<RequestLoggingMiddleware>();
            //app.UseRequestLogging();
            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}



/* dotnet new mvc -n CustomerManagementWebApp
 * 
 * cd CustomerManagementWebApp
 *
 * dotnet add package System.Data.SQLite.Core
 * OR
 * Nuget Package Manager
 * 
 * dotnet restore
 * 
 * dotnet build
 * 
 * dotnet run
 * OR
 * Visual Studio RUN button
 * 
 */

// ============================================================================
//                    BUILDER → BUILD → APP → RUN
// ============================================================================
//
//                         builder
//                            │
//                            │ Configure everything
//                            │
//              ┌─────────────┼─────────────────────┐
//              │             │                     │
//              ▼             ▼                     ▼
//       Register DI     Load Configuration   Configure Environment
//         Services
//              │
//              ├── AddSingleton<T>()
//              ├── AddScoped<T>()
//              ├── AddTransient<T>()
//              ├── appsettings.json
//              ├── Environment Variables
//              └── Command-line arguments
//                            │
//                            │ Configure MVC
//                            │ Configure other services
//                            │
//                            │ builder.Build()
//                            ▼
//                           app
//                            │
//                            │ Configure and RUN
//                            │
//              ┌─────────────┼─────────────────────┐
//              │             │                     │
//              ▼             ▼                     ▼
//          Middleware      Routing            Authorization
//              │             │                     │
//              ├── HTTPS     ├── Routes            └── Access Control
//              ├── Error     └── Endpoints
//              └── Static Files
//                            │
//                            ▼
//                         Endpoints
//                            │
//                            ▼
//                     HTTP Requests
//                            │
//                            │ app.Run()
//                            ▼
//                     APPLICATION RUNNING
//
// ============================================================================
//
// builder.Services
//      → REGISTER services
//
// builder.Build()
//      → BUILD the configured application
//
// app.Services
//      → RESOLVE registered services from the built DI container
//
// app.Services.CreateScope()
//      → CREATE a temporary DI scope when outside an HTTP request
//
// app.Run()
//      → START the web application and wait for HTTP requests
//
// ============================================================================

// app.UseCors()
//     → Controls Cross-Origin Resource Sharing.
//     → Important for APIs called from another domain/frontend.
//
//     Example:
//     app.UseCors("MyPolicy");
//
//--------------------------------------------------------------------------------
//
// app.UseSession()
//     → Enables ASP.NET Core Session.
//
//     Example:
//     app.UseSession();
//
//--------------------------------------------------------------------------------
//
// app.UseResponseCompression()
//     → Compresses HTTP responses.
//     → Reduces response size.
//
//     Example:
//     app.UseResponseCompression();
//
//--------------------------------------------------------------------------------
//
// app.UseAntiforgery()
//     → Helps protect against CSRF attacks.
//     → Common with MVC/Razor applications.
//
//     Example:
//     app.UseAntiforgery();

// ===============================================================================================================
// ASP.NET Core Dependency Injection (DI) Service Lifetimes
// ===============================================================================================================
//
// Registration                                      Lifetime       Instance Creation                  Scope
// ----------------------------------------------------------------------------------------------------------------
// builder.Services.AddSingleton<DatabaseSettings>();
//                                                     Singleton      ONE instance                      Application
//                                                                                                      lifetime
//
// builder.Services.AddSingleton<string[]>(args);
//                                                     Singleton      ONE instance of args              Application
//                                                                                                      lifetime
//
// builder.Services.AddScoped<DbHelper>();
//                                                     Scoped         ONE instance per HTTP request     Request
//
// builder.Services.AddTransient<ICustomerService,
//                                CustomerService>();
//                                                     Transient      NEW instance every time          Every request
//                                                                                                      / injection
//
// ===============================================================================================================
//
// QUICK RULE
// ----------------------------------------------------------------------------------------------------------------
//
// Singleton  → ONE instance for the entire application
//
// Scoped     → ONE instance per HTTP request
//
// Transient  → NEW instance every time it is requested
//
// ===============================================================================================================
//
// EXAMPLE
// ----------------------------------------------------------------------------------------------------------------
//
// Request 1  → DbHelper instance #1
// Request 2  → DbHelper instance #2
// Request 3  → DbHelper instance #3
//
// Singleton DatabaseSettings → Same instance for Request 1, 2, 3
//
// Transient CustomerService:
//     Request 1 → New instance when requested
//     Request 2 → New instance when requested
//     Request 3 → New instance when requested
//
// ===============================================================================================================