import React from 'react';
import { League } from '../../types/types';
import { useLeagueMembers } from '../../hooks/useLeagueMembers';
import { useLeaguePosts } from '../../hooks/useLeaguePosts';
import { LeaderboardTable } from './LeaderboardTable';
import { Forum } from './Forum';

interface LeagueInlineDetailProps {
  league: League;
}

export const LeagueInlineDetail: React.FC<LeagueInlineDetailProps> = ({ league }) => {
  const { members, loading: membersLoading } = useLeagueMembers(league.id);
  const { posts, loading: postsLoading, addPost } = useLeaguePosts(league.id);

  return (
    <div className="mt-2 p-4 md:p-6 bg-slate-50 rounded-b-xl border-t border-slate-100 animate-fade-in">
      <div className="flex flex-col gap-8">
        {/* Leaderboard section */}
        <div className="space-y-4">
           <div className="px-1 flex justify-between items-center">
             <h4 className="text-xs font-bold text-slate-400 uppercase tracking-widest">Tabell</h4>
             {membersLoading && <span className="text-[10px] text-slate-400 animate-pulse italic">Uppdaterar...</span>}
           </div>
           <LeaderboardTable members={members} />
        </div>

        {/* Forum section - Always bottom */}
        <div className="space-y-4">
          <div className="flex justify-between items-center px-1">
            <h4 className="text-xs font-bold text-slate-400 uppercase tracking-widest">Klotterplank</h4>
            {postsLoading && <span className="text-[10px] text-slate-400 animate-pulse italic">Hämtar inlägg...</span>}
          </div>
          <div className="flex flex-col h-[500px]">
            <Forum posts={posts} onAddPost={addPost} />
          </div>
        </div>
      </div>
    </div>
  );
};