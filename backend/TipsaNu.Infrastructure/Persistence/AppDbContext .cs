using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Infrastructure.Data.Configurations;

namespace TipsaNu.Infrastructure.Presistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
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
        public DbSet<ExtraBetOptionChoice> ExtraBetOptionChoices => Set<ExtraBetOptionChoice>();
        public DbSet<ExtraBetOptionCorrectValue> ExtraBetOptionCorrectValues => Set<ExtraBetOptionCorrectValue>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TournamentTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new PointRuleConfiguration());
            modelBuilder.ApplyConfiguration(new TournamentConfiguration());
            modelBuilder.ApplyConfiguration(new TournamentTiebreakerConfiguration());
            modelBuilder.ApplyConfiguration(new MatchConfiguration());
            modelBuilder.ApplyConfiguration(new PredictionConfiguration());
            modelBuilder.ApplyConfiguration(new LeagueConfiguration());
            modelBuilder.ApplyConfiguration(new LeagueMemberConfiguration());
            modelBuilder.ApplyConfiguration(new LeaderboardEntryConfiguration());
            modelBuilder.ApplyConfiguration(new CompetitorConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new GroupCompetitorConfiguration());
            modelBuilder.ApplyConfiguration(new GroupStandingConfiguration());
            modelBuilder.ApplyConfiguration(new ExtraBetOptionConfiguration());
            modelBuilder.ApplyConfiguration(new ExtraBetOptionChoiceConfiguration());
            modelBuilder.ApplyConfiguration(new ExtraBetOptionCorrectValueConfiguration());
            modelBuilder.ApplyConfiguration(new ExtraBetConfiguration());
        }
    }
}
