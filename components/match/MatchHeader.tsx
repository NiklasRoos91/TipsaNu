import React from 'react';
import { Match, MatchStatus } from '../../types';

interface MatchHeaderProps {
  match: Match;
}

export const MatchHeader: React.FC<MatchHeaderProps> = ({ match }) => {
  const isLive = match.status === MatchStatus.LIVE;

  return (
    <div className="bg-primary p-6 text-white text-center relative">
      <div className="text-sm font-medium opacity-75 mb-4 uppercase tracking-wider">
        {match.group} • {new Date(match.startTime).toLocaleString()}
      </div>
      
      <div className="flex justify-center items-center gap-4 md:gap-12">
        <div className="flex flex-col items-center gap-2 flex-1">
           <img src={match.homeTeam.flagUrl} alt="" className="w-16 h-10 md:w-24 md:h-16 object-cover rounded shadow border-2 border-white/20" />
           <span className="font-bold text-lg md:text-xl">{match.homeTeam.name}</span>
        </div>
        
        <div className="text-3xl md:text-5xl font-mono font-bold mx-2">
          {match.status === MatchStatus.SCHEDULED ? 'VS' : `${match.homeScore} - ${match.awayScore}`}
        </div>
        
        <div className="flex flex-col items-center gap-2 flex-1">
           <img src={match.awayTeam.flagUrl} alt="" className="w-16 h-10 md:w-24 md:h-16 object-cover rounded shadow border-2 border-white/20" />
           <span className="font-bold text-lg md:text-xl">{match.awayTeam.name}</span>
        </div>
      </div>
      
      {isLive && (
        <div className="mt-6 inline-flex items-center gap-2 bg-red-600 px-4 py-1 rounded-full text-xs font-bold animate-pulse shadow-lg">
            <span className="w-2 h-2 bg-white rounded-full"></span> LIVE
        </div>
      )}
    </div>
  );
};