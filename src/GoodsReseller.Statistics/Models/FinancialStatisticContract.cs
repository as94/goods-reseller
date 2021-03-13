namespace GoodsReseller.Statistics.Models
{
    public class FinancialStatisticContract
    {
        public decimal Revenue { get; set; }
        public decimal Costs { get; set; }
        public decimal GrossProfit { get; set; }
        public decimal NetProfit { get; set; }
    }
}