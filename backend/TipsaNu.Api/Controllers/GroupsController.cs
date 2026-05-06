using MediatR;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.Features.Groups.Queries.GetGroupStandingsByGroupId;
using TipsaNu.Application.Features.Groups.Queries.GetMatchesByGroupId;

namespace TipsaNu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IMediator mediator) : ControllerBase
    {
        // GET /api/groups/{groupId}/matches
        // Retrieves all matches for a specific group.
        [HttpGet("{groupId:int}/matches")]
        public async Task<IActionResult> GetMatchesByGroupId(int groupId, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetMatchesByGroupIdQuery(groupId), cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ErrorMessages?.Any() == true
                    ? BadRequest(result.ErrorMessages)
                    :NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        // GET /api/groups/{groupId}/standings
        // Retrieves the current standings for a specific group.
        [HttpGet("{groupId:int}/standings")]
        public async Task<IActionResult> GetGroupStandings(int groupId, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetGroupStandingsByGroupIdQuery(groupId), cancellationToken);

            if (!result.IsSuccess)
            {
                return result.ErrorMessages?.Any() == true
                    ? BadRequest(result.ErrorMessages)
                    :NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}