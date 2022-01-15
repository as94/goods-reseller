using System;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork;

namespace GoodsReseller.OrderContext.Handlers.Converters
{
    public static class OrderStatusConverters
    {
        public static OrderStatus ToDomain(this string status)
        {
            if (!Enumeration.TryParse<OrderStatus>(status, out var parsedStatus))
            {
                // TODO: business rule, add translations
                throw new ArgumentException($"Status '{status}' is invalid");
            }

            return parsedStatus;
        }
    }
}