import { useState, useEffect } from 'react';
import { getMatchById } from '../services/matchService';
import { Match } from '../types/matchTypes'; 

export const useMatchById = (matchId: number) => {
  const [match, setMatch] = useState<Match | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchMatch = async () => {
      setLoading(true);
      setError(null);

      try {
        const fetchedMatch = await getMatchById(matchId);
        setMatch(fetchedMatch);
      } catch (err: any) {
        setError(err.message || 'Kunde inte hämta matchdata');
      } finally {
        setLoading(false);
      }
    };

    if (matchId) {
      fetchMatch();
    }
  }, [matchId]);

  return { match, loading, error };
};
