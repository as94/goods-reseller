using System;
using FluentAssertions;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork.ValueObjects;
using Xunit;

namespace GoodsReseller.UnitTests.Orders
{
    public class CreateTests
    {
        [Fact]
        public void NewOrderTest()
        {
            var orderId = Guid.NewGuid();
            var orderItems = new[]
            {
                new OrderItem(
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    new Money(500),
                    new Quantity(1),
                    Discount.Empty)
            };

            var order = new Order(
                orderId,
                1,
                OrderStatus.Accepted,
                new Address("Moscow", "Moscow Street", "123123"),
                new CustomerInfo("+7 999 111 22 33"),
                new Money(300),
                new Money(1000),
                orderItems
            );

            order.Should().BeEquivalentTo(new Order(
                orderId,
                1,
                OrderStatus.Accepted,
                new Address("Moscow", "Moscow Street", "123123"),
                new CustomerInfo("+7 999 111 22 33"),
                new Money(300),
                new Money(1000),
                orderItems),
                options => options
                    .Excluding(x => x.IsRemoved)
                    .Excluding(x => x.CreationDate)
                    .Excluding(x => x.LastUpdateDate)
                    .Excluding(x => x.TotalCost));

            order.IsRemoved.Should().BeFalse();
            order.CreationDate.Should().NotBeNull();
            order.LastUpdateDate.Should().BeNull();
            order.TotalCost.Should().BeEquivalentTo(new Money(1800));
        }
    }
}