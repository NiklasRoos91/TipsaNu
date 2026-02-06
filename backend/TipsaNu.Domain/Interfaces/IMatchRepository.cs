using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public interface IMatchRepository
    {
        Task<List<Match>> GetMatchesByGroupIdAsync(int groupId);
    }
}