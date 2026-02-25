using E_Commerce.App.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.App.Infrastructre.presistent._Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> vendors { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
    }
}
