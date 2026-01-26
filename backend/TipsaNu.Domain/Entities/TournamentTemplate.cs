namespace TipsaNu.Domain.Entities
{
    public class TournamentTemplate
    {
        public int TemplateId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CreatedByUserId { get; set; }
        public bool IsPublic { get; set; }
        public int TotalGroups { get; set; }
        public int AdvancingPerGroup { get; set; }
        public bool AllowsBestThird { get; set; }

        // Navigation
        public User CreatedByUser { get; set; } = null!;
        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
        public ICollection<PointRule> PointRules { get; set; } = new List<PointRule>();
    }
}
