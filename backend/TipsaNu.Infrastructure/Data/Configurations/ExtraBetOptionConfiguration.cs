using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class ExtraBetOptionConfiguration : IEntityTypeConfiguration<ExtraBetOption>
    {
        public void Configure(EntityTypeBuilder<ExtraBetOption> builder)
        {
            builder.ToTable("ExtraBetOption", "dbo");

            builder.HasKey(e => e.OptionId);
            builder.Property(e => e.OptionId)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Description)
                   .HasMaxLength(1000);

            builder.Property(e => e.Points)
                   .IsRequired();

            builder.Property(e => e.ExpiresAt)
                   .IsRequired(false);

            // Relations

            builder.HasOne(e => e.Tournament)
                   .WithMany(t => t.ExtraBetOptions)
                   .HasForeignKey(e => e.TournamentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Match)
                   .WithMany()
                   .HasForeignKey(e => e.MatchId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.ExtraBets)
                   .WithOne(b => b.ExtraBetOption)
                   .HasForeignKey(b => b.OptionId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.ExtraBetOptionChoices)
                   .WithOne(c => c.ExtraBetOption)
                   .HasForeignKey(c => c.OptionId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.ExtraBetOptionCorrectValues)
                   .WithOne(c => c.ExtraBetOption)
                   .HasForeignKey(c => c.OptionId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
