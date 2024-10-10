using App.Exam.Emite.Data.Constants;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Helpers;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using Microsoft.EntityFrameworkCore;

namespace App.Exam.Emite.Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly DataContext _context;

        public TicketRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task DeleteAsync(int currentUserId, int id)
        {
            var entity =
                await _context.Tickets.FirstOrDefaultAsync(x =>
                 x.Id == id && x.EntityStatus == (int)EntityStatus.Active);

            if (entity == null)
            {
                throw new Exception(string.Format(ErrorMessage.DoesNotExist, "Ticket"));
            }

            AuditTrailHelper.SetUpdated(currentUserId, entity);
            entity.EntityStatus = (int)EntityStatus.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Ticket> EnsureAsync(int currentUserId, Ticket entity)
        {
            if (entity.Id == 0)
            {
                AuditTrailHelper.SetCreated(currentUserId, entity);
                _context.Tickets.Add(entity);
            }
            else
            {
                AuditTrailHelper.SetUpdated(currentUserId, entity);
                _context.Tickets.Update(entity);
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Ticket>>GetAllAsync()
        {
            return await GetIQueryable() 
             .Where(x => x.EntityStatus == (int)EntityStatus.Active)
             .OrderBy(x => x.CustomerId)
             .ToListAsync();
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            var data = await GetIQueryable()
                 .FirstOrDefaultAsync(x => x.Id == id && x.EntityStatus == (int)EntityStatus.Active);

            if (data == null)
            {
                throw new Exception(string.Format(ErrorMessage.DoesNotExist, "Ticket"));
            }

            return data;
        }

        public IQueryable<Ticket> GetIQueryable()
        {
            return _context.Tickets.AsQueryable();
        }
    }
}
