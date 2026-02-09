    using global::TipsaNu.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class ExtraBetOptionChoiceConfiguration : IEntityTypeConfiguration<ExtraBetOptionChoice>
    {
        public void Configure(EntityTypeBuilder<ExtraBetOptionChoice> builder)
        {
            builder.ToTable("ExtraBetOptionChoices", "dbo");

            builder.HasKey(e => e.ChoiceId);
            builder.Property(e => e.ChoiceId).ValueGeneratedOnAdd();

            builder.Property(e => e.Value)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(e => e.ExtraBetOption)
                   .WithMany(o => o.ExtraBetOptionChoices)
                   .HasForeignKey(e => e.OptionId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}