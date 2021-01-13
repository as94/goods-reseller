using System;
using System.Collections.Generic;
using GoodsReseller.SeedWork;

namespace GoodsReseller.OrderContext.Domain.Orders.ValueObjects
{
    public sealed class Address : ValueObject
    {
        private const string DefaultCountry = "Россия";

        public string Country { get; }
        public string City { get; }
        public string Street { get; }
        public string ZipCode { get; }
        
        public string HouseNumber { get; }
        public string ApartmentNumber { get; }
        public string EntranceNumber { get; set; }
        public string Floor { get; set; }
        public string Intercom { get; set; }

        public Address(
            string city,
            string street,
            string zipCode,
            string houseNumber = null,
            string apartmentNumber = null,
            string entranceNumber = null,
            string floor = null,
            string intercom = null)
        {
            if (city == null)
            {
                throw new ArgumentNullException(nameof(city));
            }

            if (street == null)
            {
                throw new ArgumentNullException(nameof(street));
            }

            if (zipCode == null)
            {
                throw new ArgumentNullException(nameof(zipCode));
            }

            Country = DefaultCountry;
            City = city;
            Street = street;
            ZipCode = zipCode;
            
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
            EntranceNumber = entranceNumber;
            Floor = floor;
            Intercom = intercom;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return City;
            yield return Street;
            yield return ZipCode;
            yield return HouseNumber;
            yield return ApartmentNumber;
            yield return EntranceNumber;
            yield return Floor;
            yield return Intercom;
        }
    }
}