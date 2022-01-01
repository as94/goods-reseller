using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SeedWork.ValueObjects;
using GoodsReseller.SupplyContext.Contracts.Supplies.Create;
using GoodsReseller.SupplyContext.Domain.Supplies;
using GoodsReseller.SupplyContext.Domain.Supplies.Entities;
using GoodsReseller.SupplyContext.Domain.Supplies.ValueObjects;
using MediatR;

namespace GoodsReseller.SupplyContext.Handlers.Supplies
{
    public class CreateSupplyHandler : IRequestHandler<CreateSupplyRequest>
    {
        private readonly ISuppliesRepository _suppliesRepository;

        public CreateSupplyHandler(ISuppliesRepository suppliesRepository)
        {
            _suppliesRepository = suppliesRepository;
        }

        public async Task<Unit> Handle(CreateSupplyRequest request, CancellationToken cancellationToken)
        {
            var supplierInfo = new SupplierInfo(request.Supply.SupplierInfo.Name);
            var supplyItems = request.Supply.SupplyItems.Select(x => new SupplyItem(
                x.Id,
                x.ProductId,
                new Money(x.UnitPrice),
                new Quantity(x.Quantity),
                new Discount(x.DiscountPerUnit)));
            
            var supply = new Supply(
                request.Supply.Id,
                request.Supply.Version,
                supplierInfo,
                supplyItems);

            await _suppliesRepository.SaveAsync(supply, cancellationToken);
            
            return Unit.Value;
        }
    }
}