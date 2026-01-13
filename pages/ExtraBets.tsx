import React from 'react';
import { Award } from 'lucide-react';
import { useTournaments } from '../hooks/useTournaments';
import { TournamentCard } from '../components/tournament/TournamentCard';

export const ExtraBets = () => {
  const { tournaments, loading, error } = useTournaments();

  if (loading) {
    return <div className="p-8 text-center text-slate-500">Laddar...</div>;
  }

  if (error) {
    return <div className="p-8 text-center text-red-500 font-bold">{error}</div>;
  }

  return (
    <div className="max-w-4xl mx-auto pb-12">
      <div className="bg-gradient-to-r from-purple-800 to-indigo-900 rounded-2xl p-8 mb-8 text-white shadow-xl">
        <h2 className="text-3xl font-bold mb-2 flex items-center gap-3">
          <Award className="text-yellow-400" size={32} />
          Extratips
        </h2>
        <p className="text-indigo-200 max-w-xl">
          Välj en turnering nedan för att se tillgängliga extratips och specialtips.
        </p>
      </div>

      <div className="grid gap-4">
        {tournaments.length > 0 ? (
          tournaments.map((tournament) => (
            <TournamentCard key={tournament.id} tournament={tournament} />
          ))
        ) : (
          <div className="text-center p-12 bg-slate-50 rounded-xl border-2 border-dashed border-slate-200 text-slate-400">
            Inga aktiva turneringar tillgängliga just nu.
          </div>
        )}
      </div>
    </div>
  );
};