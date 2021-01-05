using GoodsReseller.Orders.Domain.SeedWork;

namespace GoodsReseller.OrderContext.Domain.Orders.ValueObjects
{
    public sealed class Currency : Enumeration
    {
        public Currency(int id, string name) : base(id, name)
        {
        }
        
        public static readonly Currency RUB = new Currency(1, "RUB");
    }
}