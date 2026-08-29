using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UserManagementApi.Data;
using UserManagementApi.Middleware;
using UserManagementApi.Repositories;
using UserManagementApi.Seed;
using UserManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var dbNameWithPath = Environment.GetEnvironmentVariable("DB_PATH") ?? "Database/cs_users_db.db";
//      OR
var dbNameWithPath2 = Path.Combine(AppContext.BaseDirectory.Replace("\\bin\\Debug\\net8.0", ""), "Database", "cs_users_db.db");

var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? "dev_key";
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "api";
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? "api_users";
var frontend = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:5173";

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

builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("frontend", p =>
        p.WithOrigins(frontend).AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<PasswordService>();

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

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

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

var app = builder.Build();

app.UseSwagger();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementApi v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("frontend");

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    await DatabaseSeeder.SeedAsync(db);
}

app.Run();


// DB Packages
//# SQLite provider
//dotnet add package Microsoft.EntityFrameworkCore.Sqlite

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