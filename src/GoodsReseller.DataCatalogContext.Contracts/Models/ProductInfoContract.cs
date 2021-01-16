using System;

namespace GoodsReseller.DataCatalogContext.Contracts.Models
{
    public class ProductInfoContract
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPerUnit { get; set; }
        public Guid[] ProductIds { get; set; }
    }
}