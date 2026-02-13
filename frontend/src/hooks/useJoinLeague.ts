// src/hooks/useJoinLeague.ts
import { useState } from 'react';
import type { JoinLeagueDto, LeagueMemberDto } from '../types/leagueTypes';
import { joinLeague } from '../services/leagueService';

export const useJoinLeague = () => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const execute = async (
    tournamentId: number,
    dto: JoinLeagueDto
  ): Promise<LeagueMemberDto | null> => {
    setLoading(true);
    setError(null);
    try {
      const result = await joinLeague(tournamentId, dto);
      return result;
    } catch (err: any) {
      setError(err.message || 'Kunde inte gå med i ligan');
      return null;
    } finally {
      setLoading(false);
    }
  };

  return { execute, loading, error };
};
