using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GoodsReseller.SeedWork
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        Task<TAggregateRoot> GetAsync(Guid id, CancellationToken cancellationToken);
        
        Task<IEnumerable<TAggregateRoot>> FindAsync(
            Expression<Func<TAggregateRoot, bool>> predicate,
            CancellationToken cancellationToken);
    }
}