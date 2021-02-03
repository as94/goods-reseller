using GoodsReseller.OrderContext.Contracts.Models;

namespace GoodsReseller.OrderContext.Contracts.Orders.BatchByQuery
{
    public class BatchOrdersByQueryResponse
    {
        public OrderListContract OrderList { get; set; }
    }
}