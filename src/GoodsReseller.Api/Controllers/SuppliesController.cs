using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.SupplyContext.Contracts.Models;
using GoodsReseller.SupplyContext.Contracts.Queries;
using GoodsReseller.SupplyContext.Contracts.Supplies.BatchByQuery;
using GoodsReseller.SupplyContext.Contracts.Supplies.Create;
using GoodsReseller.SupplyContext.Contracts.Supplies.DeleteById;
using GoodsReseller.SupplyContext.Contracts.Supplies.GetById;
using GoodsReseller.SupplyContext.Contracts.Supplies.Update;
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
        
        [HttpPost]
        public async Task<IActionResult> CreateSupplyAsync(
            [FromBody] [Required] SupplyInfoContract supply,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CreateSupplyRequest
            {
                Supply = supply
            }, cancellationToken);

            return Ok(response);
        }

        [HttpPut("{supplyId}")]
        public async Task<IActionResult> UpdateSupplyAsync(
            [FromRoute] [Required] Guid supplyId,
            [FromBody] [Required] SupplyInfoContract supply,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateSupplyRequest
            {
                SupplyId = supplyId,
                Supply = supply
            }, cancellationToken);
            
            return Ok();
        }

        [HttpDelete("{supplyId}")]
        public async Task<IActionResult> DeleteSupplyAsync(
            [FromRoute] [Required] Guid supplyId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteSupplyByIdRequest
            {
                SupplyId = supplyId
            }, cancellationToken);
            
            return Ok();
        }
    }
}