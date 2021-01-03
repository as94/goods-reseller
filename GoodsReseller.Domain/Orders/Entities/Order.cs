using System;
using System.Collections.Generic;
using System.Linq;
using GoodsReseller.Domain.Orders.ValueObjects;
using GoodsReseller.Domain.SeedWork;

namespace GoodsReseller.Domain.Orders.Entities
{
    public sealed class Order : Entity, IAggregateRoot
    {
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        
        // TODO: order status
        
        // payment method
        // card info
        
        public Address Address { get; }
        public DateAndDateUtcPair CreationDate { get; }
        public DateAndDateUtcPair? LastUpdateDate { get; }
        
        public Order(Guid id, int version, Address address, DateAndDateUtcPair creationDate)
            : base(id, version)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }
            
            if (creationDate == null)
            {
                throw new ArgumentNullException(nameof(creationDate));
            }
            
            Address = address;
            CreationDate = creationDate;
            LastUpdateDate = null;
            _orderItems = new List<OrderItem>();
        }

        // TODO: add unit test
        public void AddOrderItem(Guid productId, Product product, Money unitPrice, Factor totalDiscount)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            
            if (unitPrice == null)
            {
                throw new ArgumentNullException(nameof(unitPrice));
            }
            
            if (totalDiscount == null)
            {
                throw new ArgumentNullException(nameof(totalDiscount));
            }

            var existingOrderItem = _orderItems.FirstOrDefault(x => x.Product.Id == productId);
            if (existingOrderItem != null)
            {
                existingOrderItem.IncrementQuantity();
            }
            
            var newOrderItem = new OrderItem(Guid.NewGuid(), 1, product, unitPrice, totalDiscount, new Quantity(1));
            _orderItems.Add(newOrderItem);
        }
        
        // TODO: add unit test
        public void RemoveOrderItem(Guid productId)
        {
            var existingOrderItem = _orderItems.FirstOrDefault(x => x.Product.Id == productId);
            if (existingOrderItem != null)
            {
                if (existingOrderItem.Quantity.Value > 0)
                {
                    existingOrderItem.DecrementQuantity();
                }
                else
                {
                    _orderItems.Remove(existingOrderItem);
                }
            }
        }
    }
}