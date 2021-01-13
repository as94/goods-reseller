using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GoodsReseller.OrderContext.Domain.ValidationRules;
using GoodsReseller.SeedWork;

namespace GoodsReseller.OrderContext.Domain.Orders.ValueObjects
{
    public sealed class CustomerInfo : ValueObject
    {
        public CustomerInfo(string phoneNumber, string name = null)
        {
            if (phoneNumber == null)
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }

            if (!PhoneNumberValidator.IsValid(phoneNumber))
            {
                // TODO: business rule, add translations
                throw new ArgumentException($"Phone number '{phoneNumber}' is invalid");
            }
            
            PhoneNumber = phoneNumber;
            Name = name;
        }

        public string PhoneNumber { get; }
        public string Name { get; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PhoneNumber;
            yield return Name;
        }
    }
}