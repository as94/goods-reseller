using System;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.Orders.DeleteById
{
    public class DeleteOrderByIdRequest : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
    }
}