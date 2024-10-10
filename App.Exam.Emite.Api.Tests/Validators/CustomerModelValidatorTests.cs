using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Validators;
using App.Exam.Emite.Data;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;

namespace App.Exam.Emite.Api.Core.Tests.Validators
{
    public class CustomerModelValidatorTests : BaseContextSetup
    {
        private readonly DataContext _context;
        private readonly CustomerModelValidator _validator;

        public CustomerModelValidatorTests()
        {
            _context = new DataContext(_options);
            _validator = new CustomerModelValidator(_context);

            SeedData();
        }

        private void SeedData()
        {
            _context.Customers.Add(new Customer
            {
                Id = 1,
                Name = "Duplicate Customer",
                Email = "duplicate@example.com",
                PhoneNumber = "1234567890",
                LastContactDate = DateTime.UtcNow,
                EntityStatus = (int)EntityStatus.Active
            });
            _context.SaveChanges();
        }

        [Fact]
        public async Task IsValidAsync_ValidModel_ReturnsTrue()
        {
            // Arrange
            var customerModel = new CustomerModel
            {
                Id = 2,
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "0987654321",
                LastContactDate = DateTime.UtcNow
            };

            // Act
            var result = await _validator.IsValidAsync(customerModel);

            // Assert
            Assert.True(result);
            Assert.Empty(customerModel.ValidationResult);
        }

        [Fact]
        public async Task IsValidAsync_ModelWithEmptyName_ReturnsFalse()
        {
            // Arrange
            var customerModel = new CustomerModel
            {
                Id = 2,
                Name = "",
                Email = "john.doe@example.com",
                PhoneNumber = "0987654321",
                LastContactDate = DateTime.UtcNow
            };

            // Act
            var result = await _validator.IsValidAsync(customerModel);

            // Assert
            Assert.False(result);
            Assert.Contains(customerModel.ValidationResult, e => e.Key == "name" && e.Message == "This field is required.");
        }

        [Fact]
        public async Task IsValidAsync_DuplicateName_ReturnsFalse()
        {
            // Arrange
            var customerModel = new CustomerModel
            {
                Id = 1,
                Name = "Duplicate Customer", 
                Email = "john.doe@example.com",
                PhoneNumber = "0987654321",
                LastContactDate = DateTime.UtcNow
            };

            // Act
            var result = await _validator.IsValidAsync(customerModel);

            // Assert
            Assert.False(result);
            Assert.Contains(customerModel.ValidationResult, e => e.Key == "name" && e.Message == "Customer name already exists.");
        }

        [Fact]
        public async Task IsValidAsync_MissingEmail_ReturnsFalse()
        {
            // Arrange
            var customerModel = new CustomerModel
            {
                Id = 2,
                Name = "John Doe",
                Email = "", 
                PhoneNumber = "0987654321",
                LastContactDate = DateTime.UtcNow
            };

            // Act
            var result = await _validator.IsValidAsync(customerModel);

            // Assert
            Assert.False(result);
            Assert.Contains(customerModel.ValidationResult, e => e.Key == "email" && e.Message == "The Email field is required.");
        }

        [Fact]
        public async Task IsValidAsync_MissingPhoneNumber_ReturnsFalse()
        {
            // Arrange
            var customerModel = new CustomerModel
            {
                Id = 2,
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "", 
                LastContactDate = DateTime.UtcNow
            };

            // Act
            var result = await _validator.IsValidAsync(customerModel);

            // Assert
            Assert.False(result);
            Assert.Contains(customerModel.ValidationResult, e => e.Key == "phoneNumber" && e.Message == "The PhoneNumber field is required.");
        }

        [Fact]
        public async Task IsValidAsync_MissingLastContactDate_ReturnsFalse()
        {
            // Arrange
            var customerModel = new CustomerModel
            {
                Id = 2,
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "0987654321",
                LastContactDate = null
            };

            // Act
            var result = await _validator.IsValidAsync(customerModel);

            // Assert
            Assert.False(result);
            Assert.Contains(customerModel.ValidationResult, e => e.Key == "lastContactDate" && e.Message == "The LastContactDate field is required.");
        }
    }
}
