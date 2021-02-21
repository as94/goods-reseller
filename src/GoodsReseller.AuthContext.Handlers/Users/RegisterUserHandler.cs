using System;
using System.Threading;
using System.Threading.Tasks;
using GoodsReseller.AuthContext.Contracts.Users.Register;
using GoodsReseller.AuthContext.Domain.Users;
using GoodsReseller.AuthContext.Domain.Users.Entities;
using GoodsReseller.AuthContext.Domain.Users.ValueObjects;
using MediatR;

namespace GoodsReseller.AuthContext.Handlers.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IUsersRepository _usersRepository;

        public RegisterUserHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        
        public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _usersRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"User with Email = {request.Email} has already been existed");
            }
            
            var userId = Guid.NewGuid();
            var version = 1;
            var passwordHash = PasswordHash.Generate(request.Password);
            
            var user = new User(
                userId,
                version,
                request.Email,
                passwordHash,
                request.Role);

            await _usersRepository.SaveUserAsync(user, cancellationToken);
            
            return new RegisterUserResponse
            {
                UserId = userId
            };
        }
    }
}