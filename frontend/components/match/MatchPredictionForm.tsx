
import React from 'react';
import { FormButtons } from '../common/FormButtons';

interface MatchPredictionFormProps {
  homeTeamName: string;
  awayTeamName: string;
  homePred: number | '';
  awayPred: number | '';
  isSubmitting: boolean;
  showSuccess: boolean;
  hasExistingPrediction: boolean;
  onHomeChange: (val: number | '') => void;
  onAwayChange: (val: number | '') => void;
  onSubmit: (e: React.FormEvent) => void;
  onCancel: () => void;
}

export const MatchPredictionForm: React.FC<MatchPredictionFormProps> = ({
  homeTeamName,
  awayTeamName,
  homePred,
  awayPred,
  isSubmitting,
  showSuccess,
  hasExistingPrediction,
  onHomeChange,
  onAwayChange,
  onSubmit,
  onCancel,
}) => {
  const displayHome = homePred === '' ? 0 : homePred;
  const displayAway = awayPred === '' ? 0 : awayPred;

  const handleChange = (val: string, setter: (v: number | '') => void) => {
    if (val === '') {
      setter(0);
      return;
    }
    const num = parseInt(val, 10);
    if (!isNaN(num) && num >= 0 && num <= 99) {
      setter(num);
    }
  };

  return (
    <form onSubmit={onSubmit} className="flex flex-col gap-8 max-w-sm mx-auto">
      <div className="flex justify-center items-center gap-6">
         <div className="text-center">
             <label className="block text-[10px] font-black text-slate-400 mb-2 uppercase tracking-widest">{homeTeamName}</label>
             <input 
               type="number" 
               min="0"
               max="99"
               value={displayHome}
               onChange={e => handleChange(e.target.value, onHomeChange)}
               className="w-20 h-20 md:w-24 md:h-24 text-center text-3xl md:text-4xl font-black bg-slate-50 border-2 border-slate-200 rounded-2xl focus:border-accent focus:ring-4 focus:ring-accent/10 outline-none transition-all text-slate-900 shadow-inner"
             />
         </div>
         <span className="text-3xl md:text-4xl font-black text-slate-300 mt-6">-</span>
         <div className="text-center">
             <label className="block text-[10px] font-black text-slate-400 mb-2 uppercase tracking-widest">{awayTeamName}</label>
             <input 
               type="number" 
               min="0"
               max="99"
               value={displayAway}
               onChange={e => handleChange(e.target.value, onAwayChange)}
               className="w-20 h-20 md:w-24 md:h-24 text-center text-3xl md:text-4xl font-black bg-slate-50 border-2 border-slate-200 rounded-2xl focus:border-accent focus:ring-4 focus:ring-accent/10 outline-none transition-all text-slate-900 shadow-inner"
             />
         </div>
      </div>
      
      <FormButtons 
        isSubmitting={isSubmitting}
        showSuccess={showSuccess}
        hasExistingData={hasExistingPrediction}
        onCancel={onCancel}
        saveLabel="Tips"
        successLabel="Tippat & Klart!"
      />
    </form>
  );
};
