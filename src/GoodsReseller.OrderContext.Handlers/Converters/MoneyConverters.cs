using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.OrderContext.Handlers.Converters
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

        public static Money ToDomain(this MoneyContract money)
        {
            return new Money(money.Value);
        }
    }
}