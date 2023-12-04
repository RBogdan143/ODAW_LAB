using Backend.Helpers.Seeders;
using Backend.Repositories.UserRepository;
using Backend.Services.UserService;

namespace Backend.Helpers.Extensions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddSeeders (this IServiceCollection services)
        {
            services.AddTransient<UsersSeeder>();

            return services;
        }
    }
}
