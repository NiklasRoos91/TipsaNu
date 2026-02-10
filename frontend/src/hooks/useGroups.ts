import { useState, useEffect } from 'react';
import { Group } from '../types/tournamentTypes';
import { getGroups } from '../services/tournamentService';

export const useGroups = (tournamentId: number) => {
  const [groups, setGroups] = useState<Group[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!tournamentId) return;

    const fetchGroups = async () => {
      setLoading(true);
      setError(null);
      try {
        const data = await getGroups(tournamentId);
        setGroups(data);
      } catch (err: any) {
        setError(err.message || 'Fel vid hämtning av grupper');
      } finally {
        setLoading(false);
      }
    };

    fetchGroups();
  }, [tournamentId]);

  return { groups, loading, error };
};
