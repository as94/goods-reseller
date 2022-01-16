using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using GoodsReseller.IntegrationTests.Infrastructure;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;
using Xunit;

namespace GoodsReseller.IntegrationTests.Orders
{
    public class CreateOrderTest
    {
        [Fact]
        public async Task WhenCreateOrder_ThenStatusCodeIsOk()
        {
            var client = new GoodsResellerClient();
            var order = new OrderInfoContract
            {
                Id = Guid.NewGuid(),
                Version = 1,
                Status = OrderStatus.Accepted.Name,
                Address = new AddressContract
                {
                    City = "Moscow",
                    Street = "Moscow Street",
                    ZipCode = "123321"
                },
                CustomerInfo = new CustomerInfoContract
                {
                    PhoneNumber = "+7 987 123 34 56"
                },
                DeliveryCost = 300,
                AddedCost = 1000,
                OrderItems = new []
                {
                    new OrderItemContract
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        UnitPrice = 100,
                        DiscountPerUnit = 0,
                        Quantity = 1
                    }
                }
            };

            var response = await client.PostAsync(Endpoints.PublicOrdersUrlPath, order);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}