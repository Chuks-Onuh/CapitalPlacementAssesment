using ConsoleProject.Dtos.RetrievalModels;
using ConsoleProject.Dtos.RetrievalModels.Stages;
using ConsoleProject.Dtos.ServiceRequestModels.Stage;
using ConsoleProject.Enums;
using ConsoleProject.Models.Stages;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Context;
using WebApiProject.Contracts.Interfaces;

namespace WebApiProject.Contracts.Repositories
{
    public class WorkFlowRepository : IWorkFlowRepository
    {
        private readonly ApplicationContext _context;
        public WorkFlowRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> CreateStageAsync(CreateStage stageRequestModel)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                // Check if a stage with the same name and type already exists
                var existingStage = await _context.Stages.FirstOrDefaultAsync(stage => stage.StageName.ToLower() == stageRequestModel.StageName.ToLower());

                if (existingStage != null && existingStage.StageType != StageType.VideoInterview)
                {
                    // Return an error response
                    return new BaseResponse<bool>
                    {
                        Status = false,
                        Message = $"Stage with Stage Name: {stageRequestModel.StageName} already exists",
                    };
                }

                // Handle VideoInterviewStage questions
                if (stageRequestModel.StageType == StageType.VideoInterview)
                {
                    var videoInterviewQuestions = stageRequestModel.CreateVideoInterviewStage.VideoInterviewQuestions;

                    // Check the maximum limit
                    if (videoInterviewQuestions.Count > 3)
                    {
                        return new BaseResponse<bool>
                        {
                            Status = false,
                            Message = "VideoInterview Stage Video Interview Questions should not exceed 3",
                        };
                    }

                    // Add new questions to the existing stage
                    if (existingStage != null)
                    {
                        existingStage.VideoInterviewStage.VideoInterviewQuestions.AddRange(videoInterviewQuestions);
                        _context.Update(existingStage);
                        await _context.SaveChangesAsync();
                    }
                }

                // Create and add stages based on type
                Stage stageCreated;
                switch (stageRequestModel.StageType)
                {
                    case StageType.VideoInterview:
                        // Create a VideoInterviewStage
                        stageCreated = new Stage
                        {
                            StageName = stageRequestModel.StageName,
                            StageType = stageRequestModel.StageType,
                            VideoInterviewStage = new VideoInterviewStage
                            {
                                VideoInterviewQuestions = stageRequestModel.CreateVideoInterviewStage.VideoInterviewQuestions
                                    .Select(stageQuestion => new VideoInterviewQuestion
                                    {
                                        // Map VideoInterviewQuestion properties
                                        AdditionalSubmissionInformation = stageQuestion.AdditionalSubmissionInformation,
                                        DeadlineInNumberOfDays = stageQuestion.DeadlineInNumberOfDays,
                                        MaxDurationOfVideo = stageQuestion.MaxDurationOfVideo,
                                        VideoTextQuestion = stageQuestion.VideoTextQuestion
                                    }).ToList(),
                            },
                        };
                        break;
                    default:
                        // Create a usual stage
                        stageCreated = new Stage
                        {
                            StageName = stageRequestModel.StageName,
                            StageType = stageRequestModel.StageType,
                        };
                        break;
                }

                // Add the stage to the repository
                var saveResponse = await _context.Stages.AddAsync(stageCreated);
                await _context.SaveChangesAsync(); 

                if (saveResponse != null)
                {
                    baseResponse.Status = true;
                    baseResponse.Message = "Stage successfully added";
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (log, rethrow, or return an error response)
                baseResponse.Status = false;
                baseResponse.Message = "An error occurred while creating the stage";
            }
            finally
            {
                Console.WriteLine("Function End...");
            }

            return baseResponse;
        }


