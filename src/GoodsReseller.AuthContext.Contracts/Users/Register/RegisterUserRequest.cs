using MediatR;

namespace GoodsReseller.AuthContext.Contracts.Users.Register
{
    public class RegisterUserRequest : IRequest<RegisterUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}