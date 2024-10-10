using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Validators;
using App.Exam.Emite.Data;
using App.Exam.Emite.Data.Entities.Enums;

namespace App.Exam.Emite.Api.Core.Tests.Validators
{
    public class CallModelValidatorTests : BaseContextSetup
    {
        private readonly DataContext _context;
        private readonly CallModelValidator _validator;

        public CallModelValidatorTests()
        {
            _context = new DataContext(_options);
            _validator = new CallModelValidator(_context);
        }

        [Fact]
        public async Task IsValidAsync_ValidCallModel_ReturnsTrue()
        {
            // Arrange
            var callModel = new CallModel
            {
                Id = 1,
                CustomerId = 123,
                AgentId = 456,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(10),
                Status = CallStatus.Completed,
                Notes = "Test call"
            };

            // Act
            var result = await _validator.IsValidAsync(callModel);

            // Assert
            Assert.True(result);
            Assert.Empty(callModel.ValidationResult); 
        }

        [Fact]
        public async Task IsValidAsync_MissingCustomerId_ReturnsFalse()
        {
            // Arrange
            var callModel = new CallModel
            {
                Id = 1,
                CustomerId = null,
                AgentId = 456,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(10),
                Status = CallStatus.Completed,
                Notes = "Test call"
            };

            // Act
            var result = await _validator.IsValidAsync(callModel);

            // Assert
            Assert.False(result);
            Assert.Contains(callModel.ValidationResult, e => e.Key == "customerId" && e.Message == string.Format("The CustomerId field is required."));
        }

        [Fact]
        public async Task IsValidAsync_MissingAgentId_ReturnsFalse()
        {
            // Arrange
            var callModel = new CallModel
            {
                Id = 1,
                CustomerId = 123,
                AgentId = null,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(10),
                Status = CallStatus.Completed,
                Notes = "Test call"
            };

            // Act
            var result = await _validator.IsValidAsync(callModel);

            // Assert
            Assert.False(result);
            Assert.Contains(callModel.ValidationResult, e => e.Key == "agentId" && e.Message == string.Format("The AgentId field is required."));
        }

        [Fact]
        public async Task IsValidAsync_MissingStartTime_ReturnsFalse()
        {
            // Arrange
            var callModel = new CallModel
            {
                Id = 1,
                CustomerId = 123,
                AgentId = 456,
                StartTime = null, 
                EndTime = DateTime.Now.AddMinutes(10),
                Status = CallStatus.Completed,
                Notes = "Test call"
            };

            // Act
            var result = await _validator.IsValidAsync(callModel);

            // Assert
            Assert.False(result);
            Assert.Contains(callModel.ValidationResult, e => e.Key == "startTime" && e.Message == string.Format("The StartTime field is required."));
        }

        [Fact]
        public async Task IsValidAsync_MissingEndTime_ReturnsFalse()
        {
            // Arrange
            var callModel = new CallModel
            {
                Id = 1,
                CustomerId = 123,
                AgentId = 456,
                StartTime = DateTime.Now,
                EndTime = null,
                Status = CallStatus.Completed,
                Notes = "Test call"
            };

            // Act
            var result = await _validator.IsValidAsync(callModel);

            // Assert
            Assert.False(result);
            Assert.Contains(callModel.ValidationResult, e => e.Key == "endTime" && e.Message == string.Format("The EndTime field is required."));
        }

        [Fact]
        public async Task IsValidAsync_MissingNotes_ReturnsFalse()
        {
            // Arrange
            var callModel = new CallModel
            {
                Id = 1,
                CustomerId = 123,
                AgentId = 456,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(10),
                Status = CallStatus.Completed,
                Notes = null 
            };

            // Act
            var result = await _validator.IsValidAsync(callModel);

            // Assert
            Assert.False(result);
            Assert.Contains(callModel.ValidationResult, e => e.Key == "notes" && e.Message == string.Format("The Notes field is required."));
        }
    }
}
