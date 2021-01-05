using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;

namespace GoodsReseller.Infrastructure.Orders
{
    internal sealed class OrdersRepository : IOrdersRepository
    {
        public Task<Order> GetAsync(Guid orderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Order order, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}