using GoodsReseller.SupplyContext.Contracts.Models;
using GoodsReseller.SupplyContext.Domain.Supplies.Entities;

namespace GoodsReseller.SupplyContext.Handlers.Converters
{
    internal static class SupplyItemConverters
    {
        public static SupplyItemContract ToContract(this SupplyItem supplyItem)
        {
            return new SupplyItemContract
            {
                Id = supplyItem.Id,
                ProductId = supplyItem.ProductId,
                UnitPrice = supplyItem.UnitPrice.ToContract(),
                Quantity = supplyItem.Quantity.Value,
                DiscountPerUnit = supplyItem.DiscountPerUnit.Value
            };
        }
    }
}