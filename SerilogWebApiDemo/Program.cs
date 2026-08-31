using Serilog;
using Serilog.Events;

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