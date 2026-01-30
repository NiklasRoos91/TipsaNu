namespace TipsaNu.Application.Features.Auth.Interfaces
{
    public interface IPasswordService
    {
        string Hash(string password);
        bool Verify(string hashedPassword, string providedPassword);
    }
}
