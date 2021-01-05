using System.Collections.Generic;
using GoodsReseller.Orders.Domain.SeedWork;

namespace GoodsReseller.OrderContext.Domain.Orders.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string Country { get; }
        public string City { get; }
        public string Street { get; }
        public string ZipCode { get; }

        public Address()
        {
        }

        public Address(string country, string city, string street, string zipCode)
        {
            Country = country;
            City = city;
            Street = street;
            ZipCode = zipCode;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return City;
            yield return Street;
            yield return ZipCode;
        }
    }
}