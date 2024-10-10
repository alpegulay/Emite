using App.Exam.Emite.Api.Core;
using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.Exam.Emite.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Calls")]
    public class CallController : Controller
    {
        private readonly ICallService _callService;
        private readonly IValidator<CallModel> _validator;
        private readonly IMemoryCache _memoryCache;

        public CallController(ICallService callService, IValidator<CallModel> validator, IMemoryCache memoryCache)
        {
            _callService = callService ?? throw new ArgumentNullException(nameof(callService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _memoryCache = memoryCache;
        }

        [HttpGet("")]
        [AllowAnonymous]
        //  [Authorize(Policy = nameof(RolePolicyConstants.All))]
        public async Task<ActionResult> GetAllAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }
                var cacheKey = $"CallItems_Page_{page}_PageSize_{pageSize}";

                if (!_memoryCache.TryGetValue(cacheKey, out PaginatedResponse<CallModel> cachedCalls))
                {
                    var (data, dataCount) = await _callService.GetAllAsync(page, pageSize);

                    var paginatedResponse = new PaginatedResponse<CallModel>
                    {
                        Page = page,
                        PageSize = pageSize,
                        TotalRecords = dataCount,
                        Data = data
                    };

                    _memoryCache.Set(cacheKey, paginatedResponse, AppSettings.GetCacheOption());
                    return Ok(paginatedResponse);
                }

                return Ok(cachedCalls);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Search")]
        [AllowAnonymous]
        //  [Authorize(Policy = nameof(RolePolicyConstants.All))]
        public async Task<ActionResult> SearchAsync(CallSearchModel callSearchModel, int page = 1, int pageSize = 10)
        {
            try
            {
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest("Page number and page size must be greater than zero.");
                }

                var (data, dataCount) = await _callService.SearchAsync(callSearchModel,page, pageSize);

                var paginatedResponse = new PaginatedResponse<CallModel>
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalRecords = dataCount,
                    Data = data
                };

                    return Ok(paginatedResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        // [Authorize(Policy = nameof(RolePolicyConstants.All))]
        public async Task<ActionResult> GetByEmployeeIdAsync(int id)
        {
            try
            {
                var data = await _callService.GetByIdAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Ensure")]
        [AllowAnonymous]
        // [Authorize(Policy = nameof(RolePolicyConstants.All))]
        public async Task<ActionResult> EnsureAsync([FromBody] CallModel model)
        {
            try
            {
                if (await _validator.IsValidAsync(model))
                {
                    var data = await _callService.EnsureAsync(0, model);
                    return Ok(data);
                }

                return BadRequest(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        // [Authorize(Policy = nameof(RolePolicyConstants.Admin))]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _callService.DeleteAsync(0, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
