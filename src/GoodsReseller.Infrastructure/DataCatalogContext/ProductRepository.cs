using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.Infrastructure.DataCatalogContext.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace GoodsReseller.Infrastructure.DataCatalogContext
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<ProductDocument> _products;

        public ProductRepository(IMongoDatabase mongoDatabase)
        {
            _products = mongoDatabase.GetCollection<ProductDocument>("products");
            
            var indexKeysDefinition = Builders<ProductDocument>.IndexKeys.Ascending(x => x.Label);
            var indexOptions = new CreateIndexOptions
            {
                Unique = true,
                Sparse = true
            };
            
            _products.Indexes.CreateOne(new CreateIndexModel<ProductDocument>(indexKeysDefinition, indexOptions));
        }
        
        public async Task<Product> GetAsync(Guid productId, CancellationToken cancellationToken)
        {
            var existing = await GetExisting(productId, cancellationToken);

            if (existing == null)
            {
                return null;
            }

            var state = GetState(existing);

            return state?.ToDomain();
        }

        public async Task<Product> GetAsync(string label, CancellationToken cancellationToken)
        {
            if (label == null)
            {
                throw new ArgumentNullException(nameof(label));
            }
            
            var existing = await GetExisting(label, cancellationToken);
            
            if (existing == null)
            {
                return null;
            }

            var state = GetState(existing);

            return state?.ToDomain();
        }

        public async Task<IEnumerable<Product>> BatchAsync(int offset, int count, CancellationToken cancellationToken)
        {
            if (offset < 0)
            {
                throw new ArgumentException(nameof(offset));
            }
            
            if (count < 0)
            {
                throw new ArgumentException(nameof(count));
            }

            if (count > 100)
            {
                throw new ArgumentException($"{nameof(count)} more than 100");
            }
            
            var cursor = await _products.FindAsync(
                new FilterDefinitionBuilder<ProductDocument>().Empty,
                new FindOptions<ProductDocument>
                {
                    Skip = offset,
                    Limit = count,
                    MaxTime = TimeSpan.FromSeconds(5),
                    MaxAwaitTime = TimeSpan.FromSeconds(5),
                },
                cancellationToken);
            
            return (await cursor.ToListAsync(cancellationToken))
                .Select(x => GetState(x).ToDomain())
                .ToList()
                .AsReadOnly();
        }

        public async Task<IEnumerable<Product>> GetListByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken)
        {
            if (productIds == null)
            {
                throw new ArgumentNullException(nameof(productIds));
            }

            var ids = productIds.ToArray();

            if (ids.Length == 0)
            {
                return Array.Empty<Product>();
            }

            if (ids.Length == 1)
            {
                return new [] { await GetAsync(ids[0], cancellationToken) };
            }
            
            var cursor = await _products.FindAsync(
                new FilterDefinitionBuilder<ProductDocument>().In(x=> x.Id , ids),
                new FindOptions<ProductDocument>
                {
                    MaxTime = TimeSpan.FromSeconds(5),
                    MaxAwaitTime = TimeSpan.FromSeconds(5),
                },
                cancellationToken);
            
            return (await cursor.ToListAsync(cancellationToken))
                .Select(x => GetState(x).ToDomain())
                .ToList()
                .AsReadOnly();
        }

        public async Task SaveAsync(Product product, CancellationToken cancellationToken)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            
            var json = JsonConvert.SerializeObject(
                product,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                });
            
            var document = new ProductDocument
            {
                Id = product.Id,
                Version = product.Version,
                Label = product.Label,
                CreationDateUtc = product.CreationDate.DateUtc,
                LastUpdateDateUtc = product.LastUpdateDate?.DateUtc,
                IsRemoved = product.IsRemoved,
                Document = BsonDocument.Parse(json)
            };

            var existing = await GetExisting(product.Id, cancellationToken);

            if (existing == null)
            {
                await _products.InsertOneAsync(document, new InsertOneOptions(), cancellationToken);
            }
            else if (existing.Version < document.Version)
            {
                await _products.ReplaceOneAsync(x => x.Id == product.Id, document, new ReplaceOptions(), cancellationToken);
            }
        }

        private async Task<ProductDocument> GetExisting(Guid id, CancellationToken cancellationToken)
        {
            return await (await _products.FindAsync(
                x => x.Id == id && !x.IsRemoved,
                new FindOptions<ProductDocument>(),
                cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        }
        
        private async Task<ProductDocument> GetExisting(string label, CancellationToken cancellationToken)
        {
            return await (await _products.FindAsync(
                x => x.Label == label && !x.IsRemoved,
                new FindOptions<ProductDocument>(),
                cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        }

        private static ProductState GetState(ProductDocument existing)
        {
            return JsonConvert.DeserializeObject<ProductState>(
                existing.Document.ToJson(new JsonWriterSettings {OutputMode = JsonOutputMode.Shell}),
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                });
        }
    }
}