        public async Task<BaseResponse<bool>> DeleteStageAsync(string Id)
        {
            try
            {
                var stage = await _context.Stages.FirstOrDefaultAsync(p => p.Id == Id);

                if (stage == null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"Application stage with Id {Id} does  not exist",
                        Status = false,
                        Data = false
                    };
                }

                _context.Remove(stage);
                await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Message = "Application stage deleted successfully",
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

        public async Task<BaseResponse<StageModel>> GetStageAsync(string Id)
        {
            var stage = await _context.Stages.FirstOrDefaultAsync(x => x.Id == Id);

            if (stage == null)
            {
                return new BaseResponse<StageModel>
                {
                    Status = false,
                    Message = "Application stage does not exist",
                };
            }
            // Perform the mapping using Mapster's Adapt method
            var stageModel = stage.Adapt<StageModel>();

            return new BaseResponse<StageModel>
            {
                Status = true,
                Message = "Application stage retrieved successfully",
                Data = stageModel
            };
        }

        public async Task<BaseResponse<IEnumerable<StageModel>>> GetStagesAsync()
        {
            try
            {
                var stages = await _context.Stages.ToListAsync();

                if (!stages.Any())
                {
                    return new BaseResponse<IEnumerable<StageModel>>
                    {
                        Message = "No stage at moment",
                        Data = stages.Select(pg => pg.Adapt<StageModel>()).ToList(),
                        Status = false
                    };
                }

                return new BaseResponse<IEnumerable<StageModel>>
                {
                    Message = "Stages retrieved successfully",
                    Data = stages.Select(pg => pg.Adapt<StageModel>()).ToList(),
                    Status = true
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<IEnumerable<StageModel>>
                {
                    Message = "An error occurred while retrieving stages",
                    Status = false
                };
            }
        }

        public async Task<BaseResponse<bool>> UpdateUsualStageAsync(UpdateUsualStage stageUpdateRequestModel, string stageName)
        {
            try
            {
                var stage = await _context.Stages.FirstOrDefaultAsync(pg => pg.StageName.ToLower() == stageName.ToLower());

                if (stage == null)
                {
                    return new BaseResponse<bool>
                    {
                        Status = false,
                        Message = $"Stage with Stage Name: {stageUpdateRequestModel.StageName} does not exist",
                    };
                }

                stage.StageName = stageUpdateRequestModel.StageName;
                _context.Update(stage);
                await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Status = true,
                    Message = "Stage has been updated successfully!",
                };
            }
            catch (Exception ex)
            {
                // Handle the exception (log, rethrow, or return an error response)
                return new BaseResponse<bool>
                {
                    Status = false,
                    Message = "An error occurred while updating the stage",
                };
            }
        }


        public async Task<BaseResponse<bool>> UpdateVideoInterviewStageAsync(UpdateVideoInterviewStage stageUpdateRequest, string stageNameToUpdate)
        {
            try
            {
                var existingStage = await _context.Stages.FirstOrDefaultAsync(stage => stage.StageName.ToLower() == stageNameToUpdate.ToLower());

                if (existingStage == null)
                {
                    return new BaseResponse<bool>
                    {
                        Status = false,
                        Message = $"Stage with Stage Name: {stageUpdateRequest.StageName} does not exist",
                    };
                }

                // Update the stage name and VideoInterviewQuestions
                existingStage.StageName = stageUpdateRequest.StageName;

                if (existingStage.StageType == StageType.VideoInterview)
                {
                    var currentQuestionsCount = existingStage.VideoInterviewStage.VideoInterviewQuestions.Count;
                    var newQuestions = stageUpdateRequest.VideoInterviewQuestions;

                    if (currentQuestionsCount + newQuestions.Count > 3)
                    {
                        return new BaseResponse<bool>
                        {
                            Status = false,
                            Message = "VideoInterview Stage Video Interview Questions should not exceed 3",
                        };
                    }

                    existingStage.VideoInterviewStage.VideoInterviewQuestions.AddRange(newQuestions);
                }

                // Update the stage in the repository
                _context.Update(existingStage);
                await _context.SaveChangesAsync();

                
                // Return a success response
                return new BaseResponse<bool>
                {
                    Status = true,
                    Message = "Stage has been updated successfully!",
                };
            }
            catch (Exception ex)
            {
                // Handle the exception (log, rethrow, or return an error response)
                return new BaseResponse<bool>
                {
                    Status = false,
                    Message = "An error occurred while updating the stage",
                };
            }
        }

    }
}
