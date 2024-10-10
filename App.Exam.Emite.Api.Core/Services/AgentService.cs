using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using Microsoft.Extensions.Logging;

namespace App.Exam.Emite.Api.Core.Services
{
    public class AgentService : IAgentService
    {
        private readonly ILogger<AgentService> _logger;
        private readonly IAgentRepository _agentRepository;

        public AgentService( IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository ?? throw new ArgumentNullException(nameof(agentRepository));
        }

        public async Task DeleteAsync(int currentUserId, int id)
        {
            await _agentRepository.DeleteAsync(currentUserId, id);
        }

        public async Task<AgentModel> EnsureAsync(int currentUserId, AgentModel model)
        {
            var entity = new Agent();
            if (model.Id != 0)
            {
                entity = await _agentRepository.GetByIdAsync(model.Id);
            }

            model.ToEntity(entity);

            entity = await _agentRepository.EnsureAsync(currentUserId, entity);
            model.Id = entity.Id;

            return model;
        }

        public async Task<List<AgentModel>> GetAllAsync()
        {
            var result = (await _agentRepository.GetAllAsync())
                .Select(x => new AgentModel(x))
                .ToList();

            return result;
        }

        public async Task<AgentModel> GetByIdAsync(int id)
        {
            var agentEntity = await _agentRepository.GetByIdAsync(id);

            var model = new AgentModel(agentEntity);

            return model;
        }
    }
}
