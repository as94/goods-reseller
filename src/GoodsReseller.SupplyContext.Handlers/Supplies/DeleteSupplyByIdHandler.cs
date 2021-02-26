using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Contracts.Supplies.DeleteById;
using GoodsReseller.SupplyContext.Domain.Supplies;
using MediatR;

namespace GoodsReseller.SupplyContext.Handlers.Supplies
{
    public class DeleteSupplyByIdHandler : IRequestHandler<DeleteSupplyByIdRequest, Unit>
    {
        private readonly ISuppliesRepository _suppliesRepository;
        
        public DeleteSupplyByIdHandler(ISuppliesRepository suppliesRepository)
        {
            _suppliesRepository = suppliesRepository;
        }
        
        public async Task<Unit> Handle(DeleteSupplyByIdRequest request, CancellationToken cancellationToken)
        {
            var supply = await _suppliesRepository.GetAsync(request.SupplyId, cancellationToken);
            if (supply == null)
            {
                throw new InvalidOperationException($"Supply with Id = {request.SupplyId} doesn't exist");
            }
            
            supply.Remove();

            await _suppliesRepository.SaveAsync(supply, cancellationToken);
            
            return new Unit();
        }
    }
}