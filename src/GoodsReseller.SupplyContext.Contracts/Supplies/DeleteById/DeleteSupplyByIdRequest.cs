using System;
using MediatR;

namespace GoodsReseller.SupplyContext.Contracts.Supplies.DeleteById
{
    public class DeleteSupplyByIdRequest : IRequest<Unit>
    {    
        public Guid SupplyId { get; set; }
    }
}