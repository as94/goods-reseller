using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Configurations;
using GoodsReseller.NotificationContext.Contracts;
using GoodsReseller.NotificationContext.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace GoodsReseller.NotificationContext
{
    public sealed class OrderAcceptedNotificationHandler :
        IRequestHandler<SubscribeOnChatsRequest>,
        IRequestHandler<OrderAcceptedNotificationRequest>
    {
        private readonly ITelegramChatsRepository _telegramChatsRepository;
        private readonly bool _enabled;
        private readonly TelegramBotClient _bot;

        public OrderAcceptedNotificationHandler(
            IOptions<TelegramApiOptions> telegramApiOptions,
            ITelegramChatsRepository telegramChatsRepository)
        {
            _enabled = telegramApiOptions.Value.Enabled;
            if (!_enabled)
            {
                return;
            }

            _telegramChatsRepository = telegramChatsRepository;
            _bot = new TelegramBotClient(telegramApiOptions.Value.ApiKey);
        }

        public Task<Unit> Handle(SubscribeOnChatsRequest request, CancellationToken cancellationToken)
        {
            if (!_enabled)
            {
                return Task.FromResult(new Unit());
            }
            
            _bot.StartReceiving(
                new DefaultUpdateHandler(
                    async (botClient, update, ct) =>
                    {
                        if (update.Message.Chat != null)
                        {
                            var chatIds = await GetChatIdsAsync(ct);
                            if (!chatIds.Contains(update.Message.Chat.Id))
                            {
                                await _telegramChatsRepository.CreateAsync(new TelegramChat(
                                        update.Message.Chat.Id,
                                        update.Message.Chat.Username),
                                    ct);
                            }
                        }
                    },
                    (botClient, exception, ct) => Task.CompletedTask), cancellationToken: 
                cancellationToken);

            return Task.FromResult(new Unit());
        }

        public async Task<Unit> Handle(OrderAcceptedNotificationRequest request, CancellationToken cancellationToken)
        {
            if (!_enabled)
            {
                return Unit.Value;
            }
            
            var message = $"Поступил заказ от клиента с номером {request.ClientPhoneNumber}";
            if (!string.IsNullOrWhiteSpace(request.ClientName))
            {
                message += $" ({request.ClientName})";
            }

            var chatIds = await GetChatIdsAsync(cancellationToken);
            foreach (var chatId in chatIds)
            {
                await _bot.SendTextMessageAsync(
                    new ChatId(chatId),
                    message,
                    cancellationToken: cancellationToken);
            }

            return Unit.Value;
        }

        private async Task<IEnumerable<long>> GetChatIdsAsync(CancellationToken cancellationToken)
        {
            return (await _telegramChatsRepository.GetAllAsync(cancellationToken))
                .Select(x => x.ChatId);
        }
    }
}