using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.Update;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, Unit>
    {
        public Task<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}