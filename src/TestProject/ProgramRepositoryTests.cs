using CapitalPlacementAssesementTaskApi.Contracts.Interfaces;
using CapitalPlacementAssesementTaskApi.Contracts.Repositories;
using ConsoleProject.Dtos.ServiceRequestModels.Application;
using ConsoleProject.Dtos.ServiceRequestModels.Program;
using ConsoleProject.Enums;
using ConsoleProject.Models;
using ConsoleProject.Models.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using WebApiProject.Context;

namespace TestProject
{
    public class ApplicationRepositoryTests : IDisposable
    {
        private readonly ApplicationContext _dbContext;
        private readonly IProgramDetailsRepository _programRepository;

        public ApplicationRepositoryTests()
        {
            // Initialize a new in-memory DbContext for each test
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationContext(options);

            // Initialize the repository with the DbContext
            _programRepository = new ProgramRepository(_dbContext);
        }

        public void Dispose()
        {
            // Clean up the in-memory database after each test
            _dbContext.Dispose();
        }

        [Fact]
        public async Task CreateApplicationProgramAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            var createApplicationModel = new CreateProgram
            {
                ProgramTitle = "Tech Event",
                Description = "Annual tech even",
                MaxApplications = 10000,
                DurationInMonths = 1,
                ProgramStart = DateTime.UtcNow.AddMonths(2),
                ApplicantQualification = MinQualification.College,
                ApplicantSkills = new List<Skills>
                {
                    Skills.SocialMedia,
                    Skills.UIUx,
                    Skills.SEO,
                    Skills.GraphicsDesign
                },
                ApplicationCriteria = new List<string>
                {
                    "Must Be of Age 18-100",
                },
                ApplicationEnds = DateTime.Now.AddDays(3),
                ApplicationStart = DateTime.Now,
                Benefits = new List<string>
                {
                    "Accommodation",
                    "Feeding", "Allowances",
                },
            };

            // Act
            var result = await _programRepository.AddProgramAsync(createApplicationModel);

            // Assert
            Assert.True(result.Status);
        }

        [Fact]
        public async Task DeleteProgramAsync_ExistingId_ReturnsTrue()
        {
            // Arrange
            var application = new Application
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@gmail.com"
            };
            _dbContext.Applications.Add(application);
            _dbContext.SaveChanges();

            // Act
            var result = await _programRepository.DeleteProgramAsync(application.Id);

            // Assert
            Assert.True(result.Status);
        }

        [Fact]
        public async Task GetProgramAsync_ExistingId_ReturnsApplicationModel()
        {
            // Arrange
            var application = new Application
            {
                FirstName = "John",
                LastName = "James",
                EmailAddress = "abc@gmail.com"
            };
            _dbContext.Applications.Add(application);
            _dbContext.SaveChanges();

            // Act
            var result = await _programRepository.GetProgramAsync(application.Id);

            // Assert
            Assert.True(result.Status);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetProgramsAsync_NoApplications_ReturnsEmptyList()
        {
            // Act
            var result = await _programRepository.GetAllProgramsAsync();

            // Assert
            Assert.False(result.Status);
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task UpdateProgramAsync_ExistingId_ReturnsTrue()
        {
            // Arrange
            var application = new Application
            {
                FirstName = "Nma",
                LastName = "Peter",
                EmailAddress = "peternma@gmail.com"
            };
            _dbContext.Applications.Add(application);
            _dbContext.SaveChanges();
            var updateApplicationModel = new UpdateProgram 
            { 
                ProgramLocations = new List<ProgramLocation> {},
                ApplicationCriteria = new List<string> { "Must be upto 18"}
            };

            // Act
            var result = await _programRepository.EditProgramAsync(updateApplicationModel, application.Id);

            // Assert
            Assert.True(result.Status);
        }
    }
}



