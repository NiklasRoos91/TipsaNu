using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class TournamentTemplateConfiguration : IEntityTypeConfiguration<TournamentTemplate>
    {
        public void Configure(EntityTypeBuilder<TournamentTemplate> builder)
        {
            builder.ToTable("TournamentTemplates", "dbo");

            builder.HasKey(t => t.TemplateId);
            builder.Property(t => t.TemplateId).ValueGeneratedOnAdd();

            builder.Property(t => t.Name).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Description).HasMaxLength(1000);

            builder.Property(t => t.IsPublic).HasDefaultValue(false);
            builder.Property(t => t.TotalGroups).IsRequired();
            builder.Property(t => t.AdvancingPerGroup).IsRequired();
            builder.Property(t => t.AllowsBestThird).IsRequired();

            builder.HasOne(t => t.CreatedByUser)
                   .WithMany()
                   .HasForeignKey(t => t.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.PointRules)
                   .WithOne(pr => pr.Template)
                   .HasForeignKey(pr => pr.TemplateId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(t => t.Tournaments)
                   .WithOne(tr => tr.Template)
                   .HasForeignKey(tr => tr.TemplateId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
