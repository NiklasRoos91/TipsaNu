
import React from 'react';
import { ListOrdered, Plus, Trash2 } from 'lucide-react';
import { TournamentTiebreaker, TiebreakerCriterion } from '../../types';

interface TiebreakersInputProps {
  tiebreakers: TournamentTiebreaker[];
  onChange: (tb: TournamentTiebreaker[]) => void;
}

export const TiebreakersInput: React.FC<TiebreakersInputProps> = ({ tiebreakers, onChange }) => {
  const addTiebreaker = () => {
    const newTb: TournamentTiebreaker = {
      criterion: TiebreakerCriterion.GoalDifference,
      priority: tiebreakers.length + 1
    };
    onChange([...tiebreakers, newTb]);
  };

  const removeTiebreaker = (index: number) => {
    const updated = tiebreakers.filter((_, i) => i !== index).map((tb, i) => ({ ...tb, priority: i + 1 }));
    onChange(updated);
  };

  const updateCriterion = (index: number, criterion: TiebreakerCriterion) => {
    onChange(tiebreakers.map((tb, i) => i === index ? { ...tb, criterion } : tb));
  };

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between px-1">
        <div className="flex items-center gap-2">
          <ListOrdered size={18} className="text-blue-600" />
          <h4 className="text-xs font-bold text-slate-500 uppercase tracking-widest">Skiljeregel (Tiebreakers)</h4>
        </div>
        <button 
          type="button" 
          onClick={addTiebreaker}
          className="p-1.5 bg-blue-50 text-blue-600 rounded-lg hover:bg-blue-100 transition-colors"
        >
          <Plus size={16} />
        </button>
      </div>

      <div className="space-y-2">
        {tiebreakers.map((tb, idx) => (
          <div key={idx} className="flex items-center gap-3 bg-white border border-slate-200 p-3 rounded-xl shadow-sm">
            <span className="w-6 h-6 rounded-full bg-slate-100 flex items-center justify-center text-[10px] font-black text-slate-400">
              {tb.priority}
            </span>
            <select
              value={tb.criterion}
              onChange={e => updateCriterion(idx, e.target.value as TiebreakerCriterion)}
              className="flex-1 bg-transparent border-none focus:ring-0 text-sm font-bold text-slate-700 outline-none"
            >
              {Object.entries(TiebreakerCriterion).map(([key, label]) => (
                <option key={key} value={label}>{label}</option>
              ))}
            </select>
            <button 
              type="button" 
              onClick={() => removeTiebreaker(idx)}
              className="text-slate-300 hover:text-red-500 transition-colors"
            >
              <Trash2 size={16} />
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};
