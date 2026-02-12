using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Matches.Queries.GetMatchById
{
    public class GetMatchByIdQueryHandler : IRequestHandler<GetMatchByIdQuery, OperationResult<MatchDto>>
    {
        private readonly IGenericRepository<Match> _matchRepository;
        private readonly IMapper _mapper;

        public GetMatchByIdQueryHandler(IGenericRepository<Match> matchRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<MatchDto>> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetByIdAsync(request.MatchId, cancellationToken);

            if (match == null)
                return OperationResult<MatchDto>.Failure("Match not found");

            var dto = _mapper.Map<MatchDto>(match);
            return OperationResult<MatchDto>.Success(dto);
        }
    }
}