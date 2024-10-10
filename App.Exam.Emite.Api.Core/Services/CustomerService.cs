using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using Microsoft.Extensions.Logging;

namespace App.Exam.Emite.Api.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task DeleteAsync(int currentUserId, int id)
        {
            await _customerRepository.DeleteAsync(currentUserId, id);
        }

        public async Task<CustomerModel> EnsureAsync(int currentUserId, CustomerModel model)
        {
            var entity = new Customer();
            if (model.Id != 0)
            {
                entity = await _customerRepository.GetByIdAsync(model.Id);
            }

            model.ToEntity(entity);

            entity = await _customerRepository.EnsureAsync(currentUserId, entity);
            model.Id = entity.Id;

            return model;
        }

        public async Task<List<CustomerModel>> GetAllAsync()
        {
            var result = (await _customerRepository.GetAllAsync())
                .Select(x => new CustomerModel(x))
                .ToList();

            return result;
        }
            public async Task<CustomerModel> GetByIdAsync(int id)
        {
            var customerEntity = await _customerRepository.GetByIdAsync(id);

            var model = new CustomerModel(customerEntity);

            return model;
        }
    }
}
