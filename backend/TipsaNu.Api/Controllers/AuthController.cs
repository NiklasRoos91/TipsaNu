using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TipsaNu.Application.Feature.Auth.Commands.Register;
using TipsaNu.Application.Features.Auth.Commands.Login;
using TipsaNu.Application.Features.Auth.Commands.RefreshToken;
using TipsaNu.Application.Features.Auth.DTOs;

namespace TipsaNu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(request);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessages);

            return Ok(result.Data);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(request);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return Unauthorized(result.ErrorMessages);

            return Ok(result.Data);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new RefreshTokenCommand(request), cancellationToken);

            if (!result.IsSuccess)
                return Unauthorized(result.ErrorMessages);

            return Ok(result.Data);
        }
    }
}
