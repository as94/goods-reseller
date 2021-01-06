using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.OrderItems.AddOrderItem
{
    public class AddOrderItemRequest : IRequest<AddOrderItemResponse>
    {
        public Guid OrderId { get; set; }
        
        [Required]
        public Guid ProductId { get; set; }
    }
}