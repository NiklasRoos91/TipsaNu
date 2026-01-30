namespace TipsaNu.Application.Features.Groups.DTOs
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; } = null!;
    }
}
