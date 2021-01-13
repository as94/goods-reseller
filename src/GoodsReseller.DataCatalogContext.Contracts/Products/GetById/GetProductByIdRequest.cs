using System;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.GetById
{
    public class GetProductByIdRequest : IRequest<GetProductByIdResponse>
    {
        public Guid ProductId { get; set; }
    }
}