import { useEffect, useState, useCallback } from "react";
import type { LeagueDto } from "../types/leagueTypes";
import { getMyLeaguesInTournament } from "../services/leagueService";

export const useMyLeaguesInTournament = (tournamentId: number) => {
  const [leagues, setLeagues] = useState<LeagueDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchLeagues = useCallback(async () => {
    if (!tournamentId || isNaN(tournamentId)) return;

    try {
      setLoading(true);
      setError(null);

      const data = await getMyLeaguesInTournament(tournamentId);
      setLeagues(data);
    } catch (err: any) {
      setError(err.message || "Kunde inte hämta ligor");
    } finally {
      setLoading(false);
    }
  }, [tournamentId]);

  useEffect(() => {
    fetchLeagues();
  }, [fetchLeagues]);

  return {
    leagues,
    loading,
    error,
    refetch: fetchLeagues,
    setLeagues,
  };
};
