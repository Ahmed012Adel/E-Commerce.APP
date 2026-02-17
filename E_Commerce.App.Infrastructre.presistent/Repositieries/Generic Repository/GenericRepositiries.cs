using E_Commerce.App.Domain.Common;
using E_Commerce.App.Domain.Contract;
using E_Commerce.App.Domain.Contract.Peresistence;
using E_Commerce.App.Domain.Entities.Product;
using E_Commerce.App.Infrastructre.presistent._Data;
using E_Commerce.App.Infrastructre.presistent.Repositieries.Generic_Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructre.presistent.Repositieries
{
    internal class GenericRepositiries<TEntity, TKey>(StoreDbContext dbContext) : IGenericRepositieries<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>

    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking)
        {
           return WithTracking ? await dbContext.Set<TEntity>().ToListAsync() :
                  await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }


        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllSpecAsync(ISpecifications<TEntity, TKey> spec, bool WithTracking = false)
        {
            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(dbContext.Set<TEntity>(), spec).ToListAsync();
        }

        public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(dbContext.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
            => await dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);



        public void Update(TEntity entity)
            => dbContext.Set<TEntity>().Update(entity);
    }
}
