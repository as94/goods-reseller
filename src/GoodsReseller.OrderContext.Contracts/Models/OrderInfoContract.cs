using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderInfoContract
    {
        [Required]
        public AddressContract Address { get; set; }
        
        [Required]
        public CustomerInfoContract CustomerInfo { get; set; }
    }
}