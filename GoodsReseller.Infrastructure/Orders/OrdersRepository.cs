using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Infrastructure.Configurations;
using GoodsReseller.Infrastructure.Orders.Models;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace GoodsReseller.Infrastructure.Orders
{
    internal sealed class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoCollection<OrderDocument> _orders;
        
        public OrdersRepository(IOptions<GoodsResellerDatabaseOptions> options)
        {
            var goodsResellerDatabaseOptions = options.Value;
            
            var client = new MongoClient(goodsResellerDatabaseOptions.ConnectionString);
            var database = client.GetDatabase(goodsResellerDatabaseOptions.DatabaseName);

            _orders = database.GetCollection<OrderDocument>("orders");
        }
        
        public async Task<Order> GetAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var existingOrder = await GetExistingOrder(orderId, cancellationToken);

            if (existingOrder == null)
            {
                return null;
            }

            var orderState = JsonConvert.DeserializeObject<OrderState>(
                existingOrder.Document.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Shell }),
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                });

            return orderState?.ToDomain();
        }

        public async Task SaveAsync(Order order, CancellationToken cancellationToken)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var json = JsonConvert.SerializeObject(
                order,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                });
            
            var orderDocument = new OrderDocument
            {
                Id = order.Id,
                Version = order.Version,
                Document = BsonDocument.Parse(json)
            };

           var existingOrder = await GetExistingOrder(order.Id, cancellationToken);

           if (existingOrder == null)
           {
               await _orders.InsertOneAsync(orderDocument, new InsertOneOptions(), cancellationToken);
           }
           else if (existingOrder.Version < orderDocument.Version)
           {
               await _orders.ReplaceOneAsync(x => x.Id == order.Id, orderDocument, new ReplaceOptions(), cancellationToken);
           }
        }

        private async Task<OrderDocument> GetExistingOrder(Guid orderId, CancellationToken cancellationToken)
        {
            return await (await _orders.FindAsync(
                x => x.Id == orderId,
                new FindOptions<OrderDocument>(),
                cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        }
    }
}