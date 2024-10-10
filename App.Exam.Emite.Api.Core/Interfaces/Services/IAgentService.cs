using App.Exam.Emite.Api.Core.Models;

namespace App.Exam.Emite.Api.Core.Interfaces.Services
{
    public interface IAgentService
    {
        Task<List<AgentModel>> GetAllAsync();
        Task<AgentModel> GetByIdAsync(int id);
        Task<AgentModel> EnsureAsync(int currentUserId, AgentModel model);
        Task DeleteAsync(int currentUserId, int id);
    }
}
