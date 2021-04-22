using System;
using System.ComponentModel.DataAnnotations;
using GoodsReseller.OrderContext.Contracts.Models;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.Orders.Update
{
    public class UpdateOrderRequest : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
        public OrderInfoContract OrderInfo { get; set; }
    }
}