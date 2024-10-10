using App.Exam.Emite.Data.Entities;

namespace App.Exam.Emite.Data.Interfaces.Repositiories
{
    public interface IAgentRepository
    {
        Task<List<Agent>> GetAllAsync();
        Task<Agent> GetByIdAsync(int id);
        Task<Agent> EnsureAsync(int currentUserId, Agent entity);
        Task DeleteAsync(int currentUserId, int id);

        IQueryable<Agent> GetIQueryable();
    }
}
