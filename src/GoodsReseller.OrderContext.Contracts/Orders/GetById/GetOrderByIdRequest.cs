using System;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.Orders.GetById
{
    public class GetOrderByIdRequest : IRequest<GetOrderByIdResponse>
    {
        public Guid OrderId { get; set; }
    }
}