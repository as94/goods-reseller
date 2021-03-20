using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.OrderContext.Domain.Orders.Entities
{
    public sealed class OrderInfo
    {
        public OrderInfo(string? status, Address? address, CustomerInfo? customerInfo, Money? deliveryCost)
        {
            Status = status;
            Address = address;
            CustomerInfo = customerInfo;
            DeliveryCost = deliveryCost;
        }

        public string? Status { get; }
        public Address? Address { get; }
        public CustomerInfo? CustomerInfo { get; }
        public Money? DeliveryCost { get; }
    }
}