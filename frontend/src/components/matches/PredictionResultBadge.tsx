import React from 'react';
import { MatchStatusEnum } from '../../types/enums/matchEnums';

interface PredictionResultBadgeProps {
  prediction: { predictedHomeScore: number; predictedAwayScore: number } | null;
  matchResult: { scoreHome: number | null; scoreAway: number | null };
  matchStatus: MatchStatusEnum;
}

export const PredictionResultBadge: React.FC<PredictionResultBadgeProps> = ({
  prediction,
  matchResult,
  matchStatus,
}) => {
    // Only show badge if there is a prediction and the match is finished
  if (!prediction || matchStatus !== MatchStatusEnum.Finished) return null;

  const { predictedHomeScore, predictedAwayScore } = prediction;
  const { scoreHome, scoreAway } = matchResult;

  if (scoreHome == null || scoreAway == null) return null;

  let color = '';
  let text = '';

  // Exact match
  if (predictedHomeScore === scoreHome && predictedAwayScore === scoreAway) {
    color = 'bg-emerald-50 text-emerald-600 border-emerald-100';
    text = `${predictedHomeScore}-${predictedAwayScore}`;
  } else {
    // Correct outcome but wrong score
    const predictedDiff = predictedHomeScore - predictedAwayScore;
    const actualDiff = scoreHome - scoreAway;

    if (
      (predictedDiff > 0 && actualDiff > 0) ||  // Correctly predicted home win
      (predictedDiff < 0 && actualDiff < 0) ||  // Correctly predicted away win
      (predictedDiff === 0 && actualDiff === 0) // Correctly predicted draw
    ) {
      color = 'bg-yellow-50 text-yellow-600 border-yellow-100';
      text = `${predictedHomeScore}-${predictedAwayScore}`;
    } else {
        // Wrong outcome
        color = 'bg-red-50 text-red-600 border-red-100';
      text = `${predictedHomeScore}-${predictedAwayScore}`;
    }
  }

  return (
    <div className={`flex items-center gap-1 text-[10px] font-bold px-2 py-0.5 rounded-full border ${color}`}>
      {text}
    </div>
  );
};
