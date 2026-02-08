using MediatR;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.Features.Predictions.Queries.GetMyPredictionForMatch;
using TipsaNu.Application.Features.Predictions.Queries.GetMyPredictions;

namespace TipsaNu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PredictionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET /api/predictions/me
        // Get all predictions of the currently authenticated user
        [HttpGet("me")]
        public async Task<IActionResult> GetMyPredictions(CancellationToken cancellationToken)
        {
            var query = new GetMyPredictionsQuery();
            var result = await _mediator.Send(query, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessages ?? new List<string> { result.ErrorMessage! });

            return Ok(result.Data);
        }

        // GET /api/predictions/{matchId}/me
        // Get the prediction of the currently authenticated user for a specific match
        [HttpGet("{matchId}/me")]
        public async Task<IActionResult> GetMyPredictionForMatch(int matchId)
        {
            var query = new GetMyPredictionForMatchQuery(matchId);
            var result = await _mediator.Send(query);

            if (!result.IsSuccess)
                return NotFound(result.ErrorMessage);

            return Ok(result.Data);
        }
    }
}
