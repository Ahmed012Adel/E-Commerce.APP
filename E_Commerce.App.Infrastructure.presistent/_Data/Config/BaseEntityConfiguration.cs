using E_Commerce.App.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.App.Infrastructure.presistent._Data.Config
{
    internal class BaseEntityConfiguration<TEntity, Tkey> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity<Tkey> where Tkey : IEquatable<Tkey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(E => E.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
