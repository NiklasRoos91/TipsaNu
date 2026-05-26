using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class TournamentCompetitorConfiguration : IEntityTypeConfiguration<TournamentCompetitor>
    {
        public void Configure(EntityTypeBuilder<TournamentCompetitor> builder)
        {
            builder.ToTable("TournamentCompetitors", "dbo");

            builder.HasKey(tc => tc.TournamentCompetitorId);
            builder.Property(tc => tc.TournamentCompetitorId).ValueGeneratedOnAdd();

            builder.HasIndex(tc => new { tc.TournamentId, tc.CompetitorId })
                .IsUnique();

            // Relation till Tournament
            builder.HasOne(tc => tc.Tournament)
                .WithMany(t => t.TournamentCompetitors)
                .HasForeignKey(tc => tc.TournamentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation till Competitor
            builder.HasOne(tc => tc.Competitor)
                .WithMany(c => c.TournamentCompetitors)
                .HasForeignKey(tc => tc.CompetitorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}