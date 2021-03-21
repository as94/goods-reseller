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
            
            var ordersTotalCostQuery = GetOrdersTotalCostQuery(startDate, endDate);
            var revenue = await _dbContext.SingleAsync(
                ordersTotalCostQuery, 
                reader => reader[0] == DBNull.Value ? 0 : (decimal)reader[0],
                cancellationToken);

            var suppliesTotalCostQuery = GetSuppliesTotalCostQuery(startDate, endDate);
            var costs = await _dbContext.SingleAsync(
                suppliesTotalCostQuery, 
                reader => reader[0] == DBNull.Value ? 0 : (decimal)reader[0],
                cancellationToken);
            
            var ordersDeliveryCostQuery = GetOrdersDeliveryCostQuery(startDate, endDate);
            var deliveryCosts = await _dbContext.SingleAsync(
                ordersDeliveryCostQuery, 
                reader => reader[0] == DBNull.Value ? 0 : (decimal)reader[0],
                cancellationToken);

            costs += deliveryCosts;
            
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
            
            var ordersTotalCostQuery = GetOrdersTotalCostQuery(startDate, endDate);
            var revenue = await _dbContext.SingleAsync(
                ordersTotalCostQuery, 
                reader => reader[0] == DBNull.Value ? 0 : (decimal)reader[0],
                cancellationToken);
            
            var suppliesTotalCostQuery = GetSuppliesTotalCostQuery(startDate, endDate);
            var costs = await _dbContext.SingleAsync(
                suppliesTotalCostQuery, 
                reader => reader[0] == DBNull.Value ? 0 : (decimal)reader[0],
                cancellationToken);
            
            var ordersDeliveryCostQuery = GetOrdersDeliveryCostQuery(startDate, endDate);
            var deliveryCosts = await _dbContext.SingleAsync(
                ordersDeliveryCostQuery, 
                reader => reader[0] == DBNull.Value ? 0 : (decimal)reader[0],
                cancellationToken);

            costs += deliveryCosts;
            
            return new FinancialStatisticContract
            {
                Revenue = revenue,
                Costs = costs,
                GrossProfit = revenue - costs,
                NetProfit = revenue - costs
            };
        }

        private static string GetOrdersTotalCostQuery(string startDate, string endDate)
        {
            return $@"select SUM(""TotalCostValue"") from orders
                where ""IsRemoved"" = false
                and ""Status_Id"" = 6
                and ""CreationDateUtc"" between TO_TIMESTAMP('{startDate}', 'YYYY-MM-DD')
                    and TO_TIMESTAMP('{endDate}', 'YYYY-MM-DD')";
        }

        private static string GetSuppliesTotalCostQuery(string startDate, string endDate)
        {
            return $@"select SUM(""TotalCostValue"") from supplies
                where ""IsRemoved"" = false
                and ""CreationDateUtc"" between TO_TIMESTAMP('{startDate}', 'YYYY-MM-DD')
                    and TO_TIMESTAMP('{endDate}', 'YYYY-MM-DD')";
        }
        
        private static string GetOrdersDeliveryCostQuery(string startDate, string endDate)
        {
            return $@"select SUM(""DeliveryCostValue"") from orders
                where ""IsRemoved"" = false
                and ""Status_Id"" = 6
                and ""CreationDateUtc"" between TO_TIMESTAMP('{startDate}', 'YYYY-MM-DD')
                    and TO_TIMESTAMP('{endDate}', 'YYYY-MM-DD')";
        }
    }
}