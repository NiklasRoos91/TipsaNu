
import React, { useState } from 'react';
import { LogIn } from 'lucide-react';
import { useMyLeaguesInTournament } from "../../hooks/useMyLeaguesInTournament.ts";
import { ActionButton } from '../commons/ActionButton';
import { FormButtons } from '../commons/FormButtons';
import { JoinButtons } from '../commons/JoinButtons';
import { LeagueAccordion } from '../league/LeagueAccordion.tsx';
import { useLeagueForm } from '../../hooks/useLeagueForm';

interface TournamentLeaguesProps {
  tournamentId: string;
}

export const TournamentLeagues: React.FC<TournamentLeaguesProps> = ({ tournamentId}) => {
  const tid = Number(tournamentId);
  const [expandedLeagueId, setExpandedLeagueId] = useState<string | null>(null);
  const [createSuccess, setCreateSuccess] = useState(false);
  const { leagues, loading, error, refetch } = useMyLeaguesInTournament(tid);
  
  const handleLeagueCreated = (newLeague: any) => {
  leagueForm.setShowCreate(false);
  leagueForm.setNewLeagueName('');

  setExpandedLeagueId(newLeague.leagueDto.leagueId.toString());
  refetch();
};

const handleLeagueJoined = (member: any) => {
  const leagueId = member.leagueId?.toString();
  if (!leagueId) return;

  setExpandedLeagueId(leagueId);
  refetch();
  leagueForm.setShowJoin(false); 
  leagueForm.setJoinCode(''); 
};

  const leagueForm = useLeagueForm(tournamentId, handleLeagueCreated, handleLeagueJoined);

  const handleCancelCreate = () => {
    leagueForm.setShowCreate(false);
    leagueForm.setNewLeagueName('');
  };

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
                leagueForm.setShowJoin(!leagueForm.showJoin);
                if (!leagueForm.showJoin) leagueForm.setShowCreate(false);
              }
            }}
            isActive={leagueForm.showJoin}
          />

          <ActionButton 
            label="Skapa ny" 
            onClick={() => { 
              leagueForm.setShowCreate(!leagueForm.showCreate);
              if (!leagueForm.showCreate) leagueForm.setShowJoin(false);
            }}
            isActive={leagueForm.showCreate}
          />
        </div>
      </div>

      {leagueForm.showCreate && (
        <div className="bg-white border border-slate-200 rounded-2xl p-6 shadow-sm animate-fade-in max-w-lg">
          <form onSubmit={leagueForm.handleCreate}>
            <label className="block text-[10px] font-black text-slate-400 uppercase mb-2 tracking-widest">
              Liganamn
            </label>
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
              showSuccess={createSuccess}
              onCancel={handleCancelCreate}
              isCreate={true}
              saveLabel="Liga"
              successLabel="Skapad!"
              layout="row"
            />
          </form>
        </div>
      )}

      {leagueForm.showJoin && (
        <div className="bg-white border border-slate-200 rounded-2xl p-6 shadow-sm animate-fade-in max-w-lg">
          <div className="mb-4 border-b border-slate-100 pb-3">
            <h3 className="font-bold text-primary">Gå med i liga</h3>
          </div>

          <form onSubmit={leagueForm.handleJoin}>
            <label className="block text-[10px] font-black text-slate-400 uppercase mb-2 tracking-widest">
              Inbjudningskod
              </label>

            <div className="space-y-4">
              <input 
                value={leagueForm.joinCode}
                onChange={e => 
                  leagueForm.setJoinCode(e.target.value.toUpperCase())
                }
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

      {/* League List */}
      <div className="space-y-4">
        {loading && (
          <div className="text-center p-10 text-slate-500">
            Laddar ligor...
          </div>
        )}

        {error && (
          <div className="text-center p-10 text-red-500">{error}</div>
        )}

        {!loading && !error && leagues.length > 0 && (
          <>
            {leagues.map((l) => (
              <LeagueAccordion
                key={l.leagueId}
                league={l}
                isExpanded={expandedLeagueId === l.leagueId.toString()}
                onToggle={() =>
                  setExpandedLeagueId(
                    expandedLeagueId === l.leagueId.toString() ? null : l.leagueId.toString()
                  )
                }
              />
            ))}
          </>
        )}

        {!loading && !error && leagues.length === 0 && (
          <div className="text-center p-16 bg-slate-50 rounded-3xl border-2 border-dashed border-slate-200 text-slate-400">
            <p className="font-medium">Inga ligor för denna turnering ännu.</p>
            <p className="text-sm">Skapa en egen liga och bjud in dina vänner!</p>
          </div>
        )}
      </div>
    </div>
  );
};