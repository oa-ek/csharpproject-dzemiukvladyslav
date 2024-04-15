﻿using BCS.Repositories.Cityes;
using BCS.Repositories.Complaints;
using BCS.Repositories.Statuses;
using BCS.Repositories.Streets;
using BCS.Repositories.Suggestions;
using BCS.Repositories.Types;
using Microsoft.Extensions.DependencyInjection;

namespace BCS.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITypeRepository, TypeRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IStreetRepository, StreetRepository>();
            services.AddScoped<IComplaintRepository, ComplaintRepository>();
            services.AddScoped<ISuggestionRepository, SuggestionRepository>();
            return services;
        }
    }
}
