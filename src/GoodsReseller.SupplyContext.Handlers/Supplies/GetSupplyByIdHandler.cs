using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Contracts.Supplies.GetById;
using GoodsReseller.SupplyContext.Domain.Supplies;
using GoodsReseller.SupplyContext.Handlers.Converters;
using MediatR;

namespace GoodsReseller.SupplyContext.Handlers.Supplies
{
    public class GetSupplyByIdHandler : IRequestHandler<GetSupplyByIdRequest, GetSupplyByIdResponse>
    {
        private readonly ISuppliesRepository _suppliesRepository;

        public GetSupplyByIdHandler(ISuppliesRepository suppliesRepository)
        {
            _suppliesRepository = suppliesRepository;
        }

        public async Task<GetSupplyByIdResponse> Handle(GetSupplyByIdRequest request, CancellationToken cancellationToken)
        {
            var supply = await _suppliesRepository.GetAsync(request.SupplyId, cancellationToken);
            if (supply == null)
            {
                return new GetSupplyByIdResponse();
            }

            return new GetSupplyByIdResponse
            {
                Supply = supply.ToContract()
            };
        }
    }
}