
import React from 'react';
import type { GroupStanding } from "../../mocks/groupMock";
import { MOCK_GROUP_STANDINGS } from "../../mocks/groupMock";

interface GroupTableProps {
  standings: GroupStanding[];
}

export const GroupTable: React.FC<GroupTableProps> = ({ standings }) => {
  if (!standings || standings.length === 0) return null;

  // Ensure sorted by rank
  const sortedStandings = [...standings].sort((a, b) => a.rank - b.rank);

  return (
    <div className="bg-white rounded-xl border border-slate-200 overflow-hidden shadow-sm mb-6 animate-fade-in">
      <div className="overflow-x-auto">
        <table className="w-full text-sm text-left">
          <thead className="bg-slate-50 text-slate-500 uppercase text-[10px] font-bold tracking-wider">
            <tr>
              <th className="px-4 py-3 text-center w-10">#</th>
              <th className="px-4 py-3">Lag</th>
              <th className="px-3 py-3 text-center">S</th>
              <th className="px-3 py-3 text-center">V</th>
              <th className="px-3 py-3 text-center">O</th>
              <th className="px-3 py-3 text-center">F</th>
              <th className="px-3 py-3 text-center hidden sm:table-cell">GM</th>
              <th className="px-3 py-3 text-center hidden sm:table-cell">IM</th>
              <th className="px-3 py-3 text-center">MS</th>
              <th className="px-4 py-3 text-center font-bold text-primary">P</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-100">
            {sortedStandings.map((s) => {
              const isTopTwo = s.rank <= 2;
              return (
                <tr key={s.competitor.id} className={`${isTopTwo ? 'bg-emerald-50/20' : 'hover:bg-slate-50'} transition-colors`}>
                  <td className="px-4 py-3 text-center">
                    <span className={`inline-flex items-center justify-center w-6 h-6 rounded-full text-[11px] font-bold ${
                      isTopTwo ? 'bg-accent text-white' : 'bg-slate-100 text-slate-500'
                    }`}>
                      {s.rank}
                    </span>
                  </td>
                  <td className="px-4 py-3 font-semibold text-primary">
                    <div className="flex items-center gap-2">
                      <img src={s.competitor.flagUrl} alt="" className="w-5 h-3.5 object-cover rounded-sm border border-slate-100" />
                      <span className="truncate max-w-[120px]">{s.competitor.name}</span>
                    </div>
                  </td>
                  <td className="px-3 py-3 text-center text-slate-600 font-medium">{s.played}</td>
                  <td className="px-3 py-3 text-center text-slate-600">{s.wins}</td>
                  <td className="px-3 py-3 text-center text-slate-600">{s.draws}</td>
                  <td className="px-3 py-3 text-center text-slate-600">{s.losses}</td>
                  <td className="px-3 py-3 text-center text-slate-400 hidden sm:table-cell">{s.goalsFor}</td>
                  <td className="px-3 py-3 text-center text-slate-400 hidden sm:table-cell">{s.goalsAgainst}</td>
                  <td className="px-3 py-3 text-center text-slate-600 font-mono text-xs">{s.goalDifference > 0 ? `+${s.goalDifference}` : s.goalDifference}</td>
                  <td className="px-4 py-3 text-center font-bold text-primary bg-slate-50/50">{s.points}</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
      <div className="bg-slate-50 px-4 py-2 border-t border-slate-100">
        <div className="flex items-center gap-2 text-[10px] text-slate-400 font-bold uppercase tracking-tighter">
          <div className="w-2 h-2 rounded-full bg-accent"></div>
          Går vidare till slutspel
        </div>
      </div>
    </div>
  );
};
