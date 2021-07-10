namespace GoodsReseller.NotificationContext.Models
{
    public sealed class TelegramChat
    {
        public TelegramChat(long chatId, string userName)
        {
            ChatId = chatId;
            UserName = userName;
        }

        public long ChatId { get; }
        public string UserName { get; }
    }
}