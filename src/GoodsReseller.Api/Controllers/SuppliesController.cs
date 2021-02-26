using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Contracts.Queries;
using GoodsReseller.SupplyContext.Contracts.Supplies.BatchByQuery;
using GoodsReseller.SupplyContext.Contracts.Supplies.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/supplies")]
    public class SuppliesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuppliesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("{supplyId}")]
        public async Task<IActionResult> GetSupplyAsync(Guid supplyId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetSupplyByIdRequest
            {
                SupplyId = supplyId
            }, cancellationToken);

            if (response.Supply == null)
            {
                return NotFound();
            }

            return Ok(response.Supply);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSupplyListAsync([FromQuery] BatchSuppliesQuery query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new BatchSuppliesByQueryRequest
            {
                Query = query
            }, cancellationToken);
            
            return Ok(response.SupplyList);
        }
    }
}