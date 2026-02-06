using MediatR;
using Microsoft.AspNetCore.Mvc;
using TipsaNu.Application.Features.Groups.Queries.GetMatchesByGroupId;

namespace TipsaNu.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET /api/groups/{groupId}/matches
        // Retrieves all matches for a specific group.
        [HttpGet("{groupId:int}/matches")]
        public async Task<IActionResult> GetMatchesByGroupId(int groupId)
        {
            var result = await _mediator.Send(new GetMatchesByGroupIdQuery(groupId));

            if (!result.IsSuccess)
            {
                if (result.ErrorMessages?.Any() == true)
                    return BadRequest(result.ErrorMessages);

                return NotFound(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}