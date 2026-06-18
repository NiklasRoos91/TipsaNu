import React, { useState, useMemo } from 'react';
import { Calendar, Filter, ChevronDown, ChevronUp } from 'lucide-react';
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
  const [isRange, setIsRange] = useState(false);
  const [selectedCompetitorId, setSelectedCompetitorId] = useState<number | ''>('');

  const { competitors, loading: loadingCompetitors } = useCompetitors(tournamentId);

  const filteredMatches = useMemo(() => {
    return matches.filter(m => {
      const matchDate = new Date(m.startTime);

      if (date) {
        const from = new Date(`${date}T00:00:00`);
        const to = isRange && toDate
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
  }, [matches, date, toDate, isRange, selectedCompetitorId]);

  const filteredPredictions = useMemo(() => {
    return predictions.filter(p => filteredMatches.some(m => m.matchId === p.matchId));
  }, [predictions, filteredMatches]);

  return (
    <div className="space-y-4">
      <div className="bg-white rounded-xl border border-slate-200 p-4 space-y-4">
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">

          {/* Date */}
          <div className="space-y-2">
            <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1.5 ml-1">
              Datum
            </label>
            <div className="relative">
              <Calendar size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
              <input
                type="date"
                value={date}
                onChange={e => setDate(e.target.value)}
                className="w-full pl-10 pr-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all font-semibold text-slate-900 shadow-inner"
              />
            </div>

            {isRange ? (
              <div className="space-y-1">
                <div className="relative">
                  <Calendar size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
                  <input
                    type="date"
                    value={toDate}
                    min={date || undefined}
                    onChange={e => setToDate(e.target.value)}
                    className="w-full pl-10 pr-4 py-2.5 bg-slate-50 border border-slate-200 rounded-xl focus:ring-4 focus:ring-accent/10 focus:border-accent outline-none transition-all font-semibold text-slate-900 shadow-inner"
                  />
                </div>
                <button
                  type="button"
                  onClick={() => { setIsRange(false); setToDate(''); }}
                  className="flex items-center gap-1 text-xs font-bold text-slate-400 hover:text-accent transition-colors ml-1"
                >
                  <ChevronUp size={13} /> Ta bort slutdatum
                </button>
              </div>
            ) : (
              <button
                type="button"
                onClick={() => setIsRange(true)}
                className="flex items-center gap-1 text-xs font-bold text-slate-400 hover:text-accent transition-colors ml-1"
              >
                <ChevronDown size={13} /> Lägg till slutdatum
              </button>
            )}
          </div>

          {/* Team select */}
          <div>
            <label className="block text-xs font-bold text-slate-400 uppercase tracking-widest mb-1.5 ml-1">
              Lag
            </label>
            <select
              value={selectedCompetitorId}
              onChange={e => setSelectedCompetitorId(e.target.value ? Number(e.target.value) : '')}
              disabled={loadingCompetitors}
              className="w-full bg-slate-50 border border-slate-200 rounded-xl px-4 py-2.5 text-slate-900 font-medium outline-none focus:ring-4 focus:ring-accent/10 focus:border-accent transition-all disabled:opacity-50"
            >
              <option value="">
                {loadingCompetitors ? '-- Laddar lag... --' : '-- Alla lag --'}
              </option>
              {competitors.map(c => (
                <option key={c.competitorId} value={c.competitorId}>
                  {c.name}
                </option>
              ))}
            </select>
          </div>
        </div>

        <div className="flex items-center gap-2 text-xs font-bold text-slate-400 uppercase tracking-wider pt-1">
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
