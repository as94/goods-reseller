using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.OrderItems.AddOrderItem;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.OrderItems
{
    public sealed class AddOrderItemHandler : IRequestHandler<AddOrderItemRequest, AddOrderItemResponse>
    {
        private readonly IOrdersRepository _ordersRepository;

        public AddOrderItemHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        
        public async Task<AddOrderItemResponse> Handle(AddOrderItemRequest request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetAsync(request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new InvalidOperationException($"Order with Id = {request.OrderId} doesn't exist");
            }
            
            // TODO: add
            // var product = await _mediator.Send(new GetProductByIdRequest { ProductId = request.ProductId }, cancellationToken);
            // if (product == null) ...
            
            // var money = await _mediator.Send(new GetMoneyByProductIdRequest { ProductId = request.ProductId }, cancellationToken);
            // if (money == null) ...
            
            // var discount = await _mediator.Send(new GetDiscountByProductIdRequest { ProductId = request.ProductId }, cancellationToken);
            // if (discount == null) ...
            
            var product = new Product(request.ProductId, 1, "Table");
            var money = new Money(10000M);
            
            order.AddOrderItem(product, money, Factor.Empty, new DateValueObject(DateTime.Now));

            await _ordersRepository.SaveAsync(order, cancellationToken);
            
            return new AddOrderItemResponse
            {
                OrderId = order.Id
            };
        }
    }
}