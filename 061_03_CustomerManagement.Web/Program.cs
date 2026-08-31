using CustomerManagement.Core;
using System.Configuration;

namespace CustomerManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ------------------------------------------------------------
            // ASP.NET Core automatically loads appsettings.json.
            // ------------------------------------------------------------

            // Read the shared ConnectionStringSettings class from
            // the ConnectionStrings section of appsettings.json.
            //var connectionSettings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStringSettings>() ?? new ConnectionStringSettings();

            // Register the settings object so it can be injected into
            // Controllers or other services later.
            //builder.Services.AddSingleton(connectionSettings);

            builder.Services.AddSingleton<ConnectionStringSetup>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
