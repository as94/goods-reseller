using System;
using MediatR;

namespace GoodsReseller.SupplyContext.Contracts.Supplies.GetById
{
    public class GetSupplyByIdRequest : IRequest<GetSupplyByIdResponse>
    {
        public Guid SupplyId { get; set; }
    }
}