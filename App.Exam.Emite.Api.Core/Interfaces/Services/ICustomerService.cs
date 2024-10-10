using App.Exam.Emite.Api.Core.Models;

namespace App.Exam.Emite.Api.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerModel>> GetAllAsync();
        Task<CustomerModel> GetByIdAsync(int id);
        Task<CustomerModel> EnsureAsync(int currentUserId, CustomerModel model);
        Task DeleteAsync(int currentUserId, int id);
    }
}
