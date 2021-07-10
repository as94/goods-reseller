using GoodsReseller.NotificationContext.Contracts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsReseller.NotificationContext
{
    public static class HandlersRegistry
    {
        public static void RegisterNotificationContextHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(HandlersRegistry).Assembly, typeof(OrderAcceptedNotificationRequest).Assembly);
        }
    }
}