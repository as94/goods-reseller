using System.Linq;
using GoodsReseller.SupplyContext.Contracts.Models;
using GoodsReseller.SupplyContext.Domain.Supplies.Entities;

namespace GoodsReseller.SupplyContext.Handlers.Converters
{
    public static class SupplyConverters
    {
        public static SupplyContract ToContract(this Supply supply)
        {
            return new SupplyContract
            {
                Id = supply.Id,
                Date = supply.LastUpdateDate != null
                    ? supply.LastUpdateDate.Date 
                    : supply.CreationDate.Date,
                SupplierInfo = supply.SupplierInfo.ToContract(),
                SupplyItems = supply.SupplyItems.Select(x => x.ToContract()).ToArray(),
                TotalCost = supply.TotalCost.ToContract()
            };
        }
        
        public static SupplyListItemContract ToListItemContract(this Supply supply)
        {
            return new SupplyListItemContract
            {
                Id = supply.Id,
                Date = supply.LastUpdateDate != null
                    ? supply.LastUpdateDate.Date 
                    : supply.CreationDate.Date,
                SupplierName = supply.SupplierInfo.Name,
                TotalCost = supply.TotalCost.Value
            };
        }
    }
}