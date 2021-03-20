namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderInfoContract
    {
        public string? Status { get; set; }
        public AddressContract? Address { get; set; }
        
        public CustomerInfoContract? CustomerInfo { get; set; }

        public MoneyContract? DeliveryCost { get; set; }
    }
}