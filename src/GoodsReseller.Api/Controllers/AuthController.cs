using System;
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
            [FromBody] [Required] RegisterUserContract registerUser,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new RegisterUserRequest
            {
                Email = registerUser.Email,
                Password = registerUser.Password,
                Role = Role.Admin
            }, cancellationToken);
            
            await AuthenticateAsync(response.UserId, registerUser.Email, Role.Admin);
        }
        
        [HttpPost("register")]
        public async Task RegisterUserAsync(
            [FromBody] [Required] RegisterUserContract registerUser,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new RegisterUserRequest
            {
                Email = registerUser.Email,
                Password = registerUser.Password,
                Role = Role.Customer
            }, cancellationToken);
            
            await AuthenticateAsync(response.UserId, registerUser.Email, Role.Customer);
        }

        [HttpPost("login")]
        public async Task LoginAsync(
            [FromBody] [Required] LoginUserContract loginUser,
            CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new LoginUserRequest
            {
                Email = loginUser.Email,
                Password = loginUser.Password
            }, cancellationToken);
            
            await AuthenticateAsync(response.UserId, loginUser.Email, response.Role);
        }
        
        [Authorize]
        [HttpPost("logout")]
        public async Task LogoutAsync()
        {
            var a = User.Claims;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private async Task AuthenticateAsync(Guid id, string email, Role role)
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(AuthContext.Domain.Users.Entities.User.Id), id.ToString()),
                new Claim(nameof(AuthContext.Domain.Users.Entities.User.Email), email),
                new Claim(nameof(AuthContext.Domain.Users.Entities.User.Role), role.Name),
            };
            
            var identity = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                nameof(AuthContext.Domain.Users.Entities.User.Id),
                nameof(AuthContext.Domain.Users.Entities.User.Role));

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                });
        }
    }
}