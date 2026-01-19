
import React from 'react';
import { Users, LogIn, CheckSquare } from 'lucide-react';

interface GroupSettingsInputProps {
  totalGroups: number;
  advancingPerGroup: number;
  allowsBestThird: boolean;
  onChange: (field: string, value: any) => void;
}

export const GroupSettingsInput: React.FC<GroupSettingsInputProps> = ({
  totalGroups,
  advancingPerGroup,
  allowsBestThird,
  onChange
}) => (
  <div className="bg-slate-50 p-6 rounded-2xl border border-slate-200 space-y-6">
    <div className="flex items-center gap-2 mb-2">
      <Users size={18} className="text-accent" />
      <h4 className="text-xs font-bold text-slate-500 uppercase tracking-widest">Gruppinställningar</h4>
    </div>
    
    <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
      <div>
        <label className="block text-[10px] font-black text-slate-400 uppercase mb-1.5 ml-1">Antal Grupper</label>
        <input 
          type="number"
          min="1"
          value={totalGroups}
          onChange={e => onChange('totalGroups', parseInt(e.target.value))}
          className="w-full px-4 py-2 bg-white border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none font-bold text-slate-900 shadow-inner"
        />
      </div>
      <div>
        <label className="block text-[10px] font-black text-slate-400 uppercase mb-1.5 ml-1">Vidare per grupp</label>
        <input 
          type="number"
          min="1"
          value={advancingPerGroup}
          onChange={e => onChange('advancingPerGroup', parseInt(e.target.value))}
          className="w-full px-4 py-2 bg-white border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none font-bold text-slate-900 shadow-inner"
        />
      </div>
    </div>

    <button
      type="button"
      onClick={() => onChange('allowsBestThird', !allowsBestThird)}
      className={`flex items-center gap-3 w-full px-4 py-3 rounded-xl border transition-all ${
        allowsBestThird ? 'bg-accent/10 border-accent text-accent' : 'bg-white border-slate-200 text-slate-400 hover:border-slate-300'
      }`}
    >
      <CheckSquare size={18} className={allowsBestThird ? 'text-accent' : 'text-slate-300'} />
      <span className="text-sm font-bold">Tillåt "Bästa treor"</span>
    </button>
  </div>
);
