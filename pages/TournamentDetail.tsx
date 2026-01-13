import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import { Calendar, Trophy } from 'lucide-react';
import { useAuth } from '../hooks/useAuth';
import { useTournamentData } from '../hooks/useTournamentData';

import { TournamentBanner } from '../components/tournament/TournamentBanner';
import { TournamentTabs, TabType } from '../components/tournament/TournamentTabs';
import { TournamentMatches } from '../components/tournament/TournamentMatches';
import { TournamentLeagues } from '../components/tournament/TournamentLeagues';
import { TournamentExtraBets } from '../components/tournament/TournamentExtraBets';

export const TournamentDetail = () => {
  const { id } = useParams();
  const { user } = useAuth();
  const isAdmin = user?.username === 'admin';

  const { 
    tournament, 
    matches, 
    extraBets, 
    setExtraBets, 
    extraBetPredictions,
    updateExtraBetPredictions,
    leagues, 
    setLeagues, 
    predictions, 
    loading 
  } = useTournamentData(id);

  const [activeTab, setActiveTab] = useState<TabType>('matches');

  if (loading) return <div className="p-8 text-center text-slate-500">Laddar turnering...</div>;
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
              matches={matches} 
              predictions={predictions} 
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
              <div>
                <div className="flex justify-between items-center mb-2">
                  <span className="text-sm text-slate-600">Tippade matcher</span>
                  <span className="font-bold text-primary">{predictions.length} / {matches.length}</span>
                </div>
                <div className="w-full bg-slate-100 rounded-full h-2 overflow-hidden">
                  <div 
                    className="bg-accent h-2 rounded-full transition-all duration-1000 ease-out" 
                    style={{ width: `${matches.length > 0 ? (predictions.length / matches.length) * 100 : 0}%` }}
                  ></div>
                </div>
              </div>
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