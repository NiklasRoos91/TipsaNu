using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public  interface IGroupStandingRepository
    {
        Task<List<GroupStanding>> GetGroupStandingsByGroupIdAsync(int groupId, CancellationToken cancellationToken = default);
    }
}
