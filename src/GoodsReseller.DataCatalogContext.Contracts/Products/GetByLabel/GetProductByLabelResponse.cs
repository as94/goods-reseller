using GoodsReseller.DataCatalogContext.Contracts.Models.Products;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.GetByLabel
{
    public class GetProductByLabelResponse
    {
        public ProductContract Product { get; set; }
    }
}