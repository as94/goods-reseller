using GoodsReseller.AuthContext.Domain.Users;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.Infrastructure.AuthContext;
using GoodsReseller.Infrastructure.Configurations;
using GoodsReseller.Infrastructure.DataCatalogContext;
using GoodsReseller.Infrastructure.OrderContext;
using GoodsReseller.OrderContext.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace GoodsReseller.Infrastructure
{
    public static class InfrastructureRegistry
    {
        public static void RegisterInfrastructure(this IServiceCollection serviceCollection,
            GoodsResellerDatabaseOptions goodsResellerDatabaseOptions,
            DatabaseOptions databaseOptions)
        {
            serviceCollection.AddDbContextPool<GoodsResellerDbContext>(options =>
                options.UseNpgsql(databaseOptions.ConnectionString));
            
            var mongoClient = new MongoClient(goodsResellerDatabaseOptions.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(goodsResellerDatabaseOptions.DatabaseName);

            serviceCollection.AddSingleton<IMongoClient>(mongoClient);
            serviceCollection.AddSingleton(mongoDatabase);
            
            // serviceCollection.AddSingleton<IUsersRepository, UsersRepositoryOld>();
            serviceCollection.AddScoped<IUsersRepository, UserRepository>();
            serviceCollection.AddSingleton<IProductRepository, ProductRepository>();
            serviceCollection.AddSingleton<IOrdersRepository, OrdersRepository>();
        }
    }
}