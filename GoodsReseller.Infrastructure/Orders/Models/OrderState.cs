using System;
using System.Linq;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.Infrastructure.Orders.Models
{
    internal sealed class OrderState
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        
        public Address Address { get; set; }
        
        public DateValueObject CreationDate { get; set; }
        public DateValueObject? LastUpdateDate { get; set; }
        
        public OrderItemState[] OrderItems { get; set; }
        
        public Money TotalCost { get; set; }

        public Order ToDomain()
        {
            return Order.Restore(
                Id,
                Version,
                Address,
                CreationDate,
                LastUpdateDate,
                OrderItems.Select(x => x.ToDomain())
                    .ToList(),
                TotalCost);
        }
    }
}