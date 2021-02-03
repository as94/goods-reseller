using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Domain.Orders.Entities;

namespace GoodsReseller.OrderContext.Domain.Orders
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Order>> BatchAsync(int offset, int count, CancellationToken cancellationToken);
        Task<Order> GetAsync(Guid orderId, CancellationToken cancellationToken);
        Task SaveAsync(Order order, CancellationToken cancellationToken);
    }
}