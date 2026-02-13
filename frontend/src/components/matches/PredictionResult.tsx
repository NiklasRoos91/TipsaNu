import React from 'react';
import { MatchPredictionDto } from '../../types/matchTypes';

interface PredictionResultProps {
  prediction: MatchPredictionDto;
}

export const PredictionResult: React.FC<PredictionResultProps> = ({ prediction }) => {
  const isSuccess = prediction.pointsAwarded && prediction.pointsAwarded > 0;

  return (
    <div className={`text-center p-6 rounded-xl mb-6 border-2 ${
       isSuccess 
       ? 'bg-emerald-50 text-emerald-800 border-emerald-200' 
       : 'bg-slate-50 text-slate-600 border-slate-100'
    }`}>
      <p className="font-medium text-lg mb-1">Du tippade</p>
      <div className="text-3xl font-bold font-mono">{prediction.predictedHomeScore} - {prediction.predictedAwayScore}</div>
      {prediction.pointsAwarded !== undefined && (
        <div className="mt-3 inline-block bg-white px-3 py-1 rounded-full text-sm font-bold shadow-sm">
          Poäng: {prediction.pointsAwarded}
        </div>
      )}
    </div>
  );
};