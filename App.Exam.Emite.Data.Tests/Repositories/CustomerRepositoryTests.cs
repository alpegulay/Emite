using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using App.Exam.Emite.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Exam.Emite.Data.Tests.Repositories
{
    public class CustomerRepositoryTests
    {
        private readonly DataContext _context;
        private readonly ICustomerRepository _customerRepository;

        public CustomerRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "CustomerTestDatabase")
                .EnableSensitiveDataLogging() 
                .Options;

            _context = new DataContext(options);
            _customerRepository = new CustomerRepository(_context);
        }

        [Fact]
        public async Task EnsureAsync_ShouldUpdateCustomer_WhenCustomerExists()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                Id =5,
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                PhoneNumber = "987-654-3210",
                LastContactDate = DateTime.Now,
                EntityStatus = (int)EntityStatus.Active
            };

            _context.Customers.Add(existingCustomer);
            await _context.SaveChangesAsync();

            existingCustomer.Name = "Jane Smith";

            // Act
            var result = await _customerRepository.EnsureAsync(1, existingCustomer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jane Smith", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            var existingCustomer = new Customer
            {
                Id = 1,
                Name = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                LastContactDate = DateTime.Now,
                EntityStatus = (int)EntityStatus.Active
            };

            _context.Customers.Add(existingCustomer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerRepository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingCustomer.Name, result.Name);
        }
    }
}
