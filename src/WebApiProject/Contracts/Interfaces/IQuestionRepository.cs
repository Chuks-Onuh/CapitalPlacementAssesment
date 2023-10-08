using ConsoleProject.Dtos.RetrievalModels;
using ConsoleProject.Dtos.ServiceRequestModels.Question;

namespace WebApiProject.Contracts.Interfaces
{
    public interface IQuestionRepository
    {
        Task<BaseResponse<bool>> CreateQuestionAsync(BaseQuestionRequestModel questionRequestModel);
        Task<BaseResponse<bool>> UpdateQuestionAsync(UpdateQuestionModel questionUpdateModel, string Id);
        Task<BaseResponse<IEnumerable<QuestionResponseModel>>> GetQuestionsAsync();
        Task<BaseResponse<QuestionResponseModel>> GetQuestionAsync(string Id);
        Task<BaseResponse<bool>> DeleteQuestionAsync(string Id);
    }
}
