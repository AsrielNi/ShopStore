using Microsoft.EntityFrameworkCore;

namespace LogInAPI.Data
{
    public class RegistrantContext: DbContext
    {
        public RegistrantContext(DbContextOptions<RegistrantContext> options) : base(options) { }
    }
}
