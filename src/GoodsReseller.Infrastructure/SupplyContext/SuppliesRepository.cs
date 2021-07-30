using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Domain.Supplies;
using GoodsReseller.SupplyContext.Domain.Supplies.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodsReseller.Infrastructure.SupplyContext
{
    internal sealed class SuppliesRepository : ISuppliesRepository
    {
        private readonly GoodsResellerDbContext _dbContext;

        public SuppliesRepository(GoodsResellerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Supply> GetAsync(Guid supplyId, CancellationToken cancellationToken)
        {
            return await _dbContext.Supplies
                .Include(x => x.SupplyItems)
                .FirstOrDefaultAsync(x => x.Id == supplyId && !x.IsRemoved,
                    cancellationToken);
        }

        public async Task<IEnumerable<Supply>> BatchAsync(int offset, int count, CancellationToken cancellationToken)
        {
            return (await _dbContext.Supplies
                    .Include(x => x.SupplyItems)
                    .Where(x => !x.IsRemoved)
                    .OrderBy(x => x.LastUpdateDate != null ? x.LastUpdateDate.DateUtc : x.CreationDate.DateUtc)
                    .Skip(offset)
                    .Take(count)
                    .ToListAsync(cancellationToken))
                .AsReadOnly();
        }

        public async Task SaveAsync(Supply supply, CancellationToken cancellationToken)
        {
            if (supply == null)
            {
                throw new ArgumentNullException(nameof(supply));
            }
            // TODO: add handling concurrency (like OrdersRepository)
            
            var existing = await _dbContext.Supplies.FirstOrDefaultAsync(
                x => x.Id == supply.Id,
                cancellationToken);
            
            if (existing == null)
            {
                await _dbContext.Supplies.AddAsync(supply, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}