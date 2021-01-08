using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GoodsReseller.Infrastructure.OrderContext.Models
{
    internal sealed class OrderDocument
    {
        [BsonId]
        [BsonElement("id")]
        public Guid Id { get; set; }
        
        [BsonElement("version")]
        public int Version { get; set; }

        [BsonElement("creationDateUtc")]
        public DateTime CreationDateUtc { get; set; }

        [BsonElement("lastUpdateDateUtc")]
        public DateTime? LastUpdateDateUtc { get; set; }
        
        public BsonDocument Document { get; set; }
    }
}