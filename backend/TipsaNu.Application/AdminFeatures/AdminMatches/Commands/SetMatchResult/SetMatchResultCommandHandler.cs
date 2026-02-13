using AutoMapper;
using MediatR;
using TipsaNu.Application.AdminFeatures.AdminMatches.Events.MatchResultUpdated;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Commands.SetMatchResult
{
    public class SetMatchResultCommandHandler : IRequestHandler<SetMatchResultCommand, OperationResult<MatchDto>>
    {
        private readonly IGenericRepository<Match> _matchRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public SetMatchResultCommandHandler(IGenericRepository<Match> matchRepository, IMapper mapper, IMediator mediator)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<OperationResult<MatchDto>> Handle(SetMatchResultCommand request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(request.MatchId, cancellationToken);
            if (match == null)
                return OperationResult<MatchDto>.Failure("Match not found");

            // TODO: Only allow setting the result if the match has started or is finished.
            // Currently, any match can have its score set. Later we can add a check using match.StartTime 
            // or match.Status to prevent updating scores for matches that haven't been played yet.

            if (request.Dto.ScoreHome.HasValue)
                match.ScoreHome = request.Dto.ScoreHome;

            if (request.Dto.ScoreAway.HasValue)
                match.ScoreAway = request.Dto.ScoreAway;

            if (match.ScoreHome.HasValue && match.ScoreAway.HasValue)
            {
                if (match.ScoreHome > match.ScoreAway)
                    match.WinnerCompetitorId = match.HomeCompetitorId;
                else if (match.ScoreAway > match.ScoreHome)
                    match.WinnerCompetitorId = match.AwayCompetitorId;
                else
                    match.WinnerCompetitorId = null;
            }

            match.Status = MatchStatusEnum.Finished;

            await _matchRepository.UpdateAsync(match, cancellationToken);

            await _mediator.Publish(new MatchResultUpdatedEvent(match.MatchId), cancellationToken);

            var dto = _mapper.Map<MatchDto>(match);
            return OperationResult<MatchDto>.Success(dto);
        }
    }
}
