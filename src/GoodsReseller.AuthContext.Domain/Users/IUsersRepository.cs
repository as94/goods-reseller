using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.AuthContext.Domain.Users.Entities;

namespace GoodsReseller.AuthContext.Domain.Users
{
    public interface IUsersRepository
    {
        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task SaveUserAsync(User user, CancellationToken cancellationToken);
    }
}