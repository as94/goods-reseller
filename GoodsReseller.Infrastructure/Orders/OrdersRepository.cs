using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Infrastructure.Configurations;
using GoodsReseller.Infrastructure.Orders.DataModels;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GoodsReseller.Infrastructure.Orders
{
    // TODO: check
    internal sealed class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoCollection<OrderData> _orders;
        
        public OrdersRepository(IOptions<GoodsResellerDatabaseOptions> options)
        {
            var goodsResellerDatabaseOptions = options.Value;
            
            var client = new MongoClient(goodsResellerDatabaseOptions.ConnectionString);
            var database = client.GetDatabase(goodsResellerDatabaseOptions.DatabaseName);

            _orders = database.GetCollection<OrderData>("orders");
        }
        
        public async Task<Order> GetAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var existingOrder = await GetExistingOrder(orderId, cancellationToken);

            if (existingOrder == null)
            {
                return null;
            }

            var order = JsonSerializer.Deserialize<Order>(existingOrder.Document.AsString, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return order;
        }

        public async Task SaveAsync(Order order, CancellationToken cancellationToken)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var json = JsonSerializer.Serialize(order, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            
            var orderData = new OrderData
            {
                Id = order.Id,
                Version = order.Version,
                Document = BsonDocument.Parse(json)
            };

           var existingOrder = await GetExistingOrder(order.Id, cancellationToken);

           if (existingOrder == null)
           {
               await _orders.InsertOneAsync(orderData, new InsertOneOptions(), cancellationToken);
           }
           else if (existingOrder.Version < orderData.Version)
           {
               await _orders.ReplaceOneAsync(x => x.Id == order.Id, orderData, new ReplaceOptions(), cancellationToken: cancellationToken);
           }
        }

        private async Task<OrderData> GetExistingOrder(Guid orderId, CancellationToken cancellationToken)
        {
            return await (await _orders.FindAsync(
                x => x.Id == orderId,
                new FindOptions<OrderData>(),
                cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        }
    }
}