using JobCandidateHub.Core.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JobCandidateHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ICsvStorageService, CsvStorageService>();
            return services;
        }
    }
}
