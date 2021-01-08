using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Contracts.OrderItems.PatchOrderItem;
using GoodsReseller.OrderContext.Contracts.Orders.CreateOrder;
using GoodsReseller.OrderContext.Contracts.Orders.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var request = await _mediator.Send(new GetOrderByIdRequest
            {
                OrderId = orderId
            }, cancellationToken);

            if (request.Order == null)
            {
                return NotFound();
            }

            return Ok(request.Order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new CreateOrderRequest(), cancellationToken));
        }

        [HttpPatch("{orderId}/orderItems")]
        public async Task<IActionResult> PatchOrderItemAsync(
            [FromRoute] Guid orderId,
            [FromBody][Required] PatchOrderItemContract patchOrderItem,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new PatchOrderItemRequest
            {
                OrderId = orderId,
                Op = patchOrderItem.Op,
                ProductId = patchOrderItem.ProductId
            }, cancellationToken);
            
            return Ok();
        }
    }
}