using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using UserManagementApi.Data;
using UserManagementApi.Middleware;
using UserManagementApi.Repositories;
using UserManagementApi.Seed;
using UserManagementApi.Services;

namespace UserManagementApi;

public class Program
{
    record Employee(int Id, string Name, decimal Salary);
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Initializing a new instance of the WebApplicationBuilder");

        var builder = WebApplication.CreateBuilder(args);

        // ============================================================================
        // .NET WEB API — builder has DEPENDENCY INJECTION Services
        // ============================================================================
        //
        // Program.cs
        //     │
        //     ├── IConfiguration
        //     └── Reads configuration/appsettings.json
        //     |
        //     |
        //     ├── IWebHostEnvironment
        //     │   └── Provides Root Path, Development/Production info
        //     │
        //     ↓
        // IServiceCollection
        //     │
        //     ├── AddSingleton()
        //     ├── AddScoped()
        //     ├── AddTransient()
        //     ├── AddDbContext()
        //     ├── AddControllers()
        //     └── AddAuthentication()
        //     │
        //     ↓
        // IServiceProvider
        //     │
        //     ├── Controller
        //     ├── Service
        //     ├── Repository
        //     ├── DbContext
        //     ├── ILogger
        //     └── Other dependencies
        //
        // ============================================================================
        //

        // Loading Envorinment Varaibles
        Env.Load();

        var dbNameWithPath = Environment.GetEnvironmentVariable("DB_PATH") ?? "Database/cs_users_db.db";
        //      OR
        var dbNameWithPath2 = Path.Combine(AppContext.BaseDirectory.Replace("\\bin\\Debug\\net8.0", ""), "Database", "cs_users_db.db");

        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? "dev_key";
        var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "api";
        var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "api_users";
        var frontend = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:5173";

        // Database Packages
        // -----------------

        //# EF Core base package
        //dotnet add package Microsoft.EntityFrameworkCore

        //# SQL Server provider
        //dotnet add package Microsoft.EntityFrameworkCore.SqlServer

        //# In-Memory database provider (mainly for testing)
        //dotnet add package Microsoft.EntityFrameworkCore.InMemory

        //# EF Core Design package (Migrations, Scaffolding, etc.)
        //dotnet add package Microsoft.EntityFrameworkCore.Design

        //# EF Core CLI Tools (migrations/database commands)
        //dotnet tool install --global dotnet-ef

