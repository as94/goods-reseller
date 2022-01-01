using System;
using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.SupplyContext.Contracts.Models
{
    public class SupplyInfoContract
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public int Version { get; set; }
        
        [Required]
        public SupplierInfoContract SupplierInfo { get; set; }

        [Required]
        public SupplyItemContract[] SupplyItems { get; set; } = Array.Empty<SupplyItemContract>();
    }
}