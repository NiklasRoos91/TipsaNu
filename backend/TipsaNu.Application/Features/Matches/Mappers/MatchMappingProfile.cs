using AutoMapper;
using TipsaNu.Application.Features.Groups.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Matches.Mappers
{
    public class MatchMappingProfile : Profile
    {
        public MatchMappingProfile()
        {
            CreateMap<Match, MatchDto>()
                .ForMember(dest => dest.HomeCompetitorName,
                    opt => opt.MapFrom(src => src.HomeCompetitor.Name))
                .ForMember(dest => dest.AwayCompetitorName,
                    opt => opt.MapFrom(src => src.AwayCompetitor.Name))
                .ForMember(dest => dest.WinnerCompetitorName,
                    opt => opt.MapFrom(src => src.WinnerCompetitor != null ? src.WinnerCompetitor.Name : null));
        }
    }
}