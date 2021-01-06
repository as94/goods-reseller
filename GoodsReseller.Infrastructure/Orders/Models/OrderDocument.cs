using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GoodsReseller.Infrastructure.Orders.Models
{
    internal sealed class OrderDocument
    {
        [BsonId]
        [BsonElement("id")]
        public Guid Id { get; set; }
        
        [BsonElement("version")]
        public int Version { get; set; }
        
        public BsonDocument Document { get; set; }
    }
}