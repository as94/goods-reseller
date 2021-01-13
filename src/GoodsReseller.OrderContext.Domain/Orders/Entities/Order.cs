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
        private List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        
        // TODO: order status
        
        // payment method
        // card info
        
        public Address Address { get; }
        public CustomerInfo CustomerInfo { get; }
        public DateValueObject CreationDate { get; }
        public DateValueObject? LastUpdateDate { get; private set; }
        
        public Money TotalCost { get; private set; }

        public Order(Guid id, int version, Address address, CustomerInfo customerInfo, DateValueObject creationDate)
            : base(id, version)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (customerInfo == null)
            {
                throw new ArgumentNullException(nameof(customerInfo));
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException(nameof(creationDate));
            }
            
            Address = address;
            CustomerInfo = customerInfo;
            CreationDate = creationDate;
            _orderItems = new List<OrderItem>();
            TotalCost = Money.Zero;
        }

        public static Order Restore(
            Guid id,
            int version,
            Address address,
            CustomerInfo customerInfo,
            DateValueObject creationDate,
            DateValueObject? lastUpdateDate,
            List<OrderItem> orderItems,
            Money totalCost)
        {
            var order = new Order(id, version, address, customerInfo, creationDate)
            {
                LastUpdateDate = lastUpdateDate,
                _orderItems = orderItems,
                TotalCost = totalCost
            };

            return order;
        }

        public void AddOrderItem(Product product, Money unitPrice, Discount discountPerUnit, DateValueObject lastUpdateDate)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            
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

            var existingOrderItem = _orderItems.FirstOrDefault(x => x.Product.Id == product.Id);
            if (existingOrderItem != null)
            {
                existingOrderItem.IncrementQuantity();
            }
            else
            {
                var newOrderItem = new OrderItem(Guid.NewGuid(), product, unitPrice, new Quantity(1), discountPerUnit);
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
            
            var existingOrderItem = _orderItems.FirstOrDefault(x => x.Product.Id == productId);
            if (existingOrderItem != null)
            {
                if (existingOrderItem.Quantity.Value > 1)
                {
                    existingOrderItem.DecrementQuantity();
                }
                else
                {
                    _orderItems.Remove(existingOrderItem);
                }

                RecalculateTotalCost();
                IncrementVersion();
                LastUpdateDate = lastUpdateDate;
            }
        }

        private void RecalculateTotalCost()
        {
            var totalCost = Money.Zero;

            foreach (var orderItem in _orderItems)
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