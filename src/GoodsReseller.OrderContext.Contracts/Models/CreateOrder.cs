using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class CreateOrder
    {
        [Required]
        public AddressContract Address { get; set; }
        
        [Required]
        public CustomerInfoContract CustomerInfo { get; set; }
    }
}