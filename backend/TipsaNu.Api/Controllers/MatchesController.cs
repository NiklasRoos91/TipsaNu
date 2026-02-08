using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.Commands.CreatePrediction;
using TipsaNu.Application.Features.Matches.DTOs;

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

        [HttpPost("{matchId}/predictions")]
        public async Task<IActionResult> CreatePrediction(int matchId, [FromBody] CreatePredictionRequestDto predictionDto)
        {
            var command = new CreatePredictionCommand(predictionDto, matchId);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessages ?? new List<string> { result.ErrorMessage! });

            return Ok(result.Data);
        }
    }
}
