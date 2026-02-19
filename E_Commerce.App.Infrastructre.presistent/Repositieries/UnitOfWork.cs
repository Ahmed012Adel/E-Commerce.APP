using E_Commerce.App.Domain.Common;
using E_Commerce.App.Domain.Contract.Peresistence;
using E_Commerce.App.Infrastructre.presistent._Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructre.presistent.Repositieries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbcontext;
        private readonly ConcurrentDictionary<string, object> _Repositiries;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbcontext = dbContext;
            _Repositiries = new ConcurrentDictionary<string, object>();
        }


        public IGenericRepositieries<TEntity, Tkey> GetRepositieries<TEntity, Tkey>()
            where TEntity : BaseEntity<Tkey>
            where Tkey : IEquatable<Tkey>
        => (IGenericRepositieries<TEntity, Tkey>)_Repositiries.GetOrAdd(typeof(TEntity).Name, new GenericRepositiries<TEntity, Tkey>(_dbcontext));
        

        public async Task<int> SaveChangesAsync() => await _dbcontext.SaveChangesAsync();
        public async ValueTask DisposeAsync()=> await _dbcontext.DisposeAsync();

    }
}
