using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Application.Features.Matches.Mappers;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Matches.Queries.GetMatchesByTournamentId
{
    public class GetMatchesByTournamentIdHandler(IGenericRepository<Domain.Entities.Match> matchRepository)
        : IRequestHandler<GetMatchesByTournamentIdQuery, OperationResult<List<MatchDto>>>
    {
        public async Task<OperationResult<List<MatchDto>>> Handle(GetMatchesByTournamentIdQuery request, CancellationToken cancellationToken)
        {
            var matches = (await matchRepository.GetAllAsync(
                cancellationToken,
                m => m.HomeCompetitor,
                m => m.AwayCompetitor
            ))
            .Where(m => m.TournamentId == request.TournamentId)
            .ToList();

            if (matches.Count == 0)
                return OperationResult<List<MatchDto>>.Failure("No matches found for this tournament");

            var dtoList = matches
                .Select(m => m.ToMatchDto())
                .OrderBy(m => m.StartTime)
                .ToList();

            return OperationResult<List<MatchDto>>.Success(dtoList);
        }
    }
}
