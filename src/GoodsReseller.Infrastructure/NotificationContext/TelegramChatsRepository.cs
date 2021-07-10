using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.NotificationContext.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsReseller.Infrastructure.NotificationContext
{
    internal sealed class TelegramChatsRepository : ITelegramChatsRepository
    {
        private readonly GoodsResellerDbContext _dbContext;

        public TelegramChatsRepository(GoodsResellerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<TelegramChat>> GetAllAsync(CancellationToken cancellationToken)
        {
            return (await _dbContext.TelegramChats.ToListAsync(cancellationToken))
                .AsReadOnly();
        }

        public async Task CreateAsync(TelegramChat chat, CancellationToken cancellationToken)
        {
            if (chat == null)
            {
                throw new ArgumentNullException(nameof(chat));
            }

            await _dbContext.TelegramChats.AddAsync(chat, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}