import React from 'react';

interface PredictionProgressBarProps {
  totalMatches: number;
  predictedMatches: number;
}

export const PredictionProgressBar: React.FC<PredictionProgressBarProps> = ({
  totalMatches,
  predictedMatches
}) => {
  const width = totalMatches > 0 ? (predictedMatches / totalMatches) * 100 : 0;

  return (
    <div className="mb-4">
      <div className="flex justify-between items-center mb-2">
        <span className="text-sm text-slate-600">Tippade matcher</span>
        <span className="font-bold text-primary">{predictedMatches} / {totalMatches}</span>
      </div>
      <div className="w-full bg-slate-100 rounded-full h-2 overflow-hidden">
        <div 
          className="bg-accent h-2 rounded-full transition-all duration-1000 ease-out" 
          style={{ width: `${width}%` }}
        />
      </div>
    </div>
  );
};
