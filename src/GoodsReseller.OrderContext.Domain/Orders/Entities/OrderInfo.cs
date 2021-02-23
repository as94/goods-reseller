using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;

namespace GoodsReseller.OrderContext.Domain.Orders.Entities
{
    public sealed class OrderInfo
    {
        public OrderInfo(Address? address, CustomerInfo? customerInfo)
        {
            Address = address;
            CustomerInfo = customerInfo;
        }

        public Address? Address { get; }
        public CustomerInfo? CustomerInfo { get; }
    }
}