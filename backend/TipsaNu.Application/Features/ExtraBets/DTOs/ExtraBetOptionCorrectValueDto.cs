namespace TipsaNu.Application.Features.ExtraBets.DTOs
{
    public class ExtraBetOptionCorrectValueDto
    {
        public int CorrectValueId { get; set; }
        public int OptionId { get; set; }
        public string Value { get; set; } = null!;
    }
}
