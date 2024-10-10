using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Validators;
using App.Exam.Emite.Data;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;

namespace App.Exam.Emite.Api.Core.Tests.Validators
{
    public class TicketModelValidatorTests : BaseContextSetup
    {
        private readonly DataContext _context;
        private readonly TicketModelValidator _validator;

        public TicketModelValidatorTests()
        {
            _context = new DataContext(_options);
            _validator = new TicketModelValidator(_context);

            SeedData();
        }

        private void SeedData()
        {
            _context.Tickets.Add(new Ticket
            {
                Id = 1,
                CustomerId = 1,
                AgentId = 1,
                Status = TicketStatus.Open,
                Priority = TicketPriority.High,
                Description = "Test ticket",
                Resolution = "Pending",
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task IsValidAsync_ValidModel_ReturnsTrue()
        {
            // Arrange
            var ticketModel = new TicketModel
            {
                Id = 2,
                CustomerId = 1,
                AgentId = 1,
                Status = TicketStatus.Open,
                Priority = TicketPriority.High,
                Description = "New ticket description",
                Resolution = "Resolved"
            };

            // Act
            var result = await _validator.IsValidAsync(ticketModel);

            // Assert
            Assert.True(result);
            Assert.Empty(ticketModel.ValidationResult); 
        }

        [Fact]
        public async Task IsValidAsync_ModelWithEmptyDescription_ReturnsFalse()
        {
            // Arrange
            var ticketModel = new TicketModel
            {
                Id = 2,
                CustomerId = 1,
                AgentId = 1,
                Status = TicketStatus.Open,
                Priority = TicketPriority.High,
                Description = "", 
                Resolution = "Resolved"
            };

            // Act
            var result = await _validator.IsValidAsync(ticketModel);

            // Assert
            Assert.False(result);
            Assert.Contains(ticketModel.ValidationResult, e => e.Key == "description" && e.Message == string.Format("The Description field is required."));
        }

        [Fact]
        public async Task IsValidAsync_ModelWithEmptyResolution_ReturnsFalse()
        {
            // Arrange
            var ticketModel = new TicketModel
            {
                Id = 2,
                CustomerId = 1,
                AgentId = 1,
                Status = TicketStatus.Open,
                Priority = TicketPriority.High,
                Description = "Description",
                Resolution = ""
            };

            // Act
            var result = await _validator.IsValidAsync(ticketModel);

            // Assert
            Assert.False(result);
            Assert.Contains(ticketModel.ValidationResult, e => e.Key == "resolution" && e.Message == string.Format("The Resolution field is required."));
        }

        [Fact]
        public async Task IsValidAsync_ModelWithEmptyCustomerId_ReturnsFalse()
        {
            // Arrange
            var ticketModel = new TicketModel
            {
                Id = 2,
                CustomerId = null,
                AgentId = 1,
                Status = TicketStatus.Open,
                Priority = TicketPriority.High,
                Description = "Description",
                Resolution = "Resolved"
            };

            // Act
            var result = await _validator.IsValidAsync(ticketModel);

            // Assert
            Assert.False(result);
            Assert.Contains(ticketModel.ValidationResult, e => e.Key == "customerId" && e.Message == string.Format("The CustomerId field is required."));
        }
    }
}
