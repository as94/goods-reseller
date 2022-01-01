using System.ComponentModel.DataAnnotations;
using GoodsReseller.OrderContext.Contracts.Models;
using MediatR;

namespace GoodsReseller.OrderContext.Contracts.Orders.Create
{
    public class CreateOrderRequest : IRequest
    {
        public OrderInfoContract OrderInfo { get; set; }
    }
}