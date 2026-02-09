using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOption;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;

namespace TipsaNu.Api.AdminControllers
{
    [Route("api/admin/extrabets/options")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminExtraBetOptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminExtraBetOptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: api/admin/extrabets/options
        // Creates a new extra bet option for a tournament or match.
        [HttpPost()]
        public async Task<IActionResult> CreateExtraBetOption([FromBody] CreateExtraBetOptionDto dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateExtraBetOptionCommand(dto), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }
    }
}