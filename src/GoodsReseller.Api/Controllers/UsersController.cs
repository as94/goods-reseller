using GoodsReseller.Api.Extensions;
using GoodsReseller.AuthContext.Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet("me")]
        public UserContract GetMyUserInfo()
        {
            return new UserContract
            {
                Id = User.GetUserId(),
                Email = User.GetUserEmail(),
                Role = User.GetUserRole()?.Name ?? string.Empty
            };
        }
    }
}