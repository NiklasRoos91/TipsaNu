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
    <div className="max-w-5xl mx-auto pb-20 px-4 md:px-0">
      <TournamentBanner tournament={tournament} />

      <TournamentTabs 
        activeTab={activeTab} 
        setActiveTab={setActiveTab} 
        leaguesCount={0}
        extraBetsCount={0} 
      />

      <div className="grid lg:grid-cols-3 gap-8">
        <div className="lg:col-span-2">
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

        {/* Sidebar Actions */}
        <div className="space-y-6">
          <div className="bg-white p-6 rounded-xl shadow-sm border border-slate-200">
            <h3 className="text-lg font-bold text-primary mb-4 flex items-center gap-2">
              <Trophy size={20} className="text-accent" />
              Turneringsstatus
            </h3>
            <div className="space-y-4">
              <div className="flex items-center gap-3 text-sm text-slate-500 pt-2 border-t border-slate-100">
                <Calendar size={18} className="text-slate-400" />
                <span>Slutar {new Date(tournament.endDate).toLocaleDateString('sv-SE')}</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};