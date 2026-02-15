import React, { useState } from 'react';
import { ChevronRight, ChevronDown, Users, Copy, Check } from 'lucide-react';
import { LeagueInlineDetail } from './LeagueInlineDetail';
import type { LeagueDto } from '../../types/leagueTypes';


interface LeagueAccordionProps {
  league: LeagueDto;
  isExpanded: boolean;
  onToggle: () => void;
}

export const LeagueAccordion: React.FC<LeagueAccordionProps> = ({ league, isExpanded, onToggle }) => {
  const [copied, setCopied] = useState(false);

  const handleCopy = (e: React.MouseEvent) => {
    e.preventDefault();
    e.stopPropagation(); // Prevent the accordion from toggling when clicking copy
    navigator.clipboard.writeText(league.invitationCode);
    setCopied(true);
    setTimeout(() => setCopied(false), 2000);
  };

  return (
    <div className={`bg-white rounded-xl border transition-all ${
      isExpanded
       ? 'border-accent shadow-lg' 
       : 'border-slate-200 shadow-sm hover:border-accent'
       }`}>

      <button 
        onClick={onToggle}
        className="w-full text-left p-6 flex justify-between items-center group gap-4"
      >
        <div className="flex items-start gap-4 flex-1">
          <div className={`p-2 rounded-lg mt-1 transition-colors flex-shrink-0 ${
            isExpanded
             ? 'bg-accent text-white' 
             : 'bg-slate-50 text-slate-400 group-hover:bg-accent/10 group-hover:text-accent'
             }`}>
            <Users size={20} />
          </div>

          <div className="min-w-0 flex-1">
            <h3 className={`text-lg font-bold transition-colors truncate ${
              isExpanded ? 'text-accent' 
              : 'text-primary'
              }`}
            >
                {league.name}
            </h3>

            {league.description && (
              <p className="text-xs text-slate-400 mt-0.5 line-clamp-1 italic font-medium">
                {league.description}
              </p>
            )}

            <div className="flex items-center gap-2 text-[10px] text-slate-400 font-bold uppercase tracking-wider mt-2">
              <span className="flex items-center gap-1">
                <Users size={10} /> medlemmar: -
              </span>

              <span>•</span>

              <div 
                onClick={handleCopy}
                className="flex items-center gap-1.5 group/copy relative"
              >
                <span className="text-[9px] text-slate-300">Ligakod:</span>
                
                <span className={`flex items-center gap-1 font-mono px-1.5 py-0.5 rounded border transition-all ${
                  copied 
                  ? 'bg-accent border-accent text-white' 
                  : 'bg-slate-50 border-slate-100 text-slate-600 group-hover/copy:border-accent group-hover/copy:text-accent'
                }`}
                >
                  {league.invitationCode}
                  {copied ? (
                    <Check size={10} />
                  ) : ( 
                     <Copy size={10}className="opacity-40 group-hover/copy:opacity-100"/>
                  )}
                </span>

                {copied && (
                  <span className="absolute -top-7 left-1/2 -translate-x-1/2 bg-slate-800 text-white text-[9px] px-1.5 py-0.5 rounded shadow-lg whitespace-nowrap">
                    Kopierad!
                  </span>
                )}
              </div>
            </div>
          </div>
        </div>

        <div className="flex-shrink-0">
          {isExpanded ? (
            <ChevronDown size={24} className="text-accent" /> 
          ) : (
             <ChevronRight
              size={24}
              className="text-slate-300 group-hover:text-accent" 
              />
            )}
        </div>
      </button>

      {isExpanded && <LeagueInlineDetail leagueId={league.leagueId} />}
    </div>
  );
};