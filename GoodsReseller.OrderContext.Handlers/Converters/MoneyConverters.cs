using System;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;

namespace GoodsReseller.OrderContext.Handlers.Converters
{
    public static class MoneyConverters
    {
        public static MoneyContract ToContract(this Money money)
        {
            if (money == null)
            {
                throw new ArgumentNullException(nameof(money));
            }
            
            return new MoneyContract
            {
                Value = money.Value
            };
        }
    }
}