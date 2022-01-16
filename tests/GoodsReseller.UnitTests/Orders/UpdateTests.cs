using System;
using FluentAssertions;
using GoodsReseller.OrderContext.Domain.Orders.Entities;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using GoodsReseller.SeedWork.ValueObjects;
using Xunit;

namespace GoodsReseller.UnitTests.Orders
{
    public class UpdateTests
    {
        [Fact]
        public void UpdateOrderTest()
        {
            var orderId = Guid.NewGuid();
            var orderItem = new OrderItem(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Money(100),
                new Quantity(1),
                Discount.Empty);
            var order = new Order(
                orderId,
                1,
                OrderStatus.Accepted,
                new Address("Moscow", "Moscow Street", "123123"),
                new CustomerInfo("+7 999 111 22 33"),
                new Money(300),
                new Money(1000),
                new[] { orderItem }
            );

            order.Update(
                new OrderInfo(
                    OrderStatus.Packed,
                    new Address("Novosibirsk", "Novosibirsk Street", "321321"),
                    new CustomerInfo("+7 111 222 33 44", "Ivan V"),
                    new Money(400),
                    new Money(900),
                    new[] { orderItem }), 
                2);

            order.Should().BeEquivalentTo(new Order(
                orderId,
                2,
                OrderStatus.Packed,
                new Address("Novosibirsk", "Novosibirsk Street", "321321"),
                new CustomerInfo("+7 111 222 33 44", "Ivan V"),
                new Money(400),
                new Money(900),
                new[] { orderItem }),
                options => options
                    .Excluding(x => x.IsRemoved)
                    .Excluding(x => x.CreationDate)
                    .Excluding(x => x.LastUpdateDate)
                    .Excluding(x => x.TotalCost));
            
            order.IsRemoved.Should().BeFalse();
            order.CreationDate.Should().NotBeNull();
            order.LastUpdateDate.Should().NotBeNull();
            order.TotalCost.Should().BeEquivalentTo(new Money(1400));
        }
        
        
        [Fact]
        public void UpdateOrderItemUnitPriceTest()
        {
            var orderId = Guid.NewGuid();
            var orderItem = new OrderItem(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Money(100),
                new Quantity(1),
                Discount.Empty);
            var order = new Order(
                orderId,
                1,
                OrderStatus.Accepted,
                new Address("Moscow", "Moscow Street", "123123"),
                new CustomerInfo("+7 999 111 22 33"),
                new Money(300),
                new Money(1000),
                new[] { orderItem }
            );

            var updatedOrderItem = new OrderItem(
                orderItem.Id,
                orderItem.ProductId,
                new Money(200),
                orderItem.Quantity,
                orderItem.DiscountPerUnit);
            order.Update(
                new OrderInfo(
                    order.Status,
                    order.Address,
                    order.CustomerInfo,
                    order.DeliveryCost,
                    order.AddedCost,
                    new[] { updatedOrderItem }), 
                2);

            order.GetExistingOrderItems().Should().BeEquivalentTo(new[] { updatedOrderItem });
            order.TotalCost.Should().BeEquivalentTo(new Money(1500));
        }
        
        [Fact]
        public void UpdateOrderItemQuantityTest()
        {
            var orderId = Guid.NewGuid();
            var orderItem = new OrderItem(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Money(100),
                new Quantity(1),
                Discount.Empty);
            var order = new Order(
                orderId,
                1,
                OrderStatus.Accepted,
                new Address("Moscow", "Moscow Street", "123123"),
                new CustomerInfo("+7 999 111 22 33"),
                new Money(300),
                new Money(1000),
                new[] { orderItem }
            );

            var updatedOrderItem = new OrderItem(
                orderItem.Id,
                orderItem.ProductId,
                orderItem.UnitPrice,
                new Quantity(2),
                orderItem.DiscountPerUnit);
            order.Update(
                new OrderInfo(
                    order.Status,
                    order.Address,
                    order.CustomerInfo,
                    order.DeliveryCost,
                    order.AddedCost,
                    new[] { updatedOrderItem }), 
                2);

            order.GetExistingOrderItems().Should().BeEquivalentTo(new[] { updatedOrderItem });
            order.TotalCost.Should().BeEquivalentTo(new Money(1500));
        }
        
        
        [Fact]
        public void UpdateOrderItemDiscountTest()
        {
            var orderId = Guid.NewGuid();
            var orderItem = new OrderItem(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Money(100),
                new Quantity(1),
                Discount.Empty);
            var order = new Order(
                orderId,
                1,
                OrderStatus.Accepted,
                new Address("Moscow", "Moscow Street", "123123"),
                new CustomerInfo("+7 999 111 22 33"),
                new Money(300),
                new Money(1000),
                new[] { orderItem }
            );

            var updatedOrderItem = new OrderItem(
                orderItem.Id,
                orderItem.ProductId,
                orderItem.UnitPrice,
                orderItem.Quantity,
                new Discount(0.1M));
            order.Update(
                new OrderInfo(
                    order.Status,
                    order.Address,
                    order.CustomerInfo,
                    order.DeliveryCost,
                    order.AddedCost,
                    new[] { updatedOrderItem }), 
                2);

            order.GetExistingOrderItems().Should().BeEquivalentTo(new[] { updatedOrderItem });
            order.TotalCost.Should().BeEquivalentTo(new Money(1390));
        }
        
        [Fact]
        public void AddNewOrderItemTest()
        {
            var orderId = Guid.NewGuid();
            var orderItem = new OrderItem(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Money(100),
                new Quantity(1),
                Discount.Empty);
            var order = new Order(
                orderId,
                1,
                OrderStatus.Accepted,
                new Address("Moscow", "Moscow Street", "123123"),
                new CustomerInfo("+7 999 111 22 33"),
                new Money(300),
                new Money(1000),
                new[] { orderItem }
            );

            var anotherOrderItem = new OrderItem(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Money(500),
                new Quantity(1),
                Discount.Empty);
            order.Update(
                new OrderInfo(
                    order.Status,
                    order.Address,
                    order.CustomerInfo,
                    order.DeliveryCost,
                    order.AddedCost,
                    new[] { orderItem, anotherOrderItem }), 
                2);
            
            
            order.GetExistingOrderItems().Should().BeEquivalentTo(new[] { orderItem, anotherOrderItem });
            order.TotalCost.Should().BeEquivalentTo(new Money(1900));
        }
        
        [Fact]
        public void RemoveOrderItemTest()
        {
            var orderId = Guid.NewGuid();
            var orderItem = new OrderItem(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Money(100),
                new Quantity(1),
                Discount.Empty);
            var anotherOrderItem = new OrderItem(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new Money(500),
                new Quantity(1),
                Discount.Empty);
            var order = new Order(
                orderId,
                1,
                OrderStatus.Accepted,
                new Address("Moscow", "Moscow Street", "123123"),
                new CustomerInfo("+7 999 111 22 33"),
                new Money(300),
                new Money(1000),
                new[] { orderItem, anotherOrderItem }
            );
            
            order.Update(
                new OrderInfo(
                    order.Status,
                    order.Address,
                    order.CustomerInfo,
                    order.DeliveryCost,
                    order.AddedCost,
                    new[] { orderItem }), 
                2);

            order.GetExistingOrderItems().Should().BeEquivalentTo(new[] { orderItem });
            order.TotalCost.Should().BeEquivalentTo(new Money(1400));
        }
    }
}