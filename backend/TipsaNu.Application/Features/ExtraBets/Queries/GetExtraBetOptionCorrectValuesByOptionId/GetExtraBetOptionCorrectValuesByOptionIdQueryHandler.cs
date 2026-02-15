using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptionCorrectValuesByOptionId
{
    public class GetExtraBetOptionCorrectValuesByOptionIdQueryHandler
    : IRequestHandler<GetExtraBetOptionCorrectValuesByOptionIdQuery, OperationResult<List<ExtraBetOptionCorrectValueDto>>>
    {
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly IMapper _mapper;

        public GetExtraBetOptionCorrectValuesByOptionIdQueryHandler(
            IExtraBetRepository extraBetRepository,
            IMapper mapper)
        {
            _extraBetRepository = extraBetRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<ExtraBetOptionCorrectValueDto>>> Handle(
            GetExtraBetOptionCorrectValuesByOptionIdQuery request,
            CancellationToken cancellationToken)
        {
            var entities = await _extraBetRepository
                            .GetCorrectValuesByOptionIdAsync(request.OptionId, cancellationToken);

            if (entities == null || entities.Count == 0)
                return OperationResult<List<ExtraBetOptionCorrectValueDto>>
                    .Failure("No correct values found for this option");

            var dto = _mapper.Map<List<ExtraBetOptionCorrectValueDto>>(entities);

            return OperationResult<List<ExtraBetOptionCorrectValueDto>>.Success(dto);
        }
    }
}
