using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GoodsReseller.Infrastructure
{
    internal static class DbContextExtensions
    {
        public static async Task<T> SingleAsync<T>(this DbContext dbContext, string query, Func<DbDataReader, T> map, CancellationToken cancellationToken)
        {
            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                await dbContext.Database.OpenConnectionAsync(cancellationToken: cancellationToken);

                using (var result = await command.ExecuteReaderAsync(cancellationToken))
                {
                    await result.ReadAsync(cancellationToken);

                    return map(result);
                }
            }
        }
    }
}