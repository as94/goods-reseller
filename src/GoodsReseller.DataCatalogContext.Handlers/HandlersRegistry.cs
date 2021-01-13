using GoodsReseller.DataCatalogContext.Handlers.Products;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsReseller.DataCatalogContext.Handlers
{
    public static class HandlersRegistry
    {
        public static void RegisterDataCatalogContextHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(HandlersRegistry).Assembly, typeof(CreateProductHandler).Assembly);
        }
    }
}