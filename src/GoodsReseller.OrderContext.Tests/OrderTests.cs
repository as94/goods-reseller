using System;
using FluentAssertions;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork.ValueObjects;
using Xunit;

namespace GoodsReseller.OrderContext.Tests
{
    public class OrderTests
    {
        [Fact]
        public void WhenAddOrderItem_ThenOrderHaveCorrectTotalCost()
        {
            var orderId = Guid.NewGuid();
            var newOrder = GetOrder(orderId);

            newOrder.AddOrderItem(
                Guid.NewGuid(),
                new Money(10000),
                Discount.Empty,
                new DateValueObject());

            newOrder.TotalCost.Should().Be(new Money(10000));
        }
        
        [Fact]
        public void WhenAddOrderItemWithDiscount_ThenOrderHaveCorrectTotalCost()
        {
            var orderId = Guid.NewGuid();
            var newOrder = GetOrder(orderId);

            newOrder.AddOrderItem(
                Guid.NewGuid(),
                new Money(10000),
                new Discount(0.3M), 
                new DateValueObject());

            newOrder.TotalCost.Should().Be(new Money(7000));
        }

        [Fact]
        public void WhenAddOrderItemWithDiscountTwice_ThenOrderHaveCorrectTotalCost()
        {
            var orderId = Guid.NewGuid();
            var newOrder = GetOrder(orderId);
            var productId = Guid.NewGuid();
            
            newOrder.AddOrderItem(
                productId,
                new Money(10000),
                new Discount(0.3M), 
                new DateValueObject());
            newOrder.AddOrderItem(
                productId,
                new Money(10000),
                new Discount(0.3M), 
                new DateValueObject());

            newOrder.TotalCost.Should().Be(new Money(14000));
        }

        [Fact]
        public void WhenRemoveNotExistingOrderItem_ThenOrderStateDidNotChange()
        {
            var orderId = Guid.NewGuid();
            var newOrder = GetOrder(orderId);
            var productId = Guid.NewGuid();
            
            newOrder.RemoveOrderItem(productId, new DateValueObject());
            
            newOrder.Should().BeEquivalentTo(GetOrder(orderId));
        }

        [Fact]
        public void WhenRemoveOrderItemAfterAddingIt_ThenOrderHaveCorrectTotalCost()
        {
            var orderId = Guid.NewGuid();
            var newOrder = GetOrder(orderId);
            var productId = Guid.NewGuid();
            
            newOrder.AddOrderItem(
                productId,
                new Money(10000),
                Discount.Empty,
                new DateValueObject());
            newOrder.RemoveOrderItem(productId, new DateValueObject());

            newOrder.TotalCost.Should().Be(Money.Zero);
        }
        
        [Fact]
        public void WhenRemoveOrderItemAfterAddingItTwice_ThenOrderHaveCorrectTotalCost()
        {
            var orderId = Guid.NewGuid();
            var newOrder = GetOrder(orderId);
            var productId = Guid.NewGuid();
            
            newOrder.AddOrderItem(
                productId,
                new Money(10000),
                Discount.Empty,
                new DateValueObject());
            newOrder.AddOrderItem(
                productId,
                new Money(10000),
                Discount.Empty,
                new DateValueObject());
            newOrder.RemoveOrderItem(productId, new DateValueObject());

            newOrder.TotalCost.Should().Be(new Money(10000));
        }

        private Order GetOrder(Guid orderId)
        {
            return new Order(
                orderId,
                1,
                OrderStatus.DataReceived.Name,
                new Address("Russia", "Moscow", "Tverskoy Boulevard", "123104"),
                new CustomerInfo("+7 999 999 99 99"));
        }
    }
}