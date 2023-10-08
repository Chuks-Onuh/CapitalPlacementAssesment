using ConsoleProject.Dtos.RetrievalModels;

namespace WebApiProject.Contracts.Interfaces
{
    public interface IPreviewRepository
    {
        Task<BaseResponse<PreviewServiceModel>> GetProgramPreviewAsync(string programTitle);
    }
}
