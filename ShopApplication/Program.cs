using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShopApplication.Data;

namespace ShopApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ShopContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("ShopDataBase")));


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Connect to LogInAPI via WebAPI
            LogInAPI.APItoLINK.AttachAPI(builder);
            ProductSystemAPI.APItoLINK.AttachAPI(builder);

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

            // Connect to wwwroot of APIs
            LogInAPI.APItoLINK.AttachSource(app);
            ProductSystemAPI.APItoLINK.AttachSource(app);

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");



            app.Run();
        }
    }
}
