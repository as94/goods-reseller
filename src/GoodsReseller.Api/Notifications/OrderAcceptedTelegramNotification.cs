namespace GoodsReseller.Api.Notifications
{
    public class OrderAcceptedTelegramNotification
    {
        public OrderAcceptedTelegramNotification(string clientPhoneNumber, string clientName)
        {
            ClientPhoneNumber = clientPhoneNumber;
            ClientName = clientName;
        }

        public string ClientPhoneNumber { get; }
        public string ClientName { get; }
    }
}