using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Statistics;
using GoodsReseller.Statistics.Models;

namespace GoodsReseller.Infrastructure.Statistics
{
    internal sealed class StatisticsRepository : IStatisticsRepository
    {
        private readonly GoodsResellerDbContext _dbContext;

        public StatisticsRepository(GoodsResellerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<FinancialStatisticContract> GetAsync(StatisticsQueryContract query, CancellationToken cancellationToken)
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

        private async Task<FinancialStatisticContract> GetAsync(int year, int month, CancellationToken cancellationToken)
        {
            var startDate = $"{year}-{month}-01";
            var endDate = $"{year}-{month}-{DateTime.DaysInMonth(year, month)}";
            
            var ordersStatisticQuery = GetOrdersStatisticQuery(startDate, endDate);
            var revenue = await _dbContext.SingleAsync(
                ordersStatisticQuery, 
                reader => reader[0] == DBNull.Value ? 0 : (decimal)reader[0],
                cancellationToken);

            var suppliesStatisticQuery = GetSuppliesStatisticQuery(startDate, endDate);
            var costs = await _dbContext.SingleAsync(
                suppliesStatisticQuery, 
                reader => reader[0] == DBNull.Value ? 0 : (decimal)reader[0],
                cancellationToken);
            
            return new FinancialStatisticContract
            {
                Revenue = revenue,
                Costs = costs,
                GrossProfit = revenue - costs,
                NetProfit = revenue - costs
            };
        }

        private async Task<FinancialStatisticContract> GetAsync(int year, CancellationToken cancellationToken)
        {
            var startDate = $"{year}-01-01";
            var endDate = $"{year + 1}-01-01";
            
            var ordersStatisticQuery = GetOrdersStatisticQuery(startDate, endDate);
            var revenue = await _dbContext.SingleAsync(
                ordersStatisticQuery, 
                reader => reader[0] == DBNull.Value ? 0 : (decimal)reader[0],
                cancellationToken);
            
            var suppliesStatisticQuery = GetSuppliesStatisticQuery(startDate, endDate);
            var costs = await _dbContext.SingleAsync(
                suppliesStatisticQuery, 
                reader => reader[0] == DBNull.Value ? 0 : (decimal)reader[0],
                cancellationToken);
            
            return new FinancialStatisticContract
            {
                Revenue = revenue,
                Costs = costs,
                GrossProfit = revenue - costs,
                NetProfit = revenue - costs
            };
        }

        private static string GetOrdersStatisticQuery(string startDate, string endDate)
        {
            return $@"select SUM(""TotalCostValue"") from orders
                where ""IsRemoved"" = false
                and ""Status_Id"" = 6
                and ""CreationDateUtc"" between TO_TIMESTAMP('{startDate}', 'YYYY-MM-DD')
                    and TO_TIMESTAMP('{endDate}', 'YYYY-MM-DD')";
        }

        private static string GetSuppliesStatisticQuery(string startDate, string endDate)
        {
            return $@"select SUM(""TotalCostValue"") from supplies
                where ""IsRemoved"" = false
                and ""CreationDateUtc"" between TO_TIMESTAMP('{startDate}', 'YYYY-MM-DD')
                    and TO_TIMESTAMP('{endDate}', 'YYYY-MM-DD')";
        }
    }
}