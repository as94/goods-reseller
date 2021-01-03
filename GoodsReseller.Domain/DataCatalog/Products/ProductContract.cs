using System;

namespace GoodsReseller.Domain.DataCatalog.Products
{
    public class ProductContract
    {
        public Guid ProductId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}