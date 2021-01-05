using System;
using System.Linq;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.Entities;

namespace GoodsReseller.OrderContext.Handlers.Converters
{
    public static class OrderConverters
    {
        public static OrderContract ToContract(this Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            
            return new OrderContract
            {
                Id = order.Id,
                Version = order.Version,
                OrderItems = order.OrderItems.Select(x => x.ToContract()).ToArray(),
                TotalCost = order.TotalCost.ToContract()
            };
        }
    }
}