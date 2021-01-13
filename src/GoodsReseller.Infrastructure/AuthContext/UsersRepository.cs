using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.AuthContext.Domain.Users;
using GoodsReseller.AuthContext.Domain.Users.Entities;
using GoodsReseller.Infrastructure.AuthContext.Models;
using GoodsReseller.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace GoodsReseller.Infrastructure.AuthContext
{
    internal sealed class UsersRepository : IUsersRepository
    {
        private readonly IMongoCollection<UserDocument> _users;
        
        public UsersRepository(IMongoDatabase mongoDatabase)
        {
            _users = mongoDatabase.GetCollection<UserDocument>("users");
            
            var indexKeysDefinition = Builders<UserDocument>.IndexKeys.Ascending(x => x.Email);
            var indexOptions = new CreateIndexOptions
            {
                Unique = true,
                Sparse = true
            };
            
            _users.Indexes.CreateOne(new CreateIndexModel<UserDocument>(indexKeysDefinition, indexOptions));
        }
        
        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            
            var existing = await GetExisting(email, cancellationToken);

            if (existing == null)
            {
                return null;
            }

            var state = JsonConvert.DeserializeObject<UserState>(
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

        public async Task SaveUserAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            var json = JsonConvert.SerializeObject(
                user,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                });

            var document = new UserDocument
            {
                Id = user.Id,
                Version = user.Version,
                CreationDateUtc = user.CreationDate.DateUtc,
                LastUpdateDateUtc = user.LastUpdateDate?.DateUtc,
                IsRemoved = user.IsRemoved,
                Email = user.Email,
                Document = BsonDocument.Parse(json)
            };

            var existing = await GetExisting(user.Email, cancellationToken);

            if (existing == null)
            {
                await _users.InsertOneAsync(document, new InsertOneOptions(), cancellationToken);
            }
            else if (existing.Version < document.Version)
            {
                await _users.ReplaceOneAsync(x => x.Email == user.Email, document, new ReplaceOptions(), cancellationToken);
            }
        }
        
        private async Task<UserDocument> GetExisting(string email, CancellationToken cancellationToken)
        {
            return await (await _users.FindAsync(
                x => x.Email == email && !x.IsRemoved,
                new FindOptions<UserDocument>(),
                cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        }
    }
}