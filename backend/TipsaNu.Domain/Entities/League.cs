namespace TipsaNu.Domain.Entities
{
    public class League
    {
        public int LeagueId { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int AdminUserId { get; set; }
        public string InvitationCode { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int MaxMembers { get; set; }

        // Navigation
        public Tournament Tournament { get; set; } = null!;
        public User AdminUser { get; set; } = null!;
        public ICollection<LeagueMember> Members { get; set; } = new List<LeagueMember>();
    }
}
