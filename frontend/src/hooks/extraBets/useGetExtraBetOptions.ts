import { useState, useEffect, useCallback } from "react";
import type { ExtraBetOptionDto } from "../../types/extrabetTypes";
import { extraBetService } from "../../services/extraBetService";

export const useGetExtraBetOptions = (tournamentId: number, status: string = "all") => {
  const [options, setOptions] = useState<ExtraBetOptionDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchOptions = useCallback(async () => {
    setLoading(true);
    setError(null);

    try {
      const data = await extraBetService.getExtraBetOptions(tournamentId, status);
      setOptions(data);
    } catch (err: any) {
      console.error(err);
      setError(err?.message || "Could not fetch extra bet options.");
    } finally {
      setLoading(false);
    }
  }, [tournamentId, status]);

  useEffect(() => {
    if (tournamentId) fetchOptions();
  }, [tournamentId, status, fetchOptions]);

  return { options, loading, error, refetch: fetchOptions };
};