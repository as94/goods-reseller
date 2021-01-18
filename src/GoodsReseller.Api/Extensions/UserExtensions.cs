using System;
using System.Linq;
using System.Security.Claims;
using GoodsReseller.AuthContext.Domain.Users.Entities;
using GoodsReseller.AuthContext.Domain.Users.ValueObjects;
using GoodsReseller.SeedWork;

namespace GoodsReseller.Api.Extensions
{
    public static class UserExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal == null)
            {
                throw new ArgumentNullException(nameof(userPrincipal));
            }

            return Guid.Parse(userPrincipal.Claims.First(x => x.Type == nameof(User.Id)).Value);
        }
        
        public static string GetUserEmail(this ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal == null)
            {
                throw new ArgumentNullException(nameof(userPrincipal));
            }

            return userPrincipal.Claims.First(x => x.Type == nameof(User.Email)).Value;
        }
        
        public static Role? GetUserRole(this ClaimsPrincipal userPrincipal)
        {
            if (userPrincipal == null)
            {
                throw new ArgumentNullException(nameof(userPrincipal));
            }

            var isParsed = Enumeration.TryParse<Role>(
                userPrincipal.Claims.First(x => x.Type == nameof(User.Role)).Value,
                out var role);
            
            return isParsed ? role : null;
        }
    }
}