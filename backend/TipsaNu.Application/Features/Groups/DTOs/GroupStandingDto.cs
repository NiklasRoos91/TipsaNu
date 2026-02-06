namespace TipsaNu.Application.Features.Groups.DTOs
{
    public class GroupStandingDto
    {
        public int CompetitorId { get; set; }
        public string CompetitorName { get; set; } = string.Empty;
        public int Played { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Points { get; set; }
        public int Rank { get; set; }
    }
}
