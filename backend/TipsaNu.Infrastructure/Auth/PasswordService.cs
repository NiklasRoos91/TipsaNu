using Microsoft.AspNetCore.Identity;
using TipsaNu.Application.Features.Auth.Interfaces;

namespace TipsaNu.Infrastructure.Auth
{
    public class PasswordService : IPasswordService
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string Hash(string password)
        {

            return _hasher.HashPassword(null!, password);
        }

        public bool Verify(string hashedPassword, string providedPassword)
        {

            var result = _hasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
