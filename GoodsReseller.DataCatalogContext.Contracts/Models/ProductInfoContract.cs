namespace GoodsReseller.DataCatalogContext.Contracts.Models
{
    public class ProductInfoContract
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPerUnit { get; set; }
    }
}