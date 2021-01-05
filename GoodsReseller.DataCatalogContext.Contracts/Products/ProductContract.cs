using System;

namespace GoodsReseller.DataCatalogContext.Contracts.Products
{
    public class ProductContract
    {
        public Guid ProductId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}