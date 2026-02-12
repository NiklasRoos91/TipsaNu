import React, { useState } from 'react';
import { Users, Copy, Check } from 'lucide-react';

// Plceholder type for league, replace with actual type from your data model
type League = {
  id: string;
  tournamentId: string;
  ownerId: string | number;

  name: string;
  description?: string | null;
  code: string;
  membersCount: number;
};

interface LeagueCardProps {
  league: League;
}

export const LeagueCard: React.FC<LeagueCardProps> = ({ league }) => {
  const [copied, setCopied] = useState(false);

  const handleCopy = (e: React.MouseEvent) => {
    e.preventDefault();
    e.stopPropagation();
    navigator.clipboard.writeText(league.code);
    setCopied(true);
    setTimeout(() => setCopied(false), 2000);
  };

  return (
    <div className="bg-white p-6 rounded-xl shadow-sm border border-slate-200 flex flex-col h-full transition-all hover:shadow-md hover:border-accent/30">
      <div className="mb-3">
        <h3 className="text-lg font-bold text-primary group-hover:text-accent transition-colors">
          {league.name}
        </h3>
      </div>
      
      <p className="text-slate-500 text-sm mb-6 flex-grow leading-relaxed line-clamp-2">
        {league.description || 'Ingen beskrivning tillgänglig.'}
      </p>
      
      <div className="flex items-center justify-between border-t border-slate-50 pt-4 mt-auto">
        <div className="flex items-center gap-2 text-slate-400 text-xs font-medium">
          <Users size={14} className="text-accent" />
          <span>{league.membersCount} medlemmar</span>
        </div>
        <button 
          onClick={handleCopy}
          className="flex items-center gap-1.5 group/copy relative"
          title="Klicka för att kopiera kod"
        >
          <span className="text-[10px] font-bold uppercase tracking-tight text-slate-400">Ligakod:</span>
          <div className={`flex items-center gap-1.5 px-2 py-0.5 rounded border font-mono text-[10px] font-bold transition-all shadow-sm ${
            copied 
            ? 'bg-emerald-500 border-emerald-500 text-white' 
            : 'bg-slate-100 border-slate-200 text-primary hover:border-accent hover:text-accent'
          }`}>
            {league.code}
            {copied ? <Check size={10} /> : <Copy size={10} className="opacity-40 group-hover/copy:opacity-100" />}
          </div>
          {copied && (
            <span className="absolute -top-8 left-1/2 -translate-x-1/2 bg-slate-800 text-white text-[10px] px-2 py-1 rounded shadow-lg animate-bounce whitespace-nowrap">
              Kopierad!
            </span>
          )}
        </button>
      </div>
    </div>
  );
};