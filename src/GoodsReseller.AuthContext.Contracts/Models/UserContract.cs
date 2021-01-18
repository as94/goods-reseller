using System;

namespace GoodsReseller.AuthContext.Contracts.Models
{
    public class UserContract
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}