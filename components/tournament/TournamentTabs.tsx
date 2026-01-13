import React from 'react';

export type TabType = 'matches' | 'extrabets' | 'leagues';

interface TournamentTabsProps {
  activeTab: TabType;
  setActiveTab: (tab: TabType) => void;
  leaguesCount: number;
  extraBetsCount: number;
}

export const TournamentTabs: React.FC<TournamentTabsProps> = ({ 
  activeTab, 
  setActiveTab, 
  leaguesCount, 
  extraBetsCount 
}) => {
  return (
    <div className="flex border-b border-slate-200 mb-8 overflow-x-auto gap-8">
      <button 
        onClick={() => setActiveTab('matches')}
        className={`pb-4 px-2 text-sm font-bold transition-all border-b-2 whitespace-nowrap ${activeTab === 'matches' ? 'border-accent text-accent' : 'border-transparent text-slate-400 hover:text-slate-600'}`}
      >
        SCHEMA & MATCHER
      </button>
      <button 
        onClick={() => setActiveTab('leagues')}
        className={`pb-4 px-2 text-sm font-bold transition-all border-b-2 whitespace-nowrap flex items-center gap-2 ${activeTab === 'leagues' ? 'border-accent text-accent' : 'border-transparent text-slate-400 hover:text-slate-600'}`}
      >
        👥 LIGOR
        {leaguesCount > 0 && <span className="bg-slate-100 text-slate-500 px-2 py-0.5 rounded-full text-[10px]">{leaguesCount}</span>}
      </button>
      <button 
        onClick={() => setActiveTab('extrabets')}
        className={`pb-4 px-2 text-sm font-bold transition-all border-b-2 whitespace-nowrap flex items-center gap-2 ${activeTab === 'extrabets' ? 'border-accent text-accent' : 'border-transparent text-slate-400 hover:text-slate-600'}`}
      >
        🎯 Extratips
        {extraBetsCount > 0 && <span className="bg-slate-100 text-slate-500 px-2 py-0.5 rounded-full text-[10px]">{extraBetsCount}</span>}
      </button>
    </div>
  );
};