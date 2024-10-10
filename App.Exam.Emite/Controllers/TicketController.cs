using App.Exam.Emite.Api.Core;
using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Models.Pagination;
using App.Exam.Emite.Api.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.Exam.Emite.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Tickets")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IValidator<TicketModel> _validator;
        private readonly IMemoryCache _memoryCache;

        public TicketController(ITicketService ticketService, IValidator<TicketModel> validator, IMemoryCache memoryCache)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
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

                var cacheKey = $"TicketItems_Page_{page}_PageSize_{pageSize}";

                if (!_memoryCache.TryGetValue(cacheKey, out PaginatedResponse<TicketModel> cacheTickets))
                {
                    var (data, dataCount) = await _ticketService.GetAllAsync(page, pageSize);

                    var paginatedResponseData = new PaginatedResponse<TicketModel>
                    {
                        Page = page,
                        PageSize = pageSize,
                        TotalRecords = dataCount,
                        Data = data
                    };

                    // Store the result in the cache
                    _memoryCache.Set(cacheKey, paginatedResponseData, AppSettings.GetCacheOption());

                    return Ok(paginatedResponseData);
                }
                return Ok(cacheTickets);
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
                var data = await _ticketService.GetByIdAsync(id);
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
        public async Task<ActionResult> EnsureAsync([FromBody] TicketModel model)
        {
            try
            {
                if (await _validator.IsValidAsync(model))
                {
                    var data = await _ticketService.EnsureAsync(0, model);
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
                await _ticketService.DeleteAsync(0, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
