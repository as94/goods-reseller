using System;
using GoodsReseller.DataCatalogContext.Contracts.Models;
using GoodsReseller.DataCatalogContext.Models.Products;

namespace GoodsReseller.DataCatalogContext.Handlers.Converters
{
    public static class ProductConverters
    {
        public static ProductContract ToContract(this Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            
            return new ProductContract
            {
                Id = product.Id,
                Version = product.Version,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice.Value,
                DiscountPerUnit = product.DiscountPerUnit.Value
            };
        }
    }
}