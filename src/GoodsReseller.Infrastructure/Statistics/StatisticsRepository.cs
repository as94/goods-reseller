using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Statistics;
using GoodsReseller.Statistics.Models;
using GoodsReseller.Statistics.Models.FinancialMetrics;

namespace GoodsReseller.Infrastructure.Statistics
{
    internal sealed class StatisticsRepository : IStatisticsRepository
    {
        private readonly GoodsResellerDbContext _dbContext;

        public StatisticsRepository(GoodsResellerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<FinancialStatistic> GetAsync(StatisticsQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            
            if (query.Month.HasValue)
            {
                return await GetAsync(query.Year, query.Month.Value, cancellationToken);
            }
            
            return await GetAsync(query.Year, cancellationToken);
        }

        private async Task<FinancialStatistic> GetAsync(int year, int month, CancellationToken cancellationToken)
        {
            var revenue = await _dbContext.SingleAsync(
                $@"select SUM(""TotalCostValue"") from orders
                where ""IsRemoved"" = false
                and ""Status_Id"" = 6
                and ""CreationDateUtc"" between TO_TIMESTAMP('{year}-{month}-01', 'YYYY-MM-DD')
                    and TO_TIMESTAMP('{year}-{month}-{DateTime.DaysInMonth(year, month)}', 'YYYY-MM-DD')", 
                reader => new Revenue { Value = reader[0] == DBNull.Value ? 0 : (decimal)reader[0] },
                cancellationToken);
            
            var costs = await _dbContext.SingleAsync(
                $@"select SUM(""TotalCostValue"") from supplies
                where ""IsRemoved"" = false
                and ""CreationDateUtc"" between TO_TIMESTAMP('{year}-{month}-01', 'YYYY-MM-DD')
                    and TO_TIMESTAMP('{year}-{month}-{DateTime.DaysInMonth(year, month)}', 'YYYY-MM-DD')", 
                reader => new Costs { Value = reader[0] == DBNull.Value ? 0 : (decimal)reader[0] },
                cancellationToken);
            
            return new FinancialStatistic
            {
                Revenue = revenue,
                Costs = costs,
                GrossProfit = new GrossProfit { Value = revenue.Value - costs.Value },
                NetProfit = new NetProfit { Value = revenue.Value - costs.Value },
            };
        }

        private async Task<FinancialStatistic> GetAsync(int year, CancellationToken cancellationToken)
        {
            var revenue = await _dbContext.SingleAsync(
                $@"select SUM(""TotalCostValue"") from orders
                where ""IsRemoved"" = false
                and ""Status_Id"" = 6
                and ""CreationDateUtc"" between TO_TIMESTAMP('{year}-01-01', 'YYYY-MM-DD')
                    and TO_TIMESTAMP('{year + 1}-01-01', 'YYYY-MM-DD')", 
                reader => new Revenue { Value = reader[0] == DBNull.Value ? 0 : (decimal)reader[0] },
                cancellationToken);
            
            var costs = await _dbContext.SingleAsync(
                $@"select SUM(""TotalCostValue"") from supplies
                where ""IsRemoved"" = false
                and ""CreationDateUtc"" between TO_TIMESTAMP('{year}-01-01', 'YYYY-MM-DD')
                    and TO_TIMESTAMP('{year + 1}-01-01', 'YYYY-MM-DD')", 
                reader => new Costs { Value = reader[0] == DBNull.Value ? 0 : (decimal)reader[0] },
                cancellationToken);
            
            return new FinancialStatistic
            {
                Revenue = revenue,
                Costs = costs,
                GrossProfit = new GrossProfit { Value = revenue.Value - costs.Value },
                NetProfit = new NetProfit { Value = revenue.Value - costs.Value },
            };
        }
    }
}