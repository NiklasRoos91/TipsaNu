using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {

        private readonly AppDbContext _context;

        public LeagueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<League>> GetByUserIdAsync(int userId, int tournamentId, CancellationToken cancellationToken = default)
        {
            return await _context.Leagues
                .Where(l => l.TournamentId == tournamentId && l.Members.Any(m => m.UserId == userId))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> InvitationCodeExistsAsync(int tournamentId, string invitationCode, CancellationToken cancellationToken = default)
        {
            return await _context.Leagues
                .AnyAsync(l => l.TournamentId == tournamentId && l.InvitationCode == invitationCode, cancellationToken);
        }
        public async Task<List<League>> GetLeaguesForUserInTournamentAsync(int tournamentId, int userId, CancellationToken cancellationToken)
        {
            return await _context.Leagues
                .AsNoTracking()
                .Where(l => l.TournamentId == tournamentId)
                .Where(l => l.Members.Any(m => m.UserId == userId))
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync(cancellationToken);
        }
        public async Task<League?> GetLeagueByTournamentIdAndInvitationCodeAsync(int tournamentId, string invitationCode, CancellationToken cancellationToken = default)
        {
            var normalizedCode = invitationCode.Trim().ToUpperInvariant();

            return await _context.Leagues
                .AsNoTracking() 
                .FirstOrDefaultAsync(
                    l => l.TournamentId == tournamentId &&
                         l.InvitationCode.ToUpper() == normalizedCode,
                    cancellationToken
                );
        }

        public async Task<bool> IsUserMemberAsync(int leagueId, int userId, CancellationToken cancellationToken = default)
        {
            return await _context.LeagueMembers.AnyAsync(lm => lm.LeagueId == leagueId && lm.UserId == userId, cancellationToken);
        }

        public async Task<int> GetMemberCountAsync(int leagueId, CancellationToken cancellationToken = default)
        {
            return await _context.LeagueMembers.CountAsync(lm => lm.LeagueId == leagueId, cancellationToken);
        }

        public async Task<LeagueMember> AddLeagueMemberAsync(LeagueMember member, CancellationToken cancellationToken = default)
        {
            await _context.LeagueMembers.AddAsync(member, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return member;
        }

        public async Task<LeaderboardEntry> AddLeaderboardEntryAsync(LeaderboardEntry entry, CancellationToken cancellationToken = default)
        {
            await _context.LeaderboardEntries.AddAsync(entry, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entry;
        }
    }
}
