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
    public class ProgramRepositoryTests : IDisposable
    {
        private readonly ApplicationContext _dbContext;
        private readonly IApplicationRepository _applicationRepository;

        public ProgramRepositoryTests()
        {
            // Initialize a new in-memory DbContext for each test
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationContext(options);

            // Initialize the repository with the DbContext
            _applicationRepository = new ApplicationRepository(_dbContext);
        }

        public void Dispose()
        {
            // Clean up the in-memory database after each test
            _dbContext.Dispose();
        }

        [Fact]
        public async Task CreateApplicationAsync_ValidInput_ReturnsTrue()
        {
            // Arrange
            var createApplicationModel = new CreateApplication
            {
              EmailAddress = "ada@gmail.com",
              FirstName = "Ada",
              LastName = "Adanna"
            };

            // Act
            var result = await _applicationRepository.CreateApplicationAsync(createApplicationModel);

            // Assert
            Assert.True(result.Status);
        }

        [Fact]
        public async Task DeleteApplicationAsync_ExistingId_ReturnsTrue()
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
            var result = await _applicationRepository.DeleteApplicationAsync(application.Id);

            // Assert
            Assert.True(result.Status);
        }

        [Fact]
        public async Task GetApplicationAsync_ExistingId_ReturnsApplicationModel()
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
            var result = await _applicationRepository.GetApplicationAsync(application.Id);

            // Assert
            Assert.True(result.Status);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetApplicationsAsync_NoApplications_ReturnsEmptyList()
        {
            // Act
            var result = await _applicationRepository.GetApplicationsAsync();

            // Assert
            Assert.False(result.Status);
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task UpdateApplicationAsync_ExistingId_ReturnsTrue()
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
            var updateApplicationModel = new UpdateApplication 
            { 
                FirstName = "Emma",
                LastName = "James",
                PhoneNumber = "+2347023454567"
            };

            // Act
            var result = await _applicationRepository.UpdateApplicationAsync(updateApplicationModel, application.Id);

            // Assert
            Assert.True(result.Status);
        }
    }
}



