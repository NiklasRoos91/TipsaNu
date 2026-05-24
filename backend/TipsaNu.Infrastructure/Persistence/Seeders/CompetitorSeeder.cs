using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Persistence.Seeders
{
    public static class CompetitorSeeder
    {
        public static async Task<List<Competitor>> SeedAsync(AppDbContext context)
        {
            if (await context.Competitors.AnyAsync())
                return await context.Competitors.ToListAsync();

            var teamNames = new[]
            {
                "Mexiko", "Sydafrika", "Sydkorea", "Tjeckien",
                "Kanada", "Bosnien och Hercegovina", "Qatar", "Schweiz",
                "Brasilien", "Marocko", "Skottland", "Haiti",
                "USA", "Paraguay", "Australien", "Turkiet",
                "Tyskland", "Curaçao", "Elfenbenskusten", "Ecuador",
                "Nederländerna", "Japan", "Sverige", "Tunisien",
                "Belgien", "Iran", "Egypten", "Nya Zeeland",
                "Spanien", "Kap Verde", "Saudiarabien", "Uruguay",
                "Frankrike", "Senegal", "Irak", "Norge",
                "Argentina", "Algeriet", "Österrike", "Jordanien",
                "Portugal", "Kongo DR", "Uzbekistan", "Colombia",
                "England", "Kroatien", "Panama", "Ghana"
            };

            var competitors = teamNames.Select(n => new Competitor { Name = n, IsIndividual = false }).ToList();
            await context.Competitors.AddRangeAsync(competitors);
            await context.SaveChangesAsync();

            return competitors;
        }
    }
}
