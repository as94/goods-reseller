using MediatR;

namespace GoodsReseller.DataCatalogContext.Contracts.Products.Create
{
    public class CreateProductRequest : IRequest<CreateProductResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPerUnit { get; set; }
    }
}