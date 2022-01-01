using GoodsReseller.SupplyContext.Contracts.Models;
using MediatR;

namespace GoodsReseller.SupplyContext.Contracts.Supplies.Create
{
    public class CreateSupplyRequest : IRequest
    {
        public SupplyInfoContract Supply { get; set; }
    }
}