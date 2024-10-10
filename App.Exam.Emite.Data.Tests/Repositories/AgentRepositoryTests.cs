using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using App.Exam.Emite.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Exam.Emite.Data.Tests.Repositories
{
    public class AgentRepositoryTests
    {
        private readonly DataContext _context;
        private readonly IAgentRepository _agentRepository;

        public AgentRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .EnableSensitiveDataLogging()
                .Options;

            _context = new DataContext(options);
            _agentRepository = new AgentRepository(_context);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenAgentDoesNotExist()
        {
            // Arrange
            var nonExistentAgentId = 999;

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _agentRepository.DeleteAsync(1, nonExistentAgentId));
        }

        [Fact]
        public async Task EnsureAsync_ShouldAddAgent_WhenAgentIsNew()
        {
            // Arrange
            var newAgent = new Agent
            {
                Name = "New Agent",
                Email = "new.agent@example.com", 
                PhoneExtension = "123", 
                EntityStatus = (int)EntityStatus.Active
            };

            // Act
            var result = await _agentRepository.EnsureAsync(1, newAgent);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Agent", result.Name);
            Assert.Equal("new.agent@example.com", result.Email);  
            Assert.Equal("123", result.PhoneExtension);     
            Assert.Equal((int)EntityStatus.Active, result.EntityStatus);
        }

        [Fact]
        public async Task EnsureAsync_ShouldUpdateAgent_WhenAgentExists()
        {
            // Arrange
            var existingAgent = new Agent
            {
                Id = 1,
                Name = "Existing Agent",
                Email = "existing.agent@example.com",  
                PhoneExtension = "456",         
                EntityStatus = (int)EntityStatus.Active
            };
            _context.Agents.Add(existingAgent);
            await _context.SaveChangesAsync();

            // Update properties
            existingAgent.Name = "Updated Agent";
            existingAgent.Email = "updated.agent@example.com"; 
            existingAgent.PhoneExtension = "789";       

            // Act
            var result = await _agentRepository.EnsureAsync(1, existingAgent);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Agent", result.Name);
            Assert.Equal("updated.agent@example.com", result.Email);
            Assert.Equal("789", result.PhoneExtension);       
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenAgentDoesNotExist()
        {
            // Arrange
            var nonExistentAgentId = 999;

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _agentRepository.GetByIdAsync(nonExistentAgentId));
        }
    }
}
