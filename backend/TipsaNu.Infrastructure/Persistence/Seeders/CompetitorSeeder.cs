using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Infrastructure.Presistence;

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
                "Mexiko","Sydkorea","Sydafrika","Treor_A",
                "Kanada","Schweiz","Qatar","Treor_B",
                "Brasilien","Marocko","Skottland","Haiti",
                "USA","Australien","Paraguay","Treor_D",
                "Tyskland","Ecuador","Elfenbenskusten","Curaçao",
                "Nederländerna","Japan","Tunisien","Treor_F",
                "Belgien","Iran","Egypten","Nya Zeeland",
                "Spanien","Uruguay","Saudiarabien","Kap Verde",
                "Frankrike","Senegal","Norge","Treor_I",
                "Argentina","Österrike","Algeriet","Jordanien",
                "Portugal","Colombia","Uzbekistan","Treor_K",
                "England","Kroatien","Panama","Ghana"
            };

            var competitors = teamNames.Select(n => new Competitor { Name = n, IsIndividual = false }).ToList();
            await context.Competitors.AddRangeAsync(competitors);
            await context.SaveChangesAsync();

            return competitors;
        }
    }
}
