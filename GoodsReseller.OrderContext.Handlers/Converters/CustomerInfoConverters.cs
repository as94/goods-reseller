using System;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;

namespace GoodsReseller.OrderContext.Handlers.Converters
{
    public static class CustomerInfoConverters
    {
        public static CustomerInfoContract ToContract(this CustomerInfo customerInfo)
        {
            if (customerInfo == null)
            {
                throw new ArgumentNullException(nameof(customerInfo));
            }
            
            return new CustomerInfoContract
            {
                PhoneNumber = customerInfo.PhoneNumber,
                Name = customerInfo.Name
            };
        }
        
        public static CustomerInfo ToDomain(this CustomerInfoContract contract)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }
            
            return new CustomerInfo(contract.PhoneNumber, contract.Name);
        }
    }
}