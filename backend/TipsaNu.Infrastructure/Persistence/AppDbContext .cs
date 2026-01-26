using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Presistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<TournamentTemplate> TournamentTemplates => Set<TournamentTemplate>();
        public DbSet<Tournament> Tournaments => Set<Tournament>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<Prediction> Predictions => Set<Prediction>();
        public DbSet<PointRule> PointRules => Set<PointRule>();
        public DbSet<League> Leagues => Set<League>();
        public DbSet<LeagueMember> LeagueMembers => Set<LeagueMember>();
        public DbSet<LeaderboardEntry> LeaderboardEntries => Set<LeaderboardEntry>();
        public DbSet<Competitor> Competitors => Set<Competitor>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<GroupCompetitor> GroupCompetitors => Set<GroupCompetitor>();
        public DbSet<GroupStanding> GroupStandings => Set<GroupStanding>();
        public DbSet<TournamentTiebreaker> TournamentTiebreakers => Set<TournamentTiebreaker>();
        public DbSet<ExtraBetOption> ExtraBetOptions => Set<ExtraBetOption>();
        public DbSet<ExtraBet> ExtraBets => Set<ExtraBet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite keys / relationships
            modelBuilder.Entity<LeaderboardEntry>()
                .HasKey(l => l.LeagueMemberId);

            modelBuilder.Entity<LeagueMember>()
                .HasOne(lm => lm.LeaderboardEntry)
                .WithOne(lb => lb.LeagueMember)
                .HasForeignKey<LeaderboardEntry>(lb => lb.LeagueMemberId);
        }
    }
}
