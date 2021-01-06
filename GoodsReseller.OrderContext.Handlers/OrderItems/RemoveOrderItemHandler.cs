using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.OrderItems.RemoveOrderItem;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.SeedWork;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.OrderItems
{
    public sealed class RemoveOrderItemHandler : IRequestHandler<RemoveOrderItemRequest, RemoveOrderItemResponse>
    {
        private readonly IOrdersRepository _ordersRepository;

        public RemoveOrderItemHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        
        public async Task<RemoveOrderItemResponse> Handle(RemoveOrderItemRequest request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetAsync(request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new InvalidOperationException($"Order with Id = {request.OrderId} doesn't exist");
            }
            
            // var product = await _mediator.Send(new GetProductByIdRequest { ProductId = request.ProductId }, cancellationToken);
            // if (product == null) ...
            
            order.RemoveOrderItem(request.ProductId, new DateValueObject(DateTime.Now));
            
            await _ordersRepository.SaveAsync(order, cancellationToken);
            
            return new RemoveOrderItemResponse
            {
                OrderId = order.Id
            };
        }
    }
}