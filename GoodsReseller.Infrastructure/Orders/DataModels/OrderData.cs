using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GoodsReseller.Infrastructure.Orders.DataModels
{
    internal sealed class OrderData
    {
        [BsonId]
        [BsonElement("id")]
        public Guid Id { get; set; }
        
        [BsonElement("version")]
        public int Version { get; set; }
        
        public BsonDocument Document { get; set; }
    }
}