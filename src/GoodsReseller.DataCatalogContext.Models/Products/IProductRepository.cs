using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoodsReseller.DataCatalogContext.Models.Products
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid productId, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetListByIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken);
        Task SaveAsync(Product product, CancellationToken cancellationToken);
    }
}