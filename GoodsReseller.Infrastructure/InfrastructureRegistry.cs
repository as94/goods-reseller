using GoodsReseller.Infrastructure.Orders;
using GoodsReseller.OrderContext.Domain.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsReseller.Infrastructure
{
    public static class InfrastructureRegistry
    {
        public static void RegisterInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IOrdersRepository, OrdersRepository>();
        }
    }
}