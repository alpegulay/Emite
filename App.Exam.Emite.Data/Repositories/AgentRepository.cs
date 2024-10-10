using App.Exam.Emite.Data.Constants;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Helpers;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using Microsoft.EntityFrameworkCore;

namespace App.Exam.Emite.Data.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly DataContext _context;

        public AgentRepository(DataContext context)
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
                throw new Exception(string.Format(ErrorMessage.DoesNotExist, "Agent"));
            }

            AuditTrailHelper.SetUpdated(currentUserId, entity);
            entity.EntityStatus = (int)EntityStatus.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Agent> EnsureAsync(int currentUserId, Agent entity)
        {
            if (entity.Id == 0)
            {
                AuditTrailHelper.SetCreated(currentUserId, entity);
                _context.Agents.Add(entity);
            }
            else
            {
                AuditTrailHelper.SetUpdated(currentUserId, entity);
                _context.Agents.Update(entity);
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Agent>>GetAllAsync()
        {
            return await GetIQueryable() 
             .Where(x => x.EntityStatus == (int)EntityStatus.Active)
             .OrderBy(x => x.Name)
             .ToListAsync();
        }

        public async Task<Agent> GetByIdAsync(int id)
        {
            var data = await GetIQueryable()
                 .FirstOrDefaultAsync(x => x.Id == id && x.EntityStatus == (int)EntityStatus.Active);

            if (data == null)
            {
                throw new Exception(string.Format(ErrorMessage.DoesNotExist, "Agent"));
            }

            return data;
        }

        public IQueryable<Agent> GetIQueryable()
        {
            return _context.Agents.AsQueryable();
        }
    }
}
