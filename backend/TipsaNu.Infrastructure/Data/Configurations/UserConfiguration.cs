using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;

namespace TipsaNu.Infrastructure.Data.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");

            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId).ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(256);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .HasDefaultValue(UserRoleEnum.User);

            builder.Property(u => u.CreatedAt).IsRequired();

            // Relations
            builder.HasMany(u => u.Predictions)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.ExtraBets)
                   .WithOne(e => e.User)
                   .HasForeignKey(e => e.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.LeagueMemberships)
                   .WithOne(lm => lm.User)
                   .HasForeignKey(lm => lm.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.AdminLeagues)
                   .WithOne(l => l.AdminUser)
                   .HasForeignKey(l => l.AdminUserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
