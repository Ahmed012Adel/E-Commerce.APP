using E_Commerce.App.Domain.Entities.Identity;
using E_Commerce.App.Infrastructre.presistent.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructre.presistent.Identity.Config
{
    [DbContxtType(typeof(StorIdentityDbContext))]

    internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationsUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationsUser> builder)
        {
            builder.Property(U => U.DisableName)
                .IsRequired(true)
                .HasColumnType("varchar")
                .HasMaxLength(16);

            builder.HasOne(U=>U.Address)
                .WithOne(A=>A.User)
                .HasForeignKey<Address>(A => A.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
