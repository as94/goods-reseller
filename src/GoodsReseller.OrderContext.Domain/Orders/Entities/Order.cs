using System;
using System.Collections.Generic;
using System.Linq;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.OrderContext.Domain.Orders.Entities
{
    public sealed class Order : VersionedEntity, IAggregateRoot
    {
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.Where(x => !x.IsRemoved).ToList();
        
        // TODO: order status
        
        // payment method
        // card info
        
        public Address Address { get; }
        public CustomerInfo CustomerInfo { get; }
        public Money TotalCost { get; private set; }
        
        // TODO: extract to Metadata
        public DateValueObject CreationDate { get; }
        public DateValueObject? LastUpdateDate { get; private set; }
        public bool IsRemoved { get; private set; }

        public Order(Guid id, int version, Address address, CustomerInfo customerInfo)
            : this(id, version)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (customerInfo == null)
            {
                throw new ArgumentNullException(nameof(customerInfo));
            }
            
            Address = address;
            CustomerInfo = customerInfo;
        }

        private Order(Guid id, int version) : base(id, version)
        {
            CreationDate = new DateValueObject();
            _orderItems = new List<OrderItem>();
            TotalCost = Money.Zero;
        }

        public void AddOrderItem(Guid productId, Money unitPrice, Discount discountPerUnit, DateValueObject lastUpdateDate)
        {
            if (unitPrice == null)
            {
                throw new ArgumentNullException(nameof(unitPrice));
            }
            
            if (discountPerUnit == null)
            {
                throw new ArgumentNullException(nameof(discountPerUnit));
            }

            if (lastUpdateDate == null)
            {
                throw new ArgumentNullException(nameof(lastUpdateDate));
            }

            var existingOrderItem = OrderItems.FirstOrDefault(x => x.ProductId == productId);
            if (existingOrderItem != null)
            {
                existingOrderItem.IncrementQuantity(lastUpdateDate);
            }
            else
            {
                var newOrderItem = new OrderItem(Guid.NewGuid(), productId, unitPrice, new Quantity(1), discountPerUnit);
                _orderItems.Add(newOrderItem);
            }

            RecalculateTotalCost();
            IncrementVersion();
            LastUpdateDate = lastUpdateDate;
        }
        
        public void RemoveOrderItem(Guid productId, DateValueObject lastUpdateDate)
        {
            if (lastUpdateDate == null)
            {
                throw new ArgumentNullException(nameof(lastUpdateDate));
            }
            
            var existingOrderItem = OrderItems.FirstOrDefault(x => x.ProductId == productId);
            if (existingOrderItem != null)
            {
                if (existingOrderItem.Quantity.Value > 1)
                {
                    existingOrderItem.DecrementQuantity(lastUpdateDate);
                }
                else
                {
                    existingOrderItem.Remove(lastUpdateDate);
                }

                RecalculateTotalCost();
                IncrementVersion();
                LastUpdateDate = lastUpdateDate;
            }
        }
        
        // TODO: extract to VersionedEntity
        public void Remove()
        {
            if (IsRemoved)
            {
                return;
            }
            
            IsRemoved = true;
            
            IncrementVersion();
            LastUpdateDate = new DateValueObject();
        }

        private void RecalculateTotalCost()
        {
            var totalCost = Money.Zero;

            foreach (var orderItem in OrderItems)
            {
                var unitPriceFactor = new Factor(1 - orderItem.DiscountPerUnit.Value);
                var quantityFactor = new Factor(orderItem.Quantity.Value);

                var orderItemValue = orderItem.UnitPrice.Multiply(unitPriceFactor).Multiply(quantityFactor);
                
                totalCost = totalCost.Add(orderItemValue);
            }

            TotalCost = totalCost;
        }
    }
}