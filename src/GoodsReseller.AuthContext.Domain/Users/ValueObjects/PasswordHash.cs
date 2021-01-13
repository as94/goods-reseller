using System;
using System.Collections.Generic;
using System.Security.Authentication;
using GoodsReseller.SeedWork;

namespace GoodsReseller.AuthContext.Domain.Users.ValueObjects
{
    public sealed class PasswordHash : ValueObject
    {
        private readonly HashingManager _hashingManager = new HashingManager();
        
        public string Value { get; }

        public PasswordHash(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            Value = value;
        }

        public static PasswordHash Generate(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            
            return new PasswordHash(new HashingManager().HashToString(password));
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