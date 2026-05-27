using MediatR;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.Feature.Auth.Commands.Register;
using TipsaNu.Application.Features.Auth.Commands.Login;
using TipsaNu.Application.Features.Auth.Commands.Logout;
using TipsaNu.Application.Features.Auth.Commands.RefreshToken;
using TipsaNu.Application.Features.Auth.DTOs;
using TipsaNu.Application.Features.Auth.Queries.GetMe;

namespace TipsaNu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        // Post: /api/auth/register
        // Register a new user with email, userName and password, return JWT token if successful, otherwise return 400 Bad Request
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request, CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(request);
            var result = await mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessages);

            return Ok(result.Data);
        }

        // Post: /api/auth/login
        // Login with email and password, return JWT token if successful, otherwise return 401 Unauthorized
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(request);
            var result = await mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return Unauthorized(result.ErrorMessages);

            return Ok(result.Data);
        }

        // Post: /api/auth/refresh
        // Refresh JWT token using refresh token, return new JWT token if successful, otherwise return 401 Unauthorized
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new RefreshTokenCommand(request), cancellationToken);

            if (!result.IsSuccess)
                return Unauthorized(result.ErrorMessages);

            return Ok(result.Data);
        }

        // Get: /api/auth/me
        // 
        [HttpGet("me")]
        public async Task<IActionResult> Me(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetMeQuery(), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result.Data);
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new LogoutCommand(request), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessages);

            return Ok();
        }
    }
}
