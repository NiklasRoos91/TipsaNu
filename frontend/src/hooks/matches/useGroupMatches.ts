import { useState, useEffect, useCallback } from 'react';
import { getMatches } from '../../services/tournamentService';
import { Match } from '../../types/matchTypes';

export const useGroupMatches = (groupId: number | null) => {
  const [matches, setMatches] = useState<Match[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchMatches = useCallback(() => {
  if (!groupId) return;

    setLoading(true);
    setError(null);

    getMatches(groupId)
      .then(data => setMatches(data))
      .catch(err => setError(err.message || 'Fel vid hämtning av matcher'))
      .finally(() => setLoading(false));
  }, [groupId]);

  useEffect(() => {
    fetchMatches();
  }, [fetchMatches]);

  return { matches, loading, error, refetch: fetchMatches };
};
