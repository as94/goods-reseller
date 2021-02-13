using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace GoodsReseller.Infrastructure.DataCatalogContext
{
    internal sealed class ProductRepository : IProductRepository
    {
        private readonly GoodsResellerDbContext _dbContext;

        public ProductRepository(GoodsResellerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Product> GetAsync(Guid productId, CancellationToken cancellationToken)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(
                x => x.Id == productId && !x.IsRemoved,
                cancellationToken);
        }

        public async Task<Product> GetAsync(string label, CancellationToken cancellationToken)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(
                x => x.Label == label && !x.IsRemoved,
                cancellationToken);
        }

        public async Task<IEnumerable<Product>> BatchAsync(int offset, int count, CancellationToken cancellationToken)
        {
            return (await _dbContext.Products
                    .Skip(offset)
                    .Take(count)
                    .ToListAsync(cancellationToken))
                .AsReadOnly();
        }

        public async Task SaveAsync(Product product, CancellationToken cancellationToken)
        {
            var existing = await _dbContext.Products.FirstOrDefaultAsync(
                x => x.Id == product.Id,
                cancellationToken);

            if (existing == null)
            {
                await _dbContext.Products.AddAsync(product, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}