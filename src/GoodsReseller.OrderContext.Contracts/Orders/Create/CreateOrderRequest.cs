using System.ComponentModel.DataAnnotations;
using GoodsReseller.OrderContext.Contracts.Models;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.Orders.Create
{
    public class CreateOrderRequest : IRequest<CreateOrderResponse>
    {
        [Required]
        public AddressContract Address { get; set; }
        
        [Required]
        public CustomerInfoContract CustomerInfo { get; set; }

        [Required]
        public MoneyContract DeliveryCost { get; set; }
    }
}