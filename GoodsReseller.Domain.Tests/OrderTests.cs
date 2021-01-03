using System;
using FluentAssertions;
using GoodsReseller.Domain.Orders.Entities;
using GoodsReseller.Domain.Orders.ValueObjects;
using GoodsReseller.Domain.SeedWork;
using Xunit;

namespace GoodsReseller.Domain.Tests
{
    public class OrderTests
    {
        [Fact]
        public void WhenAddOrderItem_ThenOrderHaveCorrectTotalCost()
        {
            var orderId = Guid.NewGuid();
            var orderCreationDate = DateTime.Now;
            var newOrder = GetOrder(orderId, orderCreationDate);

            newOrder.AddOrderItem(
                new Product(Guid.NewGuid(), 1, "Table"),
                new Money(10000),
                Factor.Empty,
                new DateValueObject(orderCreationDate.AddMinutes(2)));

            newOrder.TotalCost.Should().Be(new Money(10000));
        }
        
        [Fact]
        public void WhenAddOrderItemWithDiscount_ThenOrderHaveCorrectTotalCost()
        {
            var orderId = Guid.NewGuid();
            var orderCreationDate = DateTime.Now;
            var newOrder = GetOrder(orderId, orderCreationDate);

            newOrder.AddOrderItem(
                new Product(Guid.NewGuid(), 1, "Table"),
                new Money(10000),
                new Factor(0.3M), 
                new DateValueObject(orderCreationDate.AddMinutes(2)));

            newOrder.TotalCost.Should().Be(new Money(7000));
        }

        [Fact]
        public void WhenAddOrderItemWithDiscountTwice_ThenOrderHaveCorrectTotalCost()
        {
            var orderId = Guid.NewGuid();
            var orderCreationDate = DateTime.Now;
            var newOrder = GetOrder(orderId, orderCreationDate);
            var product = new Product(Guid.NewGuid(), 1, "Table");
            
            newOrder.AddOrderItem(
                product,
                new Money(10000),
                new Factor(0.3M), 
                new DateValueObject(orderCreationDate.AddMinutes(2)));
            newOrder.AddOrderItem(
                product,
                new Money(10000),
                new Factor(0.3M), 
                new DateValueObject(orderCreationDate.AddMinutes(3)));

            newOrder.TotalCost.Should().Be(new Money(14000));
        }

        [Fact]
        public void WhenRemoveNotExistingOrderItem_ThenOrderStateDidNotChange()
        {
            var orderId = Guid.NewGuid();
            var orderCreationDate = DateTime.Now;
            var newOrder = GetOrder(orderId, orderCreationDate);
            var product = new Product(Guid.NewGuid(), 1, "Table");
            
            newOrder.RemoveOrderItem(product.Id, new DateValueObject(orderCreationDate.AddMinutes(2)));
            
            newOrder.Should().BeEquivalentTo(GetOrder(orderId, orderCreationDate));
        }

        [Fact]
        public void WhenRemoveOrderItemAfterAddingIt_ThenOrderHaveCorrectTotalCost()
        {
            var orderId = Guid.NewGuid();
            var orderCreationDate = DateTime.Now;
            var newOrder = GetOrder(orderId, orderCreationDate);
            var product = new Product(Guid.NewGuid(), 1, "Table");
            
            newOrder.AddOrderItem(
                product,
                new Money(10000),
                Factor.Empty,
                new DateValueObject(orderCreationDate.AddMinutes(2)));
            newOrder.RemoveOrderItem(product.Id, new DateValueObject(orderCreationDate.AddMinutes(3)));

            newOrder.TotalCost.Should().Be(Money.Zero);
        }
        
        [Fact]
        public void WhenRemoveOrderItemAfterAddingItTwice_ThenOrderHaveCorrectTotalCost()
        {
            var orderId = Guid.NewGuid();
            var orderCreationDate = DateTime.Now;
            var newOrder = GetOrder(orderId, orderCreationDate);
            var product = new Product(Guid.NewGuid(), 1, "Table");
            
            newOrder.AddOrderItem(
                product,
                new Money(10000),
                Factor.Empty,
                new DateValueObject(orderCreationDate.AddMinutes(2)));
            newOrder.AddOrderItem(
                product,
                new Money(10000),
                Factor.Empty,
                new DateValueObject(orderCreationDate.AddMinutes(3)));
            newOrder.RemoveOrderItem(product.Id, new DateValueObject(orderCreationDate.AddMinutes(4)));

            newOrder.TotalCost.Should().Be(new Money(10000));
        }

        private Order GetOrder(Guid orderId, DateTime orderCreationDate)
        {
            return new Order(
                orderId,
                1,
                new Address("Russia", "Moscow", "Tverskoy Boulevard", "123104"),
                new DateValueObject(orderCreationDate));
        }
    }
}