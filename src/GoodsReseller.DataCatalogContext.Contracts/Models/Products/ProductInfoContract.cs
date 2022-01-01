using System;
using System.ComponentModel.DataAnnotations;

namespace GoodsReseller.DataCatalogContext.Contracts.Models.Products
{
    public class ProductInfoContract
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public int Version { get; set; }
        
        [Required]
        public string Label { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPerUnit { get; set; }
        public decimal AddedCost { get; set; }
        public Guid[] ProductIds { get; set; }
    }
}