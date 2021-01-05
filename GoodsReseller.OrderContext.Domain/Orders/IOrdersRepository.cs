using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Domain.Orders.Entities;

namespace GoodsReseller.OrderContext.Domain.Orders
{
    public interface IOrdersRepository
    {
        Task<Order> GetAsync(Guid orderId, CancellationToken cancellationToken);
        Task SaveAsync(Order order, CancellationToken cancellationToken);
    }
}