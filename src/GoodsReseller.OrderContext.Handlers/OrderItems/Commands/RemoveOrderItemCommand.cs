using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.GetById;
using GoodsReseller.OrderContext.Domain.Orders;
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
            
            var response = await _mediator.Send(new GetProductByIdRequest
            {
                ProductId = parameters.ProductId
            }, cancellationToken);
            
            if (response.Product == null)
            {
                throw new InvalidOperationException($"Product with Id = {parameters.ProductId} doesn't exist");
            }
            
            order.RemoveOrderItem(parameters.ProductId, new DateValueObject(DateTime.Now));
            await _ordersRepository.SaveAsync(order, cancellationToken);
        }
    }
}