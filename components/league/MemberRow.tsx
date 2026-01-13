import React from 'react';
import { MemberWithRank } from '../../hooks/useLeagueMembers';

interface MemberRowProps {
  member: MemberWithRank;
}

export const MemberRow: React.FC<MemberRowProps> = ({ member }) => {
  const getRankBadgeStyles = (rank: number) => {
    if (rank === 1) return 'bg-yellow-400 text-yellow-900';
    if (rank === 2) return 'bg-slate-200 text-slate-600';
    if (rank === 3) return 'bg-orange-200 text-orange-800';
    return 'text-slate-400';
  };

  const isTopRank = member.rank === 1;

  return (
    <tr className={`${isTopRank ? 'bg-yellow-50/30' : 'hover:bg-slate-50'} transition-colors`}>
      <td className="px-6 py-4">
        <span className={`
          w-8 h-8 rounded-full flex items-center justify-center font-bold text-sm
          ${getRankBadgeStyles(member.rank)}
        `}>
          {member.rank}
        </span>
      </td>
      <td className="px-6 py-4 font-medium text-primary">
        <div className="flex items-center gap-2">
          <div className="w-6 h-6 rounded-full bg-slate-100 flex items-center justify-center text-[10px] font-bold text-slate-400">
            {member.name.charAt(0).toUpperCase()}
          </div>
          {member.name}
        </div>
      </td>
      <td className="px-6 py-4 font-bold text-primary text-right">
        {member.points}
      </td>
    </tr>
  );
};