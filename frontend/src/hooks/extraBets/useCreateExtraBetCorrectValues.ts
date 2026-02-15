import { useState, useCallback } from "react";
import type { SetExtraBetOptionCorrectValuesDto } from "../../types/extrabetTypes";
import { extraBetService } from "../../services/extraBetService";

export const useCreateExtraBetCorrectValues = (optionId?: number) => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<boolean>(false);

  const createCorrectValues = useCallback(
    async (dto: SetExtraBetOptionCorrectValuesDto) => {
      if (!optionId) return false;

      setLoading(true);
      setError(null);
      setSuccess(false);

      try {
        const result = await extraBetService.createCorrectValues(optionId, dto);
        setSuccess(result);
        return result;
      } catch (err: any) {
        setError(err?.message || "Kunde inte skapa korrektvärden.");
        return false;
      } finally {
        setLoading(false);
      }
    },
    [optionId]
  );

  return { createCorrectValues, loading, error, success };
};
