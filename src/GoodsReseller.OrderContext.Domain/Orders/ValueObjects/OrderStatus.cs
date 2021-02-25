using GoodsReseller.SeedWork;

namespace GoodsReseller.OrderContext.Domain.Orders.ValueObjects
{
    public sealed class OrderStatus : Enumeration
    {
        public OrderStatus(int id, string name) : base(id, name)
        {
        }
        
        public static readonly OrderStatus DataReceived = new OrderStatus(1, "DataReceived");
        public static readonly OrderStatus Accepted = new OrderStatus(2, "Accepted");
        public static readonly OrderStatus Collected = new OrderStatus(3, "Collected");
        public static readonly OrderStatus Packed = new OrderStatus(4, "Packed");
        public static readonly OrderStatus Shipped = new OrderStatus(5, "Shipped");
        public static readonly OrderStatus Completed = new OrderStatus(6, "Completed");
        public static readonly OrderStatus Canceled = new OrderStatus(7, "Canceled");
    }
}