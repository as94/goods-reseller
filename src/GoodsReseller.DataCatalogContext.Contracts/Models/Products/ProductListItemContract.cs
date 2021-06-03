using System;

namespace GoodsReseller.DataCatalogContext.Contracts.Models.Products
{
    public class ProductListItemContract
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTime Date { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPerUnit { get; set; }
        public decimal AddedCost { get; set; }
        public bool IsSet { get; set; }
        public Guid[] ProductIds { get; set; }
    }
}