import React from 'react';
import { LeaderboardTable } from './LeaderboardTable';
// import { Forum } from './Forum';
import { useLeagueDetail } from '../../hooks/useLeagueDetail';
import type { LeagueWithLeaderboardDto } from '../../types/leagueTypes';

interface LeagueInlineDetailProps {
  leagueId: number;
}

// Plceholder hooks until Api is ready
const useLeagueMembers = (_leagueId: number) => ({
  members: [], // tom lista
  loading: false,
});

const useLeaguePosts = (_leagueId: number) => ({
  posts: [],
  loading: false,
  addPost: async (_: any) => {},
});

export const LeagueInlineDetail: React.FC<LeagueInlineDetailProps> = ({ leagueId }) => {
  const { league, loading, error } = useLeagueDetail(leagueId);

  if (loading) return <div className="p-4 text-sm text-slate-500">Laddar ligadetaljer...</div>;
  if (error || !league) return <div className="p-4 text-sm text-red-500">Kunde inte hämta ligan</div>;

  return (
    <div className="mt-2 p-4 md:p-6 bg-slate-50 rounded-b-xl border-t border-slate-100 animate-fade-in">
      <div className="flex flex-col gap-8">
        {/* Leaderboard section */}
        <div className="space-y-4">
           <div className="px-1 flex justify-between items-center">
             <h4 className="text-xs font-bold text-slate-400 uppercase tracking-widest">Tabell</h4>
           </div>
           <LeaderboardTable members={league.leaderboard} />
        </div>

        {/* Forum removed for now */}
        {/* Forum section - Always bottom */}
        {/* <div className="space-y-4">
          <div className="flex justify-between items-center px-1">
            <h4 className="text-xs font-bold text-slate-400 uppercase tracking-widest">Klotterplank</h4>
            {postsLoading && <span className="text-[10px] text-slate-400 animate-pulse italic">Hämtar inlägg...</span>}
          </div>
          <div className="flex flex-col h-[500px]">
            <Forum posts={posts} onAddPost={addPost} />
          </div>
        </div> */}
      </div>
    </div>
  );
};