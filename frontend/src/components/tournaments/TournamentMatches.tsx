import React, { useState, useMemo, useEffect } from 'react';
import { Filter } from 'lucide-react';
import { MatchCard } from '../matches/MatchCard';
import { useGroups } from '../../hooks/useGroups';
import { useGroupMatches } from '../../hooks/useGroupMatches';
import { useUserPredictions } from '../../hooks/useUserPredictions';
import { ActionButton } from '../commons/ActionButton';
import { useAuth } from '../../hooks/useAuth'; 
import { CreateMatch } from '../../pages/CreateMatch';

interface TournamentMatchesProps {
  tournamentId: string;
}

type MatchCategory = 'groups' | 'knockout';

export const TournamentMatches: React.FC<TournamentMatchesProps> = ({ 
  tournamentId 
}) => {
  const [matchCategory, setMatchCategory] = useState<MatchCategory>('groups');
  const [selectedFilter, setSelectedFilter] = useState<string | null>(null);
  const { groups } = useGroups(Number(tournamentId));
  const [showCreate, setShowCreate] = useState(false);
  const groupId = selectedFilter ? groups.find(g => g.name === selectedFilter)?.groupId ?? null : null;
  const { matches, loading: loadingMatches, error: matchesError } = useGroupMatches(groupId);
  const { predictions, loading: loadingPredictions, error: predictionsError } = useUserPredictions();
  const { isAdmin } = useAuth();  

  const matchGroups = useMemo(() => {
    const categories = {
      groups: groups.map(g => g.name),
      knockout: [] as string[]
    };

    categories.groups.sort();
    return categories;
  }, [groups]);

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

  const filteredMatches = useMemo(() => {
    if (!selectedFilter) return [];
    return matches;
  }, [matches, selectedFilter]);

  const filteredPredictions = useMemo(() => {
    return predictions.filter(p => filteredMatches.some(m => m.matchId === p.matchId));
  }, [predictions, filteredMatches]);

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-4">
        <div className="flex items-center justify-between">
          <h2 className="text-2xl font-bold text-primary">Matcher</h2>

          <div className="flex items-center gap-2">  {/* wrapper för Grupp/Slut + knapp */}
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

             {isAdmin && (
              <div className="w-auto ml-auto">
                <ActionButton
                  label="Skapa ny"
                  onClick={() => setShowCreate(!showCreate)}
                  isActive={showCreate}
                />
              </div>
              )}
            </div>
          </div>

            {showCreate && isAdmin && (
              <div className="mb-10 animate-fade-in">
                <CreateMatch onCreated={() => setShowCreate(false)} />
              </div>
            )}


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
          {/* Placeholder för loading */}
          <div className="h-32 bg-slate-50 rounded-xl animate-pulse border border-slate-100" />
          
          {/* Placeholder för när ingen data finns */}
          <div className="p-4 bg-slate-50 rounded-xl text-xs text-slate-400 italic mb-6">
            Ingen tabell tillgänglig för denna grupp.
          </div>
        </div>
      )}
      
      <div className="mb-4">
        <div className="flex justify-between items-center mb-2">
          <span className="text-sm text-slate-600">Tippade matcher</span>
          <span className="font-bold text-primary">{filteredPredictions.length} / {filteredMatches.length}</span>
        </div>
        <div className="w-full bg-slate-100 rounded-full h-2 overflow-hidden">
          <div 
            className="bg-accent h-2 rounded-full transition-all duration-1000 ease-out" 
            style={{ width: `${filteredMatches.length > 0 ? (filteredPredictions.length / filteredMatches.length) * 100 : 0}%` }}
          ></div>
        </div>
      </div>
      
      <div className="space-y-4 animate-fade-in">
        {filteredMatches.length > 0 ? (
          filteredMatches.map(match => {
            const initialPrediction = filteredPredictions.find(p => p.matchId === match.matchId) ?? null;
            const mappedPrediction = initialPrediction
              ? { homeScore: initialPrediction.predictedHomeScore, awayScore: initialPrediction.predictedAwayScore }
              : null;
            return (
            <MatchCard 
              key={match.matchId} 
              match={match} 
              prediction={mappedPrediction} 
              groups={groups}
            />
            );
          })
        ) : selectedFilter ? (
          <div className="text-center p-12 bg-white rounded-xl border border-slate-200 text-slate-400">
            Inga matcher hittades för det valda filtret.
          </div>
        ) : null}
      </div>
    </div>
  );
};