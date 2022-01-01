using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.AuthContext.Domain.Users;
using GoodsReseller.AuthContext.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodsReseller.Infrastructure.AuthContext
{
    internal sealed class UsersRepository : IUsersRepository
    {
        private readonly GoodsResellerDbContext _dbContext;

        public UsersRepository(GoodsResellerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            return await _dbContext.Users.FirstOrDefaultAsync(
                x => x.Email == email && !x.IsRemoved,
                cancellationToken);
        }

        public async Task SaveAsync(User user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            var existing = await _dbContext.Users.FirstOrDefaultAsync(
                x => x.Id == user.Id,
                cancellationToken);

            if (existing == null)
            {
                await _dbContext.Users.AddAsync(user, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}