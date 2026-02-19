using E_Commerce.App.Domain.Common;
using E_Commerce.App.Domain.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructre.presistent.Repositieries.Generic_Repository
{
    internal static class SpecificationsEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery (IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        {
            var query = inputQuery; // --> dbcontext.Set<>
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if(spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            else if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

                query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;   
         }
    }
}
