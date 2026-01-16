import React from 'react';
import { Bell, CheckCircle } from 'lucide-react';

export const EmptyState: React.FC = () => {
  return (
    <div className="p-16 text-center text-slate-500 flex flex-col items-center gap-4 bg-white rounded-2xl border-2 border-dashed border-slate-100">
      <div className="w-16 h-16 bg-slate-50 rounded-full flex items-center justify-center">
        <Bell size={32} className="text-slate-300" />
      </div>
      <div>
        <h3 className="text-lg font-bold text-slate-800">Inga nya notiser</h3>
        <p className="text-sm text-slate-400 max-w-xs mx-auto mt-1">
          Du är helt up-to-date! Vi meddelar dig när något spännande händer.
        </p>
      </div>
      <div className="mt-2 flex items-center gap-2 text-accent font-bold text-xs uppercase tracking-widest bg-emerald-50 px-4 py-2 rounded-full border border-emerald-100">
        <CheckCircle size={14} /> Allt klart
      </div>
    </div>
  );
};