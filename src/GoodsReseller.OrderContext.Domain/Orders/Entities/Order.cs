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

        public OrderStatus Status { get; private set; }
        
        // payment method
        // card info
        
        public Address Address { get; private set; }
        public CustomerInfo CustomerInfo { get; private set; }
        public Money TotalCost { get; private set; }
        
        // TODO: extract to Metadata
        public DateValueObject CreationDate { get; }
        public DateValueObject? LastUpdateDate { get; private set; }
        public bool IsRemoved { get; private set; }

        public Order(Guid id, int version, string status, Address address, CustomerInfo customerInfo)
            : this(id, version)
        {
            if (status == null)
            {
                throw new ArgumentNullException(nameof(status));
            }
            
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            if (customerInfo == null)
            {
                throw new ArgumentNullException(nameof(customerInfo));
            }
            
            if (!Enumeration.TryParse<OrderStatus>(status, out var parsedStatus))
            {
                // TODO: business rule, add translations
                throw new ArgumentException($"Status '{status}' is invalid");
            }

            Status = parsedStatus;
            Address = address;
            CustomerInfo = customerInfo;
        }

        private Order(Guid id, int version) : base(id, version)
        {
            CreationDate = new DateValueObject();
            _orderItems = new List<OrderItem>();
            TotalCost = Money.Zero;
        }

        public void Update(OrderInfo orderInfo)
        {
            if (IsRemoved)
            {
                throw new InvalidOperationException($"Order with id = {Id} has already been removed");
            }

            if (orderInfo == null)
            {
                throw new ArgumentNullException(nameof(orderInfo));
            }

            if (orderInfo.Status != null)
            {
                if (!Enumeration.TryParse<OrderStatus>(orderInfo.Status, out var parsedStatus))
                {
                    // TODO: business rule, add translations
                    throw new ArgumentException($"Status '{orderInfo.Status}' is invalid");
                }
                
                Status = parsedStatus;
            }

            if (orderInfo.Address != null)
            {
                Address = orderInfo.Address.Copy();
            }

            if (orderInfo.CustomerInfo != null)
            {
                CustomerInfo = orderInfo.CustomerInfo.Copy();
            }
            
            IncrementVersion();
            LastUpdateDate = new DateValueObject();
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