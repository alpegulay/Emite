using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using App.Exam.Emite.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Exam.Emite.Data.Tests.Repositories
{
    public class TicketRepositoryTests
    {
        private readonly DataContext _context;
        private readonly ITicketRepository _ticketRepository;

        public TicketRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TicketTestDatabase")
                .EnableSensitiveDataLogging()
                .Options;

            _context = new DataContext(options);
            _ticketRepository = new TicketRepository(_context);
        }

        [Fact]
        public async Task EnsureAsync_ShouldAddTicket_WhenTicketIsNew()
        {
            // Arrange
            var newTicket = new Ticket
            {
                CustomerId = 1,
                AgentId = 1,
                Status = TicketStatus.Open, 
                Priority = TicketPriority.High, 
                Description = "Ticket description.",
                Resolution = "Pending",
            };

            // Act
            var result = await _ticketRepository.EnsureAsync(1, newTicket);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newTicket.Description, result.Description);
            Assert.Equal((int)TicketStatus.Open, (int)result.Status);
            Assert.Equal((int)TicketPriority.High, (int)result.Priority);
            Assert.True(result.Id > 0); 
        }

        [Fact]
        public async Task EnsureAsync_ShouldUpdateTicket_WhenTicketExists()
        {
            // Arrange
            var existingTicket = new Ticket
            {
                Id = 1,
                CustomerId = 1, 
                AgentId = 1,
                Status = TicketStatus.Open, 
                Priority = TicketPriority.High, 
                Description = "Existing description.",
                Resolution = "Pending",
            };

            _context.Tickets.Add(existingTicket);
            await _context.SaveChangesAsync();

            existingTicket.Description = "Updated description"; 

            // Act
            var result = await _ticketRepository.EnsureAsync(1, existingTicket);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated description", result.Description);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenTicketDoesNotExist()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                async () => await _ticketRepository.DeleteAsync(1, 99)
            );
            Assert.Contains("Ticket", exception.Message);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenTicketDoesNotExist()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(
                async () => await _ticketRepository.GetByIdAsync(99)
            );
            Assert.Contains("Ticket", exception.Message);
        }
    }
}
