using Microsoft.Extensions.DependencyInjection;
using ProiectDAW.Repositories;
using ProiectDAW.Services;
using ProiectDAW.Utilities.JWT;

namespace ProiectDAW.Utilities
{
    public static class ServiceExtension
    {
        public static IServiceCollection  AddService(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddUtils(this IServiceCollection services)
        {
            services.AddTransient<IJWTUtils, JWTUtils>();
            return services;
        }
    }
}
