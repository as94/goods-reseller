using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.AuthContext.Contracts.Models;
using GoodsReseller.AuthContext.Contracts.Users.Login;
using GoodsReseller.AuthContext.Contracts.Users.Register;
using GoodsReseller.AuthContext.Domain.Users.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodsReseller.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // TODO: uncomment after adding first admin
        // [Authorize(Roles = "Admin")]
        [HttpPost("registerAdmin")]
        public async Task RegisterAdminAsync(
            [FromBody] [Required] RegisterUser registerUser,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new RegisterUserRequest
            {
                Email = registerUser.Email,
                Password = registerUser.Password,
                Role = Role.Admin
            }, cancellationToken);
            
            await AuthenticateAsync(registerUser.Email, Role.Admin.Name);
        }
        
        [HttpPost("register")]
        public async Task RegisterUserAsync(
            [FromBody] [Required] RegisterUser registerUser,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new RegisterUserRequest
            {
                Email = registerUser.Email,
                Password = registerUser.Password,
                Role = Role.Customer
            }, cancellationToken);
            
            await AuthenticateAsync(registerUser.Email, Role.Customer.Name);
        }

        [HttpPost("login")]
        public async Task LoginAsync(
            [FromBody] [Required] LoginUserRequest loginUserRequest,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(loginUserRequest, cancellationToken);
            await AuthenticateAsync(loginUserRequest.Email, response.Role.Name);
        }
        
        [Authorize]
        [HttpPost("logout")]
        public async Task LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private async Task AuthenticateAsync(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            
            var identity = new ClaimsIdentity(
                claims, 
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));
        }
    }
}