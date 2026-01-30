using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Persistence.Seeders
{
    public static class TournamentSeeder
    {
        public static async Task<Tournament> SeedAsync(AppDbContext context)
        {
            var adminUser = await context.Users.FirstAsync(u => u.Role == UserRoleEnum.Admin);

            var template = new TournamentTemplate
            {
                Name = "VM 2026",
                Description = "FIFA VM 2026 - Gruppspel",
                CreatedByUserId = adminUser.UserId,
                IsPublic = true,
                TotalGroups = 12,
                AdvancingPerGroup = 2,
                AllowsBestThird = false
            };
            await context.TournamentTemplates.AddAsync(template);
            await context.SaveChangesAsync();

            var tournament = new Tournament
            {
                TemplateId = template.TemplateId,
                Name = "FIFA World Cup 2026",
                StartsAt = new DateTime(2026, 6, 11),
                Status = TournamentStatusEnum.Upcoming,
                CreatedByUserId = adminUser.UserId
            };
            await context.Tournaments.AddAsync(tournament);
            await context.SaveChangesAsync();

            return tournament;
        }
    }
}
