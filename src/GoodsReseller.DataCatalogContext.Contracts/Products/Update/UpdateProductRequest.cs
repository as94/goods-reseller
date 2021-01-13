using System;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.Update
{
    public class UpdateProductRequest : IRequest<Unit>
    {
        public Guid ProductId { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPerUnit { get; set; }
    }
}