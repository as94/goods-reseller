using System;
using System.Text.RegularExpressions;

namespace GoodsReseller.OrderContext.Domain.Orders.ValidationRules
{
    public static class PhoneNumberValidator
    {
        public static bool IsValid(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                throw new ArgumentNullException(nameof(phoneNumber));
            }
            
            return Regex.Match(phoneNumber, @"^(?:\+\d{1,3}|0\d{1,3}|00\d{1,2})?(?:\s?\(\d+\))?(?:[-\/\s.]|\d)+$").Success;
        }
    }
}