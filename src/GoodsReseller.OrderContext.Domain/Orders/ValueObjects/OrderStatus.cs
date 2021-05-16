using GoodsReseller.SeedWork;

namespace GoodsReseller.OrderContext.Domain.Orders.ValueObjects
{
    public sealed class OrderStatus : Enumeration
    {
        public OrderStatus(int id, string name) : base(id, name)
        {
        }
        
        public static readonly OrderStatus Accepted = new OrderStatus(1, "Accepted");
        public static readonly OrderStatus Packed = new OrderStatus(2, "Packed");
        public static readonly OrderStatus Shipped = new OrderStatus(3, "Shipped");
        public static readonly OrderStatus Canceled = new OrderStatus(4, "Canceled");
    }
}