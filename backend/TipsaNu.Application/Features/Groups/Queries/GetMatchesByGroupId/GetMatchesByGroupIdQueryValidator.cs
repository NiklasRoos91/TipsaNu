using FluentValidation;

namespace TipsaNu.Application.Features.Groups.Queries.GetMatchesByGroupId
{
    public class GetMatchesByGroupIdQueryValidator : AbstractValidator<GetMatchesByGroupIdQuery>
    {
        public GetMatchesByGroupIdQueryValidator()
        {
            Console.WriteLine("Validator created!");
            RuleFor(x => x.GroupId)
                .GreaterThan(0)
                .WithMessage("GroupId must be a positive integer.");
        }
    }
}