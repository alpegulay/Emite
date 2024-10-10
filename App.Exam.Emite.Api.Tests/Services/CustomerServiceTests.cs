using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Services;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using Microsoft.Extensions.Logging;
using Moq;

namespace App.Exam.Emite.Api.Core.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<ILogger<CustomerService>> _mockLogger;
        private readonly ICustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockLogger = new Mock<ILogger<CustomerService>>();
            _customerService = new CustomerService(_mockCustomerRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsCustomerModels()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", LastContactDate = DateTime.Now },
                new Customer { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", PhoneNumber = "0987654321", LastContactDate = DateTime.Now }
            };

            _mockCustomerRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _customerService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("John Doe", result[0].Name);
            Assert.Equal(2, result[1].Id);
            Assert.Equal("Jane Smith", result[1].Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCustomerModel()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", LastContactDate = DateTime.Now };
            _mockCustomerRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public async Task EnsureAsync_AddsNewCustomerModel()
        {
            // Arrange
            var model = new CustomerModel { Id = 0, Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", LastContactDate = DateTime.Now };
            var entity = new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", LastContactDate = DateTime.Now };

            _mockCustomerRepository.Setup(repo => repo.EnsureAsync(It.IsAny<int>(), It.IsAny<Customer>()))
                                   .ReturnsAsync(entity);

            // Act
            var result = await _customerService.EnsureAsync(1, model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task DeleteAsync_DeletesCustomerById()
        {
            // Arrange
            var customerId = 1;
            _mockCustomerRepository.Setup(repo => repo.DeleteAsync(It.IsAny<int>(), customerId)).Returns(Task.CompletedTask);

            // Act
            await _customerService.DeleteAsync(1, customerId);

            // Assert
            _mockCustomerRepository.Verify(repo => repo.DeleteAsync(It.IsAny<int>(), customerId), Times.Once);
        }

        [Fact]
        public async Task EnsureAsync_UpdatesExistingCustomerModel()
        {
            // Arrange
            var model = new CustomerModel { Id = 1, Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", LastContactDate = DateTime.Now };
            var entity = new Customer { Id = 1, Name = "John Doe", Email = "john.doe@example.com", PhoneNumber = "1234567890", LastContactDate = DateTime.Now };

            _mockCustomerRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(entity);
            _mockCustomerRepository.Setup(repo => repo.EnsureAsync(It.IsAny<int>(), It.IsAny<Customer>()))
                                   .ReturnsAsync(entity);

            // Act
            var result = await _customerService.EnsureAsync(1, model);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("John Doe", result.Name);
        }
    }
}
