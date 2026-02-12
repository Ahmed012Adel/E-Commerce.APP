using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.Base;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.ProductConfig
{
    internal class CategoryConfigurations : BaseEntityConfiguration<ProductCategory,int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);
            builder.Property(B => B.Name).IsRequired();

        }
    }
}
