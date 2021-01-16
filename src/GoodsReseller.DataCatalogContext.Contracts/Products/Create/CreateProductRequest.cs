using GoodsReseller.DataCatalogContext.Contracts.Models;
using GoodsReseller.DataCatalogContext.Contracts.Models.Products;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.Create
{
    public class CreateProductRequest : IRequest<CreateProductResponse>
    {
        public ProductInfoContract ProductInfo { get; set; }
    }
}