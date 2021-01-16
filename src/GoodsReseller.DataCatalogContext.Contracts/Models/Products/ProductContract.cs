using System;

namespace GoodsReseller.DataCatalogContext.Contracts.Models.Products
{
    public class ProductContract
    {
        public Guid Id { get; set; }
        public int Version { get; set; }

        public string Label { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPerUnit { get; set; }

        public ProductContract[] Products { get; set; }
    }
}