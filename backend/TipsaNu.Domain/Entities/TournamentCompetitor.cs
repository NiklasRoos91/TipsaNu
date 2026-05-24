namespace TipsaNu.Domain.Entities;

public class TournamentCompetitor
{
    public int TournamentCompetitorId { get; set; }
    public int TournamentId { get; set; }
    public int CompetitorId { get; set; }

    // Navigation
    public Tournament Tournament { get; set; } = null!;
    public Competitor Competitor { get; set; } = null!;
}