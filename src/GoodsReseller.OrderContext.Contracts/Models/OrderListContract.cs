namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderListContract
    {
        public OrderListItemContract[] Items { get; set; }
        public int RowsCount { get; set; }
    }
}