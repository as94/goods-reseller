using GoodsReseller.SeedWork.ValueObjects;
using GoodsReseller.SupplyContext.Contracts.Models;
using GoodsReseller.SupplyContext.Domain.Supplies.ValueObjects;

namespace GoodsReseller.SupplyContext.Handlers.Converters
{
    internal static class SupplierInfoConverters
    {
        public static SupplierInfoContract ToContract(this SupplierInfo supplierInfo)
        {
            return new SupplierInfoContract
            {
                Name = supplierInfo.Name
            };
        }
    }
}