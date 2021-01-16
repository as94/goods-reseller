using GoodsReseller.AuthContext.Domain.Users.ValueObjects;
using MediatR;

namespace GoodsReseller.AuthContext.Contracts.Users.Register
{
    public class RegisterUserRequest : IRequest<Unit>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}