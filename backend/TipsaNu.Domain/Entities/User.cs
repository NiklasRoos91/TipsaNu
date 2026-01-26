using TipsaNu.Domain.Enums;

namespace TipsaNu.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public UserRoleEnum Role { get; set; }

        // Navigation properties
        public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
        public ICollection<ExtraBet> ExtraBets { get; set; } = new List<ExtraBet>();
        public ICollection<LeagueMember> LeagueMemberships { get; set; } = new List<LeagueMember>();
        public ICollection<League> AdminLeagues { get; set; } = new List<League>();
    }
}
