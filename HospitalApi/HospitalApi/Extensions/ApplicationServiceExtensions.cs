using HospitalApi.Connection;
using HospitalApi.Interfaces;
using HospitalApi.Repositories;
using HospitalApi.Services;

namespace HospitalApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<HospitalConnection, HospitalConnection>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<UserRoleRepository, UserRoleRepository>();
            services.AddScoped<RoleRepository, RoleRepository>();
            services.AddScoped<HospitalRepository, HospitalRepository>();

            return services;
        }
    }
}
