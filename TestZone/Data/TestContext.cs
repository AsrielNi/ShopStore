using Microsoft.EntityFrameworkCore;
using TestZone.Models;

namespace TestZone.Data
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options) { }
        public DbSet<UserSessionModel> UserSession { get; set; }
    }
}
