using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.ToTable("Leagues", "dbo");

            builder.HasKey(l => l.LeagueId);
            builder.Property(l => l.LeagueId).ValueGeneratedOnAdd();

            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(l => l.Description)
                   .HasMaxLength(1000);

            builder.Property(l => l.InvitationCode)
                   .HasMaxLength(50);

            builder.Property(l => l.CreatedAt)
                   .IsRequired();

            builder.Property(l => l.MaxMembers)
                   .IsRequired();

            builder.HasOne(l => l.Tournament)
                   .WithMany(t => t.Leagues)
                   .HasForeignKey(l => l.TournamentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(l => l.AdminUser)
                   .WithMany(u => u.AdminLeagues)
                   .HasForeignKey(l => l.AdminUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(l => l.Members)
                   .WithOne(lm => lm.League)
                   .HasForeignKey(lm => lm.LeagueId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
