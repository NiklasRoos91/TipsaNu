using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class PredictionConfiguration : IEntityTypeConfiguration<Prediction>
    {
        public void Configure(EntityTypeBuilder<Prediction> builder)
        {
            builder.ToTable("Predictions", "dbo");

            builder.HasKey(p => p.PredictionId);
            builder.Property(p => p.PredictionId).ValueGeneratedOnAdd();

            builder.Property(p => p.SubmittedAt).IsRequired();
            builder.Property(p => p.PointsAwarded).HasDefaultValue(0);

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Predictions)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Match)
                   .WithMany(m => m.Predictions)
                   .HasForeignKey(p => p.MatchId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
