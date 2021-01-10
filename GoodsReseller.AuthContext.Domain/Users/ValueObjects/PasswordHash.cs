using System;
using System.Collections.Generic;
using System.Security.Authentication;
using GoodsReseller.SeedWork;

namespace GoodsReseller.AuthContext.Domain.Users.ValueObjects
{
    public sealed class PasswordHash : ValueObject
    {
        private readonly HashingManager _hashingManager = new HashingManager();
        
        public string Value { get; private set; }

        private PasswordHash()
        {
        }

        public PasswordHash(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            Value = _hashingManager.HashToString(password);;
        }

        public PasswordHash Restore(string passwordHash)
        {
            if (passwordHash == null)
            {
                throw new ArgumentNullException(nameof(passwordHash));
            }
            
            return new PasswordHash
            {
                Value = passwordHash
            };
        }

        public void Authenticate(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            if (!_hashingManager.Verify(password, Value))
            {
                throw new AuthenticationException();
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}