using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.OrderItems.Commands
{
    internal sealed class RemoveOrderItemCommand : OrderItemCommand
    {
        public const string CommandKey = "remove";
        
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMediator _mediator;

        public RemoveOrderItemCommand(IOrdersRepository ordersRepository, IMediator mediator) : base(CommandKey)
        {
            _mediator = mediator;
            _ordersRepository = ordersRepository;
        }

        public override async Task ExecuteAsync(OrderItemCommandParameters parameters, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetAsync(parameters.OrderId, cancellationToken);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with Id = {parameters.OrderId} doesn't exist");
            }
            
            // TODO: add
            // var product = await _mediator.Send(new GetProductByIdRequest { ProductId = request.ProductId }, cancellationToken);
            // if (product == null) ...
            
            order.RemoveOrderItem(parameters.ProductId, new DateValueObject(DateTime.Now));
            await _ordersRepository.SaveAsync(order, cancellationToken);
        }
    }
}