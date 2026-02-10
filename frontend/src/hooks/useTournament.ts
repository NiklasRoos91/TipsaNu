import { useState, useEffect } from 'react';
import { Tournament } from '../types/tournamentTypes';
import { getTournament } from '../services/tournamentService'; // ✅ rätt namn

export const useTournament = (id: number) => {
  const [tournament, setTournament] = useState<Tournament | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let isMounted = true;

    const loadTournament = async () => {
      try {
        const data = await getTournament(id);
        if (isMounted) setTournament(data);
      } catch (err: any) {
        if (isMounted) setError(err.response?.data?.message || 'Kunde inte hämta turnering');
      } finally {
        if (isMounted) setLoading(false);
      }
    };

    loadTournament();

    return () => { isMounted = false; };
  }, [id]);

  return { tournament, loading, error };
};
