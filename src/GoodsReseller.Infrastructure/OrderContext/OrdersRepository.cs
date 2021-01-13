using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.Infrastructure.Configurations;
using GoodsReseller.Infrastructure.OrderContext.Models;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace GoodsReseller.Infrastructure.OrderContext
{
    internal sealed class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoCollection<OrderDocument> _orders;
        
        public OrdersRepository(IMongoDatabase mongoDatabase)
        {
            _orders = mongoDatabase.GetCollection<OrderDocument>("orders");
        }
        
        public async Task<Order> GetAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var existing = await GetExisting(orderId, cancellationToken);

            if (existing == null)
            {
                return null;
            }

            var state = JsonConvert.DeserializeObject<OrderState>(
                existing.Document.ToJson(new JsonWriterSettings { OutputMode = JsonOutputMode.Shell }),
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                });

            return state?.ToDomain();
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
            
            var document = new OrderDocument
            {
                Id = order.Id,
                Version = order.Version,
                CreationDateUtc = order.CreationDate.DateUtc,
                LastUpdateDateUtc = order.LastUpdateDate?.DateUtc,
                Document = BsonDocument.Parse(json)
            };

           var existing = await GetExisting(order.Id, cancellationToken);

           if (existing == null)
           {
               await _orders.InsertOneAsync(document, new InsertOneOptions(), cancellationToken);
           }
           else if (existing.Version < document.Version)
           {
               await _orders.ReplaceOneAsync(x => x.Id == order.Id, document, new ReplaceOptions(), cancellationToken);
           }
        }

        private async Task<OrderDocument> GetExisting(Guid id, CancellationToken cancellationToken)
        {
            return await (await _orders.FindAsync(
                x => x.Id == id,
                new FindOptions<OrderDocument>(),
                cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        }
    }
}