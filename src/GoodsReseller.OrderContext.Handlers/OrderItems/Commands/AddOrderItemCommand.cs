using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.DataCatalogContext.Contracts.Products.GetById;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
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
            
            var response = await _mediator.Send(new GetProductByIdRequest
            {
                ProductId = parameters.ProductId
            }, cancellationToken);
            
            if (response.Product == null)
            {
                throw new InvalidOperationException($"Product with Id = {parameters.ProductId} doesn't exist");
            }
            
            var unitPrice = new Money(response.Product.UnitPrice);
            var discountPerUnit = new Discount(response.Product.DiscountPerUnit);
            
            order.AddOrderItem(response.Product.Id, unitPrice, discountPerUnit, new DateValueObject(DateTime.Now));
            await _ordersRepository.SaveAsync(order, cancellationToken);
        }
    }
}