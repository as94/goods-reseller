using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.AuthContext.Contracts.Users.Login;
using GoodsReseller.AuthContext.Domain.Users;
using MediatR;

namespace GoodsReseller.AuthContext.Handlers.Users
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly IUsersRepository _usersRepository;

        public LoginUserHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        
        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _usersRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (existingUser == null)
            {
                throw new AuthenticationException();
            }
            
            existingUser.Authenticate(request.Password);
            
            return new LoginUserResponse
            {
                UserId = existingUser.Id,
                Role = existingUser.Role
            };
        }
    }
}