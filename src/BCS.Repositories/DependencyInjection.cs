using BCS.Core.Entities;
using BCS.Repositories.Cityes;
using BCS.Repositories.ComplaintCommentses;
using BCS.Repositories.Complaints;
using BCS.Repositories.Statuses;
using BCS.Repositories.Streets;
using BCS.Repositories.Structures;
using BCS.Repositories.SuggestionCommentses;
using BCS.Repositories.Suggestions;
using BCS.Repositories.Types;
using BCS.Repositories.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BCS.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<RoleManager<IdentityRole<Guid>>>();
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IStreetRepository, StreetRepository>();
            services.AddScoped<IComplaintRepository, ComplaintRepository>();
            services.AddScoped<ISuggestionRepository, SuggestionRepository>();
            services.AddScoped<IStructureRepository, StructureRepository>();
            services.AddScoped<IStructureRepository, StructureRepository>();
            services.AddScoped<ISuggestionCommentsRepository, SuggestionCommentsRepository>();
            services.AddScoped<IComplaintCommentsRepository, ComplaintCommentsRepository>();
            return services;
        }
    }
}
