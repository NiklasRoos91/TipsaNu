import React, { useState, useEffect } from 'react';
import { useParams, useLocation } from 'react-router-dom';
import { Calendar, Trophy } from 'lucide-react';
import { useAuth } from '../hooks/useAuth';
import { useTournament } from '../hooks/useTournament';

import { TournamentBanner } from '../components/tournament/TournamentBanner';
import { TournamentTabs, TabType } from '../components/tournament/TournamentTabs';
import { TournamentMatches } from '../components/tournament/TournamentMatches';
import { TournamentLeagues } from '../components/tournament/TournamentLeagues';
import { TournamentExtraBets } from '../components/tournament/TournamentExtraBets';

export const TournamentDetail = () => {
  const { id } = useParams<{ id: string }>();
  const location = useLocation();

  console.log("PATH:", location.pathname, "PARAMS ID:", id);

  const tournamentId = id ? Number(id) : NaN;

if (isNaN(tournamentId)) {
  return <div className="p-8 text-center text-red-500">Ogiltigt turnerings-id</div>;
}

  const { user } = useAuth();
  const isAdmin = user?.username === 'admin';

  const { tournament, loading, error } = useTournament(Number(id));
  const [activeTab, setActiveTab] = useState<TabType>('matches');

  // State placeholders för framtida backend-data
  const [matches, setMatches] = useState<any[]>([]);
  const [leagues, setLeagues] = useState<any[]>([]);
  const [extraBets, setExtraBets] = useState<any[]>([]);
  const [extraBetPredictions, setExtraBetPredictions] = useState<any[]>([]);

  const updateExtraBetPredictions = (pred: any) => {
    setExtraBetPredictions(prev => [pred, ...prev]);
  }  

  if (loading) return <div className="p-8 text-center text-slate-500">Laddar turnering...</div>;
  if (error) return <div className="p-8 text-center text-red-500">{error}</div>;
  if (!tournament) return <div className="p-8 text-center">Turnering hittades inte</div>;

  return (
    <div className="max-w-5xl mx-auto pb-20 px-4 md:px-0">
      <TournamentBanner tournament={tournament} />

      <TournamentTabs 
        activeTab={activeTab} 
        setActiveTab={setActiveTab} 
        leaguesCount={leagues.length} 
        extraBetsCount={extraBets.length} 
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
              leagues={leagues} 
              onLeaguesUpdate={setLeagues} 
            />
          )}

          {activeTab === 'extrabets' && (
            <TournamentExtraBets 
              tournamentId={id || ''} 
              extraBets={extraBets} 
              extraBetPredictions={extraBetPredictions}
              onBetsUpdate={(eb) => setExtraBets(prev => [eb, ...prev])} 
              onPredictionUpdate={updateExtraBetPredictions}
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