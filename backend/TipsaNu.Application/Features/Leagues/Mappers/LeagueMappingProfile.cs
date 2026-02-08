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
        }
    }
}
