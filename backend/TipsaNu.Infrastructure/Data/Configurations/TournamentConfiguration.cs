using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.ToTable("Tournaments", "dbo");

            builder.HasKey(t => t.TournamentId);
            builder.Property(t => t.TournamentId).ValueGeneratedOnAdd();

            builder.Property(t => t.Name).IsRequired().HasMaxLength(200);
            builder.Property(t => t.StartsAt).IsRequired();

            builder.Property(t => t.Status)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(50);

            builder.HasOne(t => t.Template)
                   .WithMany(tt => tt.Tournaments)
                   .HasForeignKey(t => t.TemplateId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.CreatedByUser)
                   .WithMany()
                   .HasForeignKey(t => t.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Groups)
                   .WithOne(g => g.Tournament)
                   .HasForeignKey(g => g.TournamentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(t => t.Matches)
                   .WithOne(m => m.Tournament)
                   .HasForeignKey(m => m.TournamentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(t => t.Leagues)
                   .WithOne(l => l.Tournament)
                   .HasForeignKey(l => l.TournamentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(t => t.TournamentTiebreakers)
                   .WithOne(tb => tb.Tournament)
                   .HasForeignKey(tb => tb.TournamentId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(t => t.ExtraBetOptions)
                   .WithOne(ebo => ebo.Tournament)
                   .HasForeignKey(ebo => ebo.TournamentId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
