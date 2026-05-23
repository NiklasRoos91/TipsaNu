namespace TipsaNu.Application.Features.Auth.DTOs
{
    public record RegisterRequestDto(string Email, string UserName, string Password, string SignupCode);
}
