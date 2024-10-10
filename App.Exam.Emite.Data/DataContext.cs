using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Interfaces.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data
{
    public  class DataContext : DbContext
    {
        private readonly IConnectionHelper _connectionHelper;
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public DataContext(DbContextOptions<DataContext> options, IConnectionHelper connectionHelper)
        : base(options)
        {
            _connectionHelper = connectionHelper;
        }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public void DetachAll()
        {

            foreach (var dbEntityEntry in this.ChangeTracker.Entries().ToArray())
            {

                if (dbEntityEntry.Entity != null)
                {
                    dbEntityEntry.State = EntityState.Detached;
                }
            }
        }
    }
}
