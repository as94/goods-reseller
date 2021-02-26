using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Contracts.Supplies.GetById;
using MediatR;

namespace GoodsReseller.SupplyContext.Handlers.Supplies
{
    public class GetSupplyByIdHandler : IRequestHandler<GetSupplyByIdRequest, GetSupplyByIdResponse>
    {
        public Task<GetSupplyByIdResponse> Handle(GetSupplyByIdRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}