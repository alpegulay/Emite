using App.Exam.Emite.Data.Constants;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Helpers;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App.Exam.Emite.Data.Repositories
{
    public class CallRepository : ICallRepository
    {
        private readonly DataContext _context;

        public CallRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task DeleteAsync(int currentUserId, int id)
        {
            var entity =
                await _context.Agents.FirstOrDefaultAsync(x =>
                 x.Id == id && x.EntityStatus == (int)EntityStatus.Active);

            if (entity == null)
            {
                throw new Exception(string.Format(ErrorMessage.DoesNotExist, "Call"));
            }

            AuditTrailHelper.SetUpdated(currentUserId, entity);
            entity.EntityStatus = (int)EntityStatus.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Call> EnsureAsync(int currentUserId, Call entity)
        {
            if (entity.Id == 0)
            {
                AuditTrailHelper.SetCreated(currentUserId, entity);
                _context.Calls.Add(entity);
            }
            else
            {
                AuditTrailHelper.SetUpdated(currentUserId, entity);
                _context.Calls.Update(entity);
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Call>>GetAllAsync()
        {
            return await GetIQueryable() 
             .Where(x => x.EntityStatus == (int)EntityStatus.Active)
             .OrderBy(x => x.CustomerId)
             .ToListAsync();
        }

        public async Task<Call> GetByIdAsync(int id)
        {
            var data = await GetIQueryable()
                 .FirstOrDefaultAsync(x => x.Id == id && x.EntityStatus == (int)EntityStatus.Active);

            if (data == null)
            {
                throw new Exception(string.Format(ErrorMessage.DoesNotExist, "Call"));
            }

            return data;
        }

        public IQueryable<Call> GetIQueryable()
        {
            return _context.Calls.AsQueryable();
        }

        public async Task<List<Call>> SearchAsync(CallSearchEntity callSearchEntity)
        {
            var query =  GetIQueryable();

            // Apply filters based on the search criteria
            if (callSearchEntity.Status.HasValue)
            {
                query = query.Where(c => c.Status == callSearchEntity.Status.Value);
            }
            if (callSearchEntity.StartTime.HasValue)
            {
                query = query.Where(c => c.StartTime >= callSearchEntity.StartTime.Value);
            }
            if (callSearchEntity.EndTime.HasValue)
            {
                query = query.Where(c => c.EndTime <= callSearchEntity.EndTime.Value);
            }

            if (!string.IsNullOrEmpty(callSearchEntity.Email))
            {
                query = query.Where(c => c.Agent.Email.Contains(callSearchEntity.Email,StringComparison.OrdinalIgnoreCase));
            }

            return  await query.Include(x=>x.Agent) .OrderBy(x => x.CustomerId).ToListAsync();
        }
    }
}
