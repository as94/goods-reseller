using GoodsReseller.AuthContext.Domain.Users.ValueObjects;

namespace GoodsReseller.AuthContext.Contracts.Users.Login
{
    public class LoginUserResponse
    {
        public Role Role { get; set; }
    }
}