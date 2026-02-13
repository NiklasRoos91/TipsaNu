import { useState, useEffect } from 'react';
import type { LeagueWithLeaderboardDto, LeaderboardEntryBackendDto, LeaderboardEntryDto, } from '../types/leagueTypes';
import { getLeagueById } from '../services/leagueService';
import { mapLeaderboard } from '../utils/mapLeaderboard';

export const useLeagueDetail = (leagueId: number | null) => {
  const [league, setLeague] = useState<LeagueWithLeaderboardDto | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!leagueId) return;

    const fetchLeague = async () => {
      setLoading(true);
      setError(null);

      try {
        const data = await getLeagueById(
          leagueId
        ) as LeagueWithLeaderboardDto & { leaderboard: LeaderboardEntryBackendDto[] };

        const leaderboard: LeaderboardEntryDto[] = mapLeaderboard(data.leaderboard);

        setLeague({ ...data, leaderboard });
      } catch (err: any) {
        setError(err.message || 'Fel vid hämtning av liga');
      } finally {
        setLoading(false);
      }
    };

    fetchLeague();
  }, [leagueId]);

  return { league, loading, error };
};
