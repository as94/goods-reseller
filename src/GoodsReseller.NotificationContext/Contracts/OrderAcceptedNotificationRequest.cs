using MediatR;

namespace GoodsReseller.NotificationContext.Contracts
{
    public class OrderAcceptedNotificationRequest : IRequest
    {
        public string ClientPhoneNumber { get; set; }
        public string ClientName { get; set; }
    }
}