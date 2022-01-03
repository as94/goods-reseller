namespace GoodsReseller.DataCatalogContext.Contracts.Models.Products
{
    public class ProductListContract
    {
        public ProductListItemContract[] Items { get; set; }
        public int RowsCount { get; set; }
    }
}