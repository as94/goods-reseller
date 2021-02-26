using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SeedWork.ValueObjects;
using GoodsReseller.SupplyContext.Contracts.Supplies.Update;
using GoodsReseller.SupplyContext.Domain.Supplies;
using GoodsReseller.SupplyContext.Domain.Supplies.Entities;
using GoodsReseller.SupplyContext.Domain.Supplies.ValueObjects;
using MediatR;

namespace GoodsReseller.SupplyContext.Handlers.Supplies
{
    public class UpdateSupplyHandler : IRequestHandler<UpdateSupplyRequest, Unit>
    {
        private readonly ISuppliesRepository _suppliesRepository;

        public UpdateSupplyHandler(ISuppliesRepository suppliesRepository)
        {
            _suppliesRepository = suppliesRepository;
        }

        public async Task<Unit> Handle(UpdateSupplyRequest request, CancellationToken cancellationToken)
        {
            var supply = await _suppliesRepository.GetAsync(request.SupplyId, cancellationToken);
            if (supply == null)
            {
                throw new InvalidOperationException($"Supply with Id = {request.SupplyId} doesn't exist");
            }
            
            var supplierInfo = new SupplierInfo(request.Supply.SupplierInfo.Name);
            var supplyItems = request.Supply.SupplyItems.Select(x => new SupplyItem(
                x.Id,
                x.ProductId,
                new Money(x.UnitPrice.Value),
                new Quantity(x.Quantity),
                new Discount(x.DiscountPerUnit)));
            
            supply.Update(new SupplyInfo(supplierInfo, supplyItems));

            await _suppliesRepository.SaveAsync(supply, cancellationToken);
            
            return new Unit();
        }
    }
}