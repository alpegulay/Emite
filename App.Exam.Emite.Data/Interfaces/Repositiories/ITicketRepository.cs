using App.Exam.Emite.Data.Entities;

namespace App.Exam.Emite.Data.Interfaces.Repositiories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket> GetByIdAsync(int id);
        Task<Ticket> EnsureAsync(int currentUserId, Ticket entity);
        Task DeleteAsync(int currentUserId, int id);

        IQueryable<Ticket> GetIQueryable();
    }
}
