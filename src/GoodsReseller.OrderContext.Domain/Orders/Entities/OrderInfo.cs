using System;
using System.Collections.Generic;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.OrderContext.Domain.Orders.Entities
{
    public sealed class OrderInfo
    {
        public OrderInfo(
            OrderStatus status,
            Address address,
            CustomerInfo customerInfo,
            Money deliveryCost,
            Money addedCost,
            IEnumerable<OrderItem> orderItems)
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

            if (deliveryCost == null)
            {
                throw new ArgumentNullException(nameof(deliveryCost));
            }

            if (addedCost == null)
            {
                throw new ArgumentNullException(nameof(addedCost));
            }

            if (orderItems == null)
            {
                throw new ArgumentNullException(nameof(orderItems));
            }

            Status = status;
            Address = address;
            CustomerInfo = customerInfo;
            DeliveryCost = deliveryCost;
            AddedCost = addedCost;
            OrderItems = orderItems;
        }

        public OrderStatus Status { get; }
        public Address Address { get; }
        public CustomerInfo CustomerInfo { get; }
        public Money DeliveryCost { get; }
        public Money AddedCost { get; }
        public IEnumerable<OrderItem> OrderItems { get; }
    }
}