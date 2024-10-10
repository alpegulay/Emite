using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using App.Exam.Emite.Data.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace App.Exam.Emite.Api.Core.Services
{
    public class TicketService : ITicketService
    {
        private readonly ILogger<TicketService> _logger;
        private readonly ITicketRepository _ticketRepository;

        public TicketService( ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        public async Task DeleteAsync(int currentUserId, int id)
        {
            await _ticketRepository.DeleteAsync(currentUserId, id);
        }

        public async Task<TicketModel> EnsureAsync(int currentUserId, TicketModel model)
        {
            var entity = new Ticket();
            if (model.Id != 0)
            {
                entity = await _ticketRepository.GetByIdAsync(model.Id);
            }

            model.ToEntity(entity);

            entity = await _ticketRepository.EnsureAsync(currentUserId, entity);
            model.Id = entity.Id;

            return model;
        }

        public async Task<(List<TicketModel>,int)> GetAllAsync(int page, int pageSize)
        {
            var calls = (await _ticketRepository.GetAllAsync());

            var result = calls
                .Select(x => new TicketModel(x))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (result, calls.Count);
        }

        public async Task<TicketModel> GetByIdAsync(int id)
        {
            var ticketEntity = await _ticketRepository.GetByIdAsync(id);

            var model = new TicketModel(ticketEntity);

            return model;
        }
    }
}
