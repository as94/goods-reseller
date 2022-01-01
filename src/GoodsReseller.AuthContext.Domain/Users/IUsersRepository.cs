using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.AuthContext.Domain.Users.Entities;

namespace GoodsReseller.AuthContext.Domain.Users
{
    public interface IUsersRepository
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task SaveAsync(User user, CancellationToken cancellationToken);
    }
}