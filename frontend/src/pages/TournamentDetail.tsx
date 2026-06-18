import React, { useState, useEffect } from 'react';
import { useParams, useLocation } from 'react-router-dom';
import { Calendar, Trophy } from 'lucide-react';
import { useAuth } from '../hooks/useAuth';
import { useTournament } from '../hooks/useTournament';

import { TournamentBanner } from '../components/tournaments/TournamentBanner';
import { TournamentTabs, TabType } from '../components/tournaments/TournamentTabs';
import { TournamentMatches } from '../components/tournaments/TournamentMatches';
import { TournamentLeagues } from '../components/tournaments/TournamentLeagues';
import { TournamentExtraBets } from '../components/tournaments/TournamentExtraBets';

export const TournamentDetail = () => {
  const { id } = useParams<{ id: string }>();
  const location = useLocation();
  const tournamentId = id ? Number(id) : NaN;

if (isNaN(tournamentId)) {
  return <div className="p-8 text-center text-red-500">Ogiltigt turnerings-id</div>;
}

  const { isAdmin } = useAuth();

  const { tournament, loading, error } = useTournament(Number(id));
  const [activeTab, setActiveTab] = useState<TabType>('matches');

  if (loading) return <div className="p-8 text-center text-slate-500">Laddar turnering...</div>;
  if (error) return <div className="p-8 text-center text-red-500">{error}</div>;
  if (!tournament) return <div className="p-8 text-center">Turnering hittades inte</div>;

  return (
    <div className="w-full pb-20">
      <TournamentBanner tournament={tournament} />

      <TournamentTabs
        activeTab={activeTab}
        setActiveTab={setActiveTab}
        leaguesCount={0}
        extraBetsCount={0}
      />

      {activeTab === 'matches' && (
        <TournamentMatches
          tournamentId={id || ''}
        />
      )}

      {activeTab === 'leagues' && (
        <TournamentLeagues
          tournamentId={id || ''}
        />
      )}

      {activeTab === 'extrabets' && (
        <TournamentExtraBets
          tournamentId={id || ''}
          isAdmin={isAdmin}
        />
      )}
    </div>
  );
};