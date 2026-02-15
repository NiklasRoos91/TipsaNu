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

            CreateMap<ExtraBet, ExtraBetForUserDto>();

            CreateMap<ExtraBetOption, ExtraBetOptionForUserDto>()
                .ForMember(dest => dest.Choices,
                    opt => opt.MapFrom(src => src.ExtraBetOptionChoices.Select(c => c.Value).ToList()))
                .ForMember(dest => dest.MyBet,
                    opt => opt.MapFrom(src => src.ExtraBets.FirstOrDefault()));

            CreateMap<CreateExtraBetDto, ExtraBet>()
                .ForMember(dest => dest.SubmittedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<ExtraBetOptionCorrectValue, ExtraBetOptionCorrectValueDto>();
        }
    }
}