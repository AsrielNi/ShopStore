using Microsoft.EntityFrameworkCore;
using ShopApplication.Areas.LogInSystem.Models;

namespace ShopApplication.Areas.LogInSystem.Data
{
    public class LogInContext : DbContext
    {
        public LogInContext(DbContextOptions<LogInContext> options) : base(options) { }

        public DbSet<RegistrantsModel> Registrants { get; set; }
    }
}
