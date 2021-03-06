using System;

namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderListItemContract
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Status { get; set; }

        public DateTime Date { get; set; }
        
        public string CustomerPhoneNumber { get; set; }

        public string CustomerName { get; set; }
        
        public string AddressCity { get; set; }
        
        public string AddressStreet { get; set; }
        
        public string AddressZipCode { get; set; }

        public decimal TotalCost { get; set; }
    }
}