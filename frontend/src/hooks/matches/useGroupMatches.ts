import { useState, useEffect } from 'react';
import { getMatches } from '../../services/tournamentService';
import { Match } from '../../types/matchTypes';

export const useGroupMatches = (groupId: number | null) => {
  const [matches, setMatches] = useState<Match[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!groupId) return;

    setLoading(true);
    setError(null);

    getMatches(groupId)
      .then(data => setMatches(data))
      .catch(err => setError(err.message || 'Fel vid hämtning av matcher'))
      .finally(() => setLoading(false));
  }, [groupId]);

  return { matches, loading, error };
};
