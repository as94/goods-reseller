using System.Threading;
using System.Threading.Tasks;

namespace GoodsReseller.SeedWork
{
    public interface IUnitOfWork
    {
        void RegisterNew<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : class, IAggregateRoot;
        void RegisterDirty<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : class, IAggregateRoot;
        void RegisterRemove<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : class, IAggregateRoot;
        
        Task CommitAsync(CancellationToken cancellationToken);
    }
}