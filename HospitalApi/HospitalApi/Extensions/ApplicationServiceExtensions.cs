using HospitalApi.Interfaces;
using HospitalApi.Services;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<HospitalDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(Constants.DbConnectionKey));
            });

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
