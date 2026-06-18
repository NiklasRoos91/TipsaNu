import React, { useState, useMemo } from 'react';
import { Calendar, Filter } from 'lucide-react';
import { Match, UIPrediction } from '../../types/matchTypes';
import { useCompetitors } from '../../hooks/matches/useCompetitors';
import { MatchList } from '../matches/MatchList';

interface MatchSearchFilterProps {
  tournamentId: number;
  matches: Match[];
  predictions: UIPrediction[];
  groups: { name: string; groupId: number }[];
  refreshPredictions?: () => void;
  refreshMatches?: () => void;
}

export const MatchSearchFilter: React.FC<MatchSearchFilterProps> = ({
  tournamentId,
  matches,
  predictions,
  groups,
  refreshPredictions,
  refreshMatches,
}) => {
  const [date, setDate] = useState('');
  const [toDate, setToDate] = useState('');
  const [selectedCompetitorId, setSelectedCompetitorId] = useState<number | ''>('');

  const { competitors, loading: loadingCompetitors } = useCompetitors(tournamentId);

  const filteredMatches = useMemo(() => {
    return matches.filter(m => {
      const matchDate = new Date(m.startTime);

      if (date) {
        const from = new Date(`${date}T00:00:00`);
        const to = toDate
          ? new Date(`${toDate}T23:59:59`)
          : new Date(`${date}T23:59:59`);
        if (matchDate < from || matchDate > to) return false;
      }

      if (selectedCompetitorId !== '') {
        if (
          m.homeCompetitorId !== selectedCompetitorId &&
          m.awayCompetitorId !== selectedCompetitorId
        ) return false;
      }

      return true;
    });
  }, [matches, date, toDate, selectedCompetitorId]);

  const filteredPredictions = useMemo(() => {
    return predictions.filter(p => filteredMatches.some(m => m.matchId === p.matchId));
  }, [predictions, filteredMatches]);

  return (
    <div className="space-y-4">
      <div className="space-y-3">
        <div className="flex flex-col sm:flex-row items-stretch sm:items-center gap-3">

          {/* From date */}
          <div className="relative flex-1 flex items-center bg-slate-50 border border-slate-200 rounded-xl focus-within:ring-4 focus-within:ring-accent/10 focus-within:border-accent transition-all shadow-inner">
            <Calendar size={15} className="ml-3 text-slate-400 shrink-0" />
            <span className="text-xs font-medium text-slate-400 ml-2 shrink-0">Från:</span>
            <input
              type="date"
              value={date}
              onChange={e => setDate(e.target.value)}
              className="flex-1 bg-transparent outline-none pl-2 pr-3 py-2 text-sm font-semibold text-slate-900"
            />
          </div>

          {/* To date */}
          <div className="relative flex-1 flex items-center bg-slate-50 border border-slate-200 rounded-xl focus-within:ring-4 focus-within:ring-accent/10 focus-within:border-accent transition-all shadow-inner">
            <Calendar size={15} className="ml-3 text-slate-400 shrink-0" />
            <span className="text-xs font-medium text-slate-400 ml-2 shrink-0">Till:</span>
            <input
              type="date"
              value={toDate}
              min={date || undefined}
              onChange={e => setToDate(e.target.value)}
              className="flex-1 bg-transparent outline-none pl-2 pr-3 py-2 text-sm font-semibold text-slate-900"
            />
          </div>

          {/* Team select */}
          <select
            value={selectedCompetitorId}
            onChange={e => setSelectedCompetitorId(e.target.value ? Number(e.target.value) : '')}
            disabled={loadingCompetitors}
            className="bg-slate-50 border border-slate-200 rounded-xl px-3 py-2 text-sm font-medium text-slate-700 outline-none focus:ring-4 focus:ring-accent/10 focus:border-accent transition-all disabled:opacity-50 shadow-inner"
          >
            <option value="">
              {loadingCompetitors ? 'Laddar lag...' : 'Alla lag'}
            </option>
            {competitors.map(c => (
              <option key={c.competitorId} value={c.competitorId}>
                {c.name}
              </option>
            ))}
          </select>
        </div>

        <div className="flex items-center gap-2 text-xs font-bold text-slate-400 uppercase tracking-wider">
          <Filter size={14} />
          <span>Visar {filteredMatches.length} av {matches.length} matcher</span>
        </div>
      </div>

      <MatchList
        matches={filteredMatches}
        predictions={filteredPredictions}
        groups={groups}
        refreshPredictions={refreshPredictions}
        refreshMatches={refreshMatches}
      />
    </div>
  );
};
