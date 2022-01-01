using GoodsReseller.DataCatalogContext.Contracts.Models.Products;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.Create
{
    public class CreateProductRequest : IRequest
    {
        public ProductInfoContract ProductInfo { get; set; }
    }
}