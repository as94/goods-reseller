using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.OrderItems.Commands
{
    internal sealed class AddOrderItemCommand : OrderItemCommand
    {
        public const string CommandKey = "add";

        private readonly IOrdersRepository _ordersRepository;
        private readonly IMediator _mediator;

        public AddOrderItemCommand(IOrdersRepository ordersRepository, IMediator mediator) : base(CommandKey)
        {
            _ordersRepository = ordersRepository;
            _mediator = mediator;
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
            
            // product.UnitPrice
            // product.DiscountPerUnit
            
            var product = new Product(parameters.ProductId, 1, "Table");
            var money = new Money(10000M);
            
            order.AddOrderItem(product, money, Factor.Empty, new DateValueObject(DateTime.Now));
            await _ordersRepository.SaveAsync(order, cancellationToken);
        }
    }
}