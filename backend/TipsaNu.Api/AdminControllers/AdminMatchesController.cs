using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.AdminFeatures.AdminMatches.Commands.SetMatchResult;
using TipsaNu.Application.AdminFeatures.AdminMatches.DTOs;

namespace TipsaNu.Api.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminMatchesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminMatchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // PUT: api/AdminMatches/{matchId}/result
        // 
        [HttpPut("{matchId}/result")]
        public async Task<IActionResult> SetMatchResult(int matchId, [FromBody] SetMatchResultDto dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new SetMatchResultCommand(matchId, dto), cancellationToken);

            if (!result.IsSuccess)
            {
                if (result.ErrorMessages?.Any() == true)
                    return BadRequest(result.ErrorMessages);

                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
