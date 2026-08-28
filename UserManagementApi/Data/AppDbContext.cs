using Microsoft.EntityFrameworkCore;
using UserManagementApi.Models;

namespace UserManagementApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(x => x.Manager)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }

    /*
     * -- Roles definition
        CREATE TABLE "Roles" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_Roles" PRIMARY KEY AUTOINCREMENT,
            "Name" TEXT NOT NULL
        );

    -- Users definition
        CREATE TABLE "Users" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT,
            "ManagerId" INTEGER NULL,
            "Name" TEXT NOT NULL,
            "Email" TEXT NOT NULL,
            "Password" TEXT NOT NULL,
            "RoleId" INTEGER NULL,
            "JoiningDate" TEXT NOT NULL,
            "IsLoggedIn" INTEGER NOT NULL,
            CONSTRAINT "FK_Users_Roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id"),
            CONSTRAINT "FK_Users_Users_ManagerId" FOREIGN KEY ("ManagerId") REFERENCES "Users" ("Id") ON DELETE RESTRICT
        );

        CREATE UNIQUE INDEX "IX_Users_Email" ON "Users" ("Email");
        CREATE INDEX "IX_Users_ManagerId" ON "Users" ("ManagerId");
        CREATE INDEX "IX_Users_RoleId" ON "Users" ("RoleId");

    -- Tasks definition
        CREATE TABLE "Tasks" (
            "Id" INTEGER NOT NULL CONSTRAINT "PK_Tasks" PRIMARY KEY AUTOINCREMENT,
            "Title" TEXT NOT NULL,
            "Description" TEXT NOT NULL,
            "CreatedAt" TEXT NOT NULL,
            "AssignedBy" TEXT NOT NULL,
            "Status" INTEGER NOT NULL,
            "CompletionDate" TEXT NULL,
            "FilePath" TEXT NULL,
            "IsAssigned" INTEGER NOT NULL,
            "UserId" INTEGER NOT NULL,
            CONSTRAINT "FK_Tasks_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
        );

        CREATE INDEX "IX_Tasks_UserId" ON "Tasks" ("UserId");

     */
}