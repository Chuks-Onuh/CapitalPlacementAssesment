using ConsoleProject.Dtos.RetrievalModels;
using ConsoleProject.Dtos.RetrievalModels.Application;
using ConsoleProject.Dtos.ServiceRequestModels.Application;

namespace CapitalPlacementAssesementTaskApi.Contracts.Interfaces
{
    public interface IApplicationRepository
    {
        Task<BaseResponse<bool>> CreateApplicationAsync(CreateApplication applicationModel);

        Task<BaseResponse<bool>> UpdateApplicationAsync(UpdateApplication applicationModel, string Id);

        Task<BaseResponse<IEnumerable<ApplicationModel>>> GetApplicationsAsync();

        Task<BaseResponse<ApplicationModel>> GetApplicationAsync(string Id);

        Task<BaseResponse<bool>> DeleteApplicationAsync(string Id);
    }
}