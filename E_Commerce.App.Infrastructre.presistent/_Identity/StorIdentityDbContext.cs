using E_Commerce.App.Domain.Entities.Identity;
using E_Commerce.App.Infrastructre.presistent.Common;
using E_Commerce.App.Infrastructre.presistent.Identity.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructre.presistent.Identity
{
    public class StorIdentityDbContext :IdentityDbContext<ApplicationsUser>
    {
        public StorIdentityDbContext(DbContextOptions<StorIdentityDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(StorIdentityDbContext).Assembly,
                type => type.GetCustomAttribute<DbContxtTypeAttribute>()?.DbContextType == typeof(StorIdentityDbContext));
        }

        public DbSet<Address> Addresses { get; set; }
    }
}
