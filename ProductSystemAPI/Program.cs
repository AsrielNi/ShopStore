using Microsoft.EntityFrameworkCore;
using ProductSystemAPI.Data;

namespace ProductSystemAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DataBase to the container.
            builder.Services.AddDbContext<ProductContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("ProductAPIDataBase")));

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
