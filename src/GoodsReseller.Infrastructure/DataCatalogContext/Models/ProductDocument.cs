using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GoodsReseller.Infrastructure.DataCatalogContext.Models
{
    internal sealed class ProductDocument
    {
        [BsonId]
        [BsonElement("id")]
        public Guid Id { get; set; }
        
        [BsonElement("version")]
        public int Version { get; set; }

        [BsonElement("label")]
        public string Label { get; set; }

        [BsonElement("creationDateUtc")]
        public DateTime CreationDateUtc { get; set; }

        [BsonElement("lastUpdateDateUtc")]
        public DateTime? LastUpdateDateUtc { get; set; }
        
        [BsonElement("isRemoved")]
        public bool IsRemoved { get; set; }
        
        public BsonDocument Document { get; set; }
    }
}