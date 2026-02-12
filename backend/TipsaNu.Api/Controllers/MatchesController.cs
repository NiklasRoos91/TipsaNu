using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.Features.Matches.Commands.CreateMyPrediction;
using TipsaNu.Application.Features.Matches.Queries.GetMatchById;
using TipsaNu.Application.Features.Predictions.DTOs;

namespace TipsaNu.Api.Controllers
{
    [ApiController]
    [Route("api/matches")]
    [Authorize]
    public class MatchesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MatchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET /api/matches/{matchId}
        [HttpGet("{matchId}")]
        public async Task<IActionResult> GetMatchById(int matchId, CancellationToken cancellationToken)
        {
            var query = new GetMatchByIdQuery(matchId);
            var result = await _mediator.Send(query, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result.ErrorMessages ?? new List<string> { result.ErrorMessage! });

            return Ok(result.Data);
        }

        // POST /api/matches/{matchId}/predictions/mine
        // Creates the authenticated user's prediction for a specific match.
        [HttpPost("{matchId}/predictions/mine")]
        public async Task<IActionResult> CreateMyPrediction(int matchId, [FromBody] CreateMyPredictionRequestDto predictionDto, CancellationToken cancellationToken)
        {
            var command = new CreateMyPredictionCommand(predictionDto, matchId);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessages ?? new List<string> { result.ErrorMessage! });

            return Ok(result.Data);
        }
    }
}
