namespace GoodsReseller.Infrastructure.Abstractions
{
    // public abstract class Repository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    // {
    //     private readonly DbSet<TAggregateRoot> _dbSet;
    //     
    //     protected Repository(GoodsResellerDbContext dbContext)
    //     {
    //         if (dbContext == null)
    //         {
    //             throw new ArgumentNullException(nameof(dbContext));
    //         }
    //         
    //         _dbSet = dbContext.Set<TAggregateRoot>();
    //     }
    //     
    //     public Task<TAggregateRoot> GetAsync(Guid id, CancellationToken cancellationToken)
    //     {
    //         return _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    //     }
    //
    //     public async Task<IEnumerable<TAggregateRoot>> FindAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken)
    //     {
    //         return (await _dbSet.Where(predicate)
    //                 .ToListAsync(cancellationToken))
    //             .AsReadOnly();
    //     }
    // }
}