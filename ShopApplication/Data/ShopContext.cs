using Microsoft.EntityFrameworkCore;
using ShopApplication.Models;

namespace ShopApplication.Data
{
    public class ShopContext: DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
        public DbSet<CustomerInfoModel> CustomerInfo { get; set; }
    }
}
