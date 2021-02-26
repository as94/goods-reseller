using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.SupplyContext.Contracts.Models
{
    public class SupplierInfoContract
    {
        [Required]
        public string Name { get; set; }
    }
}