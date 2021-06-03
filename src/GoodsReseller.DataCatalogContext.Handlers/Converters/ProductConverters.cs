using GoodsReseller.DataCatalogContext.Contracts.Models.Products;
using GoodsReseller.DataCatalogContext.Models.Products;

namespace GoodsReseller.DataCatalogContext.Handlers.Converters
{
    internal static class ProductConverters
    {
        public static ProductContract ToContract(this Product product)
        {
            return new ProductContract
            {
                Id = product.Id,
                Version = product.Version,
                Date = product.CreationDate.Date,
                Label = product.Label,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice.Value,
                DiscountPerUnit = product.DiscountPerUnit.Value,
                AddedCost = product.AddedCost.Value,
                ProductIds = product.ProductIds
            };
        }

        public static ProductListItemContract ToListItemContract(this Product product, bool isSet)
        {
            return new ProductListItemContract
            {
                Id = product.Id,
                Version = product.Version,
                Date = product.CreationDate.Date,
                Label = product.Label,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice.Value,
                DiscountPerUnit = product.DiscountPerUnit.Value,
                AddedCost = product.AddedCost.Value,
                IsSet = isSet,
                ProductIds = product.ProductIds
            };
        }
    }
}