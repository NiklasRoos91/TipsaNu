using AutoMapper;
using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Leagues.Mappers
{
    public class LeagueMappingProfile : Profile
    {
        public LeagueMappingProfile()
        {
            CreateMap<League, LeagueDto>();

            CreateMap<LeagueMember, LeaderboardEntryDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));

            CreateMap<League, LeagueWithLeaderboardDto>()
                .ForMember(dest => dest.CurrentMembers, opt => opt.MapFrom(src => src.Members.Count))
                .ForMember(dest => dest.Leaderboard, opt => opt.MapFrom(src => src.Members.OrderByDescending(m => m.LeaderboardEntry.TotalPoints)))
                .ForMember(dest => dest.AdminUsername, opt => opt.MapFrom(src => src.AdminUser.Username));
        }
    }
}
