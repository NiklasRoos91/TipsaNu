import React from 'react';
import { Tournament } from '../../types';

interface TournamentBannerProps {
  tournament: Tournament;
}

export const TournamentBanner: React.FC<TournamentBannerProps> = ({ tournament }) => {
  return (
    <div className="mb-8 relative h-48 md:h-64 rounded-2xl overflow-hidden shadow-lg group">
      <img 
        src={tournament.bannerUrl} 
        alt={tournament.name} 
        className="w-full h-full object-cover transition-transform duration-700 group-hover:scale-105" 
      />
      <div className="absolute inset-0 bg-gradient-to-t from-slate-900/90 via-slate-900/40 to-transparent flex items-end p-8">
        <div>
           <div className="bg-accent text-white text-xs font-bold px-3 py-1 rounded-full inline-block mb-3 shadow-sm uppercase tracking-wider">
              {tournament.status === 'ACTIVE' ? 'PÅGÅR' : 'KOMMANDE'}
           </div>
           <h1 className="text-3xl md:text-5xl font-bold text-white tracking-tight">{tournament.name}</h1>
        </div>
      </div>
    </div>
  );
};