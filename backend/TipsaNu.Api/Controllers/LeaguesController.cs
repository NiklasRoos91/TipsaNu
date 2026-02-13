using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.Features.Leagues.Commands.CreateLeague;
using TipsaNu.Application.Features.Leagues.Commands.JoinLeague;
using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Application.Features.Leagues.Queries.GetLeagueDetails;
using TipsaNu.Application.Features.Leagues.Queries.GetMyLeaguesInTournament;

namespace TipsaNu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaguesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaguesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/leagues
        // Creates a new league based on the provided data.
        [HttpPost]
        public async Task<IActionResult> CreateLeague([FromBody] CreateLeagueDto dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateLeagueCommand(dto), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        // GET: api/leagues/{tournamentId}/leagues/me
        // Retrieves the leagues that the current user is a member of within a specific tournament.
        [HttpGet("{tournamentId}/leagues/me")]
        public async Task<IActionResult> GetMyLeagues(int tournamentId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetMyLeaguesInTournamentQuery(tournamentId),
                cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }


        // POST: api/leagues/{tournamentId}/join
        // Allows a user to join a league using an invitation code.
        [HttpPost("{tournamentId}/join")]
        public async Task<IActionResult> JoinLeague(int tournamentId, [FromBody] JoinLeagueDto dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new JoinLeagueCommand(tournamentId, dto), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        // 	GET /api/leagues/{leagueId}
        // Retrieves detailed information about a specific league, including its leaderboard.
        [HttpGet("{leagueId}")]
        public async Task<IActionResult> GetLeagueDetails(int leagueId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetLeagueDetailsQuery(leagueId), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }
    }
}
