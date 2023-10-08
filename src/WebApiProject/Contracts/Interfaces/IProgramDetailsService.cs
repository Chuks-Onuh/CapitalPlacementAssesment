using ConsoleProject.Dtos.RetrievalModels;
using ConsoleProject.Dtos.RetrievalModels.Program;
using ConsoleProject.Dtos.ServiceRequestModels.Program;

namespace CapitalPlacementAssesementTaskApi.Contracts.Interfaces
{
    public interface IProgramDetailsService
    {
        Task<BaseResponse<bool>> AddProgramAsync(CreateProgram programModel);
        Task<BaseResponse<bool>> EditProgramAsync(UpdateProgram programModel, string programTitle);
        Task<BaseResponse<IEnumerable<ProgramModel>>> GetAllProgramsAsync();
        Task<BaseResponse<ProgramModel>> GetProgramAsync(string Id);
        Task<BaseResponse<ProgramModel>> GetProgramByTitleAsync(string programTitle);
        Task<BaseResponse<bool>> DeleteProgramAsync(string Id);
    }
}
