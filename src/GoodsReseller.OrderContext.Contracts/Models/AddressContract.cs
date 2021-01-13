using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class AddressContract
    {
        [Required]
        public string City { get; set; }
        
        [Required]
        public string Street { get; set; }
        
        [Required]
        public string ZipCode { get; set; }
        
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string EntranceNumber { get; set; }
        public string Floor { get; set; }
        public string Intercom { get; set; }
    }
}