using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoodsReseller.NotificationContext.Models
{
    public interface ITelegramChatsRepository
    {
        Task<IEnumerable<TelegramChat>> GetAllAsync(CancellationToken cancellationToken);
        Task CreateAsync(TelegramChat chat, CancellationToken cancellationToken);
    }
}