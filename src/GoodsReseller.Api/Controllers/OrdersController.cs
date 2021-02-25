using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Contracts.OrderItems.PatchOrderItem;
using GoodsReseller.OrderContext.Contracts.Orders.BatchByQuery;
using GoodsReseller.OrderContext.Contracts.Orders.Create;
using GoodsReseller.OrderContext.Contracts.Orders.DeleteById;
using GoodsReseller.OrderContext.Contracts.Orders.GetById;
using GoodsReseller.OrderContext.Contracts.Orders.Update;
using GoodsReseller.OrderContext.Contracts.Queries;
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
        
        [HttpGet("list")]
        public async Task<IActionResult> GetOrderListAsync([FromQuery] BatchOrdersQuery query, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new BatchOrdersByQueryRequest
            {
                Query = query
            }, cancellationToken);
            
            return Ok(response.OrderList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(
            [FromBody] [Required] CreateOrderContract createOrder,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new CreateOrderRequest
            {
                Address = createOrder.Address,
                CustomerInfo = createOrder.CustomerInfo
            }, cancellationToken));
        }

        [HttpPatch("{orderId}/orderInfo")]
        public async Task<IActionResult> PatchOrderInfoAsync(
            [FromRoute] [Required] Guid orderId,
            [FromBody] [Required] OrderInfoContract orderInfo,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateOrderRequest
            {
                OrderId = orderId,
                Status = orderInfo.Status,
                Address = orderInfo.Address,
                CustomerInfo = orderInfo.CustomerInfo
            }, cancellationToken);
            
            return Ok();
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
        
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrderAsync(
            [FromRoute] [Required] Guid orderId,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteOrderByIdRequest
            {
                OrderId = orderId
            }, cancellationToken);
            
            return Ok();
        }
    }
}