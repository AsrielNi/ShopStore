using Microsoft.EntityFrameworkCore;
using LogInSystem.Models;

namespace LogInSystem.DataContext
{
    public class LogInContext : DbContext
    {
        public LogInContext(DbContextOptions<LogInContext> options) : base(options) { }
        public DbSet<RegistrantsModel> Registrants { get; set; }
    }
}