        //# SQLite provider
        //dotnet add package Microsoft.EntityFrameworkCore.Sqlite
        builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite($"Data Source={dbNameWithPath}"));

        //          OR

        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
        builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite(connectionString));

        // ------------------------------------------------------------
        // Register SQL Server DbContext
        // ------------------------------------------------------------
        //# SQL Server provider
        //dotnet add package Microsoft.EntityFrameworkCore.SqlServer

        //builder.Services.AddDbContext<AppDbContext>(options =>
        //    options.UseSqlServer(
        //        builder.Configuration.GetConnectionString("DefaultConnection")
        //    ));

        //builder.Services.AddDbContext<AppDbContext>(options =>
        //{
        //    options.UseSqlServer(
        //        connectionString,
        //        sqlOptions =>
        //        {
        //            sqlOptions.CommandTimeout(60);

        //            sqlOptions.EnableRetryOnFailure(
        //                maxRetryCount: 5,
        //                maxRetryDelay: TimeSpan.FromSeconds(10),
        //                errorNumbersToAdd: null);
        //        });

        //    if (builder.Environment.IsDevelopment())
        //    {
        //        options.EnableDetailedErrors();
        //        options.EnableSensitiveDataLogging();
        //    }
        //});

        // 1. Singleton: Create ONE instance for the entire application lifetime.
        // The same DatabaseSettings object is reused everywhere.
        //               AddSingleton<TService>(IServiceCollection)
        builder.Services.AddSingleton<DatabaseSettings>();

        //OR

        // 2. Scoped: Create ONE instance per HTTP request (scope).
        //    A new DbHelper is created for each web request.
        builder.Services.AddScoped<UserService>();
        builder.Services.AddScoped<RoleService>();
        builder.Services.AddScoped<TaskService>();
        builder.Services.AddScoped<JwtService>();
        builder.Services.AddScoped<PasswordService>();

        // 3. Transient
        // A new instance every time the service is requested.
        builder.Services.AddTransient<IUserRepository, UserRepository>();

        // Register MVC services for Controllers + Views.
        // This enables the application to use MVC Controllers and Razor Views.
        builder.Services.AddControllers();

        // Create Service for CORS (Cross Origin Resouce Sharing) Policy
        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("frontend", p => p.WithOrigins(frontend).AllowAnyHeader().AllowAnyMethod());
        });

        //Configures JWT Bearer Authentication in an ASP.NET Core Web API. Its job is to tell ASP.NET Core:
        //"When a request contains a JWT token in the Authorization: Bearer ... header, validate that token using these rules."
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });

        // Register Swagger configuration
        builder.Services.AddSwaggerGen(c =>
        {
            //Define the Bearer security scheme - To Enable Authorization on Swagger Page
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            // Tell Swagger that APIs require this security scheme
            // Use the Bearer authentication definition that I created above for API requests.
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
            });
        });

        builder.Services.AddProblemDetails();

        //==================================================================================================
        var app = builder.Build();
        //==================================================================================================

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementApi v1");
                c.RoutePrefix = "swagger";
            });
        }
        else
        {
            app.UseExceptionHandler("/error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            //It enables HSTS(HTTP Strict Transport Security) in your ASP.NET Core application.
            //It tells the browser: "Always use HTTPS for this website. Don't use HTTP."
        }

        app.UseCors("frontend");

        // Handle unhandled exceptions globally.
        app.UseMiddleware<ExceptionMiddleware>();

        // Log details of every incoming HTTP request.
        app.UseMiddleware<RequestLoggingMiddleware>();

        // Authenticate the user based on the JWT token.
        app.UseAuthentication();

        // Check whether the authenticated user is authorized to access the resource.
        app.UseAuthorization();

        // Map controller classes to HTTP endpoints.
        app.MapControllers();   //Maps [HttpGet], [HttpPost], etc. in controller classes

        app.MapGet("/hello", () => "Hello World");

        app.MapPost("/api/employee", (Employee employee) =>
        {
            return $"Employee {employee.Id} - {employee.Name} - {employee.Salary}";
        });

        // Create SQLite database tables when the application starts.
        //
        // CreateScope() creates a temporary DI scope.
        // GetRequiredService<T>() asks DI to provide an instance of T.
        // Here, DI creates DatabaseSettings and automatically supplies
        // IConfiguration and IWebHostEnvironment to its constructor.
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
            await DatabaseSeeder.SeedAsync(db);
        }

        app.Run();
    }
}


// Deliberately generate an exception
//throw new Exception("TEST: Deliberate exception from Program.cs");



// ┌──────────────────────────────────────────────────────────────────────────┐
// │ Program.cs                                                               │
// │                                                                          │
// │ Application starts and configures the Web API.                           │
// └──────────────────────────────────────────────────────────────────────────┘
//                                  │
//                                  ├── IConfiguration
//                                  │   └── Reads configuration/appsettings.json
//                                  │
//                                  ├── IWebHostEnvironment
//                                  │   └── Provides Development/Production info
//                                  │
//                                  ↓
// ┌──────────────────────────────────────────────────────────────────────────┐
// │ IServiceCollection                                                       │
// │                                                                          │
// │ Registers services that will be available through Dependency Injection.  │
// └──────────────────────────────────────────────────────────────────────────┘
//                                  │
//                                  ├── AddSingleton()
//                                  │   └── One instance for application lifetime
//                                  │
//                                  ├── AddScoped()
//                                  │   └── One instance per HTTP request
//                                  │
//                                  ├── AddTransient()
//                                  │   └── New instance whenever requested
//                                  │
//                                  ├── AddDbContext()
//                                  │   └── Registers EF Core DbContext
//                                  │
//                                  ├── AddControllers()
//                                  │   └── Registers MVC/Web API controllers
//                                  │
//                                  └── AddAuthentication()
//                                      └── Registers authentication services
//                                  │
//                                  ↓
// ┌──────────────────────────────────────────────────────────────────────────┐
// │ IServiceProvider                                                        │
// │                                                                          │
// │ DI container that creates/resolves the registered services when needed.  │
// └──────────────────────────────────────────────────────────────────────────┘
//                                  │
//                                  ├── Controller
//                                  │
//                                  ├── Service
//                                  │
//                                  ├── Repository
//                                  │
//                                  ├── DbContext
//                                  │
//                                  ├── ILogger
//                                  │
//                                  └── Other Dependencies
//
// ============================================================================


