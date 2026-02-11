import React from 'react';
import { FormButtons } from '../common/FormButtons';

interface MatchPredictionFormProps {
  homeTeamName: string;
  awayTeamName: string;
  isSubmitting: boolean;
  showSuccess: boolean;
  hasExistingPrediction: boolean;
  onSubmit: (e: React.FormEvent) => void;
  onCancel: () => void;
  homePred: number | '';   // Dynamiskt värde för hemmapoäng
  awayPred: number | '';   // Dynamiskt värde för bortapoäng
  onHomePredChange: (value: number | '') => void;  // Förändring av hemmapoäng
  onAwayPredChange: (value: number | '') => void;  // Förändring av bortapoäng
}

export const MatchPredictionForm: React.FC<MatchPredictionFormProps> = ({
  homeTeamName,
  awayTeamName,
  isSubmitting,
  showSuccess,
  hasExistingPrediction,
  onSubmit,
  onCancel,
  homePred,
  awayPred,
  onHomePredChange,
  onAwayPredChange
}) => {

  return (
    <form onSubmit={onSubmit} className="flex flex-col gap-8 max-w-sm mx-auto">
      <div className="flex justify-center items-center gap-6">
         <div className="text-center">
             <label className="block text-[10px] font-black text-slate-400 mb-2 uppercase tracking-widest">{homeTeamName}</label>
             <input 
               type="number" 
               min="0"
               max="99"
               value={homePred}
               onChange={(e) => onHomePredChange(e.target.value === '' ? '' : parseInt(e.target.value))}
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
               value={awayPred}
               onChange={(e) => onAwayPredChange(e.target.value === '' ? '' : parseInt(e.target.value))}
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
