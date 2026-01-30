using AutoMapper;
using TipsaNu.Application.Features.Tournaments.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Tournaments.Mappers
{
    public class TournamentMappingProfile : Profile
    {
        public TournamentMappingProfile()
        {
            CreateMap<Tournament, TournamentDto>();
        }
    }
}