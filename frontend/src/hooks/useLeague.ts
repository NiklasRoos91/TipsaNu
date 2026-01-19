import { useState, useEffect } from 'react';
import { League } from '../types/types';
import { getLeague } from '../services/api';

export const useLeague = (id?: string) => {
  const [league, setLeague] = useState<League | undefined>();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    getLeague(id).then((data) => {
      setLeague(data);
      setLoading(false);
    });
  }, [id]);

  return { league, loading };
};