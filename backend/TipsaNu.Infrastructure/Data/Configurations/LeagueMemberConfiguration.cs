using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class LeagueMemberConfiguration : IEntityTypeConfiguration<LeagueMember>
    {
        public void Configure(EntityTypeBuilder<LeagueMember> builder)
        {
            builder.ToTable("LeagueMembers", "dbo");

            builder.HasKey(lm => lm.LeagueMemberId);
            builder.Property(lm => lm.LeagueMemberId).ValueGeneratedOnAdd();

            builder.Property(lm => lm.JoinedAt)
                   .IsRequired();

            builder.HasIndex(lm => new { lm.LeagueId, lm.UserId })
                   .IsUnique();

            builder.HasOne(lm => lm.User)
                   .WithMany(u => u.LeagueMemberships)
                   .HasForeignKey(lm => lm.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(lm => lm.League)
                   .WithMany(l => l.Members)
                   .HasForeignKey(lm => lm.LeagueId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(lm => lm.LeaderboardEntry)
                   .WithOne(le => le.LeagueMember)
                   .HasForeignKey<LeaderboardEntry>(le => le.LeagueMemberId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
