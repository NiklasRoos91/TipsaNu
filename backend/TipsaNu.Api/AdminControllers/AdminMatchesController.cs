using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.AdminFeatures.AdminMatches.Commands.CreateMatch;
using TipsaNu.Application.AdminFeatures.AdminMatches.Commands.SetMatchResult;
using TipsaNu.Application.AdminFeatures.AdminMatches.Commands.UpdateMatchStatus;
using TipsaNu.Application.AdminFeatures.AdminMatches.DTOs;
using TipsaNu.Application.AdminFeatures.AdminMatches.Queries.GetFilteredCompetitors;
using TipsaNu.Domain.Enums;

namespace TipsaNu.Api.AdminControllers
{
    [Route("api/admin/matches")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminMatchesController(IMediator mediator) : ControllerBase
    {
        // GET: api/admin/matches/competitors
        // Fetches teams/competitors filtered by tournament, optional group and optional search term.
        [HttpGet("competitors")]
        public async Task<IActionResult> GetFilteredCompetitors(
            [FromQuery] int tournamentId,
            [FromQuery] int? groupId,
            [FromQuery] string? searchTerm,
            CancellationToken cancellationToken)
        {
            var query = new GetFilteredCompetitorsQuery(tournamentId, groupId, searchTerm);
            var result = await mediator.Send(query, cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ErrorMessages?.Any() is true 
                    ? BadRequest(result.ErrorMessages) 
                    : BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
        
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
        
        // PUT: api/admin/matches/{matchId}/update-status
        // Markerar en match som officiellt avslutad i systemet.
        [HttpPut("{matchId:int}/update-status")]
        public async Task<IActionResult> FinishMatch(int matchId, [FromQuery] MatchStatusEnum status, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new UpdateMatchStatusCommand(matchId, status), cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ErrorMessages?.Any() is true
                    ? BadRequest(result.ErrorMessages)
                    : BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
