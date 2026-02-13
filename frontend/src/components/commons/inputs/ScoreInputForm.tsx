import React, { useState } from "react";
import { FormButtons } from "../FormButtons";

interface ScoreInputFormProps {
  homeTeamName: string;
  awayTeamName: string;
  homeScore: number;
  awayScore: number;
  onHomeChange: (value: number) => void;
  onAwayChange: (value: number) => void;
  isSubmitting: boolean;
  showSuccess?: boolean;
  hasExistingData?: boolean;
  onSubmit: (e: React.FormEvent) => void;
  onCancel: () => void;
  saveLabel?: string;
  successLabel?: string;
}

export const ScoreInputForm: React.FC<ScoreInputFormProps> = ({
  homeTeamName,
  awayTeamName,
  homeScore,
  awayScore,
  onHomeChange,
  onAwayChange,
  isSubmitting,
  showSuccess,
  hasExistingData,
  onSubmit,
  onCancel,
  saveLabel = "Spara",
  successLabel = "Klart!"
}) => {
  const [internalSuccess, setInternalSuccess] = useState(false);

  const handleSubmitForm = async (e: React.FormEvent) => {
    e.preventDefault();
    await onSubmit(e);
    setInternalSuccess(true);
    setTimeout(() => setInternalSuccess(false), 2000);
  };

  return (
    <form onSubmit={handleSubmitForm} className="flex flex-col gap-8 max-w-sm mx-auto">
      <div className="flex justify-center items-center gap-6">
        <div className="text-center">
          <label className="block text-[10px] font-black text-slate-400 mb-2 uppercase tracking-widest">
            {homeTeamName}
          </label>
          <input
            type="number"
            min={0}
            max={99}
            value={homeScore}
            onChange={(e) => onHomeChange(parseInt(e.target.value))}

            className="w-20 h-20 md:w-24 md:h-24 text-center text-3xl md:text-4xl font-black bg-slate-50 border-2 border-slate-200 rounded-2xl focus:border-accent focus:ring-4 focus:ring-accent/10 outline-none transition-all text-slate-900 shadow-inner"
          />
        </div>

        <span className="text-3xl md:text-4xl font-black text-slate-300 mt-6">-</span>

        <div className="text-center">
          <label className="block text-[10px] font-black text-slate-400 mb-2 uppercase tracking-widest">
            {awayTeamName}
          </label>
          <input
            type="number"
            min={0}
            max={99}
            value={awayScore}
            onChange={(e) => onAwayChange(parseInt(e.target.value))}
            className="w-20 h-20 md:w-24 md:h-24 text-center text-3xl md:text-4xl font-black bg-slate-50 border-2 border-slate-200 rounded-2xl focus:border-accent focus:ring-4 focus:ring-accent/10 outline-none transition-all text-slate-900 shadow-inner"
          />
        </div>
      </div>

      <FormButtons
        isSubmitting={isSubmitting}
        showSuccess={showSuccess ?? internalSuccess}
        hasExistingData={hasExistingData}
        onCancel={onCancel}
        saveLabel={saveLabel}
        successLabel={successLabel}
      />
    </form>
  );
};
