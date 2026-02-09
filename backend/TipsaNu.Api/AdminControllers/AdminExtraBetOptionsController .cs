using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.AddExtraBetOptionCorrectValues;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOption;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOptionCorrectValues;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteExtraBetOptionCorrectValues;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteSingleExtraBetOptionCorrectValue;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.ReplaceExtraBetOptionCorrectValues;
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

        // POST: api/admin/extrabets/options/{optionId}/correct-values
        // Sets the correct values for an ExtraBetOption. If values already exist, frontend should PATCH instead.
        [HttpPost("{optionId}/correct-values")]
        public async Task<IActionResult> CreateExtraBetOptionCorrectValues(
            int optionId,
            [FromBody] SetExtraBetOptionCorrectValuesDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateExtraBetOptionCorrectValuesCommand(optionId, dto), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }


        // PATCH: api/admin/extrabets/options/{optionId}/correct-values
        // Adds correct values for an ExtraBetOption. Frontend should call this to add new values without affecting existing ones.
        [HttpPatch("{optionId}/correct-values")]
        public async Task<IActionResult> AddOrUpdateExtraBetOptionCorrectValues(int optionId, [FromBody] SetExtraBetOptionCorrectValuesDto dto)
        {
            var command = new AddExtraBetOptionCorrectValuesCommand(optionId, dto);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        // DELETE: api/admin/extrabets/options/{optionId}/correct-values
        // Removes all correct values for a given ExtraBetOption. Frontend should call this before POST if they want to replace all values.
        [HttpDelete("{optionId}/correct-values")]
        public async Task<IActionResult> DeleteExtraBetOptionCorrectValues(int optionId)
        {
            var command = new DeleteExtraBetOptionCorrectValuesCommand(optionId);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return NoContent();
        }

        // PUT: api/admin/extrabets/options/{optionId}/correct-values
        // Replaces all correct values for a given ExtraBetOption. This is a combination of DELETE and POST.
        [HttpPut("{optionId}/correct-values")]
        public async Task<IActionResult> ReplaceExtraBetOptionCorrectValues(int optionId, [FromBody] SetExtraBetOptionCorrectValuesDto dto)
        {
            var command = new ReplaceExtraBetOptionCorrectValuesCommand(optionId, dto);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        // DELETE: api/admin/extrabets/options/correct-values/{correctValueId}
        // Deletes a single correct value by its ID. Frontend should call this to remove specific values without affecting others.
        [HttpDelete("correct-values/{correctValueId}")]
        public async Task<IActionResult> DeleteSingleExtraBetOptionCorrectValue(int correctValueId)
        {
            var command = new DeleteSingleExtraBetOptionCorrectValueCommand(correctValueId);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return NoContent();
        }
    }
}