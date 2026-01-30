using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class TournamentTiebreakerConfiguration : IEntityTypeConfiguration<TournamentTiebreaker>
    {
        public void Configure(EntityTypeBuilder<TournamentTiebreaker> builder)
        {
            builder.ToTable("TournamentTiebreakers", "dbo");

            builder.HasKey(tb => tb.TiebreakerId);
            builder.Property(tb => tb.TiebreakerId).ValueGeneratedOnAdd();

            builder.Property(tb => tb.Criterion)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(tb => tb.Priority)
                   .IsRequired();

            builder.HasOne(tb => tb.Tournament)
                   .WithMany(t => t.TournamentTiebreakers)
                   .HasForeignKey(tb => tb.TournamentId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
