import React, { useState, useEffect } from 'react';
import { useCreateMatch } from '../../hooks/matches/useCreateMatch';
import { useGroups } from '../../hooks/useGroups';
import { useCompetitors } from '../../hooks/matches/useCompetitors';
import { MatchTypeEnum } from '../../types/enums/matchEnums'; 

interface CreateMatchProps {
  tournamentId: number; 
  groupId?: number | null;
  onCreated?: () => void;
}

export const CreateMatch: React.FC<CreateMatchProps> = ({ tournamentId, groupId = null, onCreated }) => {
  const { handleCreateMatch, loading, error, success } = useCreateMatch();

  const [matchType, setMatchType] = useState<MatchTypeEnum>(MatchTypeEnum.Group);
  const [selectedGroupId, setSelectedGroupId] = useState<string>(groupId ? String(groupId) : '');
  const [homeCompetitorId, setHomeCompetitorId] = useState<number | ''>('');
  const [awayCompetitorId, setAwayCompetitorId] = useState<number | ''>('');
  const [roundNumber, setRoundNumber] = useState<number>(1);
  const [startTime, setStartTime] = useState<string>('');
  const [predictionDeadline, setPredictionDeadline] = useState<string>('');

  const { groups, loading: loadingGroups } = useGroups(tournamentId);
  
  const currentGroupIdForFilter = matchType === MatchTypeEnum.Group && selectedGroupId ? Number(selectedGroupId) : null;
  const { competitors, loading: loadingCompetitors } = useCompetitors(tournamentId, currentGroupIdForFilter);
  
  useEffect(() => {
    if (groupId) {
      setSelectedGroupId(String(groupId));
    }
  }, [groupId]);

  useEffect(() => {
    setHomeCompetitorId('');
    setAwayCompetitorId('');
  }, [matchType, selectedGroupId]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!homeCompetitorId || !awayCompetitorId) {
      alert('Vänligen välj både hemma- och bortalag.');
      return;
    }

    if (homeCompetitorId === awayCompetitorId) {
      alert('Ett lag kan inte spela mot sig själv.');
      return;
    }

    try {
      await handleCreateMatch({
        tournamentId,
        groupId: matchType === MatchTypeEnum.Group && selectedGroupId ? Number(selectedGroupId) : null,
        homeCompetitorId: Number(homeCompetitorId),
        awayCompetitorId: Number(awayCompetitorId),
        matchType,
        roundNumber,
        startTime: new Date(startTime).toISOString(),
        predictionDeadline: predictionDeadline ? new Date(predictionDeadline).toISOString() : null,
      });

      if (onCreated) {
        onCreated();
      }
    } catch (err) {
      console.error('Kunde inte skapa matchen i formuläret:', err);
    }
  };

  return (
    <div className="bg-white rounded-2xl border border-slate-200 p-6 max-w-xl mx-auto shadow-sm">
      <h3 className="text-xl font-bold text-primary mb-4">Skapa ny match</h3>

      {error && (
        <div className="mb-4 p-3 bg-red-50 text-red-600 text-sm rounded-xl font-medium border border-red-100">
          {error}
        </div>
      )}

      {success && (
        <div className="mb-4 p-3 bg-emerald-50 text-emerald-600 text-sm rounded-xl font-medium border border-emerald-100">
          Matchen har skapats utan problem!
        </div>
      )}

      <form onSubmit={handleSubmit} className="space-y-4">
        
        {/* Match Type */}
        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1">Matchtyp</label>
            <select
              value={matchType}
              onChange={(e) => {
                const nextType = Number(e.target.value) as MatchTypeEnum;
                setMatchType(nextType);
                if (nextType !== MatchTypeEnum.Group) {
                  setSelectedGroupId('');
                }
              }}
              className="w-full bg-slate-50 border border-slate-200 rounded-xl px-4 py-2.5 text-slate-900 font-medium outline-none focus:ring-2 focus:ring-accent"
            >
              <option value={MatchTypeEnum.Group}>Gruppspel</option>
              <option value={MatchTypeEnum.RoundOf16}>Åttondelsfinal</option>
              <option value={MatchTypeEnum.QuarterFinal}>Kvartsfinal</option>
              <option value={MatchTypeEnum.SemiFinal}>Semifinal</option>
              <option value={MatchTypeEnum.Final}>Final</option>
              <option value={MatchTypeEnum.ThirdPlace}>Bronsmatch</option>
              <option value={MatchTypeEnum.CustomKnockout}>Anpassat slutspel</option>
            </select>
          </div>
          <div>
            <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1">Omgång / Spelvecka</label>
            <input
              type="number"
              value={roundNumber}
              onChange={(e) => setRoundNumber(Number(e.target.value))}
              min={1}
              className="w-full bg-slate-50 border border-slate-200 rounded-xl px-4 py-2.5 text-slate-900 font-medium outline-none focus:ring-2 focus:ring-accent"
              required
            />
          </div>
        </div>
        
        {/* Group  */}
        {matchType === MatchTypeEnum.Group && (
          <div className="animate-fade-in">
            <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1">
              Välj Grupp
            </label>
            <select
              value={selectedGroupId}
              onChange={(e) => setSelectedGroupId(e.target.value)}
              className="w-full bg-slate-50 border border-slate-200 rounded-xl px-4 py-2.5 text-slate-900 font-medium outline-none focus:ring-2 focus:ring-accent disabled:opacity-50"
              required={matchType === MatchTypeEnum.Group}
            >
              <option value="">
                {loadingGroups ? '-- Hämtar grupper... --' : groups.length === 0 ? '-- Inga grupper hittades --' : '-- Välj grupp --'}
              </option>
              {groups.map((group) => (
                <option key={group.groupId} value={group.groupId}>
                  {group.name || `Grupp ID: ${group.groupId}`}
                </option>
              ))}
            </select>
          </div>
        )}

        {/* Home and Away Teams */}
        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1">Hemmalag</label>
            <select
              value={homeCompetitorId}
              onChange={(e) => setHomeCompetitorId(e.target.value ? Number(e.target.value) : '')}
              disabled={loadingCompetitors} // Lås BARA om API-anropet för lag faktiskt körs
              className="w-full bg-slate-50 border border-slate-200 rounded-xl px-4 py-2.5 text-slate-900 font-medium outline-none focus:ring-2 focus:ring-accent disabled:opacity-50"
              required
            >
              <option value="">
                {loadingCompetitors ? '-- Laddar lag... --' : competitors.length === 0 ? '-- Inga lag hittades --' : '-- Välj hemmalag --'}
              </option>
              {competitors.map((comp) => (
                <option key={comp.competitorId} value={comp.competitorId} disabled={comp.competitorId === awayCompetitorId}>
                  {comp.name}
                </option>
              ))}
            </select>
          </div>
          <div>
            <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1">Bortalag</label>
            <select
              value={awayCompetitorId}
              onChange={(e) => setAwayCompetitorId(e.target.value ? Number(e.target.value) : '')}
              disabled={loadingCompetitors} // Lås BARA om API-anropet för lag faktiskt körs
              className="w-full bg-slate-50 border border-slate-200 rounded-xl px-4 py-2.5 text-slate-900 font-medium outline-none focus:ring-2 focus:ring-accent disabled:opacity-50"
              required
            >
              <option value="">
                {loadingCompetitors ? '-- Laddar lag... --' : competitors.length === 0 ? '-- Inga lag hittades --' : '-- Välj bortalag --'}
              </option>
              {competitors.map((comp) => (
                <option key={comp.competitorId} value={comp.competitorId} disabled={comp.competitorId === homeCompetitorId}>
                  {comp.name}
                </option>
              ))}
            </select>
          </div>
        </div>

        {/* Date and Time */}
        <div>
          <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1">Matchstart</label>
          <input
            type="datetime-local"
            value={startTime}
            onChange={(e) => setStartTime(e.target.value)}
            className="w-full bg-slate-50 border border-slate-200 rounded-xl px-4 py-2.5 text-slate-900 font-medium outline-none focus:ring-2 focus:ring-accent"
            required
          />
        </div>

        <div>
          <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1">Stängning för tips (Valfri)</label>
          <input
            type="datetime-local"
            value={predictionDeadline}
            onChange={(e) => setPredictionDeadline(e.target.value)}
            className="w-full bg-slate-50 border border-slate-200 rounded-xl px-4 py-2.5 text-slate-900 font-medium outline-none focus:ring-2 focus:ring-accent"
          />
          <p className="text-[10px] text-slate-400 mt-1 italic">Lämnas tom om tipset ska stänga exakt vid matchstart.</p>
        </div>

        {/* Buttons */}
        <div className="flex justify-end gap-3 pt-4 border-t border-slate-100">
          <button
            type="button"
            onClick={() => onCreated?.()}
            className="px-5 py-2.5 rounded-xl border border-slate-200 font-semibold text-slate-600 hover:bg-slate-50 text-sm transition-all"
          >
            Avbryt
          </button>
          <button
            type="submit"
            disabled={loading}
            className="px-6 py-2.5 bg-accent hover:bg-emerald-600 text-white font-bold rounded-xl text-sm transition-all shadow-md disabled:opacity-50"
          >
            {loading ? 'Sparar...' : 'Spara match'}
          </button>
        </div>
      </form>
    </div>
  );
};