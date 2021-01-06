using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Models.OrderItems;
using GoodsReseller.OrderContext.Contracts.OrderItems.AddOrderItem;
using GoodsReseller.OrderContext.Contracts.OrderItems.RemoveOrderItem;
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
            CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(new CreateOrderRequest(), cancellationToken));
        
        [HttpPost("{orderId}/orderItems")]
        public async Task<IActionResult> AddOrderItemAsync(
            [FromRoute] Guid orderId,
            [FromBody][Required] AddOrderItemContract addOrderItem,
            CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(new AddOrderItemRequest
            {
                OrderId = orderId,
                ProductId = addOrderItem.ProductId
            }, cancellationToken));
        
        [HttpDelete("{orderId}/orderItems")]
        public async Task<IActionResult> RemoveOrderItemAsync(
            [FromRoute] Guid orderId,
            [FromBody][Required] RemoveOrderItemContract addOrderItem,
            CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(new RemoveOrderItemRequest
            {
                OrderId = orderId,
                ProductId = addOrderItem.ProductId
            }, cancellationToken));
    }
}