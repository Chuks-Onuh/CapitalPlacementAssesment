using ConsoleProject.Dtos.RetrievalModels;
using ConsoleProject.Dtos.ServiceRequestModels.Question;
using ConsoleProject.Enums;
using ConsoleProject.Models.Questions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Context;
using WebApiProject.Contracts.Interfaces;

namespace WebApiProject.Contracts.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationContext _context;

        public QuestionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> CreateQuestionAsync(BaseQuestionRequestModel questionRequestModel)
        {
            try
            {
                var question = new Question
                {
                    QuestionContent = questionRequestModel.QuestionContent
                };

                switch (questionRequestModel.QuestionType)
                {
                    case QuestionType.YesOrNo:
                        question.YesOrNoQuestion = new YesOrNoQuestion
                        {
                            Choice = questionRequestModel.YesOrNoQuestionModel.Choice,
                            DisqualifyForNoChoice = questionRequestModel.YesOrNoQuestionModel.DisqualifyForNoChoice
                        };
                        break;

                    case QuestionType.Dropdown:
                        question.DropdownQuestion = new DropdownQuestion
                        {
                            Choices = questionRequestModel.DropdownQuestionModel.Choices,
                            EnableOtherOption = questionRequestModel.DropdownQuestionModel.EnableOtherOption
                        };
                        break;

                    case QuestionType.MultipleChoice:
                        question.MultipleChoiceQuestion = new MultipleChoiceQuestion
                        {
                            Options = questionRequestModel.MultipleChoiceQuestionModel.Options,
                            EnableOtherOption = questionRequestModel.MultipleChoiceQuestionModel.EnableOtherOption,
                            MaximumChoicesAllowed = questionRequestModel.MultipleChoiceQuestionModel.MaximumNumberOfChoicesAllowed
                        };
                        break;

                    case QuestionType.VideoQuestion:
                        question.VideoBasedQuestion = new VideoBasedQuestion
                        {
                            AdditionalSubmissionInformation = questionRequestModel.VideoQuestionModel.AdditionalSubmissionInformation,
                            MaxDurationOfVideo = questionRequestModel.VideoQuestionModel.MaxDurationOfVideo
                        };
                        break;

                    case QuestionType.FileUpload:
                        question.FileUploadQuestion = new FileUploadQuestion
                        {
                            FilePath = questionRequestModel.FileUploadQuestionModel.FilePath
                        };
                        break;

                    case QuestionType.Number:
                        question.NumberQuestion = new NumberQuestion
                        {
                            QuestionNumber = questionRequestModel.NumberQuestionModel.NumberQuestion
                        };
                        break;

                    case QuestionType.Date:
                        question.DateQuestion = new DateQuestion
                        {
                            DateQuestionToAsk = questionRequestModel.DateQuestionModel.DateQuestion
                        };
                        break;

                    default:
                        // Default case for a general question
                        break;
                }

                await _context.Questions.AddAsync(question);
                await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Status = true,
                    Message = "Question added successfully.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Status = false,
                    Message = "Something happened, please try again."
                };
            }

        }


        public async Task<BaseResponse<bool>> DeleteQuestionAsync(string Id)
        {
            try
            {
                var question = await _context.Questions.FirstOrDefaultAsync(p => p.Id == Id);

                if (question == null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"Application question with Id {Id} does  not exist",
                        Status = false,
                        Data = false
                    };
                }

                _context.Remove(question);
                await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Message = "Application question deleted successfully",
                    Status = true,
                    Data = true
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<bool>
                {
                    Message = "An error occurred while removing application",
                    Status = false
                }; ;
            }
        }

        public async Task<BaseResponse<QuestionResponseModel>> GetQuestionAsync(string Id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == Id);

            if (question == null)
            {
                return new BaseResponse<QuestionResponseModel>
                {
                    Status = false,
                    Message = "Application does not exist",
                };
            }
            // Perform the mapping using Mapster's Adapt method
            var applicationModel = question.Adapt<QuestionResponseModel>();

            return new BaseResponse<QuestionResponseModel>
            {
                Status = true,
                Message = "Program retrieved successfully",
                Data = applicationModel
            };
        }

        public async Task<BaseResponse<IEnumerable<QuestionResponseModel>>> GetQuestionsAsync()
        {
            try
            {
                var questions = await _context.Questions.ToListAsync();

                if (!questions.Any())
                {
                    return new BaseResponse<IEnumerable<QuestionResponseModel>>
                    {
                        Message = "No application question at moment",
                        Data = questions.Select(pg => pg.Adapt<QuestionResponseModel>()).ToList(),
                        Status = false
                    };
                }

                return new BaseResponse<IEnumerable<QuestionResponseModel>>
                {
                    Message = "Application questions retrieved successfully",
                    Data = questions.Select(pg => pg.Adapt<QuestionResponseModel>()).ToList(),
                    Status = true
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<IEnumerable<QuestionResponseModel>>
                {
                    Message = "An error occurred while retrieving application questions",
                    Status = false
                };
            }
        }

        public async Task<BaseResponse<bool>> UpdateQuestionAsync(UpdateQuestionModel questionUpdateModel, string Id)
        {
            try
            {
                var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == Id);

                if (question == null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = "Application question does not exist",
                        Status = false,
                        Data = false
                    };
                }

                question.Response = questionUpdateModel.Response;
                question.QuestionContent = questionUpdateModel.QuestionContent;

                _context.Update(question);
                await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Message = "Application question updated successfully",
                    Status = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<bool>
                {
                    Message = "An error occurred while updating application question",
                    Status = false
                }; ;
            }
        }
    }
}
