using System;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.UpdateProductPhoto
{
    public class UpdateProductPhotoRequest : IRequest
    {
        public Guid ProductId { get; set; }
        public string PhotoPath { get; set; }
    }
}