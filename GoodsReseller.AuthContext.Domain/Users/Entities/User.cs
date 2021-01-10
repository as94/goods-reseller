using System;
using System.Net.Mail;
using GoodsReseller.AuthContext.Domain.Users.ValueObjects;
using GoodsReseller.AuthContext.Domain.ValidationRules;
using GoodsReseller.SeedWork;

namespace GoodsReseller.AuthContext.Domain.Users.Entities
{
    public sealed class User : VersionedEntity, IAggregateRoot
    {
        public MailAddress EmailAddress { get; }
        public PasswordHash PasswordHash { get; }
        
        
        public User(Guid id, int version, string email, PasswordHash passwordHash) : base(id, version)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (passwordHash == null)
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }

            if (!EmailValidator.IsValid(email, out var emailAddress))
            {
                // TODO: business rule, add translations
                throw new ArgumentException($"Email '{email}' is invalid");
            }

            EmailAddress = emailAddress;
            PasswordHash = passwordHash;
        }
    }
}