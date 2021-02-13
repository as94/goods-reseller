using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.GetByLabel;
using GoodsReseller.DataCatalogContext.Handlers.Converters;
using GoodsReseller.DataCatalogContext.Models.Products;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class GetProductByLabelHandler : IRequestHandler<GetProductByLabelRequest, GetProductByLabelResponse>
    {
        private readonly IProductsRepository _productsRepository;

        public GetProductByLabelHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        
        public async Task<GetProductByLabelResponse> Handle(GetProductByLabelRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Label))
            {
                return new GetProductByLabelResponse();
            }
            
            var product = await _productsRepository.GetAsync(request.Label, cancellationToken);
            
            if (product == null)
            {
                return new GetProductByLabelResponse();
            }
            
            return new GetProductByLabelResponse
            {
                Product = product.ToContract()
            };
        }
    }
}