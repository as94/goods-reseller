using GoodsReseller.SupplyContext.Handlers.Supplies;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsReseller.SupplyContext.Handlers
{
    public static class HandlersRegistry
    {
        public static void RegisterSupplyContextHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(HandlersRegistry).Assembly, typeof(CreateSupplyHandler).Assembly);
        }
    }
}