using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Domain.Orders.Entities;

namespace GoodsReseller.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(Guid orderId, CancellationToken cancellationToken);
        Task SaveAsync(Order order, CancellationToken cancellationToken);
    }
}