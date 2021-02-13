using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoodsReseller.DataCatalogContext.Models.Products
{
    public interface IProductsRepository
    {
        Task<Product> GetAsync(Guid productId, CancellationToken cancellationToken);
        Task<Product> GetAsync(string label, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> BatchAsync(int offset, int count, CancellationToken cancellationToken);
        Task SaveAsync(Product product, CancellationToken cancellationToken);
    }
}