using Microsoft.EntityFrameworkCore;
using TipsaNu.Application.Feature.Auth.Interfaces;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Persistence.Seeders
{
        public static class DBSeeder
        {
        public static async Task SeedAllAsync(AppDbContext context, IPasswordService passwordService)
        {
            if (await context.Tournaments.AnyAsync())
                    return;

                // Users
                await UserSeeder.SeedAsync(context, passwordService);

                // Tournament template & tournament
                var tournament = await TournamentSeeder.SeedAsync(context);

                // Competitors
                var competitors = await CompetitorSeeder.SeedAsync(context);

                // Groups & GroupCompetitors
                var groups = await GroupSeeder.SeedAsync(context, tournament, competitors);

                // Matches
                await MatchSeeder.SeedAsync(context, tournament, groups);

                // PointRules
                await PointRuleSeeder.SeedAsync(context, tournament);

                // Tiebreakers
                await TiebreakerSeeder.SeedAsync(context, tournament);
            }
        }
    }
