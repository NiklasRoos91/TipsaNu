import React from 'react';

interface MatchCategorySelectorProps {
  matchCategory: 'groups' | 'knockout';
  setMatchCategory: (cat: 'groups' | 'knockout') => void;
}

export const MatchCategorySelector: React.FC<MatchCategorySelectorProps> = ({
  matchCategory,
  setMatchCategory
}) => {
  return (
    <div className="flex bg-slate-100 p-1 rounded-lg">
      <button
        onClick={() => setMatchCategory('groups')}
        className={`px-4 py-1.5 text-xs font-bold rounded-md transition-all ${matchCategory === 'groups' ? 'bg-white text-primary shadow-sm' : 'text-slate-500 hover:text-slate-700'}`}
      >
        Gruppspel
      </button>
      <button
        onClick={() => setMatchCategory('knockout')}
        className={`px-4 py-1.5 text-xs font-bold rounded-md transition-all ${matchCategory === 'knockout' ? 'bg-white text-primary shadow-sm' : 'text-slate-500 hover:text-slate-700'}`}
      >
        Slutspel
      </button>
    </div>
  );
};
