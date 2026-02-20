using E_Commerce.App.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Domain.Contract.Peresistence
{
    public interface IGenericRepositieries<TEntity , Tkey> 
        where TEntity : BaseEntity<Tkey> 
        where Tkey : IEquatable<Tkey>
    {
        Task <IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false);
        Task<IEnumerable<TEntity>> GetAllSpecAsync(ISpecifications<TEntity, Tkey> spec, bool WithTracking = false);
        Task<TEntity?> GetAsync(Tkey id);
        Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, Tkey> spec);
        Task<int> GetCountAsync(ISpecifications<TEntity , Tkey> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
