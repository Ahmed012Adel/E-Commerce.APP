using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Infrastructrure.Persistence.Common;

namespace LinkDev.Talabat.Infrastructrure.Persistence.Data.Config.Base
{
    [DbContextTypeAttribute(typeof(StoreDbContxt))]
    internal class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(E => E.Id)
                .ValueGeneratedOnAdd();

         
        }
    }
}
