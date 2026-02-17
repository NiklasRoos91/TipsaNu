using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptions
{
    public class GetExtraBetOptionsQueryHandler
            : IRequestHandler<GetExtraBetOptionsQuery, OperationResult<List<ExtraBetOptionDto>>>
    {
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly IMapper _mapper;

        public GetExtraBetOptionsQueryHandler(
            IExtraBetRepository extraBetRepository,
            IMapper mapper)
        {
            _extraBetRepository = extraBetRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<ExtraBetOptionDto>>> Handle(
            GetExtraBetOptionsQuery request,
            CancellationToken cancellationToken)
        {
            var entities = await _extraBetRepository.GetExtraBetOptionsAsync(
                request.TournamentId,
                request.Status,
                cancellationToken);

            var dto = _mapper.Map<List<ExtraBetOptionDto>>(entities);

            return OperationResult<List<ExtraBetOptionDto>>.Success(dto);
        }
    }
}
