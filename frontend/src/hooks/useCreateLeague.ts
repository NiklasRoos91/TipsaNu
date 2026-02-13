import { useState } from 'react';
import type { CreateLeagueDto, CreatedLeagueWithMemberDto } from '../types/leagueTypes';
import { createLeague } from '../services/leagueService';

export const useCreateLeague = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const execute = async (data: CreateLeagueDto): Promise<CreatedLeagueWithMemberDto | null> => {
    setLoading(true);
    setError(null);
    try {
      const result = await createLeague(data);
      return result;
    } catch (err: any) {
      setError(err.message || 'Could not create league');
      return null;
    } finally {
      setLoading(false);
    }
  };

  return { execute, loading, error };
};
