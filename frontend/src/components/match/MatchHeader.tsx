import React from 'react';
import { Match } from '../../types/matchTypes';
import { MatchStatusEnum } from '../../types/enums/matchEnums';

interface MatchHeaderProps {
  match: Match;
}

export const MatchHeader: React.FC<MatchHeaderProps> = ({ match }) => {
  const isLive = match.status === MatchStatusEnum.InProgress;

  const groupName = match.groupId ? `Group ${match.groupId}` : 'Unknown Group'; // Här kan du använda riktig gruppdata om du har det

  return (
    <div className="bg-primary p-6 text-white text-center relative">
      <div className="text-sm font-medium opacity-75 mb-4 uppercase tracking-wider">
        {groupName} • {new Date(match.startTime).toLocaleString()}
      </div>
      
      <div className="flex justify-center items-center gap-4 md:gap-12">
        <div className="flex flex-col items-center gap-2 flex-1">
           <img src={match.homeCompetitorName} alt="" className="w-16 h-10 md:w-24 md:h-16 object-cover rounded shadow border-2 border-white/20" />
           <span className="font-bold text-lg md:text-xl">{match.homeCompetitorName}</span>
        </div>
        
        <div className="text-3xl md:text-5xl font-mono font-bold mx-2">
          {match.status === MatchStatusEnum.Scheduled ? 'VS' : `${match.scoreHome} - ${match.scoreAway}`}
        </div>
        
        <div className="flex flex-col items-center gap-2 flex-1">
           <img src={match.awayCompetitorName} alt="" className="w-16 h-10 md:w-24 md:h-16 object-cover rounded shadow border-2 border-white/20" />
           <span className="font-bold text-lg md:text-xl">{match.awayCompetitorName}</span>
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