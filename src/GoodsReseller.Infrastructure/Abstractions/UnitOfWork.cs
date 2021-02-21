namespace GoodsReseller.Infrastructure.Abstractions
{
    // public sealed class UnitOfWork : IUnitOfWork
    // {
    //     private readonly GoodsResellerDbContext _dbContext;
    //
    //     public UnitOfWork(GoodsResellerDbContext dbContext)
    //     {
    //         if (dbContext == null)
    //         {
    //             throw new ArgumentNullException(nameof(dbContext));
    //         }
    //         
    //         _dbContext = dbContext;
    //     }
    //
    //     public void RegisterNew<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : class, IAggregateRoot
    //     {
    //         if (aggregateRoot == null)
    //         {
    //             throw new ArgumentNullException(nameof(aggregateRoot));
    //         }
    //
    //         _dbContext.Set<TAggregateRoot>().Add(aggregateRoot);
    //     }
    //
    //     public void RegisterDirty<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : class, IAggregateRoot
    //     {
    //         if (aggregateRoot == null)
    //         {
    //             throw new ArgumentNullException(nameof(aggregateRoot));
    //         }
    //
    //         _dbContext.Set<TAggregateRoot>().Update(aggregateRoot);
    //     }
    //
    //     public void RegisterRemove<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : class, IAggregateRoot
    //     {
    //         if (aggregateRoot == null)
    //         {
    //             throw new ArgumentNullException(nameof(aggregateRoot));
    //         }
    //
    //         _dbContext.Set<TAggregateRoot>().Remove(aggregateRoot);
    //     }
    //
    //     public async Task CommitAsync(CancellationToken cancellationToken)
    //     {
    //         await _dbContext.SaveChangesAsync(cancellationToken);
    //     }
    // }
}