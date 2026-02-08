using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.Commands.CreateMyPrediction;
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
