using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;

namespace GoodsReseller.OrderContext.Domain.Orders.Entities
{
    public sealed class OrderInfo
    {
        public OrderInfo(string? status, Address? address, CustomerInfo? customerInfo)
        {
            Status = status;
            Address = address;
            CustomerInfo = customerInfo;
        }

        public string? Status { get; }
        public Address? Address { get; }
        public CustomerInfo? CustomerInfo { get; }
    }
}