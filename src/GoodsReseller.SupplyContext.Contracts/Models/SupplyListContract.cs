namespace GoodsReseller.SupplyContext.Contracts.Models
{
    public class SupplyListContract
    {
        public SupplyListItemContract[] Items { get; set; }
        public int RowsCount { get; set; }
    }
}