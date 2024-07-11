using Microsoft.EntityFrameworkCore;
using LogInAPI.Models;

namespace LogInAPI.Data
{
    public class RegistrantContext: DbContext
    {
        public RegistrantContext(DbContextOptions<RegistrantContext> options) : base(options) { }
        public DbSet<Registrant> RegistrantData { get; set; }
    }
}
