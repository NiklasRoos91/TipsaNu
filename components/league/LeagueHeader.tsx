import React from 'react';
import { League } from '../../types';

interface LeagueHeaderProps {
  league: League;
}

export const LeagueHeader: React.FC<LeagueHeaderProps> = ({ league }) => {
  return (
    <div className="bg-white p-8 rounded-2xl shadow-sm border border-slate-200 animate-fade-in">
      <h1 className="text-3xl font-bold text-primary mb-2">{league.name}</h1>
      <p className="text-slate-500 mb-4">{league.description}</p>
      <div className="bg-slate-100 inline-block px-4 py-2 rounded-lg border border-slate-200">
        <span className="text-xs text-slate-500 mr-2 uppercase font-bold tracking-tight">Inbjudningskod:</span>
        <span className="font-mono font-bold text-primary select-all">{league.code}</span>
      </div>
    </div>
  );
};