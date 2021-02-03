using System.Linq;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.Entities;

namespace GoodsReseller.OrderContext.Handlers.Converters
{
    internal static class OrderConverters
    {
        public static OrderContract ToContract(this Order order)
        {
            return new OrderContract
            {
                Id = order.Id,
                Version = order.Version,
                Address = order.Address.ToContract(),
                CustomerInfo = order.CustomerInfo.ToContract(),
                OrderItems = order.OrderItems.Select(x => x.ToContract()).ToArray(),
                TotalCost = order.TotalCost.ToContract()
            };
        }

        public static OrderListItemContract ToListItemContract(this Order order)
        {
            return new OrderListItemContract
            {
                Id = order.Id,
                Version = order.Version,
                CustomerPhoneNumber = order.CustomerInfo.PhoneNumber,
                CustomerName = order.CustomerInfo.Name,
                AddressCity = order.Address.City,
                AddressStreet = order.Address.Street,
                AddressZipCode = order.Address.ZipCode,
                OrderStatus = 0, // TODO: add order status
                TotalCost = order.TotalCost.Value
            };
        }
    }
}