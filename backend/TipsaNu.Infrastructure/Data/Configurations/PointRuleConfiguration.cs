using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class PointRuleConfiguration : IEntityTypeConfiguration<PointRule>
    {
        public void Configure(EntityTypeBuilder<PointRule> builder)
        {
            builder.ToTable("PointRules", "dbo");

            builder.HasKey(pr => pr.PointRuleId);
            builder.Property(pr => pr.PointRuleId).ValueGeneratedOnAdd();

            builder.Property(pr => pr.MatchType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(pr => pr.Criterion)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(pr => pr.Points)
                   .IsRequired();

            builder.HasOne(pr => pr.Template)
                   .WithMany(t => t.PointRules)
                   .HasForeignKey(pr => pr.TemplateId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}