using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Leagues.Mappers
{
    public static class LeagueMappingExtensions
    {
        public static LeagueWithLeaderboardDto ToLeagueWithLeaderboardDto(this League entity)
        {
            return new LeagueWithLeaderboardDto
            {
                LeagueId = entity.LeagueId,
                Name = entity.Name,
                Description = entity.Description,
                InvitationCode = entity.InvitationCode,
                MaxMembers = entity.MaxMembers,
                CurrentMembers = entity.Members?.Count ?? 0,
                AdminUserId = entity.AdminUserId,
                AdminUsername = entity.AdminUser?.UserName ?? "Okänd",
                
                Leaderboard = entity.Members?
                    .Select(m => m.ToLeaderboardEntryDto())
                    .OrderByDescending(m => m.TotalPoints)
                    .ToList() ?? new List<LeaderboardEntryDto>()
            };
        }

        private static LeaderboardEntryDto ToLeaderboardEntryDto(this LeagueMember member)
        {
            return new LeaderboardEntryDto
            {
                LeagueMemberId = member.LeagueMemberId,
                UserId = member.UserId,
                UserName = member.User?.UserName ?? "Unknown",
                TotalPoints = member.LeaderboardEntry?.TotalPoints ?? 0
            };
        }
        
        public static LeagueDto ToLeagueDto(this League entity)
        {
            return new LeagueDto
            {
                LeagueId = entity.LeagueId,
                TournamentId = entity.TournamentId,
                Name = entity.Name,
                Description = entity.Description,
                InvitationCode = entity.InvitationCode,
                AdminUserId = entity.AdminUserId,
                MaxMembers = entity.MaxMembers
            };
        }
        
        
    }
}