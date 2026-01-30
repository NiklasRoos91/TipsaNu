using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class GroupCompetitorConfiguration : IEntityTypeConfiguration<GroupCompetitor>
    {
        public void Configure(EntityTypeBuilder<GroupCompetitor> builder)
        {
            builder.ToTable("GroupCompetitors", "dbo");

            builder.HasKey(gc => gc.GroupCompetitorId);
            builder.Property(gc => gc.GroupCompetitorId).ValueGeneratedOnAdd();

            builder.HasIndex(gc => new { gc.GroupId, gc.CompetitorId })
                   .IsUnique();

            builder.HasOne(gc => gc.Group)
                   .WithMany(g => g.GroupCompetitors)
                   .HasForeignKey(gc => gc.GroupId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(gc => gc.Competitor)
                   .WithMany(c => c.GroupCompetitors)
                   .HasForeignKey(gc => gc.CompetitorId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
