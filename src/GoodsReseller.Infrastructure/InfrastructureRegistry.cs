using GoodsReseller.AuthContext.Domain.Users;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.Infrastructure.AuthContext;
using GoodsReseller.Infrastructure.Configurations;
using GoodsReseller.Infrastructure.DataCatalogContext;
using GoodsReseller.Infrastructure.OrderContext;
using GoodsReseller.OrderContext.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsReseller.Infrastructure
{
    public static class InfrastructureRegistry
    {
        public static void RegisterInfrastructure(this IServiceCollection serviceCollection,
            DatabaseOptions databaseOptions)
        {
            serviceCollection.AddDbContextPool<GoodsResellerDbContext>(options =>
                options.UseNpgsql(databaseOptions.ConnectionString));
            
            serviceCollection.AddScoped<IProductsRepository, ProductsRepository>();
            serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
            serviceCollection.AddScoped<IOrdersRepository, OrdersRepository>();
        }
    }
}