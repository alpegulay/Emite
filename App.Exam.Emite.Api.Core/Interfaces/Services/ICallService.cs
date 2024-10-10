using App.Exam.Emite.Api.Core.Models;

namespace App.Exam.Emite.Api.Core.Interfaces.Services
{
    public interface ICallService
    {
        Task<(List<CallModel>,int)> GetAllAsync(int page, int pageSize);
        Task<CallModel> GetByIdAsync(int id);
        Task<CallModel> EnsureAsync(int currentUserId, CallModel model);
        Task DeleteAsync(int currentUserId, int id);

        Task<(List<CallModel>, int)> SearchAsync(CallSearchModel callSearchModel,int page, int pageSize);
    }
}
