
import React, { useState } from 'react';
import { LogIn } from 'lucide-react';
import { League } from '../../types/types';
import { useLeagueForm } from '../../hooks/useLeagueForm';
import { LeagueAccordion } from '../league/LeagueAccordion';
import { ActionButton } from '../commons/ActionButton';
import { FormButtons } from '../commons/FormButtons';
import { JoinButtons } from '../commons/JoinButtons';

interface TournamentLeaguesProps {
  tournamentId: string;
  leagues: League[];
  onLeaguesUpdate: (updatedLeagues: League[]) => void;
}

export const TournamentLeagues: React.FC<TournamentLeaguesProps> = ({ 
  tournamentId, 
  leagues, 
  onLeaguesUpdate 
}) => {
  const [expandedLeagueId, setExpandedLeagueId] = useState<string | null>(null);
  
  const leagueForm = useLeagueForm(tournamentId, (l) => {
    onLeaguesUpdate([...leagues.filter(ex => ex.id !== l.id), l]);
    setExpandedLeagueId(l.id); // Expand the newly added league
  });

  return (
    <div className="space-y-8 animate-fade-in">
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
        <h2 className="text-2xl font-bold text-primary">Ligor</h2>
        <div className="flex gap-2 ml-auto">
          <ActionButton 
            label="Gå med" 
            variant="secondary"
            icon={LogIn}
            onClick={() => { 
              if (leagueForm.showJoin) {
                leagueForm.setShowJoin(false);
              } else {
                leagueForm.setShowJoin(true); 
                leagueForm.setShowCreate(false);
              }
            }}
            isActive={leagueForm.showJoin}
          />
          <ActionButton 
            label="Skapa ny" 
            onClick={() => { 
              if (leagueForm.showCreate) {
                leagueForm.setShowCreate(false);
              } else {
                leagueForm.setShowCreate(true); 
                leagueForm.setShowJoin(false);
              }
            }}
            isActive={leagueForm.showCreate}
          />
        </div>
      </div>

      {leagueForm.showCreate && (
        <div className="bg-white border border-slate-200 rounded-2xl p-6 shadow-sm animate-fade-in max-w-lg">
          <div className="mb-4 border-b border-slate-100 pb-3">
            <h3 className="font-bold text-primary">Skapa ny liga</h3>
          </div>
          <form onSubmit={leagueForm.handleCreate}>
            <label className="block text-[10px] font-black text-slate-400 uppercase mb-2 tracking-widest">Liganamn</label>
            <div className="space-y-4">
              <input 
                value={leagueForm.newLeagueName}
                onChange={e => leagueForm.setNewLeagueName(e.target.value)}
                className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all font-medium text-slate-900 shadow-inner"
                placeholder="T.ex. Jobbkompisarna"
                required
                autoFocus
              />
              <FormButtons 
                isSubmitting={leagueForm.isProcessing}
                showSuccess={false}
                onCancel={() => leagueForm.setShowCreate(false)}
                isCreate={true}
                saveLabel="Liga"
                layout="row"
              />
            </div>
          </form>
        </div>
      )}

      {leagueForm.showJoin && (
        <div className="bg-white border border-slate-200 rounded-2xl p-6 shadow-sm animate-fade-in max-w-lg">
          <div className="mb-4 border-b border-slate-100 pb-3">
            <h3 className="font-bold text-primary">Gå med i liga</h3>
          </div>
          <form onSubmit={leagueForm.handleJoin}>
            <label className="block text-[10px] font-black text-slate-400 uppercase mb-2 tracking-widest">Inbjudningskod</label>
            <div className="space-y-4">
              <input 
                value={leagueForm.joinCode}
                onChange={e => leagueForm.setJoinCode(e.target.value.toUpperCase())}
                className="w-full px-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-blue-500/10 focus:border-blue-500 outline-none font-mono font-bold transition-all text-slate-900 shadow-inner"
                placeholder="KOD123"
                required
              />
              <JoinButtons 
                isProcessing={leagueForm.isProcessing}
                onCancel={() => leagueForm.setShowJoin(false)}
                layout="row"
              />
            </div>
          </form>
        </div>
      )}

      <div className="space-y-4">
        {leagues.map(l => (
          <LeagueAccordion 
            key={l.id} 
            league={l} 
            isExpanded={expandedLeagueId === l.id}
            onToggle={() => setExpandedLeagueId(expandedLeagueId === l.id ? null : l.id)}
          />
        ))}
        {leagues.length === 0 && (
          <div className="text-center p-16 bg-slate-50 rounded-3xl border-2 border-dashed border-slate-200 text-slate-400">
            <p className="font-medium">Inga ligor för denna turnering ännu.</p>
            <p className="text-sm">Skapa en egen liga och bjud in dina vänner!</p>
          </div>
        )}
      </div>
    </div>
  );
};
