using AutoMapper;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Mappers
{
    internal class CreateExtraBetDtoMappingProfile : Profile
    {

        public CreateExtraBetDtoMappingProfile()
        {
            CreateMap<CreateExtraBetOptionDto, ExtraBetOption>()
                .ForMember(dest => dest.OptionId, opt => opt.Ignore())
                .ForMember(dest => dest.AllowCustomChoice, opt => opt.MapFrom(src => src.AllowCustomChoice))
                .ForMember(dest => dest.ExtraBetOptionChoices,
                    opt => opt.MapFrom(src => src.Choices.Select(v => new ExtraBetOptionChoice
                    {
                        Value = v
                    }).ToList()))
                .ForMember(dest => dest.ExtraBetOptionCorrectValues, opt => opt.Ignore());
        }
    }
}
