using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GoodsReseller.Infrastructure.AuthContext.Models
{
    internal sealed class UserDocument
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
        
        [BsonElement("isRemoved")]
        public bool IsRemoved { get; set; }
        
        [BsonElement("email")]
        public string Email { get; set; }
        
        public BsonDocument Document { get; set; }
    }
}