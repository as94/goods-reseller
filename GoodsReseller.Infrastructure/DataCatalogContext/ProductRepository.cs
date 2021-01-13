using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Models.Products;
using GoodsReseller.Infrastructure.Configurations;
using GoodsReseller.Infrastructure.DataCatalogContext.Models;
using Microsoft.Extensions.Options;
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
        }
        
        public async Task<Product> GetAsync(Guid productId, CancellationToken cancellationToken)
        {
            var existing = await GetExisting(productId, cancellationToken);

            if (existing == null)
            {
                return null;
            }

            var state = JsonConvert.DeserializeObject<ProductState>(
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
    }
}