using System;
using System.ComponentModel.DataAnnotations;
using GoodsReseller.OrderContext.Contracts.Models;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.Orders.Update
{
    public class UpdateOrderRequest : IRequest<Unit>
    {
        [Required]
        public Guid OrderId { get; set; }
        
        [Required]
        public AddressContract Address { get; set; }
        
        [Required]
        public CustomerInfoContract CustomerInfo { get; set; }
    }
}