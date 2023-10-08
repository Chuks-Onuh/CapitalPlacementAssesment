using ConsoleProject.Dtos.RetrievalModels;
using ConsoleProject.Dtos.RetrievalModels.Stages;
using ConsoleProject.Dtos.ServiceRequestModels.Stage;

namespace WebApiProject.Contracts.Interfaces
{
    public interface IWorkFlowRepository
    {
        Task<BaseResponse<bool>> CreateStageAsync(CreateStage stageRequestModel);
        Task<BaseResponse<bool>> UpdateUsualStageAsync(UpdateUsualStage stageUpdateRequestModel, string stageName);
        Task<BaseResponse<bool>> UpdateVideoInterviewStageAsync(UpdateVideoInterviewStage stageUpdateRequestModel, string stageName);
        Task<BaseResponse<IEnumerable<StageModel>>> GetStagesAsync();
        Task<BaseResponse<StageModel>> GetStageAsync(string Id);
        Task<BaseResponse<bool>> DeleteStageAsync(string Id);
    }
}
