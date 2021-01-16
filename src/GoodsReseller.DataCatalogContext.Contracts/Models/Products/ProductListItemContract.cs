using System;

namespace GoodsReseller.DataCatalogContext.Contracts.Models.Products
{
    public class ProductListItemContract
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
    }
}