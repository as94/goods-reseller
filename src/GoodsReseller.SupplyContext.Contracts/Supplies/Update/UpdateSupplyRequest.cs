using System;
using GoodsReseller.SupplyContext.Contracts.Models;
using MediatR;

namespace GoodsReseller.SupplyContext.Contracts.Supplies.Update
{
    public class UpdateSupplyRequest : IRequest<Unit>
    {
        public Guid SupplyId { get; set; }
        public SupplyInfoContract Supply { get; set; }
    }
}