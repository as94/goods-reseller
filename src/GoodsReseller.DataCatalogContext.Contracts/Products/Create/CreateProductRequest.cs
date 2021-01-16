using GoodsReseller.DataCatalogContext.Contracts.Models;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.Create
{
    public class CreateProductRequest : IRequest<CreateProductResponse>
    {
        public ProductInfoContract ProductInfo { get; set; }
    }
}