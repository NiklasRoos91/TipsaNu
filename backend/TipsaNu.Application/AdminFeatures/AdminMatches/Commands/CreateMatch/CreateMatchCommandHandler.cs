using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Application.Features.Matches.Mappers;
using TipsaNu.Domain.Interfaces;
using Match = TipsaNu.Domain.Entities.Match;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Commands.CreateMatch;

public class CreateMatchCommandHandler(IGenericRepository<Match> genericMatchRepository, IMatchRepository matchRepository) : IRequestHandler<CreateMatchCommand, OperationResult<MatchDto>>
{
    public async Task<OperationResult<MatchDto>> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        var matchEntity = request.Dto.ToEntity();

        await genericMatchRepository.AddAsync(matchEntity, cancellationToken);

        var matchWithDetails = await matchRepository.GetMatchWithCompetitorsAsync(matchEntity.MatchId, cancellationToken);
        if (matchWithDetails == null)
            return OperationResult<MatchDto>.Failure("Match was created but could not be retrieved from the database.");
        
        return OperationResult<MatchDto>.Success(matchWithDetails.ToMatchDto());    
    }
}