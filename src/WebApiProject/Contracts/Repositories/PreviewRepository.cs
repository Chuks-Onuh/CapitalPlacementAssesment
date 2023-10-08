using CapitalPlacementAssesementTaskApi.Contracts.Repositories;
using ConsoleProject.Dtos.RetrievalModels;
using ConsoleProject.Dtos.RetrievalModels.Program;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Context;
using WebApiProject.Contracts.Interfaces;

namespace WebApiProject.Contracts.Repositories
{
    public class PreviewRepository : IPreviewRepository
    {
        private readonly ApplicationContext _context;
        public PreviewRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PreviewServiceModel>> GetProgramPreviewAsync(string programTitle)
        {
            try
            {
                // Retrieve program from the repository based on the program title
                var program = await _context.Programs.FirstOrDefaultAsync(p => p.ProgramTitle.ToLower() == programTitle.ToLower());

                // Check if the program is found, if not, return an error response
                if (program == null)
                {
                    return new BaseResponse<PreviewServiceModel>
                    {
                        Status = false,
                        Message = $"Program with the title '{programTitle}' could not be found",
                    };
                }

                // Retrieve application related to the program
                //var application = await _appRepository.GetAsync(a => a.ProgramId == program.Id);
                var application = await _context.Applications.FirstOrDefaultAsync(a => a.ProgramId == program.Id);

                // Create a preview model containing program and application details
                var previewModel = new PreviewServiceModel
                {
                    ProgramModel = program.Adapt<ProgramModel>(),
                    ApplicationCoverImage = application.ApplicationCoverImage,
                };

                // Return a success response with the preview model
                return new BaseResponse<PreviewServiceModel>
                {
                    Data = previewModel,
                    Message = "Successful retrieval of program",
                    Status = true,
                };
            }
            catch (Exception ex)
            {
                // Handle the exception (log, rethrow, or return an error response)
                throw;
            }

        }
    }
}
