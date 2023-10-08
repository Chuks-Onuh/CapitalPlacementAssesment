using CapitalPlacementAssesementTaskApi.Contracts.Interfaces;
using ConsoleProject.Dtos.RetrievalModels;
using ConsoleProject.Dtos.RetrievalModels.Program;
using ConsoleProject.Dtos.ServiceRequestModels.Program;
using ConsoleProject.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Context;

namespace CapitalPlacementAssesementTaskApi.Contracts.Repositories
{
    public class ProgramRepository : IProgramDetailsRepository
    {
        private readonly ApplicationContext _context;
        public ProgramRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> AddProgramAsync(CreateProgram programModel)
        {
            var existingProgram = await _context.Programs.FirstOrDefaultAsync(p => p.ProgramTitle.ToLower() == programModel.ProgramTitle.ToLower());

            // If program exists, return an error response
            if (existingProgram != null) return new BaseResponse<bool>
            {
                Status = false,
                Message = $"Program With Program Title: {programModel.ProgramTitle} already exists",
            };

            var program = new ApplicationProgram
            {
                Description = programModel.Description,
                ApplicantQualification = programModel.ApplicantQualification,
                DurationInMonths = programModel.DurationInMonths,
                ApplicantSkills = programModel.ApplicantSkills,
                ApplicationCriteria = programModel.ApplicationCriteria,
                ApplicationEnds = programModel.ApplicationEnds,
                ApplicationStart = programModel.ApplicationStart,
                Benefits = programModel.Benefits,
                MaxApplications = programModel.MaxApplications,
                ProgramLocations = programModel.ProgramLocations,
                ProgramStart = programModel.ProgramStart,
                ProgramTitle = programModel.ProgramTitle,
                ProgramType = programModel.ProgramType,
            };

            try
            {
                _context.Programs.Add(program);
                var result = await _context.SaveChangesAsync();
                return new BaseResponse<bool>
                {
                    Status = true,
                    Message = "Program details added successfully."
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

        public async Task<BaseResponse<bool>> DeleteProgramAsync(string Id)
        {
            try
            {
                var program = await _context.Programs.FirstOrDefaultAsync(p => p.Id == Id);

                if (program == null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = "Program does  not exist",
                        Status = false,
                        Data = false
                    };
                }

                _context.Remove(program);
                await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Message = "Program deleted successfully",
                    Status = true,
                    Data = true
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<bool>
                {
                    Message = "An error occurred while removing program",
                    Status = false
                }; ;
            }
        }

        public async Task<BaseResponse<bool>> EditProgramAsync(UpdateProgram programModel, string programTitle)
        {
            try
            {
                var program = await _context.Programs.FirstOrDefaultAsync(x => x.ProgramTitle.ToLower() == programTitle.ToLower());

                if (program == null)
                {
                    return new BaseResponse<bool>
                    {
                        Message = "Program does  not exist",
                        Status = false,
                        Data = false
                    };
                }

                program.ApplicationCriteria = programModel.ApplicationCriteria;
                program.Benefits = programModel.Benefits;
                program.ProgramLocations = programModel.ProgramLocations;

                _context.Update(program);
                await _context.SaveChangesAsync();

                return new BaseResponse<bool>
                {
                    Message = "Program updated successfully",
                    Status = true,
                    Data = true
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse<bool>
                {
                    Message = "An error occurred while updating program",
                    Status = false
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<ProgramModel>>> GetAllProgramsAsync()
        {
            try
            {
                var programs = await _context.Programs.ToListAsync();

                return new BaseResponse<IEnumerable<ProgramModel>>
                {
                    Message = "Programs retrieved successfully",
                    Data = programs.Select(pg => pg.Adapt<ProgramModel>()).ToList(),
                    Status = true
                };

            }
            catch (Exception ex)
            {

                return new BaseResponse<IEnumerable<ProgramModel>>
                {
                    Message = "An error occurred while retrieving programs",
                    Status = false
                };
            }
            
        }

        public async Task<BaseResponse<ProgramModel>> GetProgramAsync(string Id)
        {
            var program = await _context.Programs.FirstOrDefaultAsync(x => x.Id == Id);

            if(program == null)
            {
                return new BaseResponse<ProgramModel>
                {
                    Status = false,
                    Message = "Program does not exist",
                };
            }
            // Perform the mapping using Mapster's Adapt method
            var programModel = program.Adapt<ProgramModel>();

            return new BaseResponse<ProgramModel>
            {
                Status = true,
                Message = "Program retrieved successfully",
                Data = programModel
            };
        }

        public async Task<BaseResponse<ProgramModel>> GetProgramByTitleAsync(string programTitle)
        {
            var program = await _context.Programs.FirstOrDefaultAsync(x => x.ProgramTitle.ToLower() == programTitle.ToLower());

            if (program == null)
            {
                return new BaseResponse<ProgramModel>
                {
                    Status = false,
                    Message = "Program does not exist",
                };
            }
            // Perform the mapping using Mapster's Adapt method
            var programModel = program.Adapt<ProgramModel>();

            return new BaseResponse<ProgramModel>
            {
                Status = true,
                Message = "Program retrieved successfully",
                Data = programModel
            };
        }
    }
}
