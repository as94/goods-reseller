using GoodsReseller.Statistics.Models.FinancialMetrics;

namespace GoodsReseller.Statistics.Models
{
    public class FinancialStatistic
    {
        public Revenue Revenue { get; set; }
        public Costs Costs { get; set; }
        public GrossProfit GrossProfit { get; set; }
        public NetProfit NetProfit { get; set; }
    }
}