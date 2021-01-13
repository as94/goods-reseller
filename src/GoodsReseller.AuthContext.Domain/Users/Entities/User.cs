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

        public DateValueObject CreationDate { get; }
        public DateValueObject? LastUpdateDate { get; private set; }
        public bool IsRemoved { get; private set; }

        public User(
            Guid id,
            int version,
            string email,
            PasswordHash passwordHash,
            Role role,
            DateValueObject creationDate) : base(id,
            version)
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

            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreationDate = creationDate;
            IsRemoved = false;
        }

        public static User Restore(
            Guid id,
            int version,
            string email,
            PasswordHash passwordHash,
            Role role,
            DateValueObject creationDate,
            DateValueObject? lastUpdateDate,
            bool isRemoved)
        {
            return new User(
                id,
                version,
                email,
                passwordHash,
                role,
                creationDate)
            {
                LastUpdateDate = lastUpdateDate,
                IsRemoved = isRemoved
            };
        }

        public void Authenticate(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            
            PasswordHash.Authenticate(password);
        }
        
        public void Remove(DateValueObject lastUpdateDate)
        {
            if (IsRemoved)
            {
                return;
            }
            
            if (lastUpdateDate == null)
            {
                throw new ArgumentNullException(nameof(lastUpdateDate));
            }
            
            IsRemoved = true;
            
            IncrementVersion();
            LastUpdateDate = lastUpdateDate;
        }
    }
}