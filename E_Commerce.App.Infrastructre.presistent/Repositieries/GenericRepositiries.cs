using E_Commerce.App.Domain.Common;
using E_Commerce.App.Domain.Contract;
using E_Commerce.App.Infrastructre.presistent._Data;
using Microsoft.EntityFrameworkCore;
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
            =>  WithTracking? await dbContext.Set<TEntity>().ToListAsync() : 
                              await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey id)
            => await dbContext.Set<TEntity>().FindAsync(id);


        public async Task AddAsync(TEntity entity)
            => await dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);



        public void Update(TEntity entity)
            => dbContext.Set<TEntity>().Update(entity);
    }
}
