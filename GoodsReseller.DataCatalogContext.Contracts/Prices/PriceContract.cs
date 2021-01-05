using System;

namespace GoodsReseller.DataCatalogContext.Contracts.Prices
{
    public class PriceContract
    {
        public Guid PriceId { get; set; }
        public int Version { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int CurrencyId { get; set; }
    }
}