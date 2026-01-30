using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class ExtraBetOptionCorrectValueConfiguration : IEntityTypeConfiguration<ExtraBetOptionCorrectValue>
    {
        public void Configure(EntityTypeBuilder<ExtraBetOptionCorrectValue> builder)
        {
            builder.ToTable("ExtraBetOptionCorrectValue", "dbo");

            builder.HasKey(e => e.CorrectValueId);
            builder.Property(e => e.CorrectValueId)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.Value)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(e => e.ExtraBetOption)
                   .WithMany(o => o.ExtraBetOptionCorrectValues)
                   .HasForeignKey(e => e.OptionId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
