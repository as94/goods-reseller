using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class CreateOrderContract
    {
        [Required]
        public AddressContract Address { get; set; }
        
        [Required]
        public CustomerInfoContract CustomerInfo { get; set; }
        
        [Required]
        public MoneyContract DeliveryCost { get; set; }
    } 
}