import React from 'react';

interface ProgressBarProps {
  total: number;
  progress: number;
  label: string;     
}

export const PredictionProgressBar: React.FC<ProgressBarProps> = ({
  total,
  progress,
  label
}) => {
  const width = total > 0 ? (progress / total) * 100 : 0;

  return (
    <div className="mb-4">
      <div className="flex justify-between items-center mb-2">
        <span className="text-sm text-slate-600">{label}</span>
        <span className="font-bold text-primary">{progress} / {total}</span>
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
