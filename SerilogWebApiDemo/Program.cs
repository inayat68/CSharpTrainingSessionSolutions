using Serilog;
using Serilog.Events;
using SerilogWebApiDemo.Models;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .WriteTo.Console()
    .WriteTo.File(
        "logs/app-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        shared: true)
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting SerilogWebApiDemo application");

    var builder = WebApplication.CreateBuilder(args);

    // Replace the default ASP.NET Core logging providers with Serilog.
    builder.Services.AddSerilog((services, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId();
    });

    builder.Services.AddScoped<LoggingFilter>();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Logs every HTTP request with method, path, status code and elapsed time.
    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}


// =================================================================================================================
// ASP.NET Core: Services vs Middleware vs Endpoints
// =================================================================================================================
//
// Method / Code                    Type / Stage             Purpose
// -----------------------------------------------------------------------------------------------------------------
// builder.Services.AddSwaggerGen() Service Registration     Registers Swagger/OpenAPI generation services
//
// app.UseSwagger()                  Middleware              Serves the generated OpenAPI JSON document
//
// app.UseSwaggerUI()                Middleware              Provides the interactive Swagger UI
//
// app.UseSerilogRequestLogging()    Middleware              Logs HTTP requests and responses
//
// app.UseAuthentication()           Middleware              Authenticates the incoming request
//
// app.UseAuthorization()            Middleware              Checks authorization/permissions
//
// app.UseHttpsRedirection()          Middleware              Redirects HTTP requests to HTTPS
//
// app.MapControllers()              Endpoint Mapping        Maps controller actions to HTTP endpoints
//
// builder.Services.AddControllers() Service Registration     Registers MVC/Web API controller services
//
// =================================================================================================================
//
// EASY RULE:
//
// Add...()  → Register/configure services in Dependency Injection (DI)
// Use...()  → Add middleware to the HTTP request pipeline
// Map...()  → Map HTTP endpoints/routes
//
// =================================================================================================================
//
// Swagger:
//
// AddSwaggerGen() → Register Swagger services
// UseSwagger()   → Serve Swagger/OpenAPI JSON
// UseSwaggerUI() → Serve interactive Swagger web page
//
// =================================================================================================================

// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

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