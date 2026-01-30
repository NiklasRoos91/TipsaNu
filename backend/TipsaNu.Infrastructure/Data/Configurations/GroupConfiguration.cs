
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups", "dbo");

            builder.HasKey(g => g.GroupId);
            builder.Property(g => g.GroupId).ValueGeneratedOnAdd();

            builder.Property(g => g.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(g => g.MaxTeams)
                   .IsRequired();

            builder.HasMany(g => g.GroupCompetitors)
                   .WithOne(gc => gc.Group)
                   .HasForeignKey(gc => gc.GroupId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(g => g.Matches)
                   .WithOne(m => m.Group)
                   .HasForeignKey(m => m.GroupId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
