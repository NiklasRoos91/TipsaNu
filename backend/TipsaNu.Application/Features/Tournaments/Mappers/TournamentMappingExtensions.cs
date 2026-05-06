using TipsaNu.Application.Features.Tournaments.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Tournaments.Mappers;

public static class TournamentMappingExtensions
{
    public static TournamentDto ToTournamentDto(this Tournament entity)
    {
        return new TournamentDto
        {
            TournamentId = entity.TournamentId,
            Name = entity.Name,
            StartsAt = entity.StartsAt,
            Status = entity.Status
        };
    }
}