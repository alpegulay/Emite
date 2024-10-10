using App.Exam.Emite.Data.Entities;

namespace App.Exam.Emite.Data.Interfaces.Repositiories
{
    public interface ICallRepository
    {
        Task<List<Call>> GetAllAsync();
        Task<List<Call>> SearchAsync(CallSearchEntity callSearchEntity);
        Task<Call> GetByIdAsync(int id);
        Task<Call> EnsureAsync(int currentUserId, Call entity);
        Task DeleteAsync(int currentUserId, int id);

        IQueryable<Call> GetIQueryable();
    }
}
