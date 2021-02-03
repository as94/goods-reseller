using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.GetById;
using GoodsReseller.DataCatalogContext.Handlers.Converters;
using GoodsReseller.DataCatalogContext.Models.Products;
using MediatR;

namespace GoodsReseller.DataCatalogContext.Handlers.Products
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(request.ProductId, cancellationToken);
            
            if (product == null)
            {
                return new GetProductByIdResponse();
            }

            return new GetProductByIdResponse
            {
                Product = product.ToContract()
            };
        }
    }
}