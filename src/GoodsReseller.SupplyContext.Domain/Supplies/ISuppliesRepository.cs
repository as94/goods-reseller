using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Domain.Supplies.Entities;

namespace GoodsReseller.SupplyContext.Domain.Supplies
{
    public interface ISuppliesRepository
    {
        Task<Supply> GetAsync(Guid supplyId, CancellationToken cancellationToken);
        Task<(IEnumerable<Supply> Supplies, int RowsCount)> BatchAsync(int offset, int count, CancellationToken cancellationToken);
        Task SaveAsync(Supply supply, CancellationToken cancellationToken);
    }
}