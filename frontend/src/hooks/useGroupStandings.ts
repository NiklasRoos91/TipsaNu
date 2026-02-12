import { useState, useEffect } from 'react';
import { getStandings } from '../services/tournamentService';
import { GroupStanding } from '../types/tournamentTypes'; 

export const useGroupStandings = (groupId: number | null) => {
  const [standings, setStandings] = useState<GroupStanding[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!groupId) return; 

    setLoading(true);
    setError(null);

    getStandings(groupId)
      .then(data => setStandings(data))
      .catch(err => setError(err.message || 'Fel vid hämtning av tabell'))
      .finally(() => setLoading(false)); 
  }, [groupId]);

  return { standings, loading, error }; 
};
