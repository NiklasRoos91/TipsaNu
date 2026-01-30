using Microsoft.EntityFrameworkCore;
using TipsaNu.Application.Features.Auth.Interfaces;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Persistence.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(AppDbContext context, IPasswordService passwordService)
        {
            if (await context.Users.AnyAsync())
                return;

            var admin = new User
            {
                Username = "Admin",
                Email = "admin@admin.com",
                PasswordHash = passwordService.Hash("admin123"),
                Role = UserRoleEnum.Admin,
                CreatedAt = DateTime.UtcNow
            };
            var user = new User
            {
                Username = "User",
                Email = "user@user.com",
                PasswordHash = passwordService.Hash("user123"),
                Role = UserRoleEnum.User,
                CreatedAt = DateTime.UtcNow
            };

            await context.Users.AddRangeAsync(admin, user);
            await context.SaveChangesAsync();
        }
    }
}