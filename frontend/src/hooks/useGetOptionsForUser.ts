import { useState, useEffect, useCallback } from "react";
import type { ExtraBetOptionForUser } from "../types/extrabetTypes";
import { extraBetService } from "../services/extraBetService";

export const useGetOptionsForUser = (tournamentId: number) => {
  const [options, setOptions] = useState<ExtraBetOptionForUser[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchOptions = useCallback(async () => {
    setLoading(true);
    setError(null);

    try {
      const data = await extraBetService.getOptionsForUser(tournamentId);
      setOptions(data);
    } catch (err: any) {
      console.error(err);
      setError(err?.message || "Could not fetch extra bet options.");
    } finally {
      setLoading(false);
    }
  }, [tournamentId]);

  useEffect(() => {
    if (tournamentId) {
      fetchOptions();
    }
  }, [tournamentId, fetchOptions]);

  return { options, loading, error, refetch: fetchOptions };
};
