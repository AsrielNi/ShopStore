using Microsoft.EntityFrameworkCore;
using ProductSystemAPI.Models;

namespace ProductSystemAPI.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        public DbSet<Product> ProductInfo { get; set; }
    }
}
