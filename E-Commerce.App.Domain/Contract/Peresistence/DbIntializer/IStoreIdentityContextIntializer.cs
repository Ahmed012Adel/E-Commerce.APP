using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Domain.Contract.Peresistence.DbIntializer
{
    public interface IStoreIdentityContextIntializer
    {
        Task UpdateDateBase();
        Task SeedData();
    }
}
