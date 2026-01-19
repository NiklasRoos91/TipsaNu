
import React from 'react';
import { Target, Plus, Trash2 } from 'lucide-react';
import { PointRule } from '../../types/types';

interface PointRulesInputProps {
  rules: PointRule[];
  onChange: (rules: PointRule[]) => void;
}

export const PointRulesInput: React.FC<PointRulesInputProps> = ({ rules, onChange }) => {
  const addRule = () => {
    const newRule: PointRule = {
      id: Math.random().toString(36).substr(2, 9),
      name: 'Ny regel',
      pointsForExactScore: 10,
      pointsForCorrectGoalDifference: 6,
      pointsForCorrectWinner: 4
    };
    onChange([...rules, newRule]);
  };

  const removeRule = (id: string) => {
    if (rules.length <= 1) return;
    onChange(rules.filter(r => r.id !== id));
  };

  const updateRule = (id: string, field: keyof PointRule, value: any) => {
    onChange(rules.map(r => r.id === id ? { ...r, [field]: value } : r));
  };

  return (
    <div className="space-y-4">
      <div className="flex items-center justify-between px-1">
        <div className="flex items-center gap-2">
          <Target size={18} className="text-purple-600" />
          <h4 className="text-xs font-bold text-slate-500 uppercase tracking-widest">Poängregler</h4>
        </div>
        <button 
          type="button" 
          onClick={addRule}
          className="p-1.5 bg-purple-50 text-purple-600 rounded-lg hover:bg-purple-100 transition-colors"
        >
          <Plus size={16} />
        </button>
      </div>

      <div className="grid gap-4">
        {rules.map((rule) => (
          <div key={rule.id} className="bg-white border border-slate-200 rounded-2xl p-4 shadow-sm relative group">
            <input 
              className="text-sm font-bold text-primary mb-3 bg-transparent border-none p-0 focus:ring-0 outline-none w-4/5"
              value={rule.name}
              onChange={e => updateRule(rule.id, 'name', e.target.value)}
            />
            <button 
              type="button" 
              onClick={() => removeRule(rule.id)}
              className="absolute top-4 right-4 text-slate-300 hover:text-red-500 opacity-0 group-hover:opacity-100 transition-all"
            >
              <Trash2 size={16} />
            </button>
            <div className="grid grid-cols-3 gap-3">
              <div className="text-center">
                <span className="block text-[8px] font-black text-slate-400 uppercase mb-1">Exakt</span>
                <input 
                  type="number"
                  value={rule.pointsForExactScore}
                  onChange={e => updateRule(rule.id, 'pointsForExactScore', parseInt(e.target.value))}
                  className="w-full text-center py-1 bg-slate-50 border border-slate-100 rounded-lg text-xs font-bold"
                />
              </div>
              <div className="text-center">
                <span className="block text-[8px] font-black text-slate-400 uppercase mb-1">Diff</span>
                <input 
                  type="number"
                  value={rule.pointsForCorrectGoalDifference}
                  onChange={e => updateRule(rule.id, 'pointsForCorrectGoalDifference', parseInt(e.target.value))}
                  className="w-full text-center py-1 bg-slate-50 border border-slate-100 rounded-lg text-xs font-bold"
                />
              </div>
              <div className="text-center">
                <span className="block text-[8px] font-black text-slate-400 uppercase mb-1">Tecken</span>
                <input 
                  type="number"
                  value={rule.pointsForCorrectWinner}
                  onChange={e => updateRule(rule.id, 'pointsForCorrectWinner', parseInt(e.target.value))}
                  className="w-full text-center py-1 bg-slate-50 border border-slate-100 rounded-lg text-xs font-bold"
                />
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};
