using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.NotificationContext.Contracts;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Contracts.Orders.Create;
using GoodsReseller.OrderContext.Contracts.Orders.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers.Public
{
    [ApiController]
    [Route("api/public/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
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
    }
}