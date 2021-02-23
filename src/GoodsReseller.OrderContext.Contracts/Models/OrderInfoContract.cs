namespace GoodsReseller.OrderContext.Contracts.Models
{
    public class OrderInfoContract
    {
        public AddressContract? Address { get; set; }
        
        public CustomerInfoContract? CustomerInfo { get; set; }
    }
}