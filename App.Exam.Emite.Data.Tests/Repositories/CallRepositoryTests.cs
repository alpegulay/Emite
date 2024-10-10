using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using App.Exam.Emite.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Exam.Emite.Data.Tests.Repositories
{
    public class CallRepositoryTests
    {
        private readonly DataContext _context;
        private readonly ICallRepository _callRepository;

        public CallRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .EnableSensitiveDataLogging() 
                .Options;

            _context = new DataContext(options);
            _callRepository = new CallRepository(_context);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenCallDoesNotExist()
        {
            // Arrange
            var nonExistentCallId = 999;

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _callRepository.DeleteAsync(1, nonExistentCallId));
        }

        [Fact]
        public async Task EnsureAsync_ShouldAddCall_WhenCallIsNew()
        {
            // Arrange
            var newCall = new Call
            {
                CustomerId = 456,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(15),
                Status = CallStatus.InProgress,
                EntityStatus = (int)EntityStatus.Active,
                Notes = "New call notes",
                Agent = new Agent
                {
                    Name = "New Agent",
                    PhoneExtension = "1234",
                    Email = "new.agent@example.com"
                }
            };

            // Act
            var result = await _callRepository.EnsureAsync(1, newCall);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newCall.CustomerId, result.CustomerId);
            Assert.Equal((int)EntityStatus.Active, result.EntityStatus);
        }

        [Fact]
        public async Task EnsureAsync_ShouldUpdateCall_WhenCallExists()
        {
            // Arrange
            var existingCall = new Call
            {
                Id = 6,
                CustomerId = 790,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(20),
                Status = CallStatus.InProgress,
                EntityStatus = (int)EntityStatus.Active,
                Notes = "Existing call notes",
                Agent = new Agent
                {
                    Name = "New Agentss",
                    PhoneExtension = "12344",
                    Email = "new.agent@example.com"
                }
            };
            _context.Calls.Add(existingCall);
            await _context.SaveChangesAsync();

            // Update properties
            existingCall.CustomerId = 101112;
            existingCall.Status = CallStatus.Completed;

            // Act
            var result = await _callRepository.EnsureAsync(1, existingCall);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(101112, result.CustomerId);
            Assert.Equal(CallStatus.Completed, result.Status);
        }


        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenCallDoesNotExist()
        {
            // Arrange
            var nonExistentCallId = 999;

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _callRepository.GetByIdAsync(nonExistentCallId));
        }
    }
}
