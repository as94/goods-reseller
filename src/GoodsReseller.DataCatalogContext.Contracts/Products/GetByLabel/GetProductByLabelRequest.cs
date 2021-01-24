using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.GetByLabel
{
    public class GetProductByLabelRequest : IRequest<GetProductByLabelResponse>
    {
        public string Label { get; set; }
    }
}