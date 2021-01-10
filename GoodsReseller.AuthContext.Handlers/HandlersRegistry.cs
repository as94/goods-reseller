using GoodsReseller.AuthContext.Handlers.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GoodsReseller.AuthContext.Handlers
{
    public static class HandlersRegistry
    {
        public static void RegisterAuthContextHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(HandlersRegistry).Assembly, typeof(RegisterUserHandler).Assembly);
        }
    }
}