using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Orders.DeleteById;
using GoodsReseller.OrderContext.Domain.Orders;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.Orders
{
    public class DeleteOrderByIdHandler : IRequestHandler<DeleteOrderByIdRequest, Unit>
    {
        private readonly IOrdersRepository _ordersRepository;

        public DeleteOrderByIdHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Unit> Handle(DeleteOrderByIdRequest request, CancellationToken cancellationToken)
        {
            var order = await _ordersRepository.GetAsync(request.OrderId, cancellationToken);

            if (order == null)
            {
                throw new InvalidOperationException($"Order with Id = {request.OrderId} doesn't exist");
            }
            
            order.Remove();

            await _ordersRepository.SaveAsync(order, cancellationToken);
            
            return new Unit();
        }
    }
}