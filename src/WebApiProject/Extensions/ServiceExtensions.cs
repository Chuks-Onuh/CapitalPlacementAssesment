using CapitalPlacementAssesementTaskApi.Contracts.Interfaces;
using CapitalPlacementAssesementTaskApi.Contracts.Repositories;
using WebApiProject.Contracts.Interfaces;
using WebApiProject.Contracts.Repositories;

namespace CapitalPlacementAssesementTaskApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProgramDetailsRepository, ProgramRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IWorkFlowRepository, WorkFlowRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IPreviewRepository, PreviewRepository>();
        }
    }
}
