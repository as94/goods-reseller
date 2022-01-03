using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.NotificationContext.Contracts;
using GoodsReseller.OrderContext.Contracts.Models;
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
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public async Task<IActionResult> GetOrderListAsync(
            [FromQuery] BatchOrdersQuery query,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new BatchOrdersByQueryRequest
            {
                Query = query
            }, cancellationToken);

            return Ok(response.OrderList);
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateOrderAsync(
            [FromBody] [Required] OrderInfoContract orderInfo,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new CreateOrderRequest
            {
                OrderInfo = orderInfo
            }, cancellationToken);

            // TODO: add to local_queue
            await _mediator.Send(new OrderAcceptedNotificationRequest
            {
                ClientPhoneNumber = orderInfo.CustomerInfo.PhoneNumber,
                ClientName = orderInfo.CustomerInfo.Name
            }, cancellationToken);

            return Ok();
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrderAsync(
            [FromRoute] [Required] Guid orderId,
            [FromBody] [Required] OrderInfoContract orderInfo,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateOrderRequest
            {
                OrderId = orderId,
                OrderInfo = orderInfo
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