using GoodsReseller.OrderContext.Handlers.Orders;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsReseller.OrderContext.Handlers
{
    public static class HandlersRegistry
    {
        public static void RegisterOrderContextHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(HandlersRegistry).Assembly, typeof(CreateOrderHandler).Assembly);
        }
    }
}