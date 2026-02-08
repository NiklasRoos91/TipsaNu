using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Get all predictions of the currently authenticated user
        /// GET /api/predictions/me
        /// </summary>
        [HttpGet("me")]
        public async Task<IActionResult> GetMyPredictions(CancellationToken cancellationToken)
        {
            var query = new GetMyPredictionsQuery();
            var result = await _mediator.Send(query, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessages ?? new List<string> { result.ErrorMessage! });

            return Ok(result.Data);
        }
    }
}
