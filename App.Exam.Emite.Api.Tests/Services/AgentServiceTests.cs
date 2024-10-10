using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Services;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using Microsoft.Extensions.Logging;
using Moq;

namespace App.Exam.Emite.Api.Core.Tests.Services
{
    public class AgentServiceTests
    {
        private readonly Mock<IAgentRepository> _agentRepositoryMock;
        private readonly Mock<ILogger<AgentService>> _loggerMock;
        private readonly AgentService _agentService;

        public AgentServiceTests()
        {
            _agentRepositoryMock = new Mock<IAgentRepository>();
            _loggerMock = new Mock<ILogger<AgentService>>();
            _agentService = new AgentService(_agentRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfAgentModels()
        {
            // Arrange
            var agents = new List<Agent>
            {
                new Agent { Id = 1, Name = "Agent 1" },
                new Agent { Id = 2, Name = "Agent 2" }
            };
            _agentRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(agents);

            // Act
            var result = await _agentService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(2, result[1].Id);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ShouldReturnAgentModel()
        {
            // Arrange
            var agent = new Agent { Id = 1, Name = "Agent 1" };
            _agentRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(agent);

            // Act
            var result = await _agentService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task EnsureAsync_NewAgent_ShouldCreateAgent()
        {
            // Arrange
            var model = new AgentModel { Id = 0, Name = "New Agent" };
            var entity = new Agent { Id = 1, Name = "New Agent" };
            _agentRepositoryMock.Setup(repo => repo.EnsureAsync(It.IsAny<int>(), It.IsAny<Agent>()))
                                .ReturnsAsync(entity);

            // Act
            var result = await _agentService.EnsureAsync(1, model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task EnsureAsync_ExistingAgent_ShouldUpdateAgent()
        {
            // Arrange
            var model = new AgentModel { Id = 1, Name = "Updated Agent" };
            var existingEntity = new Agent { Id = 1, Name = "Old Agent" };
            _agentRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingEntity);
            _agentRepositoryMock.Setup(repo => repo.EnsureAsync(It.IsAny<int>(), existingEntity)).ReturnsAsync(existingEntity);

            // Act
            var result = await _agentService.EnsureAsync(1, model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Agent", result.Name);
            _agentRepositoryMock.Verify(repo => repo.EnsureAsync(1, existingEntity), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ExistingAgent_ShouldDeleteAgent()
        {
            // Arrange
            int currentUserId = 1;
            int agentId = 1;

            // Act
            await _agentService.DeleteAsync(currentUserId, agentId);

            // Assert
            _agentRepositoryMock.Verify(repo => repo.DeleteAsync(currentUserId, agentId), Times.Once);
        }
    }
}
