using GoodsReseller.SeedWork.ValueObjects;
using GoodsReseller.SupplyContext.Contracts.Models;

namespace GoodsReseller.SupplyContext.Handlers.Converters
{
    internal static class MoneyConverters
    {
        public static MoneyContract ToContract(this Money money)
        {
            return new MoneyContract
            {
                Value = money.Value
            };
        }
    }
}