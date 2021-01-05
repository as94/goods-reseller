using System;
using System.Threading;
using System.Threading.Tasks;
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

        [HttpGet("hello")]
        public string HelloOrders()
        {
            return "Hello, orders!";
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
    }
}