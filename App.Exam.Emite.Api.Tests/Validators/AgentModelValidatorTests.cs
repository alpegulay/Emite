using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Validators;
using App.Exam.Emite.Data;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using System.Linq;
using Xunit;

namespace App.Exam.Emite.Api.Core.Tests.Validators
{
    public class AgentModelValidatorTests : BaseContextSetup
    {
        private readonly DataContext _context;
        private readonly AgentModelValidator _validator;

        public AgentModelValidatorTests()
        {
            _context = new DataContext(_options);
            _validator = new AgentModelValidator(_context);

            SeedData();
        }

        private void SeedData()
        {
            _context.Agents.Add(new Agent
            {
                Id = 1,
                Name = "Duplicate Name",
                Email = "duplicate@example.com",
                PhoneExtension = "100",
                Status = AgentStatus.Available,
                EntityStatus = (int)EntityStatus.Active
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task IsValidAsync_ValidModel_ReturnsTrue()
        {
            // Arrange
            var agentModel = new AgentModel
            {
                Id = 2,
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneExtension = "123",
                Status = AgentStatus.Available
            };

            // Act
            var result = await _validator.IsValidAsync(agentModel);

            // Assert
            Assert.True(result);
            Assert.Empty(agentModel.ValidationResult); 
        }

        [Fact]
        public async Task IsValidAsync_ModelWithEmptyName_ReturnsFalse()
        {
            // Arrange
            var agentModel = new AgentModel
            {
                Id = 2,
                Name = "", 
                Email = "john.doe@example.com",
                PhoneExtension = "123",
                Status = AgentStatus.Available
            };

            // Act
            var result = await _validator.IsValidAsync(agentModel);

            // Assert
            Assert.False(result);
            Assert.Contains(agentModel.ValidationResult, e => e.Key == "name" && e.Message == string.Format("This field is required."));
        }

        [Fact]
        public async Task IsValidAsync_DuplicateName_ReturnsFalse()
        {
            // Arrange
            var agentModel = new AgentModel
            {
                Id = 1, 
                Name = "Duplicate Name",
                Email = "john.doe@example.com",
                PhoneExtension = "123",
                Status = AgentStatus.Available
            };

            // Act
            var result = await _validator.IsValidAsync(agentModel);

            // Assert
            Assert.False(result);
            Assert.Contains(agentModel.ValidationResult, e => e.Key == "name" && e.Message == string.Format("Agent name already exists."));
        }

        [Fact]
        public async Task IsValidAsync_MissingEmail_ReturnsFalse()
        {
            // Arrange
            var agentModel = new AgentModel
            {
                Id = 2,
                Name = "John Doe",
                Email = "", 
                PhoneExtension = "123",
                Status = AgentStatus.Available
            };

            // Act
            var result = await _validator.IsValidAsync(agentModel);

            // Assert
            Assert.False(result);
            Assert.Contains(agentModel.ValidationResult, e => e.Key == "email" && e.Message == string.Format("The Email field is required."));
        }
    }
}
