using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Statistics.Models;

namespace GoodsReseller.Statistics
{
    public interface IStatisticsRepository
    {
        Task<FinancialStatisticContract> GetAsync(StatisticsQueryContract query, CancellationToken cancellationToken);
    }
}