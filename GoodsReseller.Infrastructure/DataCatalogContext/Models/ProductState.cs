using System;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.Infrastructure.DataCatalogContext.Models
{
    internal sealed class ProductState
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public Money UnitPrice { get; set; }
        public Factor DiscountPerUnit { get; set; }
        
        public DateValueObject CreationDate { get; set; }
        public DateValueObject? LastUpdateDate { get; set; }
        public bool IsRemoved { get; set; }

        public Product ToDomain()
        {
            return Product.Restore(
                Id,
                Version,
                Name,
                Description,
                UnitPrice,
                DiscountPerUnit,
                CreationDate,
                LastUpdateDate,
                IsRemoved);
        }
    }
}