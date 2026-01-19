import { useState, useEffect } from 'react';
import { Tournament } from '../types';
import { getTournaments } from '../services/api';

export const useTournaments = () => {
  const [tournaments, setTournaments] = useState<Tournament[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let isMounted = true;
    getTournaments()
      .then((data) => {
        if (isMounted) {
          setTournaments(data);
          setLoading(false);
        }
      })
      .catch((err) => {
        if (isMounted) {
          setError('Kunde inte hämta turneringar');
          setLoading(false);
        }
      });

    return () => {
      isMounted = false;
    };
  }, []);

  return { tournaments, loading, error };
};
