namespace TipsaNu.Application.Feature.Auth.Interfaces
{
    public interface IPasswordService
    {
        string Hash(string password);
        bool Verify(string hashedPassword, string providedPassword);
    }
}
