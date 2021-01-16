using GoodsReseller.DataCatalogContext.Contracts.Models.Products;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.BatchByIds
{
    public class BatchProductsByIdsResponse
    {
        public ProductListContract ProductList { get; set; }
    }
}