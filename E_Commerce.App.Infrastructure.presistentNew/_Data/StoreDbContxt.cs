using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Infrastructrure.Persistence.Common;
using System.Reflection;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data
{
    public class StoreDbContxt : DbContext
    {
        public StoreDbContxt(DbContextOptions<StoreDbContxt> option) : base(option) 
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
                type => type.GetCustomAttributes<DbContextTypeAttribute>().FirstOrDefault()?.DbcontextType == typeof(StoreDbContxt));
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> categories { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
    }

}
