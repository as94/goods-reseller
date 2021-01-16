using GoodsReseller.DataCatalogContext.Contracts.Models;
using GoodsReseller.DataCatalogContext.Contracts.Models.Products;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.GetById
{
    public class GetProductByIdResponse
    {
        public ProductContract Product { get; set; }
    }
}