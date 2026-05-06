using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Application.Services.Interfaces;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Services.Implementations
{
    public class LeagueMemberService(ILeagueRepository leagueRepository) : ILeagueMemberService
    {
        public async Task<LeagueMemberDto> AddMemberWithLeaderboardAsync(int leagueId, int userId, CancellationToken cancellationToken)
        {
            var member = new LeagueMember
            {
                LeagueId = leagueId,
                UserId = userId,
                JoinedAt = DateTime.UtcNow
            };
            await leagueRepository.AddLeagueMemberAsync(member, cancellationToken);

            var leaderboardEntry = new LeaderboardEntry
            {
                LeagueMemberId = member.LeagueMemberId,
                TotalPoints = 0,
                LastUpdated = DateTime.UtcNow
            };
            await leagueRepository.AddLeaderboardEntryAsync(leaderboardEntry, cancellationToken);

            return new LeagueMemberDto
            {
                LeagueMemberId = member.LeagueMemberId,
                LeagueId = member.LeagueId,
                UserId = member.UserId,
                JoinedAt = member.JoinedAt
            };
        }
    }
}
