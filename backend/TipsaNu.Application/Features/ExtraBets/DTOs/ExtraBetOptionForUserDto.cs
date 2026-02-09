namespace TipsaNu.Application.Features.ExtraBets.DTOs
{
    public class ExtraBetOptionForUserDto
    {
        public int OptionId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Points { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool AllowCustomChoice { get; set; }
        public List<string> Choices { get; set; } = new();

        // This property will be populated with the user's bet if it exists, otherwise it will be null
        public ExtraBetForUserDto? MyBet { get; set; }
    }
}