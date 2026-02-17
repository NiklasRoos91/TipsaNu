import { useState, useEffect, useCallback } from "react";
import type { ExtraBetDto } from "../../types/extrabetTypes";
import { extraBetService } from "../../services/extraBetService";

export const useGetMyExtraBetByOptionId = (optionId: number) => {
  const [myBet, setMyBet] = useState<ExtraBetDto | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchMyBet = useCallback(async () => {
    setLoading(true);
    setError(null);

    try {
      const data = await extraBetService.getMyExtraBetByOptionId(optionId);
      setMyBet(data);
    } catch (err: any) {
      console.error(err);
      setError(err?.message || "Could not fetch your extra bet.");
    } finally {
      setLoading(false);
    }
  }, [optionId]);

  useEffect(() => {
    if (optionId) fetchMyBet();
  }, [optionId, fetchMyBet]);

  return { myBet, loading, error, refetch: fetchMyBet };
};