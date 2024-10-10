using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Exam.Emite.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    [AllowAnonymous]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerModel> _validator;

        public CustomerController(ICustomerService customerService, IValidator<CustomerModel> validator)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet("")]
        [AllowAnonymous]
        //  [Authorize(Policy = nameof(RolePolicyConstants.All))]
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var data = await _customerService.GetAllAsync();
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
                var data = await _customerService.GetByIdAsync(id);
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
        public async Task<ActionResult> EnsureAsync([FromBody] CustomerModel model)
        {
            try
            {
                if (await _validator.IsValidAsync(model))
                {
                    var data = await _customerService.EnsureAsync(0, model);
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
                await _customerService.DeleteAsync(0, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
