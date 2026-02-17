using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Domain.Contract.Peresistence
{
    public interface IStroreContextIntializer
    {
        Task UpdateDateBase();
            Task SeedData(string ContenRootpath);
    }
}
