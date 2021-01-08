using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.OrderItems.PatchOrderItem;
using GoodsReseller.OrderContext.Domain.Orders;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.OrderContext.Handlers.OrderItems.Commands;
using GoodsReseller.SeedWork;
using MediatR;

namespace GoodsReseller.OrderContext.Handlers.OrderItems
{
    public sealed class PatchOrderItemHandler : IRequestHandler<PatchOrderItemRequest, Unit>
    {
        private readonly OrderItemCommand[] _commands;

        public PatchOrderItemHandler(IOrdersRepository ordersRepository, IMediator mediator)
        {
            _commands = new OrderItemCommand[]
            {
                new AddOrderItemCommand(ordersRepository, mediator),
                new RemoveOrderItemCommand(ordersRepository, mediator) 
            };
        }
        
        public async Task<Unit> Handle(PatchOrderItemRequest request, CancellationToken cancellationToken)
        {
            var command = _commands.First(x => x.Op == request.Op);
            var commandParameters = new OrderItemCommandParameters(request.OrderId, request.ProductId);

            await command.ExecuteAsync(commandParameters, cancellationToken);

            return new Unit();
        }
    }
}