using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class LeaderboardEntryConfiguration : IEntityTypeConfiguration<LeaderboardEntry>
    {
        public void Configure(EntityTypeBuilder<LeaderboardEntry> builder)
        {
            builder.ToTable("LeaderboardEntries", "dbo");

            builder.HasKey(le => le.LeagueMemberId);

            builder.Property(le => le.TotalPoints)
                   .IsRequired();

            builder.Property(le => le.LastUpdated)
                   .IsRequired();
        }
    }
}
