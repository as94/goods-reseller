using System;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.OrderItems.PatchOrderItem
{
    public class PatchOrderItemRequest : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
        
        public string Op { get; set; }
        public Guid ProductId { get; set; }
    }
}