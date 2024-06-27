using Microsoft.EntityFrameworkCore;

namespace LogInSystem.DataContext
{
    public class LogInContext : DbContext
    {
        public LogInContext(DbContextOptions<LogInContext> options) : base(options) { }
    }
}
