using BCS.Repositories.Types;
using Microsoft.Extensions.DependencyInjection;

namespace BCS.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITypeRepository, TypeRepository>();
            return services;
        }
    }
}
