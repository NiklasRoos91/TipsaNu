using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.Features.ExtraBets.Commands.CreateExtraBet;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptionCorrectValuesByOptionId;
using TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptionsForUser;
using TipsaNu.Application.Features.ExtraBets.Queries.GetMyExtraBetByOptionId;

namespace TipsaNu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class ExtraBetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExtraBetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /api/extrabets/options?tournamentId=123&me
        // Retrieves all extra bet options for a specific tournament, including the user's current bets.
        [HttpGet("options")]
        public async Task<IActionResult> GetOptionsForUser([FromQuery] int tournamentId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new GetExtraBetOptionsForUserQuery(tournamentId), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        // POST: /api/extrabets/{optionId}/mine
        // Creates a new extra bet for the authenticated user on the specified option.
        [HttpPost("{optionId}/mine")]
        public async Task<IActionResult> CreateExtraBet(int optionId, [FromBody] CreateExtraBetDto dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateExtraBetCommand(optionId, dto), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        // GET /api/extrabets/{optionId}/correct-values
        // 
        [HttpGet("{optionId:int}/correct-values")]
        public async Task<IActionResult> GetCorrectValuesByOptionId(
            int optionId,
            CancellationToken cancellationToken)
        {
            var query = new GetExtraBetOptionCorrectValuesByOptionIdQuery(optionId);
            var result = await _mediator.Send(query, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result.ErrorMessages ?? new List<string> { result.ErrorMessage! });

            return Ok(result.Data);
        }

        // GET /api/extrabets/options/{optionId}/me
        //
        [HttpGet("options/{optionId:int}/me")]
        public async Task<IActionResult> GetMyExtraBetByOptionId(
            int optionId,
            CancellationToken cancellationToken)
        {
            var query = new GetMyExtraBetByOptionIdQuery(optionId);
            var result = await _mediator.Send(query, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result.ErrorMessages ?? new List<string> { result.ErrorMessage! });

            return Ok(result.Data);
        }
    }
}
