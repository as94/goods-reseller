using GoodsReseller.OrderContext.Contracts.Models;

namespace GoodsReseller.OrderContext.Contracts.Orders.GetById
{
    public class GetOrderByIdResponse
    {
        public OrderContract Order { get; set; }
    }
}