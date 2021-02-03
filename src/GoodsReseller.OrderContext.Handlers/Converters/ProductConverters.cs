using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.Entities;

namespace GoodsReseller.OrderContext.Handlers.Converters
{
    internal static class ProductConverters
    {
        public static ProductContract ToContract(this Product product)
        {
            return new ProductContract
            {
                Id = product.Id,
                Version = product.Version,
                Name = product.Name
            };
        }
    }
}