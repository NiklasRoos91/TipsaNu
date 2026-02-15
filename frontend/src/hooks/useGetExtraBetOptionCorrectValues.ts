import { useState, useEffect, useCallback } from "react";
import type { ExtraBetOptionCorrectValueDto } from "../types/extrabetTypes";
import { extraBetService } from "../services/extraBetService";

export const useGetExtraBetOptionCorrectValues = (optionId?: number) => {
  const [values, setValues] = useState<ExtraBetOptionCorrectValueDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchValues = useCallback(async () => {
    if (!optionId) return;

    setLoading(true);
    setError(null);

    try {
      const data = await extraBetService.getCorrectValuesByOptionId(optionId);
      setValues(data);
    } catch (err: any) {
      console.error(err);
      setError(err?.message || "Could not fetch correct values.");
    } finally {
      setLoading(false);
    }
  }, [optionId]);

  useEffect(() => {
    if (optionId) {
      fetchValues();
    }
  }, [optionId, fetchValues]);

  return { values, loading, error, refetch: fetchValues };
};
