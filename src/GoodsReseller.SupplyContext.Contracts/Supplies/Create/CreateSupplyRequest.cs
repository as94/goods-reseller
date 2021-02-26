using GoodsReseller.SupplyContext.Contracts.Models;
using MediatR;

namespace GoodsReseller.SupplyContext.Contracts.Supplies.Create
{
    public class CreateSupplyRequest : IRequest<CreateSupplyResponse>
    {
        public SupplyInfoContract Supply { get; set; }
    }
}