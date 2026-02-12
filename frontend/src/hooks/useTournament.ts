import { useState, useEffect } from 'react';
import { Tournament } from '../types/tournamentTypes';
import { getTournament } from '../services/tournamentService';

export const useTournament = (tournamentId: number) => {
  const [tournament, setTournament] = useState<Tournament | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
        if (tournamentId === undefined || tournamentId === null) {
      console.warn('useTournament: tournamentId is undefined or null');
      setLoading(false);
      return;
    }

    // Säkerställ att det är ett nummer
    const idNum = Number(tournamentId);
    if (isNaN(idNum)) {
      console.error('useTournament: tournamentId is NaN', tournamentId);
      setError('Ogiltigt turnerings-id');
      setLoading(false);
      return;
    }
    let isMounted = true;

    const loadTournament = async () => {
      try {
        const data = await getTournament(tournamentId);
        if (isMounted) setTournament(data);
      } catch (err: any) {
        if (isMounted) setError(err.response?.data?.message || 'Kunde inte hämta turnering');
      } finally {
        if (isMounted) setLoading(false);
      }
    };

    loadTournament();

    return () => { isMounted = false; };
  }, [tournamentId]);

  return { tournament, loading, error };
};
