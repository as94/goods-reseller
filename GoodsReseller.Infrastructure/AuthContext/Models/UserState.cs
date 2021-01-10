using System;
using GoodsReseller.AuthContext.Domain.Users.Entities;
using GoodsReseller.AuthContext.Domain.Users.ValueObjects;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.Infrastructure.AuthContext.Models
{
    internal sealed class UserState
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        
        public string Email { get; set; }
        public PasswordHash PasswordHash { get; set; }
        public Role Role { get; set; }
        
        public DateValueObject CreationDate { get; set; }
        public DateValueObject? LastUpdateDate { get; set; }
        public bool IsRemoved { get; set; }

        public User ToDomain()
        {
            return User.Restore(
                Id,
                Version,
                Email,
                PasswordHash,
                Role,
                CreationDate,
                LastUpdateDate,
                IsRemoved);
        }
    }
}