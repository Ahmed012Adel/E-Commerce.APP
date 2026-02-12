using E_Commerce.App.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructure.presistent._Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());    
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductBrand>? Brands { get; set; }
        public DbSet<ProductCategory>? Categories { get; set; }
    }
}
