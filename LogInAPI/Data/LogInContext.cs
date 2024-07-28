using Microsoft.EntityFrameworkCore;
using LogInAPI.Models;

namespace LogInAPI.Data
{
    public class LogInContext : DbContext
    {
        public LogInContext(DbContextOptions<LogInContext> options) : base(options) { }

        // 對應資料表AccountData
        public DbSet<AccountModel> AccountData { get; set; }
    }
}
