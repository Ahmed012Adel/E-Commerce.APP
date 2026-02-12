using LinkDev.Talabat.Core.Domain.Entities.Product;
using LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.Base;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.ProductConfig
{
    internal class BrandConfigurations : BaseEntityConfiguration<ProductBrand,int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(B=>B.Name).IsRequired();
        }
    }
}
