import React from 'react';
import { Check } from 'lucide-react';

export interface ExtraBetResultBadgeProps {
  prediction: string | null;
  correctAnswer: string | string[];
  isExpanded?: boolean; 
}

export const ExtraBetResultBadge: React.FC<ExtraBetResultBadgeProps> = ({
  prediction,
  correctAnswer,
  isExpanded = false
}) => {
  const hasCorrectAnswer = Array.isArray(correctAnswer) ? correctAnswer.length > 0 : !!correctAnswer;

  // Ingen correctAnswer → grå oavsett prediction
  if (!hasCorrectAnswer) {
    return (
      <div className={`text-[10px] font-bold px-2 py-0.5 rounded-full ${isExpanded ? 'bg-blue-200 text-blue-800' : 'bg-slate-100 text-slate-400'}`}>
        {prediction || 'TIPPA'}
      </div>
    );
  }

  // Finns correctAnswer → titta om prediction matchar
  const isCorrect = Array.isArray(correctAnswer)
    ? prediction && correctAnswer.includes(prediction)
    : prediction === correctAnswer;

  const color = !prediction
    ? 'text-slate-300 bg-slate-50 italic'          // Ej tippad
    : isCorrect
    ? 'bg-emerald-50 text-emerald-600 border-emerald-100'  // Rätt
    : 'bg-red-50 text-red-600 border-red-100';     // Fel

  return (
    <div className={`flex items-center gap-1 text-[10px] font-bold px-2 py-0.5 rounded-full border ${color}`}>
      {prediction && <Check size={10} />}
      <span>{prediction || 'Ej tippad'}</span>
      {Array.isArray(correctAnswer) && correctAnswer.length > 1 && (
        <span className="ml-1 text-xs text-slate-400">({correctAnswer.join(", ")})</span>
      )}
    </div>
  );
};
