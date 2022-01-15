using System;
using System.Linq;
using FluentAssertions;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork.ValueObjects;
using Xunit;

namespace GoodsReseller.UnitTests.Orders
{
    public class RemoveTests
    {
        [Fact]
        public void RemoveOrderTest()
        {
            var orderId = Guid.NewGuid();
            var orderItemId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var orderItems = new[]
            {
                new OrderItem(
                    orderItemId,
                    productId,
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
            
            order.Remove();

            order.Should().BeEquivalentTo(new Order(
                    orderId,
                    2,
                    OrderStatus.Accepted,
                    new Address("Moscow", "Moscow Street", "123123"),
                    new CustomerInfo("+7 999 111 22 33"),
                    new Money(300),
                    new Money(1000),
                    orderItems
                        .Select(item =>
                        {
                            var newItem = new OrderItem(
                                item.Id,
                                item.ProductId,
                                item.UnitPrice,
                                item.Quantity,
                                item.DiscountPerUnit);
                            newItem.Remove(item.LastUpdateDate);
                            return newItem;
                        })
                        .ToArray()),
                options => options
                    .Excluding(x => x.IsRemoved)
                    .Excluding(x => x.CreationDate)
                    .Excluding(x => x.LastUpdateDate));

            order.IsRemoved.Should().BeTrue();
            order.CreationDate.Should().NotBeNull();
            order.LastUpdateDate.Should().NotBeNull();
        }
    }
}