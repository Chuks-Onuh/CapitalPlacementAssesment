using CapitalPlacementAssesementTaskApi.Contracts.Interfaces;
using ConsoleProject.Dtos.RetrievalModels;
using ConsoleProject.Dtos.RetrievalModels.Application;
using ConsoleProject.Dtos.ServiceRequestModels.Application;
using ConsoleProject.Dtos.ServiceRequestModels.Question;
using ConsoleProject.Models;
using ConsoleProject.Models.Stages;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Context;

namespace CapitalPlacementAssesementTaskApi.Contracts.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationContext _context;
        public ApplicationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> CreateApplicationAsync(CreateApplication applicationModel)
        {
            var applicationExists = await _context.Applications.FirstOrDefaultAsync(app => app.EmailAddress == applicationModel.EmailAddress);

            var applicationStage = await _context.Stages.FirstOrDefaultAsync(appliedStage => appliedStage.StageName == "Applied");

            var program = await _context.Programs.FirstOrDefaultAsync(p => p.ProgramTitle == applicationModel.ProgramTitle);

            var application = new Application
            {
                FirstName = applicationModel.FirstName,
                LastName = applicationModel.LastName,
                EmailAddress = applicationModel.EmailAddress,
                ApplicationCoverImage = applicationModel.ApplicationCoverImage,
                StageId = applicationStage?.Id,
                Nationality = applicationModel.Nationality,
                IdNumber = applicationModel.IdNumber,
                DateOfBirth = applicationModel.DateOfBirth,
                Gender = applicationModel.Gender,
                CurrentResidence = applicationModel.CurrentResidence,
                Profile = applicationModel.Profile,
                ProgramId = program?.Id,
            };

            try
            {
                _context.Applications.Add(application);
                var result = await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Status = true,
                    Message = "Application details added successfully.",
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

        public async Task<BaseResponse<bool>> DeleteApplicationAsync(string Id)
        {
            try
            {
                var application = await _context.Applications.FirstOrDefaultAsync(p => p.Id == Id);

                if (application == null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = $"Application with Id {Id} does  not exist",
                        Status = false,
                        Data = false
                    };
                }

                _context.Remove(application);
                await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Message = "Application deleted successfully",
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

        public async Task<BaseResponse<ApplicationModel>> GetApplicationAsync(string Id)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(x => x.Id == Id);

            if (application == null)
            {
                return new BaseResponse<ApplicationModel>
                {
                    Status = false,
                    Message = "Application does not exist",
                };
            }
            // Perform the mapping using Mapster's Adapt method
            var applicationModel = application.Adapt<ApplicationModel>();

            return new BaseResponse<ApplicationModel>
            {
                Status = true,
                Message = "Program retrieved successfully",
                Data = applicationModel
            };
        }

        public async Task<BaseResponse<IEnumerable<ApplicationModel>>> GetApplicationsAsync()
        {
            try
            {
                var applications = await _context.Applications.ToListAsync();

                if (!applications.Any())
                {
                    return new BaseResponse<IEnumerable<ApplicationModel>>
                    {
                        Message = "No application at moment",
                        Data = applications.Select(pg => pg.Adapt<ApplicationModel>()).ToList(),
                        Status = false
                    };
                }

                return new BaseResponse<IEnumerable<ApplicationModel>>
                {
                    Message = "Applications retrieved successfully",
                    Data = applications.Select(pg => pg.Adapt<ApplicationModel>()).ToList(),
                    Status = true
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<IEnumerable<ApplicationModel>>
                {
                    Message = "An error occurred while retrieving applications",
                    Status = false
                };
            }
        }      

        public async Task<BaseResponse<bool>> UpdateApplicationAsync(UpdateApplication applicationModel, string Id)
        {
            try
            {
                var application = await _context.Applications.FirstOrDefaultAsync(x => x.Id == Id);

                if (application == null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = "Application entry does not exist",
                        Status = false,
                        Data = false
                    };
                }

                application.FirstName = applicationModel.FirstName;
                application.LastName = applicationModel.LastName;
                application.PhoneNumber = applicationModel.PhoneNumber;
                application.Nationality = applicationModel.Nationality;

                _context.Update(application);
                await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Message = "Application entry updated successfully",
                    Status = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<bool>
                {
                    Message = "An error occurred while updating application entry",
                    Status = false
                }; ;
            }
        }
    }
}
