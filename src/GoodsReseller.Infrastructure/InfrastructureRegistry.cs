using GoodsReseller.AuthContext.Domain.Users;
using GoodsReseller.Configurations;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.Infrastructure.AuthContext;
using GoodsReseller.Infrastructure.DataCatalogContext;
using GoodsReseller.Infrastructure.NotificationContext;
using GoodsReseller.Infrastructure.OrderContext;
using GoodsReseller.Infrastructure.Statistics;
using GoodsReseller.Infrastructure.SupplyContext;
using GoodsReseller.NotificationContext.Models;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.Statistics;
using GoodsReseller.SupplyContext.Domain.Supplies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;

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
            serviceCollection.AddScoped<ISuppliesRepository, SuppliesRepository>();
            serviceCollection.AddScoped<IStatisticsRepository, StatisticsRepository>();
            serviceCollection.AddScoped<ITelegramChatsRepository, TelegramChatsRepository>();

            serviceCollection.AddDataProtection()
                .PersistKeysToDbContext<GoodsResellerDbContext>();
        }
    }
}