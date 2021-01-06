using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.OrderItems.RemoveOrderItem
{
    public class RemoveOrderItemRequest : IRequest<RemoveOrderItemResponse>
    {
        public Guid OrderId { get; set; }
        
        [Required]
        public Guid ProductId { get; set; }
    }
}