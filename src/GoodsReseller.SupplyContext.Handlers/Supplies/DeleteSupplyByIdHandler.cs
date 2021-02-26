using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Contracts.Supplies.DeleteById;
using MediatR;

namespace GoodsReseller.SupplyContext.Handlers.Supplies
{
    public class DeleteSupplyByIdHandler : IRequestHandler<DeleteSupplyByIdRequest, Unit>
    {
        public Task<Unit> Handle(DeleteSupplyByIdRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}