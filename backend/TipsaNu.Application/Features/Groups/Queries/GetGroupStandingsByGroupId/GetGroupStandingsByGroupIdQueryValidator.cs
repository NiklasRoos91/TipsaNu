using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipsaNu.Application.Features.Groups.Queries.GetGroupStandingsByGroupId
{
    public class GetGroupStandingsByGroupIdQueryValidator : AbstractValidator<GetGroupStandingsByGroupIdQuery>
    {
        public GetGroupStandingsByGroupIdQueryValidator()
        {
            RuleFor(x => x.GroupId)
                .GreaterThan(0)
                .WithMessage("GroupId must be a positive integer.");
        }
    }
}
