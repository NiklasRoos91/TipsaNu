import React from 'react';
import { Trophy } from 'lucide-react';
import { MemberWithRank } from '../../hooks/useLeagueMembers';
import { MemberRow } from './MemberRow';

interface LeaderboardTableProps {
  members: MemberWithRank[];
}

export const LeaderboardTable: React.FC<LeaderboardTableProps> = ({ members }) => {
  return (
    <div className="bg-white rounded-2xl shadow-sm border border-slate-200 overflow-hidden animate-fade-in">
      <div className="p-6 border-b border-slate-100 flex items-center justify-between">
        <div className="flex items-center gap-2">
          <Trophy className="text-yellow-500" />
          <h2 className="text-xl font-bold">Tabell</h2>
        </div>
        <span className="text-xs font-bold text-slate-400">{members.length} medlemmar</span>
      </div>
      <div className="overflow-x-auto">
        <table className="w-full">
          <thead className="bg-slate-50 text-left text-xs text-slate-500 uppercase tracking-wider">
            <tr>
              <th className="px-6 py-4 font-bold">Rank</th>
              <th className="px-6 py-4 font-bold">Medlem</th>
              <th className="px-6 py-4 font-bold text-right">Poäng</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-100">
            {members.map((member) => (
              <MemberRow key={member.id} member={member} />
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};