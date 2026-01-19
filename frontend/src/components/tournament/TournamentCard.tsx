import React from 'react';
import { Link } from 'react-router-dom';
import { ChevronRight } from 'lucide-react';
import { Tournament } from '../../types/types';

interface TournamentCardProps {
  tournament: Tournament;
}

export const TournamentCard: React.FC<TournamentCardProps> = ({ tournament }) => {
  return (
    <Link to={`/tournaments/${tournament.id}`} className="group">
      <div className="bg-white p-6 rounded-xl border border-slate-200 shadow-sm flex justify-between items-center group-hover:border-accent transition-all">
        <div className="flex items-center gap-4">
          <div className="w-12 h-12 rounded-lg overflow-hidden flex-shrink-0">
            <img src={tournament.bannerUrl} alt={tournament.name} className="w-full h-full object-cover" />
          </div>
          <div>
            <h3 className="font-bold text-primary group-hover:text-accent transition-colors">{tournament.name}</h3>
            <p className="text-sm text-slate-500">Visa specialtips för denna turnering</p>
          </div>
        </div>
        <div className="flex items-center gap-2 text-accent font-bold text-sm">
          Öppna <ChevronRight size={18} />
        </div>
      </div>
    </Link>
  );
};