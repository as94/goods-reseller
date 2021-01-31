using System;
using System.Collections.Generic;
using System.Linq;
using GoodsReseller.DataCatalogContext.Contracts.Models;
using GoodsReseller.DataCatalogContext.Contracts.Models.Products;
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
                Label = product.Label,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice.Value,
                DiscountPerUnit = product.DiscountPerUnit.Value,
                ProductIds = product.ProductIds
            };
        }

        public static ProductListItemContract ToListItemContract(this Product product, bool isSet)
        {
            return new ProductListItemContract
            {
                Id = product.Id,
                Version = product.Version,
                Label = product.Label,
                Name = product.Name,
                UnitPrice = product.UnitPrice.Value,
                DiscountPerUnit = product.DiscountPerUnit.Value,
                IsSet = isSet
            };
        }
    }
}