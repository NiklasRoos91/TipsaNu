import { useState } from 'react';
import { getMatchesByTournamentId } from '../services/tournamentService';
import { Match } from '../types/matchTypes';

export const useTournamentMatches = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [matches, setMatches] = useState<Match[] | null>(null);

  const fetchMatches = async (tournamentId: number) => {
    setLoading(true);
    setError(null);

    try {
      const result: Match[] = await getMatchesByTournamentId(tournamentId);
      setMatches(result);
    } catch (err: any) {
      setError(err.message || 'Något gick fel vid hämtning av matcher');
    } finally {
      setLoading(false);
    }
  };

  return { fetchMatches, loading, error, matches };
};
