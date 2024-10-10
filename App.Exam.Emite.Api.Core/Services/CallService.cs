using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Drawing.Printing;

namespace App.Exam.Emite.Api.Core.Services
{
    public class CallService : ICallService
    {
        private readonly ILogger<CallService> _logger;
        private readonly ICallRepository _callRepository;

        public CallService( ICallRepository callRepository)
        {
            _callRepository = callRepository ?? throw new ArgumentNullException(nameof(callRepository));
        }

        public async Task DeleteAsync(int currentUserId, int id)
        {
            await _callRepository.DeleteAsync(currentUserId, id);
        }

        public async Task<CallModel> EnsureAsync(int currentUserId, CallModel model)
        {
            var entity = new Call();
            if (model.Id != 0)
            {
                entity = await _callRepository.GetByIdAsync(model.Id);
            }

            model.ToEntity(entity);

            entity = await _callRepository.EnsureAsync(currentUserId, entity);
            model.Id = entity.Id;

            return model;
        }

        public async Task<(List<CallModel>,int)> GetAllAsync(int page, int pageSize)
        {
            var calls = (await _callRepository.GetAllAsync());

            var result = calls
                .Select(x => new CallModel(x))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (result, calls.Count);
        }

        public async Task<CallModel> GetByIdAsync(int id)
        {
            var callEntity = await _callRepository.GetByIdAsync(id);

            var model = new CallModel(callEntity);

            return model;
        }

        public async Task<(List<CallModel>, int)> SearchAsync(CallSearchModel callSearchModel,int page, int pageSize)
        {
            var searchEntity = new CallSearchEntity();

            callSearchModel.ToEntity(searchEntity);

            var calls = (await _callRepository.SearchAsync(searchEntity));

            var result = calls

                .Select(x => new CallModel(x))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (result, calls.Count);
        }
    }
}
