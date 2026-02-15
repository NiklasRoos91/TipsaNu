import { useState, useCallback } from "react";
import type { CreateExtraBetDto, ExtraBetForUserDto } from "../../types/extrabetTypes";
import { extraBetService } from "../../services/extraBetService";

export const useSubmitExtraBet = (optionId?: number) => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [submittedBet, setSubmittedBet] = useState<ExtraBetForUserDto | null>(null);

  const submit = useCallback(
    async (dto: CreateExtraBetDto) => {
      if (!optionId) return null;

      setLoading(true);
      setError(null);

      try {
        const data = await extraBetService.submitMyExtraBet(optionId, dto);
        setSubmittedBet(data);
        return data;
      } catch (err: any) {
        setError(err?.message || "Kunde inte skicka extrabet.");
        throw err;
      } finally {
        setLoading(false);
      }
    },
    [optionId]
  );

  return { submit, loading, error, submittedBet };
};
