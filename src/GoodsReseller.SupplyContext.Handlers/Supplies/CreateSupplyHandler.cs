using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Contracts.Supplies.Create;
using MediatR;

namespace GoodsReseller.SupplyContext.Handlers.Supplies
{
    public class CreateSupplyHandler : IRequestHandler<CreateSupplyRequest, CreateSupplyResponse>
    {
        public Task<CreateSupplyResponse> Handle(CreateSupplyRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}