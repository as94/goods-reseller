using System;
using GoodsReseller.AuthContext.Domain.Users.ValueObjects;
using GoodsReseller.AuthContext.Domain.ValidationRules;
using GoodsReseller.SeedWork;
using GoodsReseller.SeedWork.ValueObjects;

namespace GoodsReseller.AuthContext.Domain.Users.Entities
{
    public sealed class User : VersionedEntity, IAggregateRoot
    {
        public string Email { get; }
        public PasswordHash PasswordHash { get; }

        public Role Role { get; }

        private User(
            Guid id,
            int version)
            : base(id, version)
        {
        }

        public User(
            Guid id,
            int version,
            string email,
            PasswordHash passwordHash,
            string role)
                : base(id, version)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (passwordHash == null)
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            if (!EmailValidator.IsValid(email, out _))
            {
                // TODO: business rule, add translations
                throw new ArgumentException($"Email '{email}' is invalid");
            }

            if (!Enumeration.TryParse<Role>(role, out var parsedRole))
            {
                // TODO: business rule, add translations
                throw new ArgumentException($"Role '{role}' is invalid");
            }

            Email = email;
            PasswordHash = passwordHash;
            Role = parsedRole;
        }

        public void Authenticate(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            
            PasswordHash.Authenticate(password);
        }
    }
}