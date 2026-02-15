
import React, { useState } from 'react';
import { TournamentCard } from '../components/tournaments/TournamentCard';
import { useTournaments } from '../hooks/useTournaments';
import { useAuth } from '../hooks/useAuth';
import { CreateTournament } from './CreateTournament';
import { Trophy } from 'lucide-react';
import { ActionButton } from '../components/commons/ActionButton';

export const TournamentList = () => {
  const { tournaments = [], loading, error } = useTournaments();
  const [showCreate, setShowCreate] = useState(false);
  const { isAdmin} = useAuth();

  return (
    <div className="max-w-4xl mx-auto pb-12">
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 mb-8">
        <div>
          <h2 className="text-3xl font-bold text-primary">Turneringar</h2>
          <p className="text-slate-500 mt-1 font-medium">
            Här hittar du alla pågående och kommande utmaningar.
            </p>
        </div>
        
        {isAdmin && (
          <div className="w-auto ml-auto">
            <ActionButton 
              label="Skapa ny" 
              onClick={() => setShowCreate(!showCreate)} 
              isActive={showCreate}
            />
          </div>
        )}
      </div>

      {showCreate && isAdmin && (
        <div className="mb-10 animate-fade-in">
          <CreateTournament onCreated={() => setShowCreate(false)} />
        </div>
      )}

      {error &&(
       <div className="text-red-500 text-center mb-4">{error}
       </div>
      )}
      
      {loading && !showCreate ? (
        <div className="text-center p-10 animate-pulse text-slate-400 font-medium">
          Laddar turneringar...
        </div>
      ) : (
        <div className="grid gap-6 animate-fade-in">
          {tournaments.length > 0 ? (
            tournaments.map(t => (
            <TournamentCard 
            key={t.tournamentId} tournament={t} 
            />
          ))
          ) : (
            <div className="text-center p-20 bg-white rounded-2xl border-2 border-dashed border-slate-200">
              <Trophy size={48} className="mx-auto text-slate-200 mb-4" />
              <p className="text-slate-400">Inga aktiva turneringar just nu.</p>
            </div>
          )}
        </div>
      )}
    </div>
  );
};
