using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;

namespace GoodsReseller.OrderContext.Handlers.Converters
{
    internal static class CustomerInfoConverters
    {
        public static CustomerInfoContract ToContract(this CustomerInfo customerInfo)
        {
            return new CustomerInfoContract
            {
                PhoneNumber = customerInfo.PhoneNumber,
                Name = customerInfo.Name
            };
        }
        
        public static CustomerInfo ToDomain(this CustomerInfoContract contract)
        {
            return new CustomerInfo(contract.PhoneNumber, contract.Name);
        }
    }
}