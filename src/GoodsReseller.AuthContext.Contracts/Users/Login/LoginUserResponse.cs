using System;
using GoodsReseller.AuthContext.Domain.Users.ValueObjects;

namespace GoodsReseller.AuthContext.Contracts.Users.Login
{
    public class LoginUserResponse
    {
        public Guid UserId { get; set; }
        public Role Role { get; set; }
    }
}