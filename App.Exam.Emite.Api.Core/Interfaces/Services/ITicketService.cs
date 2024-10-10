using App.Exam.Emite.Api.Core.Models;

namespace App.Exam.Emite.Api.Core.Interfaces.Services
{
    public interface ITicketService
    {
        Task<(List<TicketModel>,int)> GetAllAsync(int page, int pageSize);
        Task<TicketModel> GetByIdAsync(int id);
        Task<TicketModel> EnsureAsync(int currentUserId, TicketModel model);
        Task DeleteAsync(int currentUserId, int id);
    }
}
