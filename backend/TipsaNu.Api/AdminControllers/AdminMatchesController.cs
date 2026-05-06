using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.AdminFeatures.AdminMatches.Commands.CreateMatch;
using TipsaNu.Application.AdminFeatures.AdminMatches.Commands.SetMatchResult;
using TipsaNu.Application.AdminFeatures.AdminMatches.DTOs;

namespace TipsaNu.Api.AdminControllers
{
    [Route("api/admin/matches")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminMatchesController(IMediator mediator) : ControllerBase
    {
        // POST: api/admin/matches
        // // Creates a new match between two competitors.
        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new CreateMatchCommand(dto), cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ErrorMessages?.Any() is true 
                    ? BadRequest(result.ErrorMessages) 
                    : BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // PUT: api/admin/matches/{matchId}/result
        // Sets the final score and determines the winner for a specific match.
        [HttpPut("{matchId:int}/result")]
        public async Task<IActionResult> SetMatchResult(int matchId, [FromBody] SetMatchResultDto dto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new SetMatchResultCommand(matchId, dto), cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ErrorMessages?.Any() is true
                    ? BadRequest(result.ErrorMessages)
                    :NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
