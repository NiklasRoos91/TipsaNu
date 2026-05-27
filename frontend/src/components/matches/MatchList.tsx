import React from 'react';
import { MatchCard } from '../matches/MatchCard';
import { Match, UIPrediction  } from '../../types/matchTypes';

interface MatchListProps {
  matches: Match[];
  predictions: UIPrediction [];
  groups: any[];
  refreshPredictions?: () => void;
  refreshMatches?: () => void;
}

export const MatchList: React.FC<MatchListProps> = ({matches, predictions, groups, refreshPredictions, refreshMatches }) => {
  return (
    <div className="space-y-4 animate-fade-in">
      {matches.length > 0 ? (
        matches.map(match => {
          const initialPrediction = predictions.find(p => p.matchId === match.matchId) ?? null;
          const mappedPrediction = initialPrediction
            ? { homeScore: initialPrediction.predictedHomeScore, awayScore: initialPrediction.predictedAwayScore }
            : null;
          return (
            <MatchCard 
              key={match.matchId}
              match={match}
              prediction={mappedPrediction}
              groups={groups}
              refreshPredictions={refreshPredictions}
              refreshMatches={refreshMatches}
            />
          );
        })
      ) : (
        <div className="text-center p-12 bg-white rounded-xl border border-slate-200 text-slate-400">
          Inga matcher hittades för det valda filtret.
        </div>
      )}
    </div>
  );
};
