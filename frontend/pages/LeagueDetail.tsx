import React from 'react';
import { useParams, Link } from 'react-router-dom';
import { ArrowLeft } from 'lucide-react';
import { useLeague } from '../hooks/useLeague';
import { useLeagueMembers } from '../hooks/useLeagueMembers';
import { useLeaguePosts } from '../hooks/useLeaguePosts';
import { LeagueHeader } from '../components/league/LeagueHeader';
import { LeaderboardTable } from '../components/league/LeaderboardTable';
import { Forum } from '../components/league/Forum';

export const LeagueDetail = () => {
  const { tournamentId, id } = useParams();
  const { league, loading: leagueLoading } = useLeague(id);
  const { members, loading: membersLoading } = useLeagueMembers(id);
  const { posts, loading: postsLoading, addPost } = useLeaguePosts(id);

  const isLoading = leagueLoading || membersLoading || postsLoading;

  if (isLoading || !league) {
    return (
      <div className="p-10 text-center text-slate-500 animate-pulse">
        Laddar ligainformation...
      </div>
    );
  }

  return (
    <div className="max-w-4xl mx-auto pb-10">
      <Link 
        to={`/tournaments/${tournamentId}`} 
        className="inline-flex items-center text-slate-500 hover:text-primary mb-6 transition-colors"
      >
        <ArrowLeft size={18} className="mr-1" /> Tillbaka till turnering
      </Link>

      <div className="flex flex-col gap-8">
        {/* League Info Section */}
        <LeagueHeader league={league} />
        
        {/* Leaderboard Section - Always on top */}
        <div className="space-y-4">
          <h3 className="text-sm font-bold text-slate-400 uppercase tracking-widest px-1">Tabell</h3>
          <LeaderboardTable members={members} />
        </div>

        {/* Forum Section - Always below leaderboard */}
        <div className="space-y-4">
          <h3 className="text-sm font-bold text-slate-400 uppercase tracking-widest px-1">Diskussion</h3>
          <Forum posts={posts} onAddPost={addPost} />
        </div>
      </div>
    </div>
  );
};