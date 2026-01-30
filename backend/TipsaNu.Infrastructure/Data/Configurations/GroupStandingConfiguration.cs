using global::TipsaNu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class GroupStandingConfiguration : IEntityTypeConfiguration<GroupStanding>
    {
        public void Configure(EntityTypeBuilder<GroupStanding> builder)
        {
            builder.ToTable("GroupStanding", "dbo");

            builder.HasKey(gs => gs.StandingId);
            builder.Property(gs => gs.StandingId)
                   .ValueGeneratedOnAdd();

            builder.Property(gs => gs.Rank).IsRequired();
            builder.Property(gs => gs.Points).IsRequired();
            builder.Property(gs => gs.Played).IsRequired();
            builder.Property(gs => gs.Wins).IsRequired();
            builder.Property(gs => gs.Draws).IsRequired();
            builder.Property(gs => gs.Losses).IsRequired();
            builder.Property(gs => gs.GoalsFor).IsRequired();
            builder.Property(gs => gs.GoalsAgainst).IsRequired();
            builder.Property(gs => gs.GoalDifference).IsRequired();

            builder.HasOne(gs => gs.Group)
                   .WithMany(g => g.GroupStandings)
                   .HasForeignKey(gs => gs.GroupId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(gs => gs.Competitor)
                   .WithMany(c => c.GroupStandings)
                   .HasForeignKey(gs => gs.CompetitorId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}