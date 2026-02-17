using AutoMapper;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.ExtraBets.Mappers
{
    public class ExtraBetMappingProfile : Profile
    {
        public ExtraBetMappingProfile()
        {
            CreateMap<ExtraBetOption, ExtraBetOptionDto>()
                .ForMember(dest => dest.Choices,
                    opt => opt.MapFrom(src =>
                        src.ExtraBetOptionChoices.Select(c => c.Value).ToList()
                    )
                )
                .ForMember(dest => dest.AllowCustomChoice, opt => opt.MapFrom(src => src.AllowCustomChoice));

            CreateMap<CreateExtraBetDto, ExtraBet>()
                .ForMember(dest => dest.SubmittedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<ExtraBetOptionCorrectValue, ExtraBetOptionCorrectValueDto>();

            CreateMap<ExtraBet, ExtraBetDto>();

            CreateMap<ExtraBetOptionChoice, ExtraBetOptionChoiceDto>();

            CreateMap<ExtraBetOption, ExtraBetOptionDto>()
                .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.ExtraBetOptionChoices))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}