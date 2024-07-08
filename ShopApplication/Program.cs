using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Areas.LogInSystem.Data;

namespace ShopApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ShopContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("ShopDataBase")));

            // 連線至登入系統的資料庫
            builder.Services.AddDbContext<LogInContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("LogInSystemDataBase")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Connect to LogInAPI via WebAPI
            LogInAPI.APItoLINK.AttachAPI(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapAreaControllerRoute(
                name: "LogInSystem",
                areaName: "LogInSystem",
                pattern: "LogInSystem/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");



            app.Run();
        }
    }
}
