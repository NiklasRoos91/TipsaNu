using  TipsaNu.Domain.Entities;
using TipsaNu.Application.Features.Groups.DTOs;

namespace TipsaNu.Application.Features.Groups.Mappers;

public static class GroupMappingExtensions
{
    public static GroupDto ToDto(this Group entity)
    {
        return new GroupDto
        {
            GroupId = entity.GroupId,
            TournamentId = entity.TournamentId,
            Name = entity.Name
        };
    }
    
    public static GroupStandingDto ToDto(this GroupStanding entity)
    {
        return new GroupStandingDto
        {
            CompetitorId = entity.CompetitorId,
            CompetitorName = entity.Competitor?.Name ?? "Unknown", 
            Played = entity.Played,
            Won = entity.Won,
            Draw = entity.Draw,
            Lost = entity.Lost,
            GoalsFor = entity.GoalsFor,
            GoalsAgainst = entity.GoalsAgainst,
            Points = entity.Points,
            Rank = entity.Rank
        };
    }
}