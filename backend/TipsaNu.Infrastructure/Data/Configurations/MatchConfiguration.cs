using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            // Table
            builder.ToTable("Match", "dbo");

            // Primary Key
            builder.HasKey(m => m.MatchId);

            // Properties
            builder.Property(m => m.MatchType)
                   .IsRequired();

            builder.Property(m => m.StartTime)
                   .IsRequired();

            builder.Property(m => m.RoundNumber)
                   .IsRequired();

            builder.Property(m => m.Status)
                   .IsRequired();

            // Relations
            builder.HasOne(m => m.Tournament)
                   .WithMany(t => t.Matches)
                   .HasForeignKey(m => m.TournamentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.HomeCompetitor)
                   .WithMany(c => c.HomeMatches)
                   .HasForeignKey(m => m.HomeCompetitorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.AwayCompetitor)
                   .WithMany(c => c.AwayMatches)
                   .HasForeignKey(m => m.AwayCompetitorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.WinnerCompetitor)
                   .WithMany(c => c.WinningMatches)
                   .HasForeignKey(m => m.WinnerCompetitorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Group)
                   .WithMany(g => g.Matches)
                   .HasForeignKey(m => m.GroupId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(m => m.DependsOnMatch1)
                   .WithMany()
                   .HasForeignKey(m => m.DependsOnMatch1Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.DependsOnMatch2)
                   .WithMany()
                   .HasForeignKey(m => m.DependsOnMatch2Id)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Predictions)
                   .WithOne(p => p.Match)
                   .HasForeignKey(p => p.MatchId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
