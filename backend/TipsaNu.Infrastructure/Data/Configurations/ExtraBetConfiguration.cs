using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class ExtraBetConfiguration : IEntityTypeConfiguration<ExtraBet>
    {
        public void Configure(EntityTypeBuilder<ExtraBet> builder)
        {
            builder.ToTable("ExtraBets", "dbo");

            builder.HasKey(e => e.ExtraBetId);
            builder.Property(e => e.ExtraBetId).ValueGeneratedOnAdd();

            builder.Property(e => e.Value)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.PointsAwarded)
                   .IsRequired(false);

            builder.Property(e => e.SubmittedAt)
                   .IsRequired();

            builder.HasOne(e => e.User)
                   .WithMany(u => u.ExtraBets)
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.ExtraBetOption)
                   .WithMany(o => o.ExtraBets)
                   .HasForeignKey(e => e.OptionId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
