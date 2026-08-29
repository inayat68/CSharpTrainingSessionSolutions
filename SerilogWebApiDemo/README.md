# SerilogWebApiDemo

A complete .NET 10 Web API sample showing Serilog integration.

## Packages

- Serilog.AspNetCore 10.0.0
- Serilog.Enrichers.Thread 4.0.0
- Serilog.Expressions 5.0.0
- Serilog.Sinks.Console 6.1.1
- Serilog.Sinks.File 7.0.0
- Swashbuckle.AspNetCore 10.0.1

## What is implemented?

1. Serilog bootstrap logger in `Program.cs`.
2. Serilog configuration in `appsettings.json`.
3. Console logging.
4. Rolling daily file logging under `logs/`.
5. Thread ID enrichment.
6. ASP.NET Core request logging with `UseSerilogRequestLogging()`.
7. Controller-level Information, Warning, and Error logging.
8. Swagger UI.

## Run

```powershell
dotnet restore
dotnet run
```

Open Swagger:

```text
https://localhost:7080/swagger
```

or:

```text
http://localhost:5080/swagger
```

## Test endpoints

```text
GET /api/Employee
GET /api/Employee/1
GET /api/Employee/error-demo
```

After requests, check:

```text
logs/app-YYYYMMDD.log
```

## Important

The controller demonstrates both:

```csharp
Log.Information("...");
```

and structured logging:

```csharp
Log.Information(
    "Returning {EmployeeCount} employees",
    employees.Length);
```

For larger applications, prefer injecting `ILogger<EmployeeController>` where appropriate. Serilog.AspNetCore routes ASP.NET Core's `ILogger` messages through the configured Serilog pipeline.
