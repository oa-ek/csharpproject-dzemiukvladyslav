using BCS.Core.Context;
using BCS.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCS.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection
        services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options =>
            {
                options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped<UserManager<AppUser>>();

            return services;
        }
    }
}
