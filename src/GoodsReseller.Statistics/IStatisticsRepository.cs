using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Statistics.Models;

namespace GoodsReseller.Statistics
{
    public interface IStatisticsRepository
    {
        Task<FinancialStatistic> GetAsync(StatisticsQuery query, CancellationToken cancellationToken);
    }
}