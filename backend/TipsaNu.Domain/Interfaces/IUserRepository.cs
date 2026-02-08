using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}