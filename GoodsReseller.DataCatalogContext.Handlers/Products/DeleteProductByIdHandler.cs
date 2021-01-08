using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.Delete;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdRequest, Unit>
    {
        public Task<Unit> Handle(DeleteProductByIdRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}