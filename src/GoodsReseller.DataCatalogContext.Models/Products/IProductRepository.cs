using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoodsReseller.DataCatalogContext.Models.Products
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid productId, CancellationToken cancellationToken);
        Task SaveAsync(Product product, CancellationToken cancellationToken);
    }
}