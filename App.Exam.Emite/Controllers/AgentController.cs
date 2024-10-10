using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace App.Exam.Emite.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Agents")]
    [AllowAnonymous]
    public class AgentController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IValidator<AgentModel> _validator;
        public AgentController(IAgentService agentService, IValidator<AgentModel> validator)
        {
            _agentService = agentService ?? throw new ArgumentNullException(nameof(agentService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet("")]
        [AllowAnonymous]
        //  [Authorize(Policy = nameof(RolePolicyConstants.All))]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var data = await _agentService.GetAllAsync();
                return Ok(data);
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
                var data = await _agentService.GetByIdAsync(id);
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
        public async Task<ActionResult> EnsureAsync([FromBody] AgentModel model)
        {
            try
            {
                if (await _validator.IsValidAsync(model))
                {
                    var data = await _agentService.EnsureAsync(0, model);
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
                await _agentService.DeleteAsync(0, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
