using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public  interface IGroupStandingRepository
    {
        Task<List<GroupStanding>> GetGroupStandingsByGroupIdAsync(int groupId);
    }
}
