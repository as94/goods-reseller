using System;
using GoodsReseller.DataCatalogContext.Contracts.Models;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.Update
{
    public class UpdateProductRequest : IRequest<Unit>
    {
        public Guid ProductId { get; set; }
        
        public ProductInfoContract ProductInfo { get; set; }
    }
}