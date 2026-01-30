using TipsaNu.Domain.Enums;

namespace TipsaNu.Application.Features.Tournaments.DTOs
{
    public class TournamentDto
    {
        public int TournamentId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartsAt { get; set; }
        public TournamentStatusEnum Status { get; set; }
    }
}
