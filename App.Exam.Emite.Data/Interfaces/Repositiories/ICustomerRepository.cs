using App.Exam.Emite.Data.Entities;

namespace App.Exam.Emite.Data.Interfaces.Repositiories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
        Task<Customer> EnsureAsync(int currentUserId, Customer entity);
        Task DeleteAsync(int currentUserId, int id);

        IQueryable<Customer> GetIQueryable();
    }
}
