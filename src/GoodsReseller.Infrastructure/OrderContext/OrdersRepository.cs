using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Infrastructure.Exceptions;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace GoodsReseller.Infrastructure.OrderContext
{
    internal sealed class OrdersRepository : IOrdersRepository
    {
        private readonly GoodsResellerDbContext _dbContext;

        public OrdersRepository(GoodsResellerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> GetAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == orderId && !x.IsRemoved,
                cancellationToken);
        }

        public async Task<IEnumerable<Order>> BatchAsync(int offset, int count, CancellationToken cancellationToken)
        {
            return (await _dbContext.Orders
                    .Include(x => x.OrderItems)
                    .Where(x => !x.IsRemoved)
                    .OrderBy(x => x.LastUpdateDate != null ? x.LastUpdateDate.DateUtc : x.CreationDate.DateUtc)
                    .Skip(offset)
                    .Take(count)
                    .ToListAsync(cancellationToken))
                .AsReadOnly();
        }

        public async Task SaveAsync(Order order, CancellationToken cancellationToken)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            
            var existingVersion = await GetVersionAsync(order.Id, cancellationToken);
            if (existingVersion == null)
            {
                await _dbContext.Orders.AddAsync(order, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else if (existingVersion <= order.Version)
            {
                await using var transaction =
                    await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead, cancellationToken);
                try
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        await _dbContext.Database.ExecuteSqlRawAsync(
                            @"
                                update order_items set
                                ""ProductId"" = @productId,
                                ""UnitPriceValue"" = @unitPriceValue,
                                ""QuantityValue"" = @quantityValue,
                                ""DiscountPerUnitValue"" = @discountPerUnitValue,
                                ""CreationDate"" = @creationDate,
                                ""CreationDateUtc"" = @creationDateUtc,
                                ""IsRemoved"" = @isRemoved,
                                ""LastUpdateDate"" = @lastUpdateDate,
                                ""LastUpdateDateUtc"" = @lastUpdateDateUtc
                                where ""Id"" = @id;
                            ",
                            new []
                            {
                                new NpgsqlParameter("@id", orderItem.Id),
                                new NpgsqlParameter("@productId", orderItem.ProductId),
                                new NpgsqlParameter("@unitPriceValue", orderItem.UnitPrice.Value),
                                new NpgsqlParameter("@quantityValue", orderItem.Quantity.Value),
                                new NpgsqlParameter("@discountPerUnitValue", orderItem.DiscountPerUnit.Value),
                                new NpgsqlParameter("@creationDate", orderItem.CreationDate.Date),
                                new NpgsqlParameter("@creationDateUtc", orderItem.CreationDate.DateUtc),
                                new NpgsqlParameter("@isRemoved", orderItem.IsRemoved),
                                new NpgsqlParameter("@lastUpdateDate",
                                    orderItem.LastUpdateDate != null
                                        ? (object)orderItem.LastUpdateDate?.Date
                                        : DBNull.Value),
                                new NpgsqlParameter("@lastUpdateDateUtc",
                                    orderItem.LastUpdateDate != null
                                        ? (object)orderItem.LastUpdateDate?.DateUtc
                                        : DBNull.Value),
                            },
                            cancellationToken);
                    }
                    
                    var rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync(
                        @"
                                update orders set
                                ""Version"" = @version,
                                ""Address"" = CAST(@address AS json),
                                ""CustomerInfo"" = CAST(@customerInfo AS json),
                                ""TotalCostValue"" = @totalCostValue,
                                ""Status_Id"" = @statusId,
                                ""Status_Name"" = @statusName,
                                ""DeliveryCostValue"" = @deliveryCostValue,
                                ""AddedCostValue"" = @addedCostValue,
                                ""CreationDate"" = @creationDate,
                                ""CreationDateUtc"" = @creationDateUtc,
                                ""IsRemoved"" = @isRemoved,
                                ""LastUpdateDate"" = @lastUpdateDate,
                                ""LastUpdateDateUtc"" = @lastUpdateDateUtc
                                where ""Id"" = @id and ""Version"" <= @version;
                            ",
                        new []
                        {
                            new NpgsqlParameter("@id", order.Id),
                            new NpgsqlParameter("@version", order.Version),
                            new NpgsqlParameter("@address", JsonSerializer.Serialize(order.Address)),
                            new NpgsqlParameter("@customerInfo", JsonSerializer.Serialize(order.CustomerInfo)),
                            new NpgsqlParameter("@totalCostValue", order.TotalCost.Value),
                            new NpgsqlParameter("@statusId", order.Status.Id),
                            new NpgsqlParameter("@statusName", order.Status.Name),
                            new NpgsqlParameter("@deliveryCostValue", order.DeliveryCost.Value),
                            new NpgsqlParameter("@addedCostValue", order.AddedCost.Value),
                            new NpgsqlParameter("@creationDate", order.CreationDate.Date),
                            new NpgsqlParameter("@creationDateUtc", order.CreationDate.DateUtc),
                            new NpgsqlParameter("@isRemoved", order.IsRemoved),
                            new NpgsqlParameter("@lastUpdateDate",
                                order.LastUpdateDate != null
                                    ? (object)order.LastUpdateDate?.Date
                                    : DBNull.Value),
                            new NpgsqlParameter("@lastUpdateDateUtc",
                                order.LastUpdateDate != null
                                    ? (object)order.LastUpdateDate?.DateUtc
                                    : DBNull.Value),
                        },
                        cancellationToken);

                    if (rowsAffected == 0)
                    {
                        throw new ConcurrencyException();
                    }

                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }
        
        private async Task<int?> GetVersionAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var orderVersion = await _dbContext.OrderVersions.FromSqlRaw(
                $@"select ""Version"" from orders where ""Id"" = @orderId",
                new NpgsqlParameter("@orderId", orderId))
                .FirstOrDefaultAsync(cancellationToken);

            return orderVersion?.Version;
        }
    }
}