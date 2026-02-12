using E_Commerce.App.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Domain.Contract
{
    public interface IUnitOfWork :IAsyncDisposable
    {

        IGenericRepositieries<TEntity, Tkey> GetRepositieries<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>;
        Task<int> SaveChangesAsync();
    }
}
