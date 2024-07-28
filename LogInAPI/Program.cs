using LogInAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LogInAPI
{
    public class Program
    {
        // 硂场だ祘Α秈度度琌代刚WebAPI琌Τタ盽笲
        // 代刚场だ穦硓筁SwaggerㄓЧΘ
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add database to the container.

            builder.Services.AddDbContext<LogInContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("LogInAPIDataBase")));


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
