using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodsReseller.Infrastructure.OrderContext
{
    internal sealed class OrdersRepository : IOrdersRepository
    {
        private readonly GoodsResellerDbContext _dbContext;

        public OrdersRepository(GoodsResellerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> GetAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await GetOrderAsync(orderId, cancellationToken);
        }

        public async Task<IEnumerable<Order>> BatchAsync(int offset, int count, CancellationToken cancellationToken)
        {
            return (await _dbContext.Orders
                    .Include(x => x.OrderItems)
                    .Where(x => !x.IsRemoved)
                    .OrderBy(x => x.LastUpdateDate != null ? x.LastUpdateDate.DateUtc : x.CreationDate.DateUtc)
                    .Skip(offset)
                    .Take(count)
                    .ToListAsync(cancellationToken))
                .AsReadOnly();
        }

        public async Task SaveAsync(Order order, CancellationToken cancellationToken)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            
            var existing = await GetOrderAsync(order.Id, cancellationToken);
            if (existing == null)
            {
                await _dbContext.Orders.AddAsync(order, cancellationToken);
            }
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<Order> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == orderId && !x.IsRemoved,
                    cancellationToken);
        }
    }
}