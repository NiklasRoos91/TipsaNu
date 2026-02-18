import React, { useState, useMemo, useEffect } from 'react';
import { useGroups } from '../../hooks/useGroups';
import { useUserPredictions } from '../../hooks/useUserPredictions';
import { ActionButton } from '../commons/ActionButton';
import { useAuth } from '../../hooks/useAuth'; 
import { CreateMatch } from '../matches/CreateMatch';
import { useTournamentMatches } from '../../hooks/useTournamentMatches'; 
import { MatchTypeEnum } from '../../types/enums/matchEnums'; 
import { MatchCategorySelector } from './MatchCategorySelector';
import { MatchFilterChips } from './MatchFilterChips';
import { PredictionProgressBar } from '../commons/ProgressBar';
import { MatchList } from '../matches/MatchList';
import { Match, UIPrediction  } from '../../types/matchTypes';

interface TournamentMatchesProps {
  tournamentId: string;
}

export const TournamentMatches: React.FC<TournamentMatchesProps> = ({ 
  tournamentId 
}) => {
  const [matchCategory, setMatchCategory] = useState<'groups' | 'knockout'>('groups');
  const [selectedFilter, setSelectedFilter] = useState<string | MatchTypeEnum | null>(null);
  const { groups } = useGroups(Number(tournamentId));
  const { matches, fetchMatches, loading: loadingMatches, error: matchesError } = useTournamentMatches();
  const [showCreate, setShowCreate] = useState(false);
  const { predictions, loading, error, refreshPredictions } = useUserPredictions();
  const { isAdmin } = useAuth();  

  useEffect(() => {
  fetchMatches(Number(tournamentId));
  }, [tournamentId]);

  const matchGroups = useMemo(() => ({
    groups: groups.map(g => g.name).sort(),
    knockout: Array.from(new Set(matches?.filter(m => m.matchType !== MatchTypeEnum.Group).map(m => m.matchType))) as MatchTypeEnum[]
  }), [groups, matches]);

  useEffect(() => {
    const availableFilters = matchGroups[matchCategory];
    if (availableFilters.length > 0) {
      const isSelectedValid = availableFilters.some(f => f === selectedFilter);
      if (!selectedFilter || !isSelectedValid) {setSelectedFilter(availableFilters[0]);}
    } else {
      setSelectedFilter(null);
    }
  }, [matchCategory, matchGroups, matches]);

  const filteredMatches: Match[] = useMemo(() => {
    if (!matches || !selectedFilter) return [];

    if (matchCategory === 'groups') {
      const group = groups.find(g => g.name === selectedFilter);
      return matches.filter(m => m.matchType === MatchTypeEnum.Group && m.groupId === group?.groupId);
    } else {
      return matches.filter(m => m.matchType !== MatchTypeEnum.Group && m.matchType === selectedFilter);
    }
  }, [matches, selectedFilter, matchCategory, groups]);

  const filteredPredictions: UIPrediction[] = useMemo(() => {
    return predictions
      .filter(p => filteredMatches.some(m => m.matchId === p.matchId))
      .map(p => ({
        matchId: p.matchId,
        predictedHomeScore: p.predictedHomeScore,
        predictedAwayScore: p.predictedAwayScore,
      }));
  }, [predictions, filteredMatches]);

  return (
    <div className="space-y-6">
      
      {loadingMatches && <p>Laddar matcher...</p>}
      {matchesError && <p className="text-red-500">{matchesError}</p>}

      <div className="flex flex-col gap-4">
        <div className="flex items-center justify-between">
          <h2 className="text-2xl font-bold text-primary">Matcher</h2>

          <div className="flex items-center gap-2">
            <MatchCategorySelector matchCategory={matchCategory} setMatchCategory={setMatchCategory} />
            {isAdmin && (
              <ActionButton
                label="Skapa ny"
                onClick={() => setShowCreate(!showCreate)}
                isActive={showCreate}
              />
            )}
          </div>
        </div>

        {showCreate && isAdmin && <CreateMatch onCreated={() => setShowCreate(false)} />}

        <MatchFilterChips 
          filters={matchGroups[matchCategory]}
          selectedFilter={selectedFilter}
          onSelectFilter={setSelectedFilter}
          filteredCount={filteredMatches.length}
        />

        {matchCategory === 'groups' && selectedFilter && (
          <div className="animate-fade-in">
            <div className="h-32 bg-slate-50 rounded-xl animate-pulse border border-slate-100" />
            <div className="p-4 bg-slate-50 rounded-xl text-xs text-slate-400 italic mb-6">
              Ingen tabell tillgänglig för denna grupp.
            </div>
          </div>
        )}
      
        <PredictionProgressBar 
          total={filteredMatches.length} 
          progress={filteredPredictions.length} 
          label='Tippade matcher'
        />

        <MatchList 
          matches={filteredMatches} 
          predictions={filteredPredictions} 
          groups={groups} 
          refreshPredictions={refreshPredictions}
         />
      </div>
    </div>
  );
};