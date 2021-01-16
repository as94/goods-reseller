using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GoodsReseller.AuthContext.Domain.ValidationRules;
using MediatR;

namespace GoodsReseller.AuthContext.Contracts.Users.Login
{
    public class LoginUserRequest : IRequest<LoginUserResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}