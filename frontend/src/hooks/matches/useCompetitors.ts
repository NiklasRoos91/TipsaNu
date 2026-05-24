import { useState, useEffect } from 'react';
import { getFilteredCompetitors } from '../../services/adminMatchService';
import { Competitor } from '../../types/matchTypes';

export const useCompetitors = (tournamentId: number, groupId: number | null = null, searchTerm: string = '') => {
  const [competitors, setCompetitors] = useState<Competitor[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!tournamentId) return;

    const fetchCompetitors = async () => {
      setLoading(true);
      setError(null);
      try {
        const data = await getFilteredCompetitors(tournamentId, groupId, searchTerm);
        setCompetitors(data);
      } catch (err: any) {
        setError(err.message || 'Kunde inte hämta lag till turneringen.');
        setCompetitors([]);
      } finally {
        setLoading(false);
      }
    };

    const delayDebounceFn = setTimeout(() => {
      fetchCompetitors();
    }, 300);

    return () => clearTimeout(delayDebounceFn);
    
  }, [tournamentId, groupId, searchTerm]);

  return { competitors, loading, error };
};