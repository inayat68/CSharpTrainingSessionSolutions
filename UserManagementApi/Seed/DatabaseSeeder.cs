using UserManagementApi.Data;
using UserManagementApi.Models;
using UserManagementApi.Services;

namespace UserManagementApi.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        AppDbContext db)
    {
        if (db.Roles.Any())
            return;

        var passwordService =
            new PasswordService();

        var roles = new List<Role>
        {
            new Role { Name="Admin" },
            new Role { Name="Manager" },
            new Role { Name="Employee" }
        };

        db.Roles.AddRange(roles);

        await db.SaveChangesAsync();

        var admin = new User
        {
            Name = "System Admin",
            Email = "admin@example.com",
            Password = passwordService.HashPassword("Admin123"),
            RoleId = 1,
            JoiningDate = DateTime.Now,
            IsLoggedIn = false
        };

        var manager1 = new User
        {
            Name = "Manager One",
            Email = "manager1@example.com",
            Password = passwordService.HashPassword("Manager123"),
            RoleId = 2,
            JoiningDate = DateTime.Now,
            IsLoggedIn = false
        };

        var manager2 = new User
        {
            Name = "Manager Two",
            Email = "manager2@example.com",
            Password = passwordService.HashPassword("Manager123"),
            RoleId = 2,
            JoiningDate = DateTime.Now,
            IsLoggedIn = false
        };

        db.Users.Add(admin);
        db.Users.Add(manager1);
        db.Users.Add(manager2);

        await db.SaveChangesAsync();

        var employees = new List<User>
        {
            new User
            {
                Name="Employee One",
                Email="emp1@example.com",
                Password=passwordService.HashPassword("Emp123"),
                RoleId=3,
                ManagerId=manager1.Id,
                JoiningDate=DateTime.Now
            },
            new User
            {
                Name="Employee Two",
                Email="emp2@example.com",
                Password=passwordService.HashPassword("Emp123"),
                RoleId=3,
                ManagerId=manager1.Id,
                JoiningDate=DateTime.Now
            },
            new User
            {
                Name="Employee Three",
                Email="emp3@example.com",
                Password=passwordService.HashPassword("Emp123"),
                RoleId=3,
                ManagerId=manager2.Id,
                JoiningDate=DateTime.Now
            }
        };

        db.Users.AddRange(employees);

        await db.SaveChangesAsync();

        db.Tasks.AddRange(
            new TaskItem
            {
                Title = "API Development",
                Description = "Develop User APIs",
                CreatedAt = DateTime.Now,
                AssignedBy = "admin@example.com",
                Status = 25,
                IsAssigned = true,
                UserId = employees[0].Id
            },
            new TaskItem
            {
                Title = "Testing",
                Description = "Swagger Testing",
                CreatedAt = DateTime.Now,
                AssignedBy = "manager1@example.com",
                Status = 50,
                IsAssigned = true,
                UserId = employees[1].Id
            },
            new TaskItem
            {
                Title = "Documentation",
                Description = "Create API Docs",
                CreatedAt = DateTime.Now,
                AssignedBy = "manager2@example.com",
                Status = 75,
                IsAssigned = true,
                UserId = employees[2].Id
            }
        );

        await db.SaveChangesAsync();
    }
}