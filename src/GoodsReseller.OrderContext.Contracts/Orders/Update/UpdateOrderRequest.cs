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
        
        public string? Status { get; set; }
        
        public AddressContract? Address { get; set; }
        
        public CustomerInfoContract? CustomerInfo { get; set; }
        
        public MoneyContract? DeliveryCost { get; set; }
    }
}