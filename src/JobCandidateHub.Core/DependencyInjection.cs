using JobCandidateHub.Core.Application.Interfaces;
using JobCandidateHub.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace JobCandidateHub.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMemoryCache();
            services.AddScoped<ICandidateService, CandidateService>();
            return services;
        }
    }
}
