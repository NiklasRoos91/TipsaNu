using TipsaNu.Application.Features.Leagues.DTOs;

namespace TipsaNu.Application.Services.Interfaces
{
    public interface ILeagueMemberService
    {
        Task<LeagueMemberDto> AddMemberWithLeaderboardAsync(int leagueId, int userId, CancellationToken cancellationToken);
    }
}
