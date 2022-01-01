using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.UpdateProductPhoto
{
    public class UpdateProductPhotoRequest : IRequest
    {
        public Guid ProductId { get; set; }

        public int Version { get; set; }
        public string PhotoPath { get; set; }
    }
}