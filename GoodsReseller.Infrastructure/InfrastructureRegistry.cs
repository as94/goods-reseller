using GoodsReseller.AuthContext.Domain.Users;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.Infrastructure.AuthContext;
using GoodsReseller.Infrastructure.DataCatalogContext;
using GoodsReseller.Infrastructure.OrderContext;
using GoodsReseller.OrderContext.Domain.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsReseller.Infrastructure
{
    public static class InfrastructureRegistry
    {
        public static void RegisterInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IUsersRepository, UsersRepository>();
            serviceCollection.AddSingleton<IProductRepository, ProductRepository>();
            serviceCollection.AddSingleton<IOrdersRepository, OrdersRepository>();
        }
    }
}