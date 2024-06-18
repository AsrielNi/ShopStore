using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using TestZone.Data;

namespace TestZone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext

            builder.Services.AddDbContext<ShopContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("ShopDataBase")));
            builder.Services.AddDbContext<TestContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("TestDataBase")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
