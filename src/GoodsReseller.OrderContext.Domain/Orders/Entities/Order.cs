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
        public Money DeliveryCost { get; private set; }
        public Money TotalCost { get; private set; }

        public Order(
            Guid id,
            int version,
            string status,
            Address address,
            CustomerInfo customerInfo,
            Money deliveryCost,
            IEnumerable<OrderItem> orderItems)
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
            DeliveryCost = deliveryCost;
            
            _orderItems = new List<OrderItem>(orderItems);
            RecalculateTotalCost();
        }

        private Order(Guid id, int version) : base(id, version)
        {
            _orderItems = new List<OrderItem>();
            TotalCost = Money.Zero;
        }

        public void Update(OrderInfo orderInfo, int version)
        {
            if (IsRemoved)
            {
                throw new InvalidOperationException($"Order with id = {Id} has already been removed");
            }

            if (orderInfo == null)
            {
                throw new ArgumentNullException(nameof(orderInfo));
            }

            if (!Enumeration.TryParse<OrderStatus>(orderInfo.Status, out var parsedStatus))
            {
                // TODO: business rule, add translations
                throw new ArgumentException($"Status '{orderInfo.Status}' is invalid");
            }
                
            Status = parsedStatus;

            Address = orderInfo.Address.Copy();
            CustomerInfo = orderInfo.CustomerInfo.Copy();
            DeliveryCost = new Money(orderInfo.DeliveryCost.Value);
            
            var existingOrderItemIds = OrderItems.Select(x => x.Id).ToArray();
            var incomingOrderItemIds = orderInfo.OrderItems.Select(x => x.Id).ToArray();
            
            var toCreateIds = incomingOrderItemIds.Where(id => !existingOrderItemIds.Contains(id));
            var newItems = orderInfo.OrderItems.Where(x => toCreateIds.Contains(x.Id));
            foreach (var newItem in newItems)
            {
                _orderItems.Add(newItem);
            }

            var toUpdateIds = existingOrderItemIds.Where(id => incomingOrderItemIds.Contains(id));
            foreach (var id in toUpdateIds)
            {
                var existing = _orderItems.First(x => x.Id == id);
                var incoming = orderInfo.OrderItems.First(x => x.Id == id);
                existing.Update(incoming);
            }

            var toDeleteIds = existingOrderItemIds.Where(id => !incomingOrderItemIds.Contains(id));
            foreach (var id in toDeleteIds)
            {
                var existing = _orderItems.First(x => x.Id == id);
                existing.Remove();
            }
            
            RecalculateTotalCost();

            Version = version;
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

            TotalCost = totalCost.Add(DeliveryCost);
        }
    }
}