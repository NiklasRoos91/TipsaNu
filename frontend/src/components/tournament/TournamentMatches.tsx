import React, { useState, useMemo, useEffect } from 'react';
import { Filter } from 'lucide-react';
import { Match, Prediction, GroupStanding } from '../../types/types';
import { getGroupStandings } from '../../services/api';
import { GroupTable } from './GroupTable';
import { MatchCard } from '../match/MatchCard';

interface TournamentMatchesProps {
  tournamentId: string;
  matches: Match[];
  predictions: Prediction[];
}

type MatchCategory = 'groups' | 'knockout';

export const TournamentMatches: React.FC<TournamentMatchesProps> = ({ 
  tournamentId, 
  matches, 
  predictions 
}) => {
  const [matchCategory, setMatchCategory] = useState<MatchCategory>('groups');
  const [selectedFilter, setSelectedFilter] = useState<string | null>(null);
  const [standings, setStandings] = useState<GroupStanding[]>([]);
  const [loadingStandings, setLoadingStandings] = useState(false);

  // Categorize and filter matches
  const matchGroups = useMemo(() => {
    const categories = {
      groups: [] as string[],
      knockout: [] as string[]
    };

    matches.forEach(m => {
      let g = m.group || 'Övrigt';
      // Normalize group names like "A" to "Grupp A" if they are just letters
      if (g.length === 1) g = `Grupp ${g}`;
      
      if (g.toLowerCase().includes('grupp')) {
        if (!categories.groups.includes(g)) categories.groups.push(g);
      } else {
        if (!categories.knockout.includes(g)) categories.knockout.push(g);
      }
    });

    categories.groups.sort();
    categories.knockout.sort();
    return categories;
  }, [matches]);

  // Set default filter when category changes
  useEffect(() => {
    const availableFilters = matchGroups[matchCategory];
    if (availableFilters.length > 0) {
      if (!selectedFilter || !availableFilters.includes(selectedFilter)) {
        setSelectedFilter(availableFilters[0]);
      }
    } else {
      setSelectedFilter(null);
    }
  }, [matchCategory, matchGroups]);

  // Fetch standings when group filter changes
  useEffect(() => {
    if (tournamentId && matchCategory === 'groups' && selectedFilter) {
      setLoadingStandings(true);
      getGroupStandings(tournamentId, selectedFilter)
        .then(data => {
          setStandings(data);
          setLoadingStandings(false);
        })
        .catch(() => {
          setLoadingStandings(false);
        });
    } else {
      setStandings([]);
    }
  }, [tournamentId, matchCategory, selectedFilter]);

  // Memoisering av standings
  const memoizedStandings = useMemo(() => standings, [standings]);

  const filteredMatches = useMemo(() => {
    if (!selectedFilter) return [];
    return matches.filter(m => {
      let mg = m.group;
      if (mg?.length === 1) mg = `Grupp ${mg}`;
      return mg === selectedFilter;
    });
  }, [matches, selectedFilter]);

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-4">
        <div className="flex items-center justify-between">
          <h2 className="text-2xl font-bold text-primary">Matcher</h2>
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
        </div>

        {/* Filter Chips */}
        <div className="flex flex-wrap gap-2 py-2">
          {matchGroups[matchCategory].length > 0 ? (
            matchGroups[matchCategory].map(filter => (
              <button
                key={filter}
                onClick={() => setSelectedFilter(filter)}
                className={`px-3 py-1.5 md:px-4 md:py-2 rounded-full text-[11px] md:text-xs font-bold border transition-all ${
                  selectedFilter === filter 
                  ? 'bg-accent border-accent text-white shadow-md' 
                  : 'bg-white border-slate-200 text-slate-500 hover:border-accent hover:text-accent'
                }`}
              >
                {filter}
              </button>
            ))
          ) : (
            <p className="text-sm text-slate-400 italic py-2">Inga matcher tillgängliga för denna kategori.</p>
          )}
        </div>

        {selectedFilter && (
          <div className="flex items-center gap-2 text-xs font-bold text-slate-400 uppercase tracking-wider mb-2">
            <Filter size={14} />
            <span>Visar {filteredMatches.length} matcher i {selectedFilter}</span>
          </div>
        )}
      </div>

      {/* Group Standings Table */}
      {matchCategory === 'groups' && selectedFilter && (
        <div className="animate-fade-in">
          {loadingStandings ? (
            <div className="h-32 bg-slate-50 rounded-xl animate-pulse border border-slate-100" />
          ) : memoizedStandings.length > 0 ? (
            <GroupTable standings={memoizedStandings} />
          ) : (
            <div className="p-4 bg-slate-50 rounded-xl text-xs text-slate-400 italic mb-6">Ingen tabell tillgänglig för denna grupp.</div>
          )}
        </div>
      )}

      <div className="space-y-4 animate-fade-in">
        {filteredMatches.length > 0 ? (
          filteredMatches.map(match => (
            <MatchCard 
              key={match.id} 
              match={match} 
              prediction={predictions.find(p => p.matchId === match.id)} 
            />
          ))
        ) : selectedFilter ? (
          <div className="text-center p-12 bg-white rounded-xl border border-slate-200 text-slate-400">
            Inga matcher hittades för det valda filtret.
          </div>
        ) : null}
      </div>
    </div>
  );
};