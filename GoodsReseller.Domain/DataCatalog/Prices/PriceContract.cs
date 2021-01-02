using System;

namespace GoodsReseller.Domain.DataCatalog.Prices
{
    public class PriceContract
    {
        public Guid PriceId { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int CurrencyId { get; set; }
    }
}