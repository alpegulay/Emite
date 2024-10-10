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
    public class TicketServiceTests
    {
        private readonly Mock<ITicketRepository> _mockTicketRepository;
        private readonly Mock<ILogger<TicketService>> _mockLogger;
        private readonly ITicketService _ticketService;

        public TicketServiceTests()
        {
            _mockTicketRepository = new Mock<ITicketRepository>();
            _mockLogger = new Mock<ILogger<TicketService>>();
            _ticketService = new TicketService(_mockTicketRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsTicketModels()
        {
            // Arrange
            var tickets = new List<Ticket>
            {
                new Ticket { Id = 1, CustomerId = 1, AgentId = 1, Status = TicketStatus.Open, Priority = TicketPriority.High, Description = "Issue 1", Resolution = "Resolution 1" },
                new Ticket { Id = 2, CustomerId = 2, AgentId = 2, Status = TicketStatus.Closed, Priority = TicketPriority.Low, Description = "Issue 2", Resolution = "Resolution 2" }
            };

            _mockTicketRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tickets);

            // Act
            var result = await _ticketService.GetAllAsync(1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Item1.Count);
            Assert.Equal(2, result.Item2);
            Assert.Equal(1, result.Item1[0].Id);
            Assert.Equal("Issue 1", result.Item1[0].Description);
            Assert.Equal(2, result.Item1[1].Id);
            Assert.Equal("Issue 2", result.Item1[1].Description);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsTicketModel()
        {
            // Arrange
            var ticket = new Ticket { Id = 1, CustomerId = 1, AgentId = 1, Status = TicketStatus.Open, Priority = TicketPriority.High, Description = "Issue 1", Resolution = "Resolution 1" };
            _mockTicketRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(ticket);

            // Act
            var result = await _ticketService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Issue 1", result.Description);
        }

        [Fact]
        public async Task EnsureAsync_AddsNewTicketModel()
        {
            // Arrange
            var model = new TicketModel { Id = 0, CustomerId = 1, AgentId = 1, Status = TicketStatus.Open, Priority = TicketPriority.High, Description = "Issue 1", Resolution = "Resolution 1" };
            var entity = new Ticket { Id = 1, CustomerId = 1, AgentId = 1, Status = TicketStatus.Open, Priority = TicketPriority.High, Description = "Issue 1", Resolution = "Resolution 1" };

            _mockTicketRepository.Setup(repo => repo.EnsureAsync(It.IsAny<int>(), It.IsAny<Ticket>()))
                                 .ReturnsAsync(entity);

            // Act
            var result = await _ticketService.EnsureAsync(1, model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task EnsureAsync_UpdatesExistingTicketModel()
        {
            // Arrange
            var model = new TicketModel { Id = 1, CustomerId = 1, AgentId = 1, Status = TicketStatus.Open, Priority = TicketPriority.High, Description = "Updated Issue", Resolution = "Updated Resolution" };
            var existingEntity = new Ticket { Id = 1, CustomerId = 1, AgentId = 1, Status = TicketStatus.Open, Priority = TicketPriority.High, Description = "Issue 1", Resolution = "Resolution 1" };

            _mockTicketRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingEntity);
            _mockTicketRepository.Setup(repo => repo.EnsureAsync(It.IsAny<int>(), It.IsAny<Ticket>()))
                                 .ReturnsAsync(existingEntity);

            // Act
            var result = await _ticketService.EnsureAsync(1, model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Updated Issue", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_DeletesTicketById()
        {
            // Arrange
            var ticketId = 1;
            _mockTicketRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), ticketId)).Returns(Task.CompletedTask);

            // Act
            await _ticketService.DeleteAsync(1, ticketId);

            // Assert
            _mockTicketRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>(), ticketId), Times.Once);
        }
    }
}
