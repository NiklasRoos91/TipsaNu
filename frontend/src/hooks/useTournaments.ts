import { useState, useEffect } from 'react';
import { Tournament } from '../types/tournamentTypes';
import { getTournaments } from '../services/tournamentService';

export const useTournaments = () => {
  const [tournaments, setTournaments] = useState<Tournament[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let isMounted = true;

    const loadTournaments = async () => {
      try {
        const data = await getTournaments();
        if (isMounted) {
          setTournaments(Array.isArray(data) ? data : []);
          setLoading(false);
        }
      } catch(err) {
      if (isMounted) {
        console.error('Kunde inte hämta turneringar', err);
        setError('Kunde inte hämta turneringar');
        setLoading(false);
        }
      }
  };

    loadTournaments();

    return () => {
      isMounted = false;
    };
  }, []);

  return { tournaments, loading, error };
};
