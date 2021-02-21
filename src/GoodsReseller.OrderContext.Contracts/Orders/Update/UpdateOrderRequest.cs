using System.ComponentModel.DataAnnotations;
using GoodsReseller.OrderContext.Contracts.Models;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.Orders.Update
{
    public class UpdateOrderRequest : IRequest<UpdateOrderResponse>
    {
        [Required]
        public AddressContract Address { get; set; }
        
        [Required]
        public CustomerInfoContract CustomerInfo { get; set; }
    }
}