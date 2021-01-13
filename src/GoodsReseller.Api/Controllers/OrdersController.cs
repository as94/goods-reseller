using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Contracts.OrderItems.PatchOrderItem;
using GoodsReseller.OrderContext.Contracts.Orders.Create;
using GoodsReseller.OrderContext.Contracts.Orders.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers
{
    [Authorize(Roles = "Admin, Customer")]
    [ApiController]
    [Route("api/orders")]
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
            var response = await _mediator.Send(new GetOrderByIdRequest
            {
                OrderId = orderId
            }, cancellationToken);

            if (response.Order == null)
            {
                return NotFound();
            }

            return Ok(response.Order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(
            [FromBody] [Required] CreateOrderRequest createOrder,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(createOrder, cancellationToken));
        }

        [HttpPatch("{orderId}/orderItems")]
        public async Task<IActionResult> PatchOrderItemAsync(
            [FromRoute] [Required] Guid orderId,
            [FromBody] [Required] PatchOrderItemContract patchOrderItem,
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