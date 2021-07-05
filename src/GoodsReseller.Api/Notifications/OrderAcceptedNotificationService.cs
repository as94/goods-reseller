using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Extensions.Polling;

namespace GoodsReseller.Api.Notifications
{
    public sealed class OrderAcceptedNotificationService : IDisposable
    {
        private readonly TelegramBotClient _bot;
        private readonly List<long> _chatIds;
        private readonly CancellationTokenSource _telegramReceiverCts;

        public OrderAcceptedNotificationService(IOptions<TelegramApiOptions> telegramApiOptions)
        {
            _bot = new TelegramBotClient(telegramApiOptions.Value.ApiKey);

            _telegramReceiverCts = new CancellationTokenSource();
            _bot.StartReceiving(
                new DefaultUpdateHandler(
                    (botClient, update, cancellationToken) =>
                    {
                        if (update.Message.Chat != null)
                        {
                            if (!_chatIds.Contains(update.Message.Chat.Id))
                            {
                                _chatIds.Add(update.Message.Chat.Id);
                            }
                        }

                        return Task.CompletedTask;
                    },
                    (botClient, exception, cancellationToken) => Task.CompletedTask),
                _telegramReceiverCts.Token
            );

            // TODO: save to database
            _chatIds = new List<long> { 167585499 };
        }

        public async Task SendNotificationAsync(
            OrderAcceptedTelegramNotification notification,
            CancellationToken cancellationToken)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            var message = $"Поступил заказ от клиента с номером {notification.ClientPhoneNumber}";
            if (!string.IsNullOrWhiteSpace(notification.ClientName))
            {
                message += $" ({notification.ClientName})";
            }

            foreach (var chatId in _chatIds)
            {
                await _bot.SendTextMessageAsync(
                    new ChatId(chatId),
                    message,
                    cancellationToken: cancellationToken);
            }
        }

        public void Dispose()
        {
            _telegramReceiverCts?.Cancel();
            _telegramReceiverCts?.Dispose();
        }
    }
}