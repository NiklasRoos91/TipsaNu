using MediatR;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.Features.Groups.Queries;
using TipsaNu.Application.Features.Tournaments.Queries.GetAll;
using TipsaNu.Application.Features.Tournaments.Queries.GetById;

namespace TipsaNu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TournamentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET /api/Tournaments
        // Retrieves all tournaments.
        [HttpGet]
        public async Task<IActionResult> GetAllTournaments()
        {
            var result = await _mediator.Send(new GetAllTournamentsQuery());

            if (!result.IsSuccess)
                return BadRequest(new { errors = result.ErrorMessages ?? new List<string> { result.ErrorMessage ?? "Unknown error" } });


            return Ok(result.Data);
        }

        // GET /api/Tournaments/{id}
        // Retrieves a specific tournament by its Id.
        [HttpGet("{tournamentId:int}")]
        public async Task<IActionResult> GetTournamentById(int tournamentId)
        {
            var result = await _mediator.Send(new GetTournamentByIdQuery(tournamentId));

            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        // GET /api/tournaments/{tournamentId}/groups
        // Retrieves all groups for a specific tournament.
        [HttpGet("{tournamentId:int}/groups")]
        public async Task<IActionResult> GetGroupsByTournament(int tournamentId)
        {
            var result = await _mediator.Send(new GetGroupsByTournamentQuery(tournamentId));

            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }
    }
}