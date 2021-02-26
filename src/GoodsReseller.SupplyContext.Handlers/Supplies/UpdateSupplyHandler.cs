using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Contracts.Supplies.Update;
using MediatR;

namespace GoodsReseller.SupplyContext.Handlers.Supplies
{
    public class UpdateSupplyHandler : IRequestHandler<UpdateSupplyRequest, Unit>
    {
        public Task<Unit> Handle(UpdateSupplyRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}