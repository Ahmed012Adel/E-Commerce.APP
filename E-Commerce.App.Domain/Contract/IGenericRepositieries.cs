using E_Commerce.App.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Domain.Contract
{
    public interface IGenericRepositieries<TEntity , Tkey> 
        where TEntity : BaseEntity<Tkey> 
        where Tkey : IEquatable<Tkey>
    {
        Task <IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false);
        
        Task<TEntity?> GetAsync(Tkey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
