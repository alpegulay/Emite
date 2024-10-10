using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Services;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using Microsoft.Extensions.Logging;
using Moq;

namespace App.Exam.Emite.Api.Core.Tests.Services
{
    public class CallServiceTests
    {
        private readonly Mock<ICallRepository> _mockCallRepository;
        private readonly Mock<ILogger<CallService>> _mockLogger;
        private readonly ICallService _callService;

        public CallServiceTests()
        {
            _mockCallRepository = new Mock<ICallRepository>();
            _mockLogger = new Mock<ILogger<CallService>>();
            _callService = new CallService(_mockCallRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsCallModelsAndCount()
        {
            // Arrange
            var calls = new List<Call>
            {
                new Call { Id = 1, CustomerId = 1, AgentId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(30), Status = CallStatus.Completed, Notes = "Note 1" },
                new Call { Id = 2, CustomerId = 2, AgentId = 2, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(45), Status = CallStatus.Completed, Notes = "Note 2" }
            };

            _mockCallRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(calls);

            // Act
            var result = await _callService.GetAllAsync(1, 10);

            // Assert
            Assert.Equal(2, result.Item1.Count);
            Assert.Equal(2, result.Item2);
            Assert.Equal(1, result.Item1[0].Id);
            Assert.Equal(2, result.Item1[1].Id);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCallModel()
        {
            // Arrange
            var call = new Call { Id = 1, CustomerId = 1, AgentId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(30), Status = CallStatus.Completed, Notes = "Note 1" };
            _mockCallRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(call);

            // Act
            var result = await _callService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Note 1", result.Notes);
        }

        [Fact]
        public async Task EnsureAsync_AddsNewCallModel()
        {
            // Arrange
            var model = new CallModel { Id = 0, CustomerId = 1, AgentId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(30), Status = CallStatus.Completed, Notes = "Note 1" };
            var entity = new Call { Id = 1, CustomerId = 1, AgentId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(30), Status = CallStatus.Completed, Notes = "Note 1" };

            _mockCallRepository.Setup(repo => repo.EnsureAsync(It.IsAny<int>(), It.IsAny<Call>()))
                               .ReturnsAsync(entity);

            // Act
            var result = await _callService.EnsureAsync(1, model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task DeleteAsync_DeletesCallById()
        {
            // Arrange
            var callId = 1;
            _mockCallRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), callId)).Returns(Task.CompletedTask);

            // Act
            await _callService.DeleteAsync(1, callId);

            // Assert
            _mockCallRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>(), callId), Times.Once);
        }

        [Fact]
        public async Task SearchAsync_ReturnsCallModelsAndCount()
        {
            // Arrange
            var calls = new List<Call>
            {
                new Call { Id = 1, CustomerId = 1, AgentId = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(30), Status = CallStatus.Completed, Notes = "Note 1" },
                new Call { Id = 2, CustomerId = 2, AgentId = 2, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(45), Status = CallStatus.Completed, Notes = "Note 2" }
            };

            var searchModel = new CallSearchModel(); // Populate as needed
            _mockCallRepository.Setup(repo => repo.SearchAsync(It.IsAny<CallSearchEntity>()))
                .ReturnsAsync(calls);

            // Act
            var result = await _callService.SearchAsync(searchModel, 1, 10);

            // Assert
            Assert.Equal(2, result.Item1.Count);
            Assert.Equal(2, result.Item2);
            Assert.Equal(1, result.Item1[0].Id);
            Assert.Equal(2, result.Item1[1].Id);
        }
    }
}
