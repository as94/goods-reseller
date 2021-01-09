using System;
using GoodsReseller.OrderContext.Contracts.Models;
using GoodsReseller.OrderContext.Domain.Orders.ValueObjects;

namespace GoodsReseller.OrderContext.Handlers.Converters
{
    public static class AddressConverters
    {
        public static AddressContract ToContract(this Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            return new AddressContract
            {
                City = address.City,
                Street = address.Street,
                ZipCode = address.ZipCode,
                HouseNumber = address.HouseNumber,
                ApartmentNumber = address.ApartmentNumber,
                EntranceNumber = address.EntranceNumber,
                Floor = address.Floor,
                Intercom = address.Intercom
            };
        }

        public static Address ToDomain(this AddressContract contract)
        {
            if (contract == null)
            {
                throw new ArgumentNullException(nameof(contract));
            }

            return new Address(
                contract.City,
                contract.Street,
                contract.ZipCode,
                contract.HouseNumber,
                contract.ApartmentNumber,
                contract.EntranceNumber,
                contract.Floor,
                contract.Intercom
            );
        }
    }
}