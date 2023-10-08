using CapitalPlacementAssesementTaskApi.Contracts.Interfaces;
using CapitalPlacementAssesementTaskApi.Contracts.Repositories;

namespace CapitalPlacementAssesementTaskApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProgramDetailsService, ProgramRepository>();

        }
    }
}
