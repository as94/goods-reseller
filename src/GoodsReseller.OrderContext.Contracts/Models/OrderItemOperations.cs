namespace GoodsReseller.OrderContext.Contracts.Models
{
    public static class OrderItemOperations
    {
        public const string Add = "add";
        public const string Remove = "remove";
        
        public static readonly string[] AllOperations =
        {
            Add,
            Remove
        };
    }
}