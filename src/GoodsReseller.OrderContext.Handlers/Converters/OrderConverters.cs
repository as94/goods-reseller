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
                Status = order.Status.Name,
                Date = order.LastUpdateDate != null ? order.LastUpdateDate.DateUtc : order.CreationDate.DateUtc,
                Address = order.Address.ToContract(),
                CustomerInfo = order.CustomerInfo.ToContract(),
                OrderItems = order.GetExistingOrderItems().Select(x => x.ToContract()).ToArray(),
                DeliveryCost = order.DeliveryCost.Value,
                AddedCost = order.AddedCost.Value,
                TotalCost = order.TotalCost.Value
            };
        }

        public static OrderListItemContract ToListItemContract(this Order order)
        {
            return new OrderListItemContract
            {
                Id = order.Id,
                Version = order.Version,
                Status = order.Status.Name,
                Date = order.LastUpdateDate != null ? order.LastUpdateDate.DateUtc : order.CreationDate.DateUtc,
                CustomerPhoneNumber = order.CustomerInfo.PhoneNumber,
                CustomerName = order.CustomerInfo.Name,
                AddressCity = order.Address.City,
                AddressStreet = order.Address.Street,
                AddressZipCode = order.Address.ZipCode,
                TotalCost = order.TotalCost.Value
            };
        }
    }
}