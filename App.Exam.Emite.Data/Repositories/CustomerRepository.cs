using App.Exam.Emite.Data.Constants;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Helpers;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using Microsoft.EntityFrameworkCore;

namespace App.Exam.Emite.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task DeleteAsync(int currentUserId, int id)
        {
            var entity =
                await _context.Customers.FirstOrDefaultAsync(x =>
                 x.Id == id && x.EntityStatus == (int)EntityStatus.Active);

            if (entity == null)
            {
                throw new Exception(string.Format(ErrorMessage.DoesNotExist, "Customer"));
            }

            AuditTrailHelper.SetUpdated(currentUserId, entity);
            entity.EntityStatus = (int)EntityStatus.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> EnsureAsync(int currentUserId, Customer entity)
        {
            if (entity.Id == 0)
            {
                AuditTrailHelper.SetCreated(currentUserId, entity);
                _context.Customers.Add(entity);
            }
            else
            {
                AuditTrailHelper.SetUpdated(currentUserId, entity);
                _context.Customers.Update(entity);
            }

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await GetIQueryable()
             .Where(x => x.EntityStatus == (int)EntityStatus.Active)
             .OrderBy(x => x.Name)
             .ToListAsync();
        }

        public  async Task<Customer> GetByIdAsync(int id)
        {
            var data = await GetIQueryable()
                 .FirstOrDefaultAsync(x => x.Id == id && x.EntityStatus == (int)EntityStatus.Active);

            if (data == null)
            {
                throw new Exception(string.Format(ErrorMessage.DoesNotExist, "Agent"));
            }

            return data;
        }

        public  IQueryable<Customer> GetIQueryable()
        {
            return _context.Customers.AsQueryable();
        }
    }
}
