using System;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.Delete
{
    public class DeleteProductByIdRequest : IRequest<Unit>
    {
        public Guid ProductId { get; set; }
    }
}