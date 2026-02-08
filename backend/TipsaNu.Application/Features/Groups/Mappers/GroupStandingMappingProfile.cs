using AutoMapper;
using TipsaNu.Application.Features.Groups.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Groups.Mappers
{
    public class GroupStandingMappingProfile : Profile
    {
        public GroupStandingMappingProfile()
        {
            CreateMap<GroupStanding, GroupStandingDto>()
                .ForMember(dest => dest.CompetitorName,
                    opt => opt.MapFrom(src => src.Competitor.Name));
        }
    }
}