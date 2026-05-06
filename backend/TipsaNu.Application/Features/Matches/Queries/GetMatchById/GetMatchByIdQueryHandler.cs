using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Application.Features.Matches.Mappers;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Matches.Queries.GetMatchById
{
    public class GetMatchByIdQueryHandler(IGenericRepository<Match> matchRepository)
        : IRequestHandler<GetMatchByIdQuery, OperationResult<MatchDto>>
    {
        public async Task<OperationResult<MatchDto>> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
        {
            var match = await matchRepository.GetByIdAsync(request.MatchId, cancellationToken);

            if (match == null)
                return OperationResult<MatchDto>.Failure("Match not found");

            return OperationResult<MatchDto>.Success(match.ToMatchDto());
        }
    }
}