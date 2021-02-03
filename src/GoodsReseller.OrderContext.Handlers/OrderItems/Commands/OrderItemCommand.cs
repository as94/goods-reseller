using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.OrderContext.Contracts.Models;

namespace GoodsReseller.OrderContext.Handlers.OrderItems.Commands
{
    internal abstract class OrderItemCommand
    {
        public string Op { get; }
        
        protected OrderItemCommand(string op)
        {
            if (!OrderItemOperations.AllOperations.Contains(op))
            {
                throw new InvalidOperationException(
                    $"Available operations are '{string.Join(",", OrderItemOperations.AllOperations)}'");
            }

            Op = op;
        }

        public abstract Task ExecuteAsync(OrderItemCommandParameters parameters, CancellationToken cancellationToken);
    }
}