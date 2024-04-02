using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showtime.Application.Commands.Tenant;
using Showtime.Application.Models.Tenant;
using Showtime.Core.Models;

namespace Showtime.API.Controllers.Tenant
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Tenant")]
    public class TenantController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TenantLoginRequest request)
        {
            ServiceResponse<string?> response = await _mediator.Send(new TenantLoginCommand(request));

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] TenantRegisterRequest request)
        {
            ServiceResponse<string?> response = await _mediator.Send(new TenantRegisterCommand(request));

            if (response.Success)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Message);
        }
    }
}
