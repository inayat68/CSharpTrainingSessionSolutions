using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemClient>().HasKey(ic => new
            {
                ic.ItemId,
                ic.ClientId
            });

            modelBuilder.Entity<ItemClient>().HasOne(i => i.Item).WithMany(ic => ic.ItemClients).HasForeignKey(i => i.ItemId);
            modelBuilder.Entity<ItemClient>().HasOne(c => c.Client).WithMany(ic => ic.ItemClients).HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Item>().HasData(
                new Item { Id=4, Name="microphone", Price=40, SerialNumberId=10 }
                );
            modelBuilder.Entity<SerialNumber>().HasData(
                new SerialNumber { Id = 10, Name = "MIC150", ItemId=4 }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Electronics"},
                new Category { Id = 2, Name = "Books" }
                );

            //modelBuilder.Entity<Employee>()
            //.HasOne(e => e.Department)
            //.WithMany(d => d.Employees)
            //.HasForeignKey(e => e.DepartmentId)
            //.OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Item> Items { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ItemClient> ItemClients { get; set; }
        public DbSet<ErrorViewModel> ErrorViewModels { get; set; }
        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<Department> Departments { get; set; }
    }
}
