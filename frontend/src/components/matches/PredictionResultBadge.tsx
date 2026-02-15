import React from 'react';
import { Check } from 'lucide-react';
import { MatchStatusEnum } from '../../types/enums/matchEnums';

interface PredictionResultBadgeProps {
  prediction: { predictedHomeScore: number; predictedAwayScore: number } | null;
  matchResult: { scoreHome: number | null; scoreAway: number | null };
  matchStatus: MatchStatusEnum;
  isExpanded?: boolean;
}

export const PredictionResultBadge: React.FC<PredictionResultBadgeProps> = ({
  prediction,
  matchResult,
  matchStatus,
  isExpanded = false
}) => {
  const { scoreHome, scoreAway } = matchResult;
  const isFinished = matchStatus === MatchStatusEnum.Finished;

  if (isFinished && !prediction) {
    return (
      <div className="text-[10px] font-bold text-slate-300 bg-slate-50 px-2 py-0.5 rounded-full uppercase italic">
        Ej tippad
      </div>
    );
  }

  if (prediction) {
    const { predictedHomeScore, predictedAwayScore } = prediction;

    let color = '';
    let text = `${predictedHomeScore}-${predictedAwayScore}`;

    if (scoreHome != null && scoreAway != null) {
      const predictedDiff = predictedHomeScore - predictedAwayScore;
      const actualDiff = scoreHome - scoreAway;

      if (predictedHomeScore === scoreHome && predictedAwayScore === scoreAway) {
        color = 'bg-emerald-50 text-emerald-600 border-emerald-100';
      } else if (
        (predictedDiff > 0 && actualDiff > 0) ||
        (predictedDiff < 0 && actualDiff < 0) ||
        (predictedDiff === 0 && actualDiff === 0)
      ) {
        color = 'bg-yellow-50 text-yellow-600 border-yellow-100';
      } else {
        color = 'bg-red-50 text-red-600 border-red-100';
      }
    return (
      <div className={`flex items-center gap-1 text-[10px] font-bold px-2 py-0.5 rounded-full border ${color}`}>
        <Check size={10} />
        <span>{text}</span>
      </div>
    );
  } else {
    return (
      <div className="text-[10px] font-bold px-2 py-0.5 rounded-full border bg-slate-100 text-slate-400">
        {text}
      </div>
    );
  }
}

  return (
    <div className={`text-[10px] font-bold px-2 py-0.5 rounded-full transition-colors ${
      isExpanded ? 'bg-blue-200 text-blue-800' : 'text-slate-400 bg-slate-100'
    }`}>
      TIPPA
    </div>
  );
};